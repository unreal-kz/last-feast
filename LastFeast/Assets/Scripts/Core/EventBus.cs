using System;

namespace LastFeast.Core
{
    public static class EventBus
    {
        // Resource events
        public static event Action<ResourceType, int, int> OnResourceChanged; // type, oldAmount, newAmount

        // Building events
        public static event Action<BuildingType, int> OnBuildingPlaced; // type, tier
        public static event Action<BuildingType, int> OnBuildingUpgraded; // type, newTier
        public static event Action<BuildingType> OnBuildingConstructionComplete;

        // Survivor events
        public static event Action<int> OnPopulationChanged; // newPopulation
        public static event Action<string, BuildingType> OnSurvivorAssigned; // survivorId, building

        // Hero events
        public static event Action<string> OnHeroDiscovered; // heroId
        public static event Action<string, BuildingType> OnHeroAssigned; // heroId, building

        // Crafting events
        public static event Action<string> OnRecipeCrafted; // recipeId
        public static event Action<string, float> OnCraftingProgress; // recipeId, progress 0-1

        // Game state events
        public static event Action OnGameSaved;
        public static event Action<double> OnOfflineProgressCalculated; // elapsedSeconds

        // Invocation methods
        public static void RaiseResourceChanged(ResourceType type, int oldAmount, int newAmount)
            => OnResourceChanged?.Invoke(type, oldAmount, newAmount);

        public static void RaiseBuildingPlaced(BuildingType type, int tier)
            => OnBuildingPlaced?.Invoke(type, tier);

        public static void RaiseBuildingUpgraded(BuildingType type, int newTier)
            => OnBuildingUpgraded?.Invoke(type, newTier);

        public static void RaiseBuildingConstructionComplete(BuildingType type)
            => OnBuildingConstructionComplete?.Invoke(type);

        public static void RaisePopulationChanged(int newPopulation)
            => OnPopulationChanged?.Invoke(newPopulation);

        public static void RaiseSurvivorAssigned(string survivorId, BuildingType building)
            => OnSurvivorAssigned?.Invoke(survivorId, building);

        public static void RaiseHeroDiscovered(string heroId)
            => OnHeroDiscovered?.Invoke(heroId);

        public static void RaiseHeroAssigned(string heroId, BuildingType building)
            => OnHeroAssigned?.Invoke(heroId, building);

        public static void RaiseRecipeCrafted(string recipeId)
            => OnRecipeCrafted?.Invoke(recipeId);

        public static void RaiseCraftingProgress(string recipeId, float progress)
            => OnCraftingProgress?.Invoke(recipeId, progress);

        public static void RaiseGameSaved()
            => OnGameSaved?.Invoke();

        public static void RaiseOfflineProgressCalculated(double elapsedSeconds)
            => OnOfflineProgressCalculated?.Invoke(elapsedSeconds);
    }
}
