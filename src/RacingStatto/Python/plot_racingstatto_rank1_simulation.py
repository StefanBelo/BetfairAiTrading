import matplotlib.pyplot as plt
import requests
import json

# Try to load races from the API endpoint
api_url = "http://localhost:10043/api/getAIAgentDataContextFeedback?dataContextName=RacesResultsForRacingStattoData&forLastResults=1000"
races = None
try:
    response = requests.get(api_url, timeout=10)
    if response.status_code == 200:
        races = response.json()
        # If the API returns a dict with a 'result' key, use that
        if isinstance(races, dict) and 'result' in races:
            races = races['result']
        print(f"Loaded {len(races)} races from API.")
    else:
        print(f"API request failed with status code {response.status_code}, using fallback data.")
except Exception as e:
    print(f"Error loading races from API: {e}\nUsing fallback data.")

# Simulate bets where rank == 1
matching_runners = []
cumulative_pl = []
total_pl = 0
for race in races:
    for runner in race['runners']:
        data = runner['racingStattoData']
        if data['rank'] == 1:
            pl = (runner['spPrice'] - 1) if runner['isWinner'] else -1
            total_pl += pl
            matching_runners.append({
                'race_time': race['raceData']['raceTime'],
                'racecourse': race['raceData']['racecourse'],
                'name': runner['name'],
                'rank': data['rank'],
                'spPrice': runner['spPrice'],
                'isWinner': runner['isWinner'],
                'pl': pl
            })
    cumulative_pl.append(total_pl)

# Print summary
print(f"Rule: rank == 1")
print(f"Total Bets: {len(matching_runners)}, Final P/L: {total_pl}")

# Plot P/L chart
plt.figure(figsize=(10,6))
plt.plot(range(1, len(cumulative_pl)+1), cumulative_pl, marker='o')
plt.xlabel('Race Number')
plt.ylabel('Cumulative P/L')
plt.title('Cumulative P/L for Rule: rank == 1')
plt.grid(True)
plt.tight_layout()
plt.show()