#!/usr/bin/env python3
"""Validate a Trade V2 handicapping report for internal consistency.

Reads a `TradingReport.md` produced by any model running
`docs/Prompts/HorseRacing/HorsesBetfairRaceInfoDataTradeV2.md` and re-derives
everything the report could have computed rather than copied:

  * the model probability column totals 100.00 (+/- tolerance)
  * the market probability column is reproducible as 1/price, de-vigged
  * every EV equals (p / 100) * price - 1, from that row's own p
  * every Edge equals p / market - 1, from that row's own figures
  * the Rank column is the model probability order, descending
  * the executed direction agrees with the favourite's EV sign and with
    price against break-even (a Back needs p > 100 / price)
  * the profit target equals min(round(|EV| * 100), 30) as a whole number

What it cannot check: whether the model probabilities are *good*. A report can
be perfectly self-consistent and still wrong about the horses. This only proves
the arithmetic and the cross-column relations hold.

Usage:
    python scripts/validate_trading_report.py data/TestData/TradingReport.md
    python scripts/validate_trading_report.py report.md --quiet

Exit code is 0 when every check passes, 1 otherwise.
"""

from __future__ import annotations

import argparse
import json
import re
import sys
from dataclasses import dataclass, field

CEILING_PRICE = 1000.0
MODEL_SUM_TOL = 0.02
COLUMN_TOL = 0.06
VALUE_TOL = 0.015
TARGET_CAP = 30

_PCT = re.compile(r"[-+]?\d*\.?\d+")
_BET_TYPE = re.compile(r'"OpenBetPosition\.BetType"\s*:\s*"(\w+)"')
_PROFIT = re.compile(r'"CloseBetPosition\.Profit"\s*:\s*(-?\d+(?:\.\d+)?)')


@dataclass
class Row:
    horse: str
    rating: float | None
    model: float | None
    price: float | None
    market: float | None
    ev: float | None
    edge: float | None
    rank: int | None
    line: int

    @property
    def priced(self) -> bool:
        return self.price is not None and self.price < CEILING_PRICE

    @property
    def excluded(self) -> bool:
        return self.price is not None and self.price >= CEILING_PRICE


@dataclass
class Check:
    name: str
    ok: bool
    detail: str


@dataclass
class Report:
    path: str
    rows: list[Row] = field(default_factory=list)
    bet_type: str | None = None
    profit: int | None = None
    checks: list[Check] = field(default_factory=list)

    def add(self, name: str, ok: bool, detail: str) -> None:
        self.checks.append(Check(name, ok, detail))


def _num(cell: str) -> float | None:
    """Pull the first number out of a markdown cell, or None for n/a."""
    text = cell.replace("*", "").replace("$", "").replace("%", "").strip()
    if not text or text.lower().startswith("n/a") or text.lower() in {"-", "—"}:
        return None
    match = _PCT.search(text)
    return float(match.group()) if match else None


def _rank(cell: str) -> int | None:
    value = _num(cell)
    return int(value) if value is not None else None


def parse(path: str) -> Report:
    report = Report(path=path)
    text = open(path, encoding="utf-8").read()

    header: list[str] | None = None
    for lineno, raw in enumerate(text.splitlines(), start=1):
        line = raw.strip()
        if not line.startswith("|"):
            continue
        cells = [c.strip() for c in line.strip("|").split("|")]
        if header is None:
            if any("model probability" in c.lower() for c in cells):
                header = [c.lower() for c in cells]
            continue
        if set("".join(cells)) <= set(":- "):
            continue  # separator row

        def col(*needles: str) -> str | None:
            for i, name in enumerate(header):
                if any(n in name for n in needles):
                    return cells[i] if i < len(cells) else None
            return None

        horse = col("horse")
        price_cell = col("price")
        if horse is None or price_cell is None:
            continue
        report.rows.append(
            Row(
                horse=horse.replace("*", "").strip(),
                rating=_num(col("rating") or ""),
                model=_num(col("model probability") or ""),
                price=_num(price_cell),
                market=_num(col("market probability") or ""),
                ev=_num(col("ev") or ""),
                edge=_num(col("edge") or ""),
                rank=_rank(col("rank") or ""),
                line=lineno,
            )
        )

    found = _BET_TYPE.search(text)
    report.bet_type = found.group(1) if found else None
    found = _PROFIT.search(text)
    report.profit = int(float(found.group(1))) if found else None
    return report


def validate(report: Report) -> Report:
    rows = report.rows
    if not rows:
        report.add("parse", False, "no field-probability table found")
        return report
    report.add("parse", True, f"{len(rows)} runners read from the table")

    priced = [r for r in rows if r.priced and r.model is not None]
    excluded = [r for r in rows if r.excluded]

    # 1. Model column normalises.
    model_sum = sum(r.model or 0.0 for r in rows)
    report.add(
        "model column totals 100.00",
        abs(model_sum - 100.0) <= MODEL_SUM_TOL,
        f"sum = {model_sum:.2f}"
        + ("" if abs(model_sum - 100.0) <= MODEL_SUM_TOL else "  <-- vector is not normalised"),
    )

    # 2. Market column is reproducible from the prices.
    raw = {r.horse: 1.0 / (r.price or 1.0) for r in priced}
    raw_sum = sum(raw.values())
    book = raw_sum * 100
    derived = {h: v / raw_sum * 100 for h, v in raw.items()}
    worst = max(
        ((abs((r.market or 0.0) - derived[r.horse]), r) for r in priced if r.market is not None),
        default=(0.0, None),
    )[0]
    market_total = sum(r.market or 0.0 for r in priced)
    detail = f"book = {book:.2f}%, worst row gap = {worst:.2f}pp"
    if worst > COLUMN_TOL:
        offenders = [
            f"{r.horse}: printed {r.market:.2f} vs derived {derived[r.horse]:.2f}"
            for r in priced
            if r.market is not None and abs(r.market - derived[r.horse]) > COLUMN_TOL
        ]
        detail += "  <-- " + "; ".join(offenders[:4])
    report.add("market column = de-vig of 1/price", worst <= COLUMN_TOL, detail)

    # 3. Edge is not identically zero (the signature of a copied column).
    edges = [r.edge for r in priced if r.edge is not None]
    all_zero = bool(edges) and all(abs(e) <= 1e-9 for e in edges)
    report.add(
        "edge column carries a view",
        not all_zero,
        "every Edge = 0.00 — the market column restates the model column"
        if all_zero
        else f"{len(edges)} edge values, not all zero",
    )

    # 4. EV and Edge reproduce from each row's own figures.
    bad_ev, bad_edge = [], []
    for r in priced:
        if r.ev is not None and r.model is not None and r.price is not None:
            want = r.model / 100.0 * r.price - 1.0
            if abs(r.ev - want) > VALUE_TOL:
                bad_ev.append(f"{r.horse}: printed {r.ev:+.3f}, row gives {want:+.3f}")
        if r.edge is not None and r.model is not None and r.market:
            want = r.model / r.market - 1.0
            if abs(r.edge - want) > VALUE_TOL:
                bad_edge.append(f"{r.horse}: printed {r.edge:+.3f}, row gives {want:+.3f}")
    report.add(
        "every EV reproduces from its own row",
        not bad_ev,
        "; ".join(bad_ev[:4]) if bad_ev else f"{len(priced)} rows recomputed",
    )
    report.add(
        "every Edge reproduces from its own row",
        not bad_edge,
        "; ".join(bad_edge[:4]) if bad_edge else f"{len(priced)} rows recomputed",
    )

    # 5. Rank is the model probability order.
    ranked = [r for r in rows if r.rank is not None and r.model is not None]
    order = sorted(ranked, key=lambda r: r.rank or 0)
    descending = all(
        (order[i].model or 0.0) >= (order[i + 1].model or 0.0) for i in range(len(order) - 1)
    )
    ranks_are_1_to_n = sorted(r.rank or 0 for r in ranked) == list(range(1, len(ranked) + 1))
    detail = "rank order follows model probability" if descending else "rank order is not monotonic: "
    if not descending:
        detail += "; ".join(
            f"{order[i].rank}>{order[i + 1].rank}: {order[i].model:.2f}% < {order[i + 1].model:.2f}%"
            for i in range(len(order) - 1)
            if (order[i].model or 0.0) < (order[i + 1].model or 0.0)
        )[:200]
    report.add("rank = model probability order", descending and ranks_are_1_to_n, detail)

    # 6. The traded runner and the executed direction.
    favourite = min((r for r in rows if r.priced and r.price), key=lambda r: r.price or 9e9, default=None)
    if favourite is None or favourite.model is None:
        report.add("favourite identified", False, "no priced favourite found")
        return report

    breakeven = 100.0 / (favourite.price or 1.0)
    ev = favourite.model / 100.0 * (favourite.price or 0.0) - 1.0
    want_type = "Back" if ev > 0 else "Lay"
    report.add(
        "direction agrees with the favourite's EV",
        report.bet_type is not None and report.bet_type.lower() == want_type.lower(),
        f"favourite {favourite.horse} @ {favourite.price}: model {favourite.model:.2f}% needs "
        f"p > {breakeven:.2f}% for a Back; EV = {ev:+.4f} -> {want_type}"
        f"; report says {report.bet_type}",
    )
    report.add(
        "direction agrees with price against break-even",
        (report.bet_type or "").lower() == want_type.lower()
        and ((report.bet_type or "").lower() == "back") == (favourite.price > 100.0 / favourite.model),
        f"price {favourite.price} vs break-even {100.0 / favourite.model:.3f}",
    )
    if report.profit is not None:
        want_target = min(round(abs(ev) * 100), TARGET_CAP)
        report.add(
            "profit target = min(round(|EV| x 100), 30)",
            report.profit == want_target,
            f"expected {want_target} (raw {abs(ev) * 100:.2f}), report says {report.profit}",
        )

    # 7. Exclusions still normalise to 100 across the priced runners.
    if excluded:
        report.add(
            "market column totals 100.00 across priced runners",
            abs(market_total - 100.0) <= COLUMN_TOL,
            f"{len(excluded)} excluded at the ceiling; priced column sums to {market_total:.2f}",
        )
    return report


def main(argv: list[str] | None = None) -> int:
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8", errors="replace")
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("report", help="path to TradingReport.md")
    parser.add_argument("--quiet", action="store_true", help="only print failures")
    args = parser.parse_args(argv)

    report = validate(parse(args.report))
    failed = [c for c in report.checks if not c.ok]

    width = max((len(c.name) for c in report.checks), default=10)
    print(f"\n{args.report}")
    print("-" * (width + 40))
    for check in report.checks:
        if args.quiet and check.ok:
            continue
        print(f"  {'PASS' if check.ok else 'FAIL'}  {check.name:<{width}}  {check.detail}")

    print("-" * (width + 40))
    if failed:
        print(f"  {len(failed)} of {len(report.checks)} checks FAILED")
        print("  Note: a pass means the report is self-consistent, not that its probabilities are good.\n")
        return 1
    print(f"  all {len(report.checks)} checks passed")
    print("  Note: self-consistency only — the model probabilities themselves are not checked.\n")
    return 0


if __name__ == "__main__":
    sys.exit(main())
