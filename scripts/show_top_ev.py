import pandas as pd

path = r"e:\Projects\BetfairAiTrading\outputs\features\predictions_1.255347397.csv"
df = pd.read_csv(path)
best = df.sort_values('EV_per_1', ascending=False).head(5)
print(best[['runnerName','odds','p_model_calibrated','EV_per_1','confidence_label']].to_string(index=False))
