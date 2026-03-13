import re
from pathlib import Path
from datetime import datetime
import math

def parse_context(path):
    runners = []
    current = None
    section = None
    with open(path,'r',encoding='utf-8') as f:
        for raw in f:
            line = raw.rstrip('\n')
            stripped = line.lstrip()
            if stripped.startswith('- SelectionId'):
                if current:
                    runners.append(current)
                current = {'timePriceVolumes':[], 'candleStickData':[]}
                section = None
            elif current and stripped.startswith('Name:'):
                current['Name'] = stripped.split(':',1)[1].strip().strip('"')
            elif current and stripped.startswith('Rating:'):
                try:
                    current['Rating'] = float(stripped.split(':',1)[1])
                except:
                    pass
            elif current and stripped.startswith('StarRating:'):
                try:
                    current['StarRating'] = float(stripped.split(':',1)[1])
                except:
                    pass
            elif stripped.startswith('timePriceVolumes'):
                section = 'tpv'
            elif stripped.startswith('candleStickData'):
                section = 'csd'
            elif section == 'tpv' and stripped.startswith('"'):
                parts = stripped.split(',')
                if len(parts) >=3:
                    time = parts[0].strip().strip('"')
                    price = float(parts[1])
                    vol = float(parts[2])
                    current['timePriceVolumes'].append((time,price,vol))
            elif section == 'csd' and re.match(r'\s*\d', stripped):
                parts = stripped.split(',')
                if len(parts)>=6:
                    close = float(parts[0])
                    date = parts[1].strip('"')
                    high = float(parts[2])
                    low = float(parts[3])
                    open_ = float(parts[4])
                    vol = float(parts[5])
                    current['candleStickData'].append((close,date,high,low,open_,vol))
            elif stripped.startswith('backLayRatio') and current is not None:
                try:
                    current['backLayRatio'] = float(stripped.split(':',1)[1])
                except:
                    pass
            elif any(stripped.startswith(flag) for flag in ['HorseInForm:','TrainerInForm:','JockeyInForm:','SuitedByGoing:','TimeformTopRated:','TimeformImprover:']):
                k,v = stripped.split(':',1)
                if current is not None:
                    current[k.strip()] = v.strip().lower() == 'true'
    if current:
        runners.append(current)
    return runners


def compute_expected_volume(n_runners, race_class, is_handicap):
    if race_class.lower().startswith('grade'):
        mc = 2.5
    elif race_class.startswith('Class'):
        try:
            num = int(re.search(r"\\d+", race_class).group())
        except:
            num=3
        if num <= 2:
            mc = 1.5
        elif num <= 4:
            mc = 1.0
        else:
            mc = 0.6
    else:
        mc = 1.0
    mt = 1.2 if is_handicap else 0.8
    fr = n_runners * 500
    return fr * mc * mt


def compute_depth_score(trades):
    if not trades:
        return 0.2
    times = [datetime.fromisoformat(t) for t, _, _ in trades]
    span = (max(times) - min(times)).total_seconds() / 60
    if span <= 0:
        return 1.0
    rate = len(trades) / span
    if rate > 60:
        return 1.0
    if rate < 10:
        return 0.2
    return rate / 60


def compute_volatility_score(candles):
    if not candles:
        return 0
    close, date, high, low, open_, vol = candles[-1]
    return ((high - low) / close) * 100


def softmax(scores, tau=0.15):
    exps = [math.exp(s / tau) for s in scores]
    s = sum(exps)
    return [e / s for e in exps]


def main():
    ctx_path = Path(r"c:\Users\Stefan\AppData\Roaming\Code\User\workspaceStorage\a5c8b31b567c6e6719dc75956c695ab7\GitHub.copilot-chat\chat-session-resources\33189df4-d0c3-495b-b7c6-c24a15b0b3e8\call_qs1e36GnNx2mnKQ0BXA3Em2B__vscode-1771662084402\content.txt")
    runners = parse_context(ctx_path)
    total_volume = sum(sum(v for _,_,v in r['timePriceVolumes']) for r in runners)
    race_class = "Class 3"
    is_handicap = False
    v_exp = compute_expected_volume(len(runners), race_class, is_handicap)
    v_act = total_volume
    R = (v_act - v_exp) / v_exp if v_exp else 0
    depth_scores = [compute_depth_score(r['timePriceVolumes']) for r in runners]
    vol_scores = [compute_volatility_score(r['candleStickData']) for r in runners]
    qualities, forms, flows, bias_probs, prices, vol_pct_list = [],[],[],[],[],[]
    for i,r in enumerate(runners):
        price = r['timePriceVolumes'][-1][1] if r['timePriceVolumes'] else 0
        prices.append(price)
        rating = r.get('Rating',0)
        star = r.get('StarRating',0)
        Q = 0.7*min(max(rating/140,0),1) + 0.3*min(max(star/5,0),1)
        qualities.append(Q)
        F = 0.5
        for flag in ['HorseInForm','TrainerInForm','JockeyInForm','SuitedByGoing','TimeformTopRated','TimeformImprover']:
            if r.get(flag):
                F += 0.10 if flag in ['HorseInForm','TimeformTopRated','TimeformImprover'] else 0.05
        F = min(max(F,0),1)
        forms.append(F)
        ds = depth_scores[i]
        vs = vol_scores[i]
        S = 0.5*ds + 0.5*(1 - min(vs/100,1))
        flows.append(S)
        vols = sum(v for _,_,v in r['timePriceVolumes'])
        pct = vols / total_volume if total_volume>0 else 0
        vol_pct_list.append(pct*100)
        bp = 0.1 + max(0, 0.25 - pct)
        bp = min(bp,0.25)
        bias_probs.append(bp)
    strength = [0.45*qualities[i] + 0.30*forms[i] + 0.25*flows[i] - 0.30*bias_probs[i] for i in range(len(runners))]
    probs = softmax(strength)
    edges = [probs[i] - (1/prices[i] if prices[i]>0 else 0) for i in range(len(runners))]
    sorted_idx = sorted(range(len(runners)), key=lambda i: probs[i], reverse=True)
    suggestions = []
    for i in range(len(runners)):
        pos = sorted_idx.index(i)
        if abs(edges[i]) < 0.02:
            suggestions.append('IGNORE')
        elif pos<2 and edges[i]>0:
            suggestions.append('BACK')
        elif pos<2 and edges[i]<0:
            suggestions.append('LAY')
        else:
            suggestions.append('IGNORE')
    print("| Selection | Price | Vol % | Bias Prob | Volatility | R | Strength | P(win) | Edge | Suggestion |")
    print("|---|---|---|---|---|---|---|---|---|")
    for i,r in enumerate(runners):
        print(f"| {r.get('Name','')} | {prices[i]:.2f} | {vol_pct_list[i]:.1f}% | {bias_probs[i]:.2f} | {vol_scores[i]:.2f}% | {R*100:.1f}% | {strength[i]:.3f} | {probs[i]*100:.1f}% | {edges[i]*100:.1f}% | {suggestions[i]} |")
    print("\nGate stats: R=", R)

if __name__=='__main__':
    main()
