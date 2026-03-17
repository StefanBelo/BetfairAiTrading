import argparse
import csv
import math
import os
import re
from collections import defaultdict

import numpy as np
import pandas as pd

from sklearn.calibration import CalibratedClassifierCV, calibration_curve
from sklearn.ensemble import RandomForestClassifier
from sklearn.linear_model import LogisticRegression
from sklearn.metrics import brier_score_loss, log_loss, roc_auc_score
from sklearn.model_selection import TimeSeriesSplit
from sklearn.pipeline import Pipeline
from sklearn.preprocessing import StandardScaler
import matplotlib.pyplot as plt

# Default paths (can be overridden via CLI args)
DEFAULT_MCP_YAML = r"c:\Users\Stefan\AppData\Roaming\Code\User\workspaceStorage\a5c8b31b567c6e6719dc75956c695ab7\GitHub.copilot-chat\chat-session-resources\64409f65-7bd6-4408-99cb-104e50da77d2\call_Ei8ROwrIbtixZFi88BHfRbMJ__vscode-1773584843764\content.txt"
DEFAULT_OUTPUT_CSV = r"e:\Projects\BetfairAiTrading\outputs\features\predictions.csv"


def parse_args():
    parser = argparse.ArgumentParser(description="Generate Horse Racing EV/features CSV from MCP market + RacingPost data")
    parser.add_argument("--mcp-yaml", default=DEFAULT_MCP_YAML, help="Path to MCP GetAllDataContextForMarket output (YAML-like)")
    parser.add_argument("--market-odds", help="Optional CSV (selectionId,odds) to supply odds for current market")
    parser.add_argument("--out", default=DEFAULT_OUTPUT_CSV, help="Output CSV path")
    parser.add_argument("--lastn", type=int, default=4, help="Number of last races to use")
    parser.add_argument("--commission", type=float, default=0.08, help="Commission rate (e.g. 0.08)")
    return parser.parse_args()


def load_market_odds(path):
    odds = {}
    try:
        with open(path, newline='', encoding='utf-8') as f:
            reader = csv.DictReader(f)
            for row in reader:
                sel = row.get('selectionId') or row.get('SelectionId')
                o = row.get('odds') or row.get('Odds')
                if sel and o:
                    odds[sel] = float(o)
    except Exception:
        pass
    return odds


def parse_mcp_yaml(path):
    """Parse the MCP output YAML-like file to extract market info, runners and last races."""
    active_market = {
        'marketId': None,
        'marketName': None,
        'raceDatetime': None,
        'runners': {},
    }

    with open(path, encoding='utf-8') as f:
        lines = [l.rstrip('\n') for l in f]

    current_sel = None
    in_lastraces = False

    for line in lines:
        stripped = line.lstrip()
        if stripped.startswith('MarketId:'):
            active_market['marketId'] = stripped.split(':', 1)[1].strip().strip('"')
        elif stripped.startswith('MarketName:'):
            active_market['marketName'] = stripped.split(':', 1)[1].strip().strip('"')
        elif stripped.startswith('StartTime:'):
            active_market['raceDatetime'] = stripped.split(':', 1)[1].strip().strip('"')
        elif stripped.startswith('- SelectionId:'):
            current_sel = stripped.split(':', 1)[1].strip().strip('"')
            in_lastraces = False
            active_market['runners'][current_sel] = {
                'lastRaces': [],
                'name': None,
            }
        elif current_sel and stripped.startswith('Name:'):
            active_market['runners'][current_sel]['name'] = stripped.split(':', 1)[1].strip().strip('"')
        elif current_sel and 'LastRaces' in stripped and ':' in stripped:
            in_lastraces = True
        elif in_lastraces and current_sel:
            m = re.match(r"\s*(\d+),(\d+),(\d+),(.*)", stripped)
            if not m:
                continue
            parts = []
            cur = ''
            in_quote = False
            for ch in stripped:
                if ch == '"':
                    in_quote = not in_quote
                    cur += ch
                elif ch == ',' and not in_quote:
                    parts.append(cur)
                    cur = ''
                else:
                    cur += ch
            if cur:
                parts.append(cur)
            if len(parts) < 7:
                continue
            parts = [p.strip(' "') for p in parts]
            last_run_days = int(parts[0])
            position = int(parts[1])
            distance = float(parts[2])
            beaten = float(parts[3]) if parts[3] not in ('', '0') else 0.0
            desc = parts[6]

            active_market['runners'][current_sel]['lastRaces'].append({
                'days': last_run_days,
                'pos': position,
                'distance': distance,
                'beaten': beaten,
                'desc': desc,
            })

    return active_market

# feature helpers
POSITIVE = ["led", "kept on", "won", "clear", "stayed on"]
NEGATIVE = ["weakened", "pulled up", "fell", "outpaced", "tailed off", "short of room"]

def sentiment(score_desc):
    t = score_desc.lower()
    if any(p in t for p in POSITIVE):
        return 1.0
    if any(p in t for p in NEGATIVE):
        return 0.0
    return 0.5


def main():
    global args
    args = parse_args()

    active_market = parse_mcp_yaml(args.mcp_yaml)
    odds_map = load_market_odds(args.market_odds) if args.market_odds else {}

    # fallback: if no odds file given, treat all odds as unknown (0)
    # normalize market probabilities if odds are available
    odds_list = [v for v in odds_map.values() if v > 0]
    inv = [1.0 / o for o in odds_list] if odds_list else []
    sum_inv = sum(inv) if inv else 1

    p_market_map = {}
    for sel in active_market['runners'].keys():
        o = odds_map.get(sel, 0.0)
        if o > 0:
            p_market_map[sel] = (1.0 / o) / sum_inv
        else:
            p_market_map[sel] = 0.0

    # build csv output
    with open(args.out, 'w', newline='', encoding='utf-8') as f:
        fieldnames = [
            'marketId','marketName','raceDatetime','selectionId','runnerName','odds','p_market',
            'p_model_raw','p_model_calibrated','EV_per_1','TF_stars','rpRating',
            'wins','places','avg_finish','std_finish','avg_beaten',
            'w_win','w_place','w_avg_finish','w_avg_beaten','trend','delta_avg',
            'lastN_WWinRate','lastN_WPlaceRate','lastN_WAvgFinish','lastN_StdFinish','lastN_AvgBeaten',
            'lastN_WAvgFinish_recency','lastN_WWinRate_recency','lastN_AvgBeaten_recency',
            'trend_slope_pos','delta_avg_finish','cusum_change','semScore','SurfaceSuit','DistanceSuit',
            'model_SHAP_top3','confidence_label'
        ]
        writer = csv.DictWriter(f, fieldnames=fieldnames)
        writer.writeheader()

        current_rows = []
        for sel, runner in active_market['runners'].items():
            odds = odds_map.get(sel, 0.0)
            p_market = p_market_map.get(sel, 0.0)

            races = runner.get('lastRaces', [])
            races_sorted = sorted(races, key=lambda x: x['days'])
            lastN = args.lastn
            last_races = races_sorted[:lastN]

            positions = [r['pos'] for r in last_races if r['pos'] > 0]
            beaten = [min(r['beaten'], 50) for r in last_races]

            wins = sum(1 for r in last_races if r['pos'] == 1)
            places = sum(1 for r in last_races if 0 < r['pos'] <= 3)
            avg_finish = sum(positions)/len(positions) if positions else None
            std_finish = (sum((p-avg_finish)**2 for p in positions)/len(positions))**0.5 if positions and len(positions)>1 else 0.0
            avg_beaten = sum(beaten)/len(beaten) if beaten else None

            lam = 0.03
            weights = [math.exp(-lam * min(r['days'], 365)) for r in last_races]
            w_total = sum(weights) if sum(weights) > 0 else 1

            w_win = sum(w for r,w in zip(last_races, weights) if r['pos']==1)
            w_place = sum(w for r,w in zip(last_races, weights) if 0 < r['pos'] <= 3)
            w_avg_finish = (sum(r['pos']*w for r,w in zip(last_races, weights) if r['pos']>0)/w_total) if positions else None
            w_avg_beaten = sum(r['beaten']*w for r,w in zip(last_races, weights))/w_total if beaten else None

            trend_slope = None
            delta_avg = None
            if len(positions) >= 2:
                ys = positions
                xs = list(range(len(ys)))
                n = len(xs)
                mx = sum(xs)/n
                my = sum(ys)/n
                num = sum((x-mx)*(y-my) for x,y in zip(xs, ys))
                den = sum((x-mx)**2 for x in xs)
                trend_slope = num/den if den != 0 else 0
                if len(ys) >= 4:
                    delta_avg = (sum(ys[:2])/2) - (sum(ys[2:4])/2)

            cusum = 0.0
            if w_avg_finish is not None:
                threshold = w_avg_finish
                for r,w in zip(last_races, weights):
                    x = r['pos'] if r['pos']>0 else threshold
                    cusum += x - threshold

            sems = [sentiment(r['desc']) for r in last_races]
            sem_score = sum(sems)/len(sems) if sems else 0.5

            dist_ref = last_races[0]['distance'] if last_races else None
            dist_suit = None
            if dist_ref is not None and last_races:
                within = [1 for r in last_races if abs(r['distance'] - dist_ref) <= 0.2*dist_ref]
                dist_suit = sum(within)/len(last_races)

            confidence = 'Low'
            if len(last_races) >= 3 and any(r['pos']>0 for r in last_races):
                confidence = 'Medium'
            if len(last_races) >= 5:
                confidence = 'High'

            p_model_raw = p_market
            p_model_calibrated = p_market
            ev = p_model_calibrated * (odds - 1) * (1 - args.commission) - (1 - p_model_calibrated)

            row_out = {
                'marketId': active_market.get('marketId', ''),
                'marketName': active_market.get('marketName', ''),
                'raceDatetime': active_market.get('raceDatetime', ''),
                'selectionId': sel,
                'runnerName': runner.get('name', ''),
                'odds': odds,
                'p_market': round(p_market, 6),
                'p_model_raw': round(p_model_raw, 6),
                'p_model_calibrated': round(p_model_calibrated, 6),
                'EV_per_1': round(ev, 6),
                'TF_stars': '',
                'rpRating': '',
                'lastN_WWinRate': round(wins/lastN if lastN>0 else 0, 3),
                'lastN_WPlaceRate': round(places/lastN if lastN>0 else 0, 3),
                'lastN_WAvgFinish': round(avg_finish, 3) if avg_finish is not None else '',
                'lastN_StdFinish': round(std_finish, 3),
                'lastN_AvgBeaten': round(avg_beaten, 3) if avg_beaten is not None else '',
                'lastN_WAvgFinish_recency': round(w_avg_finish, 3) if w_avg_finish is not None else '',
                'lastN_WWinRate_recency': round(w_win/w_total, 3) if w_total > 0 else 0,
                'lastN_AvgBeaten_recency': round(w_avg_beaten, 3) if w_avg_beaten is not None else '',
                'trend_slope_pos': round(trend_slope, 4) if trend_slope is not None else '',
                'delta_avg_finish': round(delta_avg, 3) if delta_avg is not None else '',
                'cusum_change': round(cusum, 3) if cusum is not None else '',
                'semScore': round(sem_score, 3),
                'SurfaceSuit': '',
                'DistanceSuit': round(dist_suit, 3) if dist_suit is not None else '',
                'model_SHAP_top3': '',
                'confidence_label': confidence,
            }
            current_rows.append(row_out)
            writer.writerow(row_out)

    # Use the current_rows dataframe for model prediction instead of re-reading csv
    df_current = pd.DataFrame(current_rows)

    # --- model training + calibration (if we have enough historical data) ---
    # Build historical dataset from the lastRaces of each runner
    train_rows = []
    market_dt = pd.to_datetime(active_market.get('raceDatetime'))
    for sel, runner in active_market['runners'].items():
        runs = sorted(runner.get('lastRaces', []), key=lambda x: x['days'], reverse=True)
        # compute actual run dates
        for r in runs:
            r['run_dt'] = market_dt - pd.Timedelta(days=r['days'])

        # build per-run training examples using preceding lastN runs
        for idx in range(args.lastn, len(runs)):
            prior = runs[idx:idx+args.lastn]
            target = 1 if runs[idx]['pos'] == 1 else 0
            if not prior:
                continue
            # feature engineering (same as output features)
            positions = [p['pos'] for p in prior if p['pos'] > 0]
            beaten = [min(p['beaten'], 50) for p in prior]
            wins = sum(1 for p in prior if p['pos']==1)
            places = sum(1 for p in prior if 0 < p['pos'] <= 3)
            avg_finish = np.mean(positions) if positions else np.nan
            std_finish = np.std(positions, ddof=0) if len(positions)>1 else 0.0
            avg_beaten = np.mean(beaten) if beaten else np.nan
            lam = 0.03
            weights = [math.exp(-lam * min(p['days'], 365)) for p in prior]
            w_total = sum(weights) if sum(weights) > 0 else 1
            w_win = sum(w for p,w in zip(prior, weights) if p['pos']==1)
            w_place = sum(w for p,w in zip(prior, weights) if 0 < p['pos'] <= 3)
            w_avg_finish = (sum(p['pos']*w for p,w in zip(prior, weights) if p['pos']>0)/w_total) if positions else np.nan
            w_avg_beaten = (sum(min(p['beaten'],50)*w for p,w in zip(prior, weights))/w_total) if beaten else np.nan
            # trend uses chronological order (most recent first)
            ys = positions
            trend = 0.0
            delta_avg = 0.0
            if len(ys) >= 2:
                xs = list(range(len(ys)))
                mx = np.mean(xs)
                my = np.mean(ys)
                num = sum((x-mx)*(y-my) for x,y in zip(xs, ys))
                den = sum((x-mx)**2 for x in xs)
                trend = num/den if den != 0 else 0.0
                if len(ys) >= 4:
                    delta_avg = (np.mean(ys[:2]) - np.mean(ys[2:4]))

            train_rows.append({
                'selectionId': sel,
                'run_dt': runs[idx]['run_dt'],
                'target_win': target,
                'odds': odds_map.get(sel, np.nan),
                'wins': wins,
                'places': places,
                'avg_finish': avg_finish,
                'std_finish': std_finish,
                'avg_beaten': avg_beaten,
                'w_win': w_win,
                'w_place': w_place,
                'w_avg_finish': w_avg_finish,
                'w_avg_beaten': w_avg_beaten,
                'trend': trend,
                'delta_avg': delta_avg,
            })

    df_train = pd.DataFrame(train_rows)
    if len(df_train) >= 20:
        # prepare training data
        df_train = df_train.sort_values('run_dt')
        features = ['wins','places','avg_finish','std_finish','avg_beaten','w_win','w_place','w_avg_finish','w_avg_beaten','trend','delta_avg']
        X = df_train[features].fillna(0)
        y = df_train['target_win']

        if y.nunique() < 2:
            # Not enough variation to train a classifier
            with open('outputs/metadata/model_info.json', 'w', encoding='utf-8') as f:
                import json
                json.dump({'model': None, 'reason': 'only one label present in training data'}, f, indent=2)
        else:
            tscv = TimeSeriesSplit(n_splits=3)
            val_results = []
            oof = np.zeros(len(X))
            for train_idx, val_idx in tscv.split(X):
                X_train, X_val = X.iloc[train_idx], X.iloc[val_idx]
                y_train, y_val = y.iloc[train_idx], y.iloc[val_idx]
                clf = Pipeline([('scaler', StandardScaler()), ('clf', RandomForestClassifier(n_estimators=100, random_state=1))])
                clf.fit(X_train, y_train)
                probs = clf.predict_proba(X_val)[:, 1]
                oof[val_idx] = probs
                if y_val.nunique() < 2:
                    continue
                val_results.append({
                    'brier': brier_score_loss(y_val, probs),
                    'logloss': log_loss(y_val, probs),
                    'roc_auc': roc_auc_score(y_val, probs),
                })
            # calibrate on whole set
            model = Pipeline([('scaler', StandardScaler()), ('clf', RandomForestClassifier(n_estimators=100, random_state=1))])
            model.fit(X, y)
            calib = CalibratedClassifierCV(model, method='isotonic', cv='prefit')
            calib.fit(X, y)

            # apply to current market
            df_current = pd.read_csv(args.out)
            df_current['p_model_raw'] = df_current['p_market']
            X_curr = df_current[features].fillna(0)
            df_current['p_model_calibrated'] = calib.predict_proba(X_curr)[:, 1]
            df_current['EV_per_1'] = df_current['p_model_calibrated'] * (df_current['odds'] - 1) * (1 - args.commission) - (1 - df_current['p_model_calibrated'])
            df_current.to_csv(args.out, index=False)

            # save summary metrics
            os.makedirs('outputs/backtest', exist_ok=True)
            summary = {
                'brier_mean': float(np.mean([m['brier'] for m in val_results])),
                'logloss_mean': float(np.mean([m['logloss'] for m in val_results])),
                'roc_auc_mean': float(np.mean([m['roc_auc'] for m in val_results])),
                'n_train': len(X),
            }
            with open('outputs/backtest/summary.json', 'w', encoding='utf-8') as f:
                import json
                json.dump(summary, f, indent=2)

            os.makedirs('outputs/metadata', exist_ok=True)
            with open('outputs/metadata/model_info.json', 'w', encoding='utf-8') as f:
                json.dump({'model': 'RandomForestClassifier', 'calibration': 'isotonic', 'features': features}, f, indent=2)

            # calibration plot
            prob_true, prob_pred = calibration_curve(y, oof, n_bins=10)
            plt.figure(figsize=(6, 6))
            plt.plot(prob_pred, prob_true, marker='o')
            plt.plot([0, 1], [0, 1], linestyle='--', color='gray')
            plt.xlabel('Predicted probability')
            plt.ylabel('True probability')
            plt.title('Calibration curve')
            os.makedirs('outputs/plots', exist_ok=True)
            plt.savefig('outputs/plots/calibration.png')

            # simple P&L based on predicted probability (bet $1 each)
            df_train['pred'] = oof
            df_train['pnl'] = df_train['pred'] * (df_train['odds'] - 1) - (1 - df_train['pred'])
            df_train['cum_pnl'] = df_train['pnl'].cumsum()
            plt.figure(figsize=(6, 4))
            plt.plot(df_train['run_dt'], df_train['cum_pnl'])
            plt.xlabel('Date')
            plt.ylabel('Cumulative P&L')
            plt.title('Backtest P&L')
            plt.tight_layout()
            plt.savefig('outputs/plots/pnl.png')

    print('Updated', args.out)


if __name__ == '__main__':
    main()

    print('Updated', args.out)


if __name__ == '__main__':
    main()
