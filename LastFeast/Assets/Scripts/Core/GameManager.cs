using UnityEngine;

namespace LastFeast.Core
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Manager References")]
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private SaveManager saveManager;
        [SerializeField] private TimeManager timeManager;

        public ResourceManager Resources => resourceManager;
        public SaveManager Save => saveManager;
        public TimeManager Time => timeManager;

        public GameState CurrentState { get; private set; } = GameState.MainMenu;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadGame();
        }

        public void StartNewGame()
        {
            saveManager.DeleteSave();
            CurrentState = GameState.Playing;
        }

        public void LoadGame()
        {
            var saveData = saveManager.Load();
            if (saveData == null)
            {
                Debug.Log("No save found — starting fresh");
                CurrentState = GameState.MainMenu;
                return;
            }

            resourceManager.LoadFromSave(saveData.Resources);
            timeManager.CalculateOfflineProgress(saveData.LastSaveTimestamp, resourceManager);
            CurrentState = GameState.Playing;
        }

        public void TriggerSave()
        {
            if (CurrentState != GameState.Playing) return;

            var saveData = new SaveData
            {
                Resources = resourceManager.GetAllResources(),
            };

            saveManager.Save(saveData);
        }

        public void SetState(GameState newState)
        {
            CurrentState = newState;
        }
    }
}
