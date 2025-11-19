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

#fields = ['goingRank', 'distanceRank', 'goingDistRank', 'courseRank', 'totalWinsRank', 'percentageTotalRank', 'orRank', 'tsRank', 'rprRank', 'jockeyPercentageRank', 'trainerPercentageRank', 'timeRank', 'fastestTimeRank', 'rank', 'averageRank']
#fields = ['jockeyPercentageRank', 'trainerPercentageRank', 'rank']
fields = ['timeRank', 'fastestTimeRank', 'rank']

# Candidate rules: global threshold for all fields
thresholds = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

results = []

for t in thresholds:
    total_profit = 0
    total_bets = 0
    for race in races:
        for runner in race['runners']:
            data = runner['racingStattoData']
            if all(data.get(f, 100) <= t for f in fields):
                total_bets += 1
                if runner['isWinner']:
                    total_profit += runner['spPrice'] - 1
                else:
                    total_profit -= 1
    if total_bets > 0:
        roi = total_profit / total_bets
        results.append({'threshold': t, 'n_bets': total_bets, 'profit': total_profit, 'roi': roi})

# Sort by ROI descending, then by n_bets descending
results = sorted(results, key=lambda x: (x['roi'], x['n_bets']), reverse=True)

# Top 10 rules
top_rules = results[:10]

# Print top rules
print("Top 10 Rules by ROI:")
for i, r in enumerate(top_rules):
    print(f"Rule {i+1}: Threshold = {r['threshold']}, N_bets = {r['n_bets']}, Profit = {r['profit']:.2f}, ROI = {r['roi']:.4f}")

# Plot
labels = [f"Rule {i+1}\nN={r['n_bets']}" for i, r in enumerate(top_rules)]
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

# Select the best rule: the top one
selected_rule = top_rules[0]['threshold'] if top_rules else 5

# Simulate bets with this rule and list matching runners
matching_runners = []
cumulative_pl = []
total_pl = 0
for race in races:
    for runner in race['runners']:
        data = runner['racingStattoData']
        if all(data.get(f, 100) <= selected_rule for f in fields):
            pl = (runner['spPrice'] - 1) if runner['isWinner'] else -1
            total_pl += pl
            matching_runners.append({
                'race_time': race['raceData']['raceTime'],
                'racecourse': race['raceData']['racecourse'],
                'name': runner['name'],
                **{f: data.get(f, 'N/A') for f in fields},
                'spPrice': runner['spPrice'],
                'isWinner': runner['isWinner'],
                'pl': pl
            })
    cumulative_pl.append(total_pl)

# Print the list of matching runners in a table
print(f"\nMatching Runners for Top Rule: All fields <= {selected_rule}")
print(f"{'Race Time':<25} {'Racecourse':<15} {'Horse Name':<20} {'SP Price':<9} {'Winner':<7} {'P/L':<5}")
print("-" * 100)
for r in matching_runners:
    print(f"{r['race_time']:<25} {r['racecourse']:<15} {r['name']:<20} {r['spPrice']:<9.2f} {str(r['isWinner']):<7} {r['pl']:<5}")

print(f"\nTotal Bets: {len(matching_runners)}, Final P/L: {total_pl}")

# Plot P/L chart
plt.figure(figsize=(10,6))
plt.plot(range(1, len(cumulative_pl)+1), cumulative_pl, marker='o')
plt.xlabel('Race Number')
plt.ylabel('Cumulative P/L')
plt.title('Cumulative P/L for Selected Rule: rank≤3, timeRank≤3, fastestTimeRank≤4, averageRank≤3')
plt.grid(True)
plt.tight_layout()
plt.savefig('./src/RacingStatto/Data/best_combination_profit_chart.png')
plt.show()
