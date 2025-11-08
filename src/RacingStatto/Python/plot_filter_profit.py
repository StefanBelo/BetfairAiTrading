import pandas as pd
import matplotlib.pyplot as plt
from itertools import product

# Load the CSV data
csv_path = r'e:\Projects\BetfairAiTrading\src\RacingStatto\Data\MlData.csv'
df = pd.read_csv(csv_path)

# Filter for winners to derive ranges
winners_df = df

# Define possible ranges for each variable based on winners' data
def generate_ranges(series, num_ranges=3):
    min_val = series.min()
    max_val = series.max()
    step = (max_val - min_val) / num_ranges
    ranges_list = []
    for i in range(num_ranges):
        start = min_val + i * step
        end = min_val + (i + 1) * step
        ranges_list.append((round(start, 1), round(end, 1)))
    return ranges_list

ranges = {}
for col in ['TimeRank', 'FastestTimeRank', 'Rank', 'AverageRank', 'Odds']:
    ranges[col] = generate_ranges(winners_df[col])

# Find the best combination
best_profit = float('-inf')
best_filters = None
best_filtered_df = None

for tr_min, tr_max in ranges['TimeRank']:
    for ftr_min, ftr_max in ranges['FastestTimeRank']:
        for r_min, r_max in ranges['Rank']:
            for ar_min, ar_max in ranges['AverageRank']:
                for o_min, o_max in ranges['Odds']:
                    filtered_df = df[
                        (df['TimeRank'] >= tr_min) & (df['TimeRank'] <= tr_max) &
                        (df['FastestTimeRank'] >= ftr_min) & (df['FastestTimeRank'] <= ftr_max) &
                        (df['Rank'] >= r_min) & (df['Rank'] <= r_max) &
                        (df['AverageRank'] >= ar_min) & (df['AverageRank'] <= ar_max) &
                        (df['Odds'] >= o_min) & (df['Odds'] <= o_max)
                    ]
                    total_profit = filtered_df['Profit'].sum()
                    if total_profit > best_profit:
                        best_profit = total_profit
                        best_filters = (tr_min, tr_max, ftr_min, ftr_max, r_min, r_max, ar_min, ar_max, o_min, o_max)
                        best_filtered_df = filtered_df

# Print best combination
tr_min, tr_max, ftr_min, ftr_max, r_min, r_max, ar_min, ar_max, o_min, o_max = best_filters
print(f"Best Combination:")
print(f"TimeRank: {tr_min}-{tr_max}")
print(f"FastestTimeRank: {ftr_min}-{ftr_max}")
print(f"Rank: {r_min}-{r_max}")
print(f"AverageRank: {ar_min}-{ar_max}")
print(f"Odds: {o_min}-{o_max}")
print(f"Total Bets: {len(best_filtered_df)}, Total Profit: {best_profit}")

# Calculate cumulative profit for best
best_filtered_df = best_filtered_df.sort_index()
cumulative_profit = best_filtered_df['Profit'].cumsum()

# Plot cumulative profit chart
plt.figure(figsize=(10, 6))
plt.plot(range(1, len(cumulative_profit) + 1), cumulative_profit, marker='o')
plt.xlabel('Bet Number')
plt.ylabel('Cumulative Profit')
plt.title(f'Best Combination Profit: {best_profit:.2f}')
plt.grid(True)
plt.tight_layout()
plt.savefig('best_combination_profit_chart.png')
plt.show()