using System;
using System.Collections.Generic;
using UnityEngine;
using LastFeast.Core;

namespace LastFeast.Buildings
{
    public enum BuildingType
    {
        Cookhouse,
        FarmPlot,
        Shelter,
        WellWaterPurifier,
        SalvageYard,
        Workshop,
        Storehouse,
        Infirmary,
        Greenhouse,
        GrandTable
    }

    [CreateAssetMenu(fileName = "NewBuilding", menuName = "LastFeast/Building Data")]
    public class BuildingData : ScriptableObject
    {
        [Header("Identity")]
        public BuildingType Type;
        public string DisplayName;
        [TextArea] public string Description;

        [Header("Construction")]
        public int MaxTier = 3;
        public float BaseConstructionTime = 60f; // seconds

        [Header("Cost Per Tier")]
        public List<TierCost> TierCosts = new List<TierCost>();

        [Header("Production")]
        public ResourceType ProducedResource;
        public float BaseProductionRate = 1f; // per second
        public float ProductionMultiplierPerTier = 1.5f;

        [Header("Requirements")]
        public int RequiredPopulation;
        public List<BuildingType> RequiredBuildings = new List<BuildingType>();
    }

    [Serializable]
    public class TierCost
    {
        public int Tier;
        public List<ResourceCost> Costs = new List<ResourceCost>();
    }

    [Serializable]
    public class ResourceCost
    {
        public ResourceType Type;
        public int Amount;
    }
}
