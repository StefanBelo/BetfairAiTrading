import sys
import subprocess
import importlib.util

import time
start = time.perf_counter()

required_packages = ["seaborn", "pandas", "matplotlib"]

for package in required_packages:
    if importlib.util.find_spec(package) is None:
        # Use Windows cmd to run pip install
        subprocess.check_call(["cmd", "/c", f"pip install {package}"])

import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
from datetime import datetime

data = [
    {"Time": datetime(2026, 4, 16, 8, 0, 0), "Price": 2.0, "Volume": 100.0},
    {"Time": datetime(2026, 4, 16, 8, 10, 30), "Price": 3.0, "Volume": 10.0},
    {"Time": datetime(2026, 4, 16, 8, 20, 0), "Price": 2.5, "Volume": 5.0},
]

df = pd.DataFrame(data)

sns.set_theme(style="whitegrid")
fig, (ax1, ax2) = plt.subplots(2, 1, sharex=True, figsize=(10, 6), gridspec_kw={"height_ratios": [3, 1]})

sns.lineplot(data=df, x="Time", y="Price", marker="o", ax=ax1, label="Price")
ax2.bar(df["Time"], df["Volume"], width=0.003, color="skyblue", align="center")

ax1.set_title("Time vs Price")
ax1.set_ylabel("Price")
ax2.set_title("Volume")
ax2.set_ylabel("Volume")
ax2.set_xlabel("Time")


fig.autofmt_xdate()
plt.tight_layout()
plt.savefig('E:\Temp\output.png')

#plt.show()

end = time.perf_counter()
print(f"Execution time: {(end - start):.2f} s")
