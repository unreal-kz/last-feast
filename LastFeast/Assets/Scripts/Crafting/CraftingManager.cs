using System.Collections.Generic;
using UnityEngine;
using LastFeast.Core;

namespace LastFeast.Crafting
{
    public class CraftingQueue
    {
        public Recipe Recipe;
        public float Progress; // 0-1
        public float ElapsedTime;
    }

    public class CraftingManager : MonoBehaviour
    {
        [SerializeField] private int maxQueueSize = 3;

        private List<CraftingQueue> _queue = new List<CraftingQueue>();

        public IReadOnlyList<CraftingQueue> Queue => _queue;

        public bool CanCraft(Recipe recipe)
        {
            if (_queue.Count >= maxQueueSize) return false;

            var resources = GameManager.Instance.Resources;
            foreach (var ingredient in recipe.Ingredients)
            {
                if (!resources.CanAfford(ingredient.Resource, ingredient.Amount))
                    return false;
            }
            return true;
        }

        public bool StartCrafting(Recipe recipe)
        {
            if (!CanCraft(recipe)) return false;

            var resources = GameManager.Instance.Resources;
            foreach (var ingredient in recipe.Ingredients)
                resources.Spend(ingredient.Resource, ingredient.Amount);

            _queue.Add(new CraftingQueue
            {
                Recipe = recipe,
                Progress = 0f,
                ElapsedTime = 0f
            });

            return true;
        }

        private void Update()
        {
            for (int i = _queue.Count - 1; i >= 0; i--)
            {
                var item = _queue[i];
                item.ElapsedTime += Time.deltaTime;
                item.Progress = Mathf.Clamp01(item.ElapsedTime / item.Recipe.CraftingTime);

                EventBus.RaiseCraftingProgress(item.Recipe.RecipeId, item.Progress);

                if (item.Progress >= 1f)
                {
                    CompleteCraft(item);
                    _queue.RemoveAt(i);
                }
            }
        }

        private void CompleteCraft(CraftingQueue item)
        {
            GameManager.Instance.Resources.Add(item.Recipe.OutputResource, item.Recipe.OutputAmount);
            EventBus.RaiseRecipeCrafted(item.Recipe.RecipeId);
        }
    }
}
