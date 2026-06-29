# CHANGELOG

## v1.3.2

* refactor url parser.
  * **remark**: throw error on calling `use()` with invalid option (invalid `entrypoint` parameter).

## v1.3.1

* update development packages
  * `@marp-team/marp-cli`, `jsdom`, `markdown-it`, `mocha`

## v1.3.0

* support `render` option see [this issue](https://github.com/kazumatu981/markdown-it-kroki/issues/4) and [this memo](memo/howToWriteRender.md)
* *for developers* support coverage test script `test:coverage`.

## v1.2.3

* support `d2`, see [this issue](https://github.com/kazumatu981/markdown-it-kroki/issues/8)

## v1.2.2

* fix readme, see [this pull-request](https://github.com/kazumatu981/markdown-it-kroki/pull/6)
* support `dbml`, see [this issue](https://github.com/kazumatu981/markdown-it-kroki/issues/5)

## v1.2.1

* Embed `<embed>` tag instead of `<img>` tag, see [this issue](https://github.com/kazumatu981/markdown-it-kroki/issues/2).
* `<img>` will be used for **compatibility**, so set option `useImg` to `true` (default `false`).

## v1.1.1

* fix readme, see [this issue](https://github.com/kazumatu981/markdown-it-kroki/issues/1)

## v1.1.0

* Obsolated Option `marpAutoScaling` and detect automatically wether it is nessesury or not.

## v1.0.1

release on npm

## v1.0.0

create new
