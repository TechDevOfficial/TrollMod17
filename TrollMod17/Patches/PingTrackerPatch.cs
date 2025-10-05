/* using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;

namespace TrollMod17.Patches
{
    [HarmonyPatch(typeof(PingTracker))]
    public static class PingTrackerPatch
    {
        private static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource("PingTrackerPatch");

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void UpdatePostfix(PingTracker __instance)
        {
            MovePingToTopRight(__instance);
        }

        private static void MovePingToTopRight(PingTracker pingTracker)
        {
            try
            {
                var aspectPosition = pingTracker.GetComponent<AspectPosition>();
                if (aspectPosition != null)
                {
                    aspectPosition.enabled = false;
                    UnityEngine.Object.Destroy(aspectPosition);
                }

                if (HudManager.Instance != null && HudManager.Instance.transform != null)
                {
                    pingTracker.transform.SetParent(HudManager.Instance.transform, false);
                }

                pingTracker.transform.localPosition = new Vector3(1.3f, 2.9f, 0f);

                if (pingTracker.text != null)
                {
                    pingTracker.text.sortingOrder = 10000;
                    pingTracker.text.color = Color.yellow;
                    pingTracker.text.gameObject.SetActive(true);

                    string originalText = pingTracker.text.text;
                    if (!originalText.Contains("made by Manu"))
                    {
                        pingTracker.text.text = originalText + "\nmade by Manu";
                    }
                }
            }
            catch (System.Exception ex)
            {
                Logger.LogError($"Error moving PingTracker: {ex}");
            }
        }
    }
} */