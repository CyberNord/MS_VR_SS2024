using UnityEngine;

namespace _Dev.Scripts.db
{
    /// <summary>
    /// This class ensures that the LearnObjects from the Resource folder are loaded once during runtime.
    /// This guarantees that the assets are loaded correctly and are included in the final build and prevents issues related to missing resources
    /// </summary>
    public static class ResourcesLoader
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        
        public static void PreloadResources()
        {
            var allObjects = Resources.LoadAll("LearnObjects");
            foreach (var obj in allObjects)
            {
                Debug.Log($"Preloaded: {obj.name}");
            }
        }
        
        private static void ValidateResources()
        {
            
            var assets = Resources.LoadAll("LearnObjects");
            foreach (var asset in assets)
            {
                Debug.Log($"Loaded asset: {asset.name}");
            }
            
            foreach (string folder in Constants.LearnObjectsFolder)
            {
                // Load each asset type
                var objectPath = "LearnObjects/" + folder + "/" + folder;
                var audioGerPath = "LearnObjects/" + folder + "/" + Constants.GermanAudioFile;
                var audioEngPath = "LearnObjects/" + folder + "/" + Constants.EnglishAudioFile;
                var audioVimPath = "LearnObjects/" + folder + "/" + Constants.VimmiAudioFile;
                var textPath = "LearnObjects/" + folder + "/" + Constants.VocabelTextFile;

                // Check if assets exist
                var obj = Resources.Load<GameObject>(objectPath);
                var audioGer = Resources.Load<AudioClip>(audioGerPath);
                var audioEng = Resources.Load<AudioClip>(audioEngPath);
                var audioVim = Resources.Load<AudioClip>(audioVimPath);
                var text = Resources.Load<TextAsset>(textPath);

                // Log results
                Debug.Log($"Checking folder: {folder}");
                bool allAssetsFound = true;

                if (obj == null)
                {
                    Debug.LogError($"Missing 3D Object: {objectPath}");
                    allAssetsFound = false;
                }
                if (audioGer == null)
                {
                    Debug.LogError($"Missing German Audio: {audioGerPath}");
                    allAssetsFound = false;
                }
                if (audioEng == null)
                {
                    Debug.LogError($"Missing English Audio: {audioEngPath}");
                    allAssetsFound = false;
                }
                if (audioVim == null)
                {
                    Debug.LogError($"Missing Vimmi Audio: {audioVimPath}");
                    allAssetsFound = false;
                }
                if (text == null)
                {
                    Debug.LogError($"Missing Text File: {textPath}");
                    allAssetsFound = false;
                }

                if (allAssetsFound)
                {
                    Debug.Log($"All assets successfully loaded for folder: {folder}");
                }

            }
        }
    }
}