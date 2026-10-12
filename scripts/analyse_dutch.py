#!/usr/bin/env python3
"""Dutch-staking check on a day's strategy reports.

For every report from the given date, builds two selections sets —
the three best `Rank` runners, and the three best `Qualifies` runners sorted by
`Rank` — and prices the Dutch that returns a level PROFIT if any member wins.

A Dutch exists only when sum(1/price) < 1; where it does not, the outcome is
"impossible" and nothing is staked. Where it does, the stake on each selection is
total_return / price, and the loss if none of them wins is the whole outlay.

Usage:  python scripts/analyse_dutch.py [YYYY-MM-DD]
"""
from __future__ import annotations

import json
import sys
from datetime import date, datetime
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent))
from parse_strategy_logs import LOG_DIR, parse  # noqa: E402

PROFIT = 10.0
WINNERS = Path("_tmp_winners.json")


def dutch(selections: list[dict]) -> tuple[list[float] | None, float]:
    """Stakes for a level PROFIT if any selection wins, plus the book sum."""
    inv = sum(1.0 / s["price"] for s in selections)
    if inv >= 1.0:
        return None, inv
    total_return = PROFIT / (1.0 - inv)
    return [total_return / s["price"] for s in selections], inv


def norm(text: str) -> str:
    return "".join(ch for ch in (text or "").lower() if ch.isalpha())


def main() -> int:
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8", errors="replace")
    want = date.fromisoformat(sys.argv[1]) if len(sys.argv) > 1 else date.today()
    winners = json.loads(WINNERS.read_text(encoding="utf-8")) if WINNERS.exists() else {}

    tot = {k: {"n": 0, "possible": 0, "hit": 0, "pnl": 0.0, "staked": 0.0,
               "returned": 0.0, "impossible": []} for k in ("rank3", "qual3")}

    for path in sorted(LOG_DIR.glob("*.md")):
        if datetime.fromtimestamp(path.stat().st_mtime).date() != want:
            continue
        rec = parse(path)
        rows = [r for r in rec.get("rowList", []) if r["price"] and r["rank"]]
        for r in rows:
            r["name"] = r["name"].strip()
        by_rank = sorted(rows, key=lambda r: r["rank"])
        sets = {
            "rank3": by_rank[:3],
            "qual3": sorted([r for r in rows if r["qualifies"]], key=lambda r: r["rank"])[:3],
        }
        winner = winners.get(rec["marketId"])
        if not winner:
            print(f'\n{rec["marketId"]}  NO SETTLED RESULT — skipped')
            continue
        print(f'\n{rec["marketId"]}  winner={winner}')
        for key, sel in sets.items():
            t = tot[key]
            t["n"] += 1
            names = ", ".join(f'{s["name"]}@{s["price"]}' for s in sel)
            if len(sel) < 3:
                print(f"  {key:6s} only {len(sel)} selection(s): {names}")
            stakes, inv = dutch(sel) if sel else (None, 9.9)
            if not sel:
                continue
            if stakes is None:
                t["impossible"].append(rec["marketId"])
                print(f"  {key:6s} IMPOSSIBLE (book {inv:.3f}): {names}")
                continue
            t["possible"] += 1
            outlay = sum(stakes)
            hit = bool(winner) and any(norm(s["name"]) == norm(winner) for s in sel)
            pnl = PROFIT if hit else -outlay
            t["hit"] += hit
            t["pnl"] += pnl
            t["staked"] += outlay
            t["returned"] += max(pnl, 0.0)
            print(f"  {key:6s} book {inv:.3f}  stakes " +
                  ", ".join(f'{s["name"]} €{st:.2f}' for s, st in zip(sel, stakes)) +
                  f'  | outlay €{outlay:.2f}  hit={hit}  pnl €{pnl:+.2f}')

    print("\n=== SUMMARY ===")
    for key, label in (("rank3", "3 best by Rank"), ("qual3", "3 best Qualifies (by Rank)")):
        t = tot[key]
        print(f"\n{label}: {t['n']} markets, dutch possible {t['possible']}, "
              f"impossible {len(t['impossible'])}, hits {t['hit']}")
        print(f"  staked €{t['staked']:.2f}  net €{t['pnl']:+.2f}  "
              f"return on outlay {(100 * t['pnl'] / t['staked'] if t['staked'] else 0):+.1f}%")
        if t["impossible"]:
            print(f"  impossible markets: {', '.join(t['impossible'])}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
