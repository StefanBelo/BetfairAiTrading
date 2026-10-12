#!/usr/bin/env python3
"""Extract the traded selection, direction, target and Rank-1 row from strategy logs.

Reads every `*.md` in `data/AIStrategyOutputs/` (excluding `Old/`) and prints one
tab-separated record per log. Logs whose field table or parameters cannot be read
are printed with the offending field blank and a `!` marker in the `issue` column,
so nothing is silently dropped.
"""
from __future__ import annotations

import re
import sys
from pathlib import Path

LOG_DIR = Path("data/AIStrategyOutputs")

FAV = [
    re.compile(r"Favourite[^\n]{0,40}?= \*\*(?P<name>[^*@]+?) @ (?P<price>[\d.]+)\*\*"),
    re.compile(r"[Ff]avourite \(shortest `price`\) = \*\*(?P<name>[^*@]+?) @ (?P<price>[\d.]+)\*\*"),
    re.compile(r"[Ff]avourite\s*[:=]\s*\*\*(?P<name>[^*@]+?) @ (?P<price>[\d.]+)\*\*"),
]
PARAMS = re.compile(
    r'"OpenBetPosition\.BetType"\s*:\s*"(?P<type>Back|Lay)"\s*,\s*'
    r'"CloseBetPosition\.Profit"\s*:\s*(?P<profit>\d+)'
)
NOBET = re.compile(r"Strategy Recommendation[^\n]{0,30}NO[ -]?BET", re.I)
_MARKER = re.compile(r"[★⭐◆◇†⌂⟡⬤§^]|\s+[\(\[][^\)\]]*[\)\]]")


def clean(name: str) -> str:
    """Strip the marker glyphs logs use to flag the favourite / shortlist / Rank 1."""
    text = name.replace("*", "").strip()
    text = re.sub(r"^[^A-Za-z0-9]+\s*", "", text)          # leading markers, ASCII or not
    text = re.split(r"[^\x00-\x7F]|\s+[\(\[][^\)\]]*[\)\]]", text)[0]
    return text.strip(" ,;:-")


def num(cell: str | None) -> float | None:
    if not cell:
        return None
    text = cell.replace("*", "").replace("$", "").replace("£", "").replace("%", "")
    found = re.search(r"-?\d+(?:\.\d+)?", text)
    return float(found.group()) if found else None


def parse(path: Path) -> dict:
    text = path.read_text(encoding="utf-8", errors="replace")
    rec: dict = {
        "marketId": path.stem,
        "kind": "VDW" if "VDW Report" in text else ("V2" if "Handicapping Report" in text else "?"),
        "noBet": bool(NOBET.search(text)),
        "issues": [],
    }

    last = None
    for match in PARAMS.finditer(text):
        last = match
    if last:
        rec["direction"] = last.group("type")
        rec["target"] = int(last.group("profit"))
    else:
        rec["direction"], rec["target"] = "", ""
        if not rec["noBet"]:
            rec["issues"].append("no-params")

    header, rows = None, []
    for raw in text.splitlines():
        line = raw.strip()
        if not line.startswith("|"):
            continue
        cells = [c.strip() for c in line.strip().strip("|").split("|")]
        if header is None:
            if any("model prob" in c.lower() for c in cells):
                header = [c.lower() for c in cells]
            continue
        if set("".join(cells)) <= set(":- "):
            continue
        # a second table (e.g. a snapshot/entry comparison) starts with its own
        # header row; stop rather than absorb its rows as field-table data
        if any(c.lower() in {"price", "gap", "rank", "horse", "ev", "edge", "p"} for c in cells):
            break

        def col(*needles: str) -> str | None:
            for i, name in enumerate(header or []):
                if any(n in name for n in needles):
                    return cells[i] if i < len(cells) else None
            return None

        name = col("horse")
        price_cell = col("entry price") or col("snapshot p", "price", "best back")
        price = num(price_cell)
        if not name or price is None:
            continue
        rows.append(
            {
                "name": clean(name),
                "raw": name,
                "qualifies": "yes" in (col("qualifies") or "").lower(),
                "price": price,
                "model": num(col("model prob", "model probability")),
                "market": num(col("market prob", "mkt prob", "market probability")),
                "ev": num(col("ev")),
                "rank": num(col("rank")),
            }
        )

    rec["rows"] = len(rows)
    rec["rowList"] = rows
    # the logs mark the favourite themselves (★ / ⭐ / "fav" in the horse cell);
    # that declaration outranks both the prose regex and the cheapest row
    marked = [r for r in rows if re.search(r"[★⭐]|\bfav", r["raw"], re.I)]
    rec["markedFav"] = (marked[0]["name"], marked[0]["price"]) if len(marked) == 1 else None
    rank1 = [r for r in rows if r["rank"] == 1]
    if rank1:
        rec["r1Name"], rec["r1Price"], rec["r1Model"] = rank1[0]["name"], rank1[0]["price"], rank1[0]["model"]
    else:
        rec["r1Name"] = rec["r1Price"] = rec["r1Model"] = ""
        rec["issues"].append("no-rank1")

    if rec.get("markedFav"):
        rec["favName"], rec["favPrice"] = rec["markedFav"]
        rec["favSource"] = "marked"
    else:
        for pat in FAV:
            hit = pat.search(text)
            if hit:
                rec["favName"] = clean(hit.group("name"))
                rec["favPrice"] = float(hit.group("price"))
                rec["favSource"] = "prose"
                break
        if "favName" not in rec:
            if rows:
                best = min(rows, key=lambda r: r["price"])
                rec["favName"], rec["favPrice"] = best["name"], best["price"]
                rec["favSource"] = "cheapest"
                rec["issues"].append("fav-inferred")
            else:
                rec["favName"] = rec["favPrice"] = ""
                rec["favSource"] = "none"
                rec["issues"].append("no-fav")

    return rec


def main() -> int:
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(encoding="utf-8", errors="replace")
    logs = sorted(p for p in LOG_DIR.glob("*.md"))
    print("marketId\tkind\tfavName\tfavPrice\tr1Name\tr1Price\tr1Model\tdir\ttarget\tnoBet\trows\tissues")
    bad = 0
    for path in logs:
        rec = parse(path)
        if rec["issues"]:
            bad += 1
        print(
            "\t".join(
                [
                    rec["marketId"], rec["kind"], str(rec["favName"]), str(rec["favPrice"]),
                    str(rec["r1Name"]), str(rec["r1Price"]), str(rec["r1Model"]),
                    str(rec["direction"]), str(rec["target"]), str(rec["noBet"]),
                    str(rec["rows"]), ",".join(rec["issues"]) or "-",
                ]
            )
        )
    print(f"\n{len(logs)} logs parsed, {bad} with issues", file=sys.stderr)
    return 0


if __name__ == "__main__":
    sys.exit(main())
