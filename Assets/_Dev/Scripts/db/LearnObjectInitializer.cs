using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _Dev.Scripts.db
{
    /// <summary>
    /// This class initializes the LearnObjects from the Resource folder
    /// and saves them in a List of LearnObjects
    /// </summary>
    public class LearnObjectInitializer
    {
        private readonly LearnObjectManager _learnObjectManager;
        
        public LearnObjectInitializer(LearnObjectManager learnObjectManager)
        {
            _learnObjectManager = learnObjectManager;
        }
        
        // Dynamically load all assets in the LearnObjects folder
        private static List<string> GetResFolders()
        {
            try
            {
                var resourcePaths = Constants.LearnObjectsFolder; 
                return resourcePaths.ToList();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("An error occurred while trying to retrieve directory names: " + ex.Message);
                return new List<string>();
            }
        }
        
        public void InitializeDefaultLearnObjects()
        {
            List<string> resLearnObj = GetResFolders();
        
            foreach (var fName in resLearnObj)
            {
                Debug.Log("ResFolderAsset " + fName);
                TextAsset vocabTextAsset = Resources.Load<TextAsset>("LearnObjects/" + fName + "/" + Constants.VocabelTextFile);
                string[] names = vocabTextAsset != null ? vocabTextAsset.text.Split(',') : new string[] { "", "", "" };
                if (names.Length >= 3) // Check if all three languages are present
                {
                    _learnObjectManager.AddLearnObject(
                        new LearnObject(
                            Resources.Load<GameObject>("LearnObjects/" + fName + "/" + fName),
                            Resources.Load<AudioClip>("LearnObjects/" + fName + "/" + Constants.GermanAudioFile),
                            Resources.Load<AudioClip>("LearnObjects/" + fName + "/" + Constants.EnglishAudioFile),
                            Resources.Load<AudioClip>("LearnObjects/" + fName + "/" + Constants.VimmiAudioFile),
                            names[0],       // German
                            names[1],       // English
                            names[2]        // Vimmi
                        )
                    );
                }
                else
                {
                    Debug.LogError("TextAsset " + fName + " is defective");
                }
            }
        }

    }
}