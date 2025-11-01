# 赛马：热门（BSP）— 静默模式

目标：仅按 BSP 对热门马执行策略；仅输出执行结果。

逻辑：
- 找到热门（最低 `price`）。
- 指标：
	- `bspEdge = (1/betfairSP) - (1/price)`
	- `bspEvNet = price/betfairSP - 1`
	- `priceRatio = price/betfairSP`
- 决策：
	- 若 `eVforPriceOrBetfairSP > 0` 且 `bspEvNet > -0.02`，则 BACK
	- 否则 LAY（无数据则跳过）

工具：`get_active_market` → `get_all_data_context_for_market("BetfairSpData")` → `execute_bfexplorer_strategy_settings`

## 执行步骤

1. 获取活跃市场（marketId）。
2. 加载该市场的“BetfairSpData”。
3. 确定热门并计算指标。
4. 执行操作 [Back 10 EUR/Lay 10 EUR]。
5. 仅输出执行结果。

## 输出格式

- 市场：[名称/时间]
- 热门：[马名]（价格：[odds]，BSP：[bsp]）
- 操作：[Back 10 EUR/Lay 10 EUR]
- selectionId：[id]
- 原因：[BACK 条件/ LAY 条件/ 无数据]
- BSP 指标：`priceRatio`=[x.xx]，`bspEdge`=[x.xxxx]，`bspEvNet`=[x.xxxx]
- 状态：[成功/失败/跳过]