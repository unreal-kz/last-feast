using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace LastFeast.Core
{
    [Serializable]
    public class SaveData
    {
        public string SaveVersion = "1.0";
        public long LastSaveTimestamp;
        public List<ResourceData> Resources = new List<ResourceData>();
        public int Population;
        public List<BuildingSaveData> Buildings = new List<BuildingSaveData>();
        public List<string> DiscoveredHeroIds = new List<string>();
    }

    [Serializable]
    public class BuildingSaveData
    {
        public BuildingType Type;
        public int Tier;
        public bool IsConstructed;
        public float ConstructionProgress;
        public string AssignedHeroId;
    }

    public class SaveManager : MonoBehaviour
    {
        private const string SaveFileName = "lastfeast_save.json";

        private string SaveFilePath => Path.Combine(Application.persistentDataPath, SaveFileName);

        public bool HasSave() => File.Exists(SaveFilePath);

        public SaveData Load()
        {
            if (!HasSave())
                return null;

            try
            {
                string json = File.ReadAllText(SaveFilePath);
                return JsonUtility.FromJson<SaveData>(json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load save: {e.Message}");
                return null;
            }
        }

        public void Save(SaveData data)
        {
            data.LastSaveTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            try
            {
                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(SaveFilePath, json);
                EventBus.RaiseGameSaved();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save: {e.Message}");
            }
        }

        public void DeleteSave()
        {
            if (HasSave())
                File.Delete(SaveFilePath);
        }

        private void OnApplicationPause(bool paused)
        {
            if (paused)
            {
                GameManager.Instance?.TriggerSave();
            }
        }

        private void OnApplicationQuit()
        {
            GameManager.Instance?.TriggerSave();
        }
    }
}
