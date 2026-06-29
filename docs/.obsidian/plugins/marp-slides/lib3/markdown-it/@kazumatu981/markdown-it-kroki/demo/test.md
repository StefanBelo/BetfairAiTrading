---
marp: true
---

# @kazumatu981/markdown-it-kroki

## Marp Sample

---

## plantuml

```plantuml[platuml image]
@startuml
left to right direction
actor Guest as g
package Professional {
  actor Chef as c
  actor "Food Critic" as fc
}
package Restaurant {
  usecase "Eat Food" as UC1
  usecase "Pay for Food" as UC2
  usecase "Drink" as UC3
  usecase "Review" as UC4
}
fc --> UC4
g --> UC1
g --> UC2
g --> UC3
@enduml
```

---

## plantuml with hyperlink

```plantuml
@startuml
left to right direction

object  "[[https://github.com/markdown-it/markdown-it{markdown-it} markdown-it]]" as MarkdownIt

object  "[[https://github.com/kazumatu981/markdown-it-kroki{markdown-it-krokii} markdown-it-krokii]]" as MarkdownItKrokii

MarkdownIt --> MarkdownItKrokii
@enduml
```

---

## dbml

```dbml
Table users {
    id integer
    username varchar
    role varchar
    created_at timestamp
}

Table posts {
    id integer [primary key]
    title varchar
    body text [note: 'Content of the post']
    user_id integer
    created_at timestamp
}

Ref: posts.user_id > users.id
```


---

## mermaid

```mermaid[mermaid image]
graph TD
  A[ Anyone ] -->|Can help | B( Go to github.com/yuzutech/kroki )
  B --> C{ How to contribute? }
  C --> D[ Reporting bugs ]
  C --> E[ Sharing ideas ]
  C --> F[ Advocating ]
```

---

## normal code

```JavaScript
function testFunc(test) {
  let sum = 0;
  for(let x = 1; x<=test; x++) {
    sum += x;
  }
  return sum;
}
```