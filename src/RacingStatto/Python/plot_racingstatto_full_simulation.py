import itertools
import matplotlib.pyplot as plt
import numpy as np

import requests

# Load races from API
url = "http://localhost:10043/api/getAIAgentDataContextFeedback?dataContextName=RacesResultsForRacingStattoData&forLastResults=1000"
try:
    response = requests.get(url)
    response.raise_for_status()
    races = response.json()
    print(f"Loaded {len(races)} races from API.")
except Exception as e:
    print(f"Failed to load races from API: {e}")
    races = []

# To simulate, we need the full data. Since it's large, I'll use a placeholder and note to replace with actual data.

# Candidate rules: combinations of thresholds for rank, timeRank, fastestTimeRank, averageRank
rank_thresh = [1, 2, 3, 4, 5]
time_thresh = [1, 2, 3, 4, 5]
fastest_thresh = [1, 2, 3, 4, 5]
avg_thresh = [3, 4, 5, 6]  # averageRank thresholds

results = []

for r, t, f, a in itertools.product(rank_thresh, time_thresh, fastest_thresh, avg_thresh):
    total_profit = 0
    total_bets = 0
    for race in races:
        for runner in race['runners']:
            data = runner['racingStattoData']
            if data['rank'] <= r and data['timeRank'] <= t and data['fastestTimeRank'] <= f and data['averageRank'] <= a:
                total_bets += 1
                if runner['isWinner']:
                    total_profit += runner['spPrice'] - 1
                else:
                    total_profit -= 1
    if total_bets > 0:
        roi = total_profit / total_bets
        results.append({'rank': r, 'timeRank': t, 'fastestTimeRank': f, 'averageRank': a, 'n_bets': total_bets, 'profit': total_profit, 'roi': roi})

# Sort by ROI descending, then by n_bets descending
results = sorted(results, key=lambda x: (x['roi'], x['n_bets']), reverse=True)

# Top 10 rules
top_rules = results[:10]

# Plot
labels = [f"r≤{r['rank']}, t≤{r['timeRank']}, f≤{r['fastestTimeRank']}, a≤{r['averageRank']}\nN={r['n_bets']}" for r in top_rules]
rois = [r['roi'] for r in top_rules]

x = np.arange(len(top_rules))
plt.figure(figsize=(14,7))
plt.bar(x, rois, color='lightgreen' if any(r > 0 for r in rois) else 'lightcoral')
plt.xticks(x, labels, rotation=45, ha='right')
plt.ylabel('ROI per Bet')
plt.title('Top 10 RacingStattoData Rules by ROI (Full Simulation with Losses)')
plt.axhline(0, color='black', linewidth=0.5)
plt.tight_layout()
plt.show()

# Select the best rule: rank <=3, timeRank <=3, fastestTimeRank <=4, averageRank <=3
selected_rule = {'rank': 3, 'timeRank': 3, 'fastestTimeRank': 4, 'averageRank': 3}
#selected_rule = {'rank': 3, 'fastestTimeRank': 3}

# Simulate bets with this rule and list matching runners
matching_runners = []
cumulative_pl = []
total_pl = 0
for race in races:
    for runner in race['runners']:
        data = runner['racingStattoData']
        if data['rank'] <= selected_rule['rank'] and data['timeRank'] <= selected_rule['timeRank'] and data['fastestTimeRank'] <= selected_rule['fastestTimeRank'] and data['averageRank'] <= selected_rule['averageRank']:
        #if data['rank'] <= selected_rule['rank'] and data['fastestTimeRank'] <= selected_rule['fastestTimeRank']:
            pl = (runner['spPrice'] - 1) if runner['isWinner'] else -1
            total_pl += pl
            matching_runners.append({
                'race_time': race['raceData']['raceTime'],
                'racecourse': race['raceData']['racecourse'],
                'name': runner['name'],
                'rank': data['rank'],
                'timeRank': data['timeRank'],
                'fastestTimeRank': data['fastestTimeRank'],
                'averageRank': data['averageRank'],
                'spPrice': runner['spPrice'],
                'isWinner': runner['isWinner'],
                'pl': pl
            })
    cumulative_pl.append(total_pl)

# Print the list of matching runners in a table
print("Matching Runners for Rule (rank≤3, timeRank≤3, fastestTimeRank≤4, averageRank≤3):")
print(f"{'Race Time':<25} {'Racecourse':<15} {'Horse Name':<20} {'Rank':<5} {'TimeRank':<9} {'FastestTimeRank':<15} {'AverageRank':<12} {'SP Price':<9} {'Winner':<7} {'P/L':<5}")
print("-" * 120)
for r in matching_runners:
    print(f"{r['race_time']:<25} {r['racecourse']:<15} {r['name']:<20} {r['rank']:<5} {r['timeRank']:<9} {r['fastestTimeRank']:<15} {r['averageRank']:<12.2f} {r['spPrice']:<9.2f} {str(r['isWinner']):<7} {r['pl']:<5}")

print(f"\nTotal Bets: {len(matching_runners)}, Final P/L: {total_pl}")

# Plot P/L chart
plt.figure(figsize=(10,6))
plt.plot(range(1, len(cumulative_pl)+1), cumulative_pl, marker='o')
plt.xlabel('Race Number')
plt.ylabel('Cumulative P/L')
plt.title('Cumulative P/L for Selected Rule: rank≤3, timeRank≤3, fastestTimeRank≤4, averageRank≤3')
plt.grid(True)
plt.tight_layout()
plt.show()
