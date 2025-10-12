# Horse Racing Win Rate Analysis by Odds Ranges

This document provides SQL queries to analyze historical win rates for horse racing based on Betfair starting prices. The analysis calculates the percentage of winners within specific odds ranges, which can inform betting strategies and expected value (EV) calculations.

## Overview

Win rates vary significantly by odds range due to market efficiency. Favorites typically have higher win rates, while longshots have lower ones. This research aims to quantify these rates using historical data.

## Basic Queries

### Total Number of Races
To get the total number of races (equivalent to total winners):

```sql
SELECT COUNT(Id) AS NumberOfRaces
FROM Runners
WHERE Position = 1
```

### Winners in a Specific Odds Range
Example for favorites (1.01-1.99):

```sql
SELECT COUNT(Id) AS Winners
FROM Runners
WHERE Position = 1
  AND BetfairStartPrice >= 1.01
  AND BetfairStartPrice <= 1.99
```

## Win Rate Calculations by Odds Range

For each odds range, we calculate:
- Total runners in the range
- Number of winners in the range
- Win rate = (winners / total runners) × 100

### 1.01-1.99 (Heavy Favorites)
**Total Runners:**
```sql
SELECT COUNT(*) AS TotalRunners
FROM Runners
WHERE BetfairStartPrice >= 1.01 AND BetfairStartPrice <= 1.99
```

**Winners:**
```sql
SELECT COUNT(*) AS Winners
FROM Runners
WHERE Position = 1
  AND BetfairStartPrice >= 1.01
  AND BetfairStartPrice <= 1.99
```

### 2.00-2.99
**Total Runners:**
```sql
SELECT COUNT(*) AS TotalRunners
FROM Runners
WHERE BetfairStartPrice >= 2.00 AND BetfairStartPrice <= 2.99
```

**Winners:**
```sql
SELECT COUNT(*) AS Winners
FROM Runners
WHERE Position = 1
  AND BetfairStartPrice >= 2.00
  AND BetfairStartPrice <= 2.99
```

### 3.00-4.99
**Total Runners:**
```sql
SELECT COUNT(*) AS TotalRunners
FROM Runners
WHERE BetfairStartPrice >= 3.00 AND BetfairStartPrice <= 4.99
```

**Winners:**
```sql
SELECT COUNT(*) AS Winners
FROM Runners
WHERE Position = 1
  AND BetfairStartPrice >= 3.00
  AND BetfairStartPrice <= 4.99
```

### 5.00-9.99
**Total Runners:**
```sql
SELECT COUNT(*) AS TotalRunners
FROM Runners
WHERE BetfairStartPrice >= 5.00 AND BetfairStartPrice <= 9.99
```

**Winners:**
```sql
SELECT COUNT(*) AS Winners
FROM Runners
WHERE Position = 1
  AND BetfairStartPrice >= 5.00
  AND BetfairStartPrice <= 9.99
```

### 10.00-19.99
**Total Runners:**
```sql
SELECT COUNT(*) AS TotalRunners
FROM Runners
WHERE BetfairStartPrice >= 10.00 AND BetfairStartPrice <= 19.99
```

**Winners:**
```sql
SELECT COUNT(*) AS Winners
FROM Runners
WHERE Position = 1
  AND BetfairStartPrice >= 10.00
  AND BetfairStartPrice <= 19.99
```

### 20.00-1000.00 (Longshots)
**Total Runners:**
```sql
SELECT COUNT(*) AS TotalRunners
FROM Runners
WHERE BetfairStartPrice >= 20.00 AND BetfairStartPrice <= 1000.00
```

**Winners:**
```sql
SELECT COUNT(*) AS Winners
FROM Runners
WHERE Position = 1
  AND BetfairStartPrice >= 20.00
  AND BetfairStartPrice <= 1000.00
```

## Combined SQL Query for Win Rate Report

To generate a comprehensive report with win rates for all ranges in a single query, use the following:

```sql
SELECT '1.01-1.99' AS OddsRange,
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2) AS WinRatePercent
FROM Runners
WHERE BetfairStartPrice >= 1.01 AND BetfairStartPrice <= 1.99

UNION ALL

SELECT '2.00-2.99',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners
WHERE BetfairStartPrice >= 2.00 AND BetfairStartPrice <= 2.99

UNION ALL

SELECT '3.00-4.99',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners
WHERE BetfairStartPrice >= 3.00 AND BetfairStartPrice <= 4.99

UNION ALL

SELECT '5.00-9.99',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners
WHERE BetfairStartPrice >= 5.00 AND BetfairStartPrice <= 9.99

UNION ALL

SELECT '10.00-19.99',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners
WHERE BetfairStartPrice >= 10.00 AND BetfairStartPrice <= 19.99

UNION ALL

SELECT '20.00+',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners
WHERE BetfairStartPrice >= 20.00
```

This query produces a table with two columns: `OddsRange` and `WinRatePercent`.

## Combined SQL Query for Win Rate Report (2025)

To generate a comprehensive report with win rates for all ranges in a single query for the year 2025, use the following:

```sql
SELECT '1.01-1.99' AS OddsRange,
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2) AS WinRatePercent
FROM Runners INNER JOIN Races ON Runners.RaceId = Races.Id
WHERE BetfairStartPrice >= 1.01 AND BetfairStartPrice <= 1.99 AND YEAR(Races.StartTime) = 2025

UNION ALL

SELECT '2.00-2.99',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners INNER JOIN Races ON Runners.RaceId = Races.Id
WHERE BetfairStartPrice >= 2.00 AND BetfairStartPrice <= 2.99 AND YEAR(Races.StartTime) = 2025

UNION ALL

SELECT '3.00-4.99',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners INNER JOIN Races ON Runners.RaceId = Races.Id
WHERE BetfairStartPrice >= 3.00 AND BetfairStartPrice <= 4.99 AND YEAR(Races.StartTime) = 2025

UNION ALL

SELECT '5.00-9.99',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners INNER JOIN Races ON Runners.RaceId = Races.Id
WHERE BetfairStartPrice >= 5.00 AND BetfairStartPrice <= 9.99 AND YEAR(Races.StartTime) = 2025

UNION ALL

SELECT '10.00-19.99',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners INNER JOIN Races ON Runners.RaceId = Races.Id
WHERE BetfairStartPrice >= 10.00 AND BetfairStartPrice <= 19.99 AND YEAR(Races.StartTime) = 2025

UNION ALL

SELECT '20.00+',
       ROUND((SUM(CASE WHEN Position = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2)
FROM Runners INNER JOIN Races ON Runners.RaceId = Races.Id
WHERE BetfairStartPrice >= 20.00 AND YEAR(Races.StartTime) = 2025
```

This query produces a table with two columns: `OddsRange` and `WinRatePercent` for races in 2025.

## Usage Instructions

1. Run the combined query against your historical racing database.
2. Review the calculated win rates for each odds range.
3. Format the results into a report, e.g.:
   - Odds 1.01-1.99 (Heavy favorites): ~42.35% win rate
   - Odds 2.00-2.99: ~28.15% win rate
   - Etc.
4. Use these empirical win rates to refine betting models, such as adjusting sentiment scores based on odds for better expected value calculations.

## Usage Instructions (2025)

1. Run the combined query against your historical racing database.
2. Review the calculated win rates for each odds range for 2025.
3. Format the results into a report, e.g.:
   - Odds 1.01-1.99 (Heavy favorites): ~XX% win rate in 2025
   - Odds 2.00-2.99: ~YY% win rate in 2025
   - Etc.
4. Compare with overall historical rates to identify year-specific trends.
5. Use these empirical win rates to refine betting models, such as adjusting sentiment scores based on odds for better expected value calculations in 2025.

## Notes

- Ensure your database schema matches the queries (e.g., `Runners` table with `Position`, `BetfairStartPrice` columns).
- Win rates may vary by track, distance, or time period; consider segmenting data further for more granular analysis.
- These calculations provide historical baselines; actual future performance may differ.