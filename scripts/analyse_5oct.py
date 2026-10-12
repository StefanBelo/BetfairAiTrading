#!/usr/bin/env python3
"""Join the 5 October strategy logs to the settled bet results.

Answers: how many trades closed before the race start time; what the executed
trades returned; and what two counterfactuals would have returned — backing only
the market favourite, and backing only the model's Rank 1 — each as a single
Back of GBP 100 at the price the log itself stated, held to the result.

Outcomes for selections that were NOT traded are supplied separately via
`_tmp_outcomes.json` ({marketId: {selectionName: "Winner"|"Loser"}}), read from
`get_market` for the settled markets.
"""
from __future__ import annotations

import json
import re
import sys
from datetime import datetime
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent))
from parse_strategy_logs import LOG_DIR, parse  # noqa: E402

RESULTS = Path("_tmp_betresults.json")
OUTCOMES = Path("_tmp_outcomes.json")
STAKE = 100.0


def dt(text: str) -> datetime:
    return datetime.fromisoformat(text)


def main() -> int:
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8", errors="replace")

    if not RESULTS.exists():
        print(
            f"{RESULTS} not found — the executed-book frames will be skipped. Stage the raw "
            f"get_all_bet_results() payload there to include them (see scripts/README.md).",
            file=sys.stderr,
        )
        bets = {}
    else:
        results = json.loads(RESULTS.read_text(encoding="utf-8"))["betResults"]
        bets = {m["market"]["marketId"]: m for m in results}
    extra = json.loads(OUTCOMES.read_text(encoding="utf-8")) if OUTCOMES.exists() else {}

    def outcome(market_id: str, name: str) -> str | None:
        m = bets.get(market_id)
        if m:
            for sb in m["selectionsBets"]:
                if sb["name"].lower() == name.lower():
                    return sb["status"]
        return (extra.get(market_id) or {}).get(name)

    rows, missing = [], []
    for path in sorted(LOG_DIR.glob("*.md")):
        rec = parse(path)
        mid = rec["marketId"]
        bet = bets.get(mid)
        by_name = {r["name"].lower(): r for r in rec.get("rowList", [])}

        def price_of(name: str | None) -> float | None:
            hit = by_name.get((name or "").lower())
            return hit["price"] if hit else None

        # the traded runner IS the market's favourite by design; trust the bet
        # record for its name, and the log's own table for the price it stated
        fav_name = bet["selectionsBets"][0]["name"] if bet else rec["favName"]
        row = {
            "marketId": mid,
            "fav": fav_name, "favPrice": price_of(fav_name) or rec["favPrice"],
            "r1": rec["r1Name"], "r1Price": rec["r1Price"],
            "dir": rec["direction"], "target": rec["target"],
            "noBet": rec["noBet"],
            "traded": bet is not None,
        }
        if bet:
            m, sb = bet["market"], bet["selectionsBets"][0]
            legs = sorted(sb["bets"], key=lambda b: dt(b["time"]))
            open_leg, close_leg = legs[0], legs[-1]
            if rec["direction"]:
                match = [b for b in legs if b["betType"] == rec["direction"]]
                if match:
                    open_leg = match[0]
                    close_leg = legs[1] if legs.index(open_leg) == 0 else legs[0]
            start = dt(m["startTime"])
            row |= {
                "event": f'{m["eventName"]} {m["marketName"]}',
                "tradedName": sb["name"], "tradedStatus": sb["status"],
                "openTime": open_leg["time"][11:19], "openType": open_leg["betType"],
                "openPrice": open_leg["price"], "openSize": open_leg["size"],
                "closeTime": close_leg["time"][11:19], "closeType": close_leg["betType"],
                "closePrice": close_leg["price"],
                "closedBeforeOff": dt(close_leg["time"]) < start,
                "secondsAfterOff": round((dt(close_leg["time"]) - start).total_seconds()),
                "dirMatched": open_leg["betType"] == rec["direction"] if rec["direction"] else None,
                "profit": sb["profit"],
            }
        rows.append(row)

    # ---- reports -----------------------------------------------------------
    print("=== PER MARKET ===")
    print("marketId\tevent\tfav\tfavP\tdir\ttgt\ttraded\ttradedName\topen@\tclose@\tclosedBeforeOff\tsecsVsOff\tprofit")
    for r in rows:
        if not r["traded"]:
            print(f'{r["marketId"]}\t-\t{r["fav"]}\t{r["favPrice"]}\t{r["dir"]}\t{r["target"]}\t'
                  f'NO-BET={r["noBet"]}\t-\t-\t-\t-\t-\t-')
            continue
        print("\t".join(str(r.get(k, "")) for k in (
            "marketId", "event", "fav", "favPrice", "dir", "target", "tradedName",
        )) + "\t" + "\t".join(str(r.get(k, "")) for k in (
            "openTime", "closeTime", "closedBeforeOff", "secondsAfterOff", "profit")))

    traded = [r for r in rows if r["traded"]]
    print("\n=== SUMMARY ===")
    print(f"logs: {len(rows)}   traded markets: {len(traded)}   logs with no bet: {len(rows) - len(traded)}")
    print(f"no-bet flagged by log: {sum(1 for r in rows if r['noBet'])}")
    before = sum(1 for r in traded if r["closedBeforeOff"])
    print(f"closed BEFORE the off: {before} of {len(traded)}")
    print(f"closed AFTER  the off: {len(traded) - before} of {len(traded)}")
    after = sorted((r for r in traded if not r["closedBeforeOff"]), key=lambda r: -r["secondsAfterOff"])
    for r in after:
        print(f"   +{r['secondsAfterOff']:>4}s  {r['marketId']}  {r['event']}  close {r['closeTime']} ({r['closeType']} {r['closePrice']})")
    print(f"executed direction matched the log: {sum(1 for r in traded if r['dirMatched'])} of {len(traded)}")
    for r in traded:
        if r["dirMatched"] is False:
            print(f"   MISMATCH {r['marketId']} {r['event']}: log says {r['dir']}, "
                  f"opened with {r['openType']} {r['openPrice']}")
    print(f"executed P&L: {sum(r['profit'] for r in traded):+.2f} over {len(traded)} markets")
    gaps = sorted(r["secondsAfterOff"] for r in traded)
    if gaps:
        print(f"close-vs-off seconds: min {gaps[0]} median {gaps[len(gaps)//2]} max {gaps[-1]}")

    print("\n=== LOGS WITH NO SETTLED BET ===")
    for r in rows:
        if r["traded"]:
            continue
        text = (LOG_DIR / f'{r["marketId"]}.md').read_text(encoding="utf-8", errors="replace")
        claims = bool(re.search(r'"status"\s*:\s*true', text, re.I))
        print(f'{r["marketId"]}  fav {r["fav"]:22s} log-says-NO-BET={r["noBet"]:<5} '
              f'log-claims-status:true={claims}')

    # ---- counterfactuals --------------------------------------------------
    def back_leg(rows_in, name_key, price_key, label):
        total, bets_made, unknown = 0.0, 0, []
        for r in rows_in:
            name, price = r[name_key], r[price_key]
            if not name or not price:
                continue
            out = outcome(r["marketId"], name)
            if out is None:
                unknown.append(f'{r["marketId"]}:{name}')
                continue
            total += STAKE * (price - 1.0) if out == "Winner" else -STAKE
            bets_made += 1
        print(f"\n{label}: {total:+.2f} over {bets_made} bets"
              f"  (ROI {100 * total / (bets_made * STAKE) if bets_made else 0:+.1f}%)")
        if unknown:
            print(f"   unknown outcomes ({len(unknown)}): {', '.join(unknown)}")
        return total

    back_leg(traded, "fav", "favPrice", "Back the market favourite only (traded markets)")
    back_leg(rows, "fav", "favPrice", "Back the market favourite only (ALL log markets)")
    back_leg(rows, "r1", "r1Price", "Back the model Rank 1 only (ALL log markets)")

    # same-selection bet-only, using the log's stated direction and its Rank-1 name/price
    bet_only, n = 0.0, 0
    unknown = []
    for r in traded:
        name, price = r["tradedName"], r.get("openPrice")
        out = outcome(r["marketId"], name)
        if out is None or not price:
            unknown.append(f'{r["marketId"]}:{name}')
            continue
        if r["dir"] == "Back":
            bet_only += STAKE * (price - 1.0) if out == "Winner" else -STAKE
        else:  # Lay: win = stake, lose = liability (price-1)*stake
            bet_only += STAKE if out == "Loser" else -STAKE * (price - 1.0)
        n += 1
    print(f"\nBet-only on the same selections, same direction, £100 (at the executed opening price): "
          f"{bet_only:+.2f} over {n} bets")
    if unknown:
        print(f"   unknown outcomes ({len(unknown)}): {', '.join(unknown)}")

    print("\n=== THE MODEL'S AGREEMENT CUT (favourite Back £100, held to result) ===")
    def agrees(r: dict) -> bool:
        return bool(r["r1"] and r["fav"]) and r["r1"].lower() == r["fav"].lower()

    for label, subset in (("favourite IS  Rank 1 (model agrees)", [r for r in rows if agrees(r)]),
                          ("favourite is NOT Rank 1 (disagrees)", [r for r in rows if r["r1"] and r["fav"] and not agrees(r)])):
        totals, wins, n = 0.0, 0, 0
        for r in subset:
            res = outcome(r["marketId"], r["fav"])
            if not res or not r["favPrice"]:
                continue
            totals += STAKE * (r["favPrice"] - 1) if res == "Winner" else -STAKE
            wins += res == "Winner"
            n += 1
        roi = 100 * totals / (n * STAKE) if n else 0.0
        print(f"  {label}: {n} markets, favourite won {wins}, net {totals:+.2f} (ROI {roi:+.1f}%)")

    print("\n=== FAVOURITE INTEGRITY (traded runner vs the log's own cheapest row) ===")
    traded_rows = [r for r in rows if r["traded"]]
    off = 0
    for r in traded_rows:
        parsed = parse(LOG_DIR / f'{r["marketId"]}.md')
        cheapest = min(parsed.get("rowList", []), key=lambda q: q["price"]) if parsed.get("rowList") else None
        if cheapest and cheapest["name"].lower() != r["tradedName"].lower():
            off += 1
            print(f'   {r["marketId"]}: traded {r["tradedName"]} (opened {r.get("openPrice")}), '
                  f'cheapest row {cheapest["name"]} @ {cheapest["price"]}')
    print(f"traded runner is the cheapest row in {len(traded_rows) - off} of {len(traded_rows)} markets")

    print("\n=== APPENDIX ROWS (market | event | fav | favP | favOut | favBack | r1 | r1P | r1Out | r1Back | dir | tgt | traded | closedBeforeOff | profit) ===")
    for r in rows:
        fo, ro = outcome(r["marketId"], r["fav"]), outcome(r["marketId"], r["r1"])
        fcf = (STAKE * (r["favPrice"] - 1) if fo == "Winner" else -STAKE) if fo and r["favPrice"] else 0.0
        rcf = (STAKE * (r["r1Price"] - 1) if ro == "Winner" else -STAKE) if ro and r["r1Price"] else 0.0
        print(" | ".join(str(x) for x in (
            r["marketId"], r.get("event", "-"), r["fav"], r["favPrice"], fo or "?", f"{fcf:+.0f}",
            r["r1"], r["r1Price"], ro or "?", f"{rcf:+.0f}",
            r["dir"] or "-", r["target"] or "-", "yes" if r["traded"] else "NO",
            r.get("closedBeforeOff", "-"), f"{r['profit']:+.2f}" if r["traded"] else "-",
        )))
    return 0


if __name__ == "__main__":
    sys.exit(main())
