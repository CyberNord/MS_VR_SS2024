using UnityEngine;

namespace _Dev.Scripts.ObjectBehaviour.UI
{
   
    public class DropdownManager : MonoBehaviour
    {
        private Constants.GameState _selectedScene = Constants.GameState.M2;
        private bool _randomizationMode = false;
        private Constants.SLanguage _fromLanguage = Constants.SLanguage.English;
        private Constants.SLanguage _toLanguage = Constants.SLanguage.Vimmi;

        public void SetScene(int value)
        {
            switch (value)
            {
                case 0:
                    _selectedScene = Constants.GameState.M1;
                    break;
                case 1:
                    _selectedScene = Constants.GameState.M2;
                    break;
                case 2:
                    _selectedScene = Constants.GameState.M3;
                    break;
                default:
                    Debug.LogWarning("Invalid Scene Selection");
                    break;
            }
            UpdateSettings(); 
            Debug.Log($"Selected Scene: {_selectedScene}");
        }
        
        public void SetRandomization(int value)
        {
            switch (value)
            {
                case 0:
                    _randomizationMode = true; // RandomSeed
                    break;
                case 1:
                    _randomizationMode = false; // FixedSeed
                    break;
                default:
                    Debug.LogWarning("Invalid Randomization Mode");
                    break;
            }
            UpdateSettings();
            Debug.Log($"Selected Randomization: {_randomizationMode}");
        }

        public void SetFromLanguage(int value)
        {
            switch (value)
            {
                case 0: 
                    _fromLanguage = Constants.SLanguage.English;
                    break;
                case 1: 
                    _fromLanguage = Constants.SLanguage.German;
                    break;
                case 2:
                    _fromLanguage = Constants.SLanguage.Vimmi;
                    break;
                default:
                    Debug.LogWarning("Invalid Language Selection");
                    break;
            }
            UpdateSettings();
            Debug.Log($"Selected From Language: {_fromLanguage}");
        }

        public void SetToLanguage(int value)
        {
            switch (value)
            {
                case 0: 
                    _toLanguage = Constants.SLanguage.Vimmi;
                    break;
                case 1:
                    _toLanguage = Constants.SLanguage.German;
                    break;
                case 2:
                    _toLanguage = Constants.SLanguage.English;
                    break;
                default:
                    Debug.LogWarning("Invalid Language Selection");
                    break;
            }
            UpdateSettings();
            Debug.Log($"Selected To Language: {_toLanguage}");
        }


        private void UpdateSettings()
        {
            Constants.SelectedScene = _selectedScene;
            Constants.RandomizationMode = _randomizationMode;
            Constants.FromLanguage = _fromLanguage;
            Constants.ToLanguage = _toLanguage;
        }
    }
}
