using System;
using System.Collections.Generic;
using UnityEngine;

namespace LastFeast.Core
{
    public enum ResourceType
    {
        Food,
        Water,
        Vegetables,
        Grain,
        Wood,
        ScrapMetal,
        MetalParts,
        Herbs,
        RareIngredients,
        Knowledge
    }

    [Serializable]
    public class ResourceData
    {
        public ResourceType Type;
        public int Amount;
        public int MaxCapacity;
    }

    public class ResourceManager : MonoBehaviour
    {
        private Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
        private Dictionary<ResourceType, int> _maxCapacity = new Dictionary<ResourceType, int>();

        [SerializeField] private int defaultMaxCapacity = 500;

        private void Awake()
        {
            foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
            {
                _resources[type] = 0;
                _maxCapacity[type] = defaultMaxCapacity;
            }
        }

        public int GetAmount(ResourceType type) => _resources[type];

        public int GetMaxCapacity(ResourceType type) => _maxCapacity[type];

        public void SetMaxCapacity(ResourceType type, int capacity)
        {
            _maxCapacity[type] = capacity;
        }

        public bool CanAfford(ResourceType type, int amount) => _resources[type] >= amount;

        public bool CanAfford(Dictionary<ResourceType, int> costs)
        {
            foreach (var cost in costs)
            {
                if (!CanAfford(cost.Key, cost.Value))
                    return false;
            }
            return true;
        }

        public void Add(ResourceType type, int amount)
        {
            if (amount <= 0) return;

            int oldAmount = _resources[type];
            _resources[type] = Mathf.Min(_resources[type] + amount, _maxCapacity[type]);
            EventBus.RaiseResourceChanged(type, oldAmount, _resources[type]);
        }

        public bool Spend(ResourceType type, int amount)
        {
            if (amount <= 0 || !CanAfford(type, amount)) return false;

            int oldAmount = _resources[type];
            _resources[type] -= amount;
            EventBus.RaiseResourceChanged(type, oldAmount, _resources[type]);
            return true;
        }

        public bool SpendMultiple(Dictionary<ResourceType, int> costs)
        {
            if (!CanAfford(costs)) return false;

            foreach (var cost in costs)
                Spend(cost.Key, cost.Value);

            return true;
        }

        public List<ResourceData> GetAllResources()
        {
            var list = new List<ResourceData>();
            foreach (var kvp in _resources)
            {
                list.Add(new ResourceData
                {
                    Type = kvp.Key,
                    Amount = kvp.Value,
                    MaxCapacity = _maxCapacity[kvp.Key]
                });
            }
            return list;
        }

        public void LoadFromSave(List<ResourceData> savedResources)
        {
            foreach (var data in savedResources)
            {
                _resources[data.Type] = data.Amount;
                _maxCapacity[data.Type] = data.MaxCapacity;
            }
        }
    }
}
