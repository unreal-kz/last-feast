# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Last Feast** is a premium single-player iOS strategy/city-builder game set in a post-apocalyptic world. Players rebuild civilization after a global food collapse by constructing buildings, gathering resources, crafting recipes, and keeping their population fed. One-time purchase (~$6.99), no ads, no IAP, offline-only.

Full design specifications are in `LastFeast_GDD.docx`.

## Tech Stack

- **Engine**: Unity 6 LTS (6000.x) with Universal Render Pipeline (URP)
- **Language**: C#
- **Platform**: iOS 15+ (iPhone primary, iPad supported)
- **Save System**: Local JSON files via `JsonUtility` (no server/cloud required)
- **Analytics**: GameAnalytics Unity SDK
- **Purchases**: Unity IAP with Apple StoreKit 2
- **Art Style**: Low-poly isometric 3D, fixed camera

## Project Structure

Unity project lives in `LastFeast/`. Key script directories:

```
LastFeast/Assets/Scripts/
├── Core/           # GameManager, ResourceManager, SaveManager, TimeManager, EventBus
├── Buildings/      # Building + BuildingData (ScriptableObject)
├── Heroes/         # Hero + HeroData (ScriptableObject)
├── Crafting/       # CraftingManager + Recipe (ScriptableObject)
├── Survivors/      # SurvivorManager
└── UI/             # UIManager
```

## Architecture

### Namespaces
- `LastFeast.Core` — singletons, resource management, save/load, events
- `LastFeast.Buildings` — building placement, construction, production, upgrades
- `LastFeast.Heroes` — hero discovery, assignment, bonuses
- `LastFeast.Crafting` — recipe definitions, crafting queue
- `LastFeast.Survivors` — population tracking, assignment
- `LastFeast.UI` — panel management

### Key Patterns
- **GameManager** is a singleton (`DontDestroyOnLoad`) that orchestrates all other managers
- **EventBus** is a static class with C# events — use it to decouple systems (e.g., `EventBus.OnResourceChanged`)
- **ScriptableObjects** define data (BuildingData, HeroData, Recipe) — create instances in `Assets/Data/ScriptableObjects/`
- **Save system** auto-saves on `OnApplicationPause` and `OnApplicationQuit`; JSON stored in `Application.persistentDataPath`

### Game Cycle
`GATHER Resources → CRAFT Ingredients → COOK Meals → FEED Survivors → BUILD Upgrades`

### Enums (defined in code)
- `ResourceType`: Food, Water, Vegetables, Grain, Wood, ScrapMetal, MetalParts, Herbs, RareIngredients, Knowledge
- `BuildingType`: Cookhouse, FarmPlot, Shelter, WellWaterPurifier, SalvageYard, Workshop, Storehouse, Infirmary, Greenhouse, GrandTable
- `HeroRole`: Chef, Farmer, Engineer, Medic, Scavenger, Forager
- `GameState`: MainMenu, Playing, Paused

## Setup

1. Open Unity Hub → Add project from `LastFeast/` directory
2. Ensure Unity 6 LTS is installed with iOS Build Support
3. URP package should be added via Package Manager
4. Create a new scene and attach GameManager, ResourceManager, SaveManager, TimeManager as components on a persistent GameObject

## Build Commands

```bash
# Unity CLI build (requires Unity installed)
/Applications/Unity/Hub/Editor/6000.*/Unity.app/Contents/MacOS/Unity \
  -batchmode -nographics -projectPath ./LastFeast \
  -buildTarget iOS -quit

# Run tests
/Applications/Unity/Hub/Editor/6000.*/Unity.app/Contents/MacOS/Unity \
  -batchmode -nographics -projectPath ./LastFeast \
  -runTests -testPlatform EditMode -quit
```
