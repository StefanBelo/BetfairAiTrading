# Скачки: фаворит (BSP) — тихий режим

Цель: Запустить стратегию только на фаворита по BSP; выводить лишь результат выполнения.

Логика:
- Найти фаворита (самый низкий `price`).
- Метрики:
	- `bspEdge = (1/betfairSP) - (1/price)`
	- `bspEvNet = price/betfairSP - 1`
	- `priceRatio = price/betfairSP`
- Решение:
	- BACK, если `eVforPriceOrBetfairSP > 0` и `bspEvNet > -0.02`
	- Иначе LAY (или пропустить при отсутствии данных)

Инструменты: `get_active_market` → `get_all_data_context_for_market("BetfairSpData")` → `execute_bfexplorer_strategy_settings`

## Шаги выполнения

1. Получить активный рынок (marketId).
2. Загрузить контекст «BetfairSpData» для этого рынка.
3. Определить фаворита и посчитать метрики.
4. Выполнить действие [Back 10 EUR/Lay 10 EUR].
5. Вывести только результат выполнения.

## Формат вывода

- Рынок: [название/время]
- Фаворит: [лошадь] (Цена: [коэфф.], BSP: [bsp])
- Действие: [Back 10 EUR/Lay 10 EUR]
- selectionId: [id]
- Причина: [критерии BACK/критерии LAY/нет данных]
- Метрики BSP: `priceRatio`=[x.xx], `bspEdge`=[x.xxxx], `bspEvNet`=[x.xxxx]
- Статус: [УСПЕХ/ОШИБКА/ПРОПУЩЕНО]