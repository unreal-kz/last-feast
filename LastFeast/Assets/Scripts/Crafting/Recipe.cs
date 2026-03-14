using System;
using System.Collections.Generic;
using UnityEngine;
using LastFeast.Core;

namespace LastFeast.Crafting
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "LastFeast/Recipe")]
    public class Recipe : ScriptableObject
    {
        [Header("Identity")]
        public string RecipeId;
        public string DisplayName;
        [TextArea] public string Description;

        [Header("Requirements")]
        public List<RecipeIngredient> Ingredients = new List<RecipeIngredient>();
        public float CraftingTime = 30f; // seconds

        [Header("Output")]
        public ResourceType OutputResource;
        public int OutputAmount = 1;

        [Header("Unlock")]
        public bool IsUnlockedByDefault;
        public int RequiredKnowledge; // knowledge resource needed to unlock
    }

    [Serializable]
    public class RecipeIngredient
    {
        public ResourceType Resource;
        public int Amount;
    }
}
