# Dostihy: Favorit (BSP) – tichý režim

Cieľ: Nasadiť stratégiu iba na favorita podľa BSP; výstup zobrazí len výsledok vykonania.

Logika:
- Nájdite favorita (najnižší kurz `price`).
- Spočítajte metriky:
	- `bspEdge = (1/betfairSP) - (1/price)`
	- `bspEvNet = price/betfairSP - 1`
	- `priceRatio = price/betfairSP`
- Rozhodnutie:
	- BACK, ak `eVforPriceOrBetfairSP > 0` a `bspEvNet > -0.02`
	- Inak LAY (alebo preskočte, ak chýbajú dáta)

Nástroje: `get_active_market` → `get_all_data_context_for_market("BetfairSpData")` → `execute_bfexplorer_strategy_settings`

## Kroky vykonania

1. Získaj aktívny trh (marketId).
2. Načítaj kontext „BetfairSpData“ pre daný trh.
3. Urči favorita a vypočítaj metriky.
4. Vykonaj vhodnú stratégiu (Back 10 EUR/Lay 10 EUR) na favorita.
5. Vypíš iba výsledok vykonania.

## Formát výstupu

- Trh: [názov/čas]
- Favorit: [kôň] (Cena: [kurz], BSP: [bsp])
- Akcia: [Back 10 EUR/Lay 10 EUR]
- selectionId: [id]
- Dôvod: [BACK kritériá/ LAY kritériá/ Chýbajúce údaje]
- BSP metriky: `priceRatio`=[x.xx], `bspEdge`=[x.xxxx], `bspEvNet`=[x.xxxx]
- Stav: [ÚSPECH/ZLYHANIE/OBÍDENÉ]