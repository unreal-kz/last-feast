using UnityEngine;

namespace LastFeast.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject hudPanel;
        [SerializeField] private GameObject buildingPanel;
        [SerializeField] private GameObject craftingPanel;
        [SerializeField] private GameObject heroPanel;

        public void ShowMainMenu()
        {
            HideAll();
            SetActive(mainMenuPanel, true);
        }

        public void ShowHUD()
        {
            HideAll();
            SetActive(hudPanel, true);
        }

        public void ShowBuildingPanel()
        {
            SetActive(buildingPanel, true);
        }

        public void ShowCraftingPanel()
        {
            SetActive(craftingPanel, true);
        }

        public void ShowHeroPanel()
        {
            SetActive(heroPanel, true);
        }

        public void HideAll()
        {
            SetActive(mainMenuPanel, false);
            SetActive(hudPanel, false);
            SetActive(buildingPanel, false);
            SetActive(craftingPanel, false);
            SetActive(heroPanel, false);
        }

        private void SetActive(GameObject panel, bool active)
        {
            if (panel != null)
                panel.SetActive(active);
        }
    }
}
