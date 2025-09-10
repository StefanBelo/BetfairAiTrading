# Stratégia stávkovania na dostihy: Stávky iba na favorita (BSP) - Tichý režim

**Cieľ**: Realizovať stávkovú stratégiu na favorita pomocou hodnotových kritérií založených na BSP. Výstup iba výsledky vykonania.

**Logika**:
- Vypočítajte BSP metriky: bspEdge = (1/betfairSP) - (1/price), bspEvNet = (1/betfairSP) × price - 1
- Vypočítajte priceRatio = price / betfairSP
- Nájdite favorita (najnižšia cena)
- BACK ak: eVforPriceOrBetfairSP > 0 A bspEvNet > -0.02
- LAY ak: kritériá kandidáta NIE sú splnené

**Nástroje**: `get_active_betfair_market` → `get_data_context_for_betfair_market` (dataContextName: "BetfairSpData") → `execute_bfexplorer_strategy_settings`

## Príkaz na vykonanie

```
1. Získaj aktívny trh betfair pre ID trhu
2. Použite ID trhu na získanie kontextu "BetfairSpData" pre tento konkrétny trh
3. Vypočítajte BSP metriky pre favorita
4. Realizujte vhodnú stratégiu (Bet 10 Euro/Lay 10 Euro) na favorita
5. Nahláste iba výsledok vykonania
```

## Formát výstupu

**Výsledok vykonania stratégie:**
- **Trh**: [Názov dostihov/Čas]
- **Favorit**: [Názov koňa] (Cena: [kurz], BSP: [bsp])
- **Vykonaná stratégia**: [Bet 10 Euro/Lay 10 Euro]
- **ID výberu**: [selectionId]
- **Dôvod**: [Splnené kritériá BACK/Splnené kritériá LAY/Chýbajúce údaje]
- **BSP metriky**: Pomerná cena [X.XX], BSP hrana [X.XXXX], BSP EV netto [X.XXXX]
- **Stav**: [ÚSPECH/ZLYHANIE/OBÍDENÉ]