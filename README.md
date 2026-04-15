# 2dAngel Shared

Shared editor UI and icon layer used across 2dAngel tools.

## Scope

This repository is for reusable editor infrastructure only:
- shared editor UI helpers
- shared icon accessors
- shared layout / visual tokens
- shared editor-only assembly definitions

This repository should **not** contain product-specific workflows from AP Core, AngelProbeTools, or AngelLightTools.

## Current naming

The canonical class names in this repo are:
- `TwoDA_UI`
- `TwoDA_Icons`

A transitional compatibility layer is also included:
- `TAE_UI`
- `TAE_Icons`
- `TAE_NavVisual`

Keep the compatibility files until AP / APT / ALT finish migrating off the old `TAE_*` references.

## Path policy

Internal path policy stays fixed at:

`Assets/2dAngel/Shared`

## Suggested git workflow

Use this repository as the source of truth for Shared, then pull it into consumer repos with `git subtree`.
