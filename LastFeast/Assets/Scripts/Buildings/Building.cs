using UnityEngine;
using LastFeast.Core;

namespace LastFeast.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private BuildingData data;

        public BuildingData Data => data;
        public BuildingType Type => data.Type;
        public int CurrentTier { get; private set; } = 1;
        public bool IsConstructed { get; private set; }
        public float ConstructionProgress { get; private set; }

        private string _assignedHeroId;
        private float _heroProductionBonus = 1f;

        public void Initialize(BuildingData buildingData, int tier = 1)
        {
            data = buildingData;
            CurrentTier = tier;
            ConstructionProgress = 0f;
            IsConstructed = false;
        }

        private void Update()
        {
            if (!IsConstructed)
            {
                UpdateConstruction();
                return;
            }

            Produce();
        }

        private void UpdateConstruction()
        {
            ConstructionProgress += UnityEngine.Time.deltaTime / data.BaseConstructionTime;

            if (ConstructionProgress >= 1f)
            {
                ConstructionProgress = 1f;
                IsConstructed = true;
                EventBus.RaiseBuildingConstructionComplete(Type);
            }
        }

        private void Produce()
        {
            float rate = data.BaseProductionRate
                * Mathf.Pow(data.ProductionMultiplierPerTier, CurrentTier - 1)
                * _heroProductionBonus;

            float produced = rate * UnityEngine.Time.deltaTime;

            if (produced >= 1f)
            {
                GameManager.Instance.Resources.Add(data.ProducedResource, Mathf.FloorToInt(produced));
            }
        }

        public bool TryUpgrade()
        {
            if (CurrentTier >= data.MaxTier) return false;

            var tierCost = data.TierCosts.Find(tc => tc.Tier == CurrentTier + 1);
            if (tierCost == null) return false;

            var costs = new System.Collections.Generic.Dictionary<ResourceType, int>();
            foreach (var cost in tierCost.Costs)
                costs[cost.Type] = cost.Amount;

            if (!GameManager.Instance.Resources.SpendMultiple(costs))
                return false;

            CurrentTier++;
            EventBus.RaiseBuildingUpgraded(Type, CurrentTier);
            return true;
        }

        public void AssignHero(string heroId, float productionBonus)
        {
            _assignedHeroId = heroId;
            _heroProductionBonus = productionBonus;
            EventBus.RaiseHeroAssigned(heroId, Type);
        }

        public void RemoveHero()
        {
            _assignedHeroId = null;
            _heroProductionBonus = 1f;
        }

        public BuildingSaveData ToSaveData()
        {
            return new BuildingSaveData
            {
                Type = Type,
                Tier = CurrentTier,
                IsConstructed = IsConstructed,
                ConstructionProgress = ConstructionProgress,
                AssignedHeroId = _assignedHeroId
            };
        }

        public void LoadFromSave(BuildingSaveData saveData)
        {
            CurrentTier = saveData.Tier;
            IsConstructed = saveData.IsConstructed;
            ConstructionProgress = saveData.ConstructionProgress;
            _assignedHeroId = saveData.AssignedHeroId;
        }
    }
}
