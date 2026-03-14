# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Last Feast** is a premium single-player iOS strategy/city-builder game set in a post-apocalyptic world. Players rebuild civilization after a global food collapse by constructing buildings, gathering resources, crafting recipes, and keeping their population fed. One-time purchase (~$6.99), no ads, no IAP, offline-only.

Full design specifications are in `LastFeast_GDD.docx`.

## Tech Stack

- **Engine**: Godot 4.x with GDScript
- **Platform**: iOS 15+ (iPhone primary, iPad supported)
- **Save System**: Local JSON files (no server/cloud required)
- **Analytics**: GameAnalytics integration
- **Purchases**: Apple StoreKit 2
- **Art Style**: Low-poly isometric 3D, fixed camera

## Core Architecture

### Gameplay Loops (3 nested)
1. **30-second loop**: Tap to gather, assign survivors, start cooking
2. **5-minute loop**: Complete builds, harvest crops, craft batches
3. **Session loop (20–40 min)**: Unlock new building tiers or regions

### Game Cycle
`GATHER Resources → CRAFT Ingredients → COOK Meals → FEED Survivors → BUILD Upgrades`

### Key Systems

**Buildings** (10 types): Cookhouse, Farm Plots, Shelter, Well/Water Purifier, Salvage Yard, Workshop, Storehouse, Infirmary, Greenhouse, The Grand Table (win condition)

**Resources** (10 types): Food/Meals, Water, Vegetables, Grain, Wood, Scrap Metal, Metal Parts, Herbs, Rare Ingredients, Knowledge

**Heroes** (12 total, found through gameplay — not gacha): Chefs, Farmers, Engineers, Medics, Scavengers, Foragers — each with unique skills affecting their assigned building

**Save/Offline**: Progress is calculated on app open based on elapsed time since last session (offline progression)

### Win Condition
Build The Grand Table (requires max building tiers + 200+ survivors + craft the "Last Feast" recipe) → unlocks New Game+
