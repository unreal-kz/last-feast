using UnityEngine;
using LastFeast.Buildings;

namespace LastFeast.Heroes
{
    public enum HeroRole
    {
        Chef,
        Farmer,
        Engineer,
        Medic,
        Scavenger,
        Forager
    }

    [CreateAssetMenu(fileName = "NewHero", menuName = "LastFeast/Hero Data")]
    public class HeroData : ScriptableObject
    {
        [Header("Identity")]
        public string HeroId;
        public string DisplayName;
        [TextArea] public string Backstory;

        [Header("Role")]
        public HeroRole Role;

        [Header("Bonuses")]
        public BuildingType BestBuilding; // building where this hero excels
        public float ProductionBonus = 1.25f; // multiplier when assigned to BestBuilding
        public float GenericBonus = 1.1f; // multiplier for other buildings

        [Header("Discovery")]
        [TextArea] public string DiscoveryCondition; // human-readable unlock condition
        public int RequiredPopulation; // minimum population to discover this hero
    }
}
