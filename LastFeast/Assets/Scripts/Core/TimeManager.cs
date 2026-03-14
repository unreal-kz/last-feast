using System;
using UnityEngine;

namespace LastFeast.Core
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float maxOfflineHours = 8f;
        [SerializeField] private float offlineEfficiency = 0.5f; // 50% of normal production while offline

        public void CalculateOfflineProgress(long lastSaveTimestamp, ResourceManager resourceManager)
        {
            if (lastSaveTimestamp == 0) return;

            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            double elapsedSeconds = now - lastSaveTimestamp;

            if (elapsedSeconds <= 0) return;

            double maxOfflineSeconds = maxOfflineHours * 3600;
            elapsedSeconds = Math.Min(elapsedSeconds, maxOfflineSeconds);

            ApplyOfflineGains(elapsedSeconds, resourceManager);
            EventBus.RaiseOfflineProgressCalculated(elapsedSeconds);
        }

        private void ApplyOfflineGains(double elapsedSeconds, ResourceManager resourceManager)
        {
            // Base offline production rates per second (scaled by offlineEfficiency)
            // These will be modified by active buildings and assigned heroes
            // For now, apply minimal passive gains

            // TODO: Calculate based on active buildings and their production rates
            // Each building's production rate * elapsedSeconds * offlineEfficiency
            // This will be expanded when the building system is fully implemented

            Debug.Log($"Offline for {elapsedSeconds:F0}s — applying {offlineEfficiency * 100}% production gains");
        }
    }
}
