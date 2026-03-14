# Last Feast

A premium single-player iOS strategy game about rebuilding civilization after a global food collapse.

## About

Last Feast is a city-builder / survival strategy game where you manage a survivor settlement — constructing buildings, gathering resources, crafting recipes, and keeping your population fed. No ads, no in-app purchases, no energy timers. One-time purchase.

**Platform**: iOS 15+ (iPhone primary, iPad supported)
**Engine**: Godot 4.x (GDScript)
**Monetization**: Premium (~$6.99)

## Gameplay

### Core Cycle
```
GATHER Resources → CRAFT Ingredients → COOK Meals → FEED Survivors → BUILD Upgrades
```

### Gameplay Loops
| Loop | Duration | Activities |
|------|----------|-----------|
| Micro | ~30 seconds | Tap to gather, assign survivors, start cooking |
| Mid | ~5 minutes | Complete builds, harvest crops, craft batches |
| Session | 20–40 minutes | Unlock new building tiers or regions |

### Win Condition
Build **The Grand Table** — requires maxed building tiers, 200+ survivors, and crafting the legendary "Last Feast" recipe. Completing the game unlocks New Game+ mode.

## Key Systems

**Buildings**: Cookhouse, Farm Plots, Shelter, Well/Water Purifier, Salvage Yard, Workshop, Storehouse, Infirmary, Greenhouse, The Grand Table

**Resources**: Food/Meals, Water, Vegetables, Grain, Wood, Scrap Metal, Metal Parts, Herbs, Rare Ingredients, Knowledge

**Heroes**: 12 unique heroes (Chefs, Farmers, Engineers, Medics, Scavengers, Foragers) — all found through gameplay, no gacha

## Technical Details

- Offline-only, no server required
- Save data stored as local JSON files
- Offline progress calculated on app open
- GameAnalytics integration
- Apple StoreKit 2 for purchase

## Art Direction

Low-poly isometric 3D with a fixed camera. The world starts desaturated (ash/grey) and gains warm amber color as the settlement grows. UI designed for phone screens.

## Development

Solo developer project. See `LastFeast_GDD.docx` for full design specifications including building details, resource tables, hero abilities, and the development roadmap.
