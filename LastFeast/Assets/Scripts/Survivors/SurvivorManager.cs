using System.Collections.Generic;
using UnityEngine;
using LastFeast.Buildings;
using LastFeast.Core;

namespace LastFeast.Survivors
{
    public class SurvivorManager : MonoBehaviour
    {
        [SerializeField] private int startingPopulation = 5;

        public int Population { get; private set; }
        public int MaxPopulation { get; private set; }

        private Dictionary<BuildingType, int> _assignments = new Dictionary<BuildingType, int>();

        private void Awake()
        {
            Population = startingPopulation;
            MaxPopulation = startingPopulation;
        }

        public void SetMaxPopulation(int max)
        {
            MaxPopulation = max;
        }

        public void AddSurvivors(int count)
        {
            int oldPop = Population;
            Population = Mathf.Min(Population + count, MaxPopulation);

            if (Population != oldPop)
                EventBus.RaisePopulationChanged(Population);
        }

        public void RemoveSurvivors(int count)
        {
            int oldPop = Population;
            Population = Mathf.Max(Population - count, 0);

            if (Population != oldPop)
                EventBus.RaisePopulationChanged(Population);
        }

        public int GetAssignedCount(BuildingType building)
        {
            return _assignments.TryGetValue(building, out int count) ? count : 0;
        }

        public int GetUnassignedCount()
        {
            int assigned = 0;
            foreach (var kvp in _assignments)
                assigned += kvp.Value;
            return Population - assigned;
        }

        public bool AssignSurvivor(BuildingType building)
        {
            if (GetUnassignedCount() <= 0) return false;

            if (!_assignments.ContainsKey(building))
                _assignments[building] = 0;

            _assignments[building]++;
            EventBus.RaiseSurvivorAssigned($"survivor_{Population}", building);
            return true;
        }

        public bool UnassignSurvivor(BuildingType building)
        {
            if (!_assignments.ContainsKey(building) || _assignments[building] <= 0)
                return false;

            _assignments[building]--;
            return true;
        }

        public void LoadFromSave(int population, Dictionary<BuildingType, int> assignments)
        {
            Population = population;
            _assignments = assignments ?? new Dictionary<BuildingType, int>();
        }
    }
}
