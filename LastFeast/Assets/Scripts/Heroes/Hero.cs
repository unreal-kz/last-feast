using UnityEngine;
using LastFeast.Buildings;
using LastFeast.Core;

namespace LastFeast.Heroes
{
    public class Hero
    {
        public HeroData Data { get; private set; }
        public string Id => Data.HeroId;
        public HeroRole Role => Data.Role;
        public bool IsAssigned => AssignedBuilding != null;
        public Building AssignedBuilding { get; private set; }

        public Hero(HeroData data)
        {
            Data = data;
        }

        public float GetBonusForBuilding(BuildingType buildingType)
        {
            return buildingType == Data.BestBuilding
                ? Data.ProductionBonus
                : Data.GenericBonus;
        }

        public void AssignToBuilding(Building building)
        {
            if (IsAssigned)
                Unassign();

            AssignedBuilding = building;
            float bonus = GetBonusForBuilding(building.Type);
            building.AssignHero(Id, bonus);
        }

        public void Unassign()
        {
            if (AssignedBuilding != null)
            {
                AssignedBuilding.RemoveHero();
                AssignedBuilding = null;
            }
        }
    }
}
