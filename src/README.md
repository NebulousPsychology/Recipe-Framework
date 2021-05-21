# Src Contents

## Assets

A collection of jsonc-encoded recipe files for use in testing the project, and as samples for authoring new recipe definitions.

A [schema validator](https://extendsclass.com/json-schema-validator.html) is a good way to build a consistent json serialization

the most interesting part of the schema so far, Qty is either a number or an array of two numbers:
```json
"Qty": {
    "$id": "#root/Steps/items/Ingredients/items/Qty",
    "title": "Qty",
    "type": ["number","array"],
    "examples": [ 6, [1,2] ],
    "items":{ "type":"number"},
    "default": 0.0,
    "maxItems": 2
},
```

## RecipeToMarkdown

A tool which converts a jsonc-encoded recipe into a markdown

Markdown generation should strive for:

- good _mis-en-place_ instructions
- units at point-of-use
- 'reserve `qty` for later' is a bad instruction. when an ingredient is used in multiple places, don't group up in the ingredients list

## TODO

- Support step-groupings, especially which can be done without time pressure. E.g. A collection of steps for Dough + A collection for the sauce + a collection for turning both into pizza
- Support multitasking/concurrent activity and other time management (UntilCondition/OnceCondition)
