
# Hello Betfair — Demonstrating a Simple Strategy in Multiple Languages

## Overview

This article presents the "Hello Betfair" strategy implemented in three different programming languages (F#, C#, and Visual Basic) using the bfexplorer app platform. The intention is to demonstrate how the same trading logic can be expressed across languages, helping learners compare structure and syntax while focusing on the core idea.

## Strategy Logic

The strategy is simple:

- Place a back bet on a market selection if the selection price is in the range from 2.5 to 3.0 (inclusive).

Each implementation follows these steps:

1. Access market and selection price updates via the bfexplorer app platform.
2. For each selection, check if the price is between 2.5 and 3.0.
3. If so, place a back bet on that selection.
4. Log or print the result for review.

## How to Use This Folder

- This folder contains educational examples of the same strategy in F#, C#, and Visual Basic.
- All code is designed to run on the bfexplorer app platform, not directly with the Betfair API.
- Compare the files to see how each language expresses the same requirements.

## Notes and Safety

- Trading real money has risk. Test thoroughly in a simulation environment.
- This repository provides educational skeletons — not financial advice.

## License

Use freely for learning and experimentation.
