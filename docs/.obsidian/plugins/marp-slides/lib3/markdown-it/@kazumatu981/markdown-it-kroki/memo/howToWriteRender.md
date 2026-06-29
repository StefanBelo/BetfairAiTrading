# How To Write `render()` call back

If you need to write code to embed into your own `HTML`,
We provide this option.

When you write call back function as `markdown-it`'s option argument object's `render` property,
this plugin call the call-back funtion with argument (encoded url for kroki and alt-text).

## example

For example, if you write call-back and `markdown-it`'s `use()` method, like bellow.

```javascript
const md = require('markdown-it');
const markdownItKroki = require('kazumatu981/markdown-it-kroki');
const urlMap = [];

function myRender(encodedUrl, altText) {
    console.log(`URL: ${encodedUrl}`);
    console.log(`param: ${altText}`);
    urlMap.push({id: altText, url:encodedUrl});
    return `<div id=${altText}></div>`
}

md.use(markdownItKroki, {
    render: myRender
});
```

and, write markdown like this.

    ## This is test figure

    ```plantuml [figure-001]
    skinparam ranksep 20
    skinparam dpi 125
    skinparam packageTitleAlignment left

    rectangle "Main" {
    (main.view)
    (singleton)
    }
    rectangle "Base" {
    (base.component)
    (component)
    (model)
    }
    rectangle "<b>main.ts</b>" as main_ts

    (component) ..> (base.component)
    main_ts ==> (main.view)
    (main.view) --> (component)
    (main.view) ...> (singleton)
    (singleton) ---> (model)
    ```

then console results are like this.

```text
URL: https://kroki.io/plantuml/svg/eNpljzEPgjAQhff-iguTDFQlcYMmuru5mwNO0tCWhjY6GP-7LRJTdHvv7r67d26QxuKEGiY0gyML5Y65b7GzEvblIalYbAfs6SK9oqOSvdFkPCi6ecYmaj2aXhFkZ5QmgycD2Ogg-V3SI4_OyTjgR5OzVwqc0NECNEHydtR2NGH3TK2dHjtSP3zViPmQd9W2ERmgg-iv3jGW4MC5-L-wTEJdi1XeRENRiFWOtMfnrclriQ5gJD-Z3x9beAM=
param: figure-001
```

## api

```TypeScript
function render(encodedUrl: string, altText: string): string;
```

### arguments

| name | type| value |
| ------------ | -------- | ----- |
| `encodedUrl` | `string` | encoded url (include encoded and compressed textual diagram) for Kroki web service. |
| `altText`    | `string` | The text you have written in markdown as alt-text |

### return

The fragment of `html` you want to embed into render result as `string`.
