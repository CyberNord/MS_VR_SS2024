namespace _Dev.Scripts
{
    public static class Constants
    {
        // Start Options
        public static GameState SelectedScene = GameState.M2;
        public static bool RandomizationMode = false;
        public static SLanguage FromLanguage = SLanguage.German;
        public static SLanguage ToLanguage = SLanguage.Vimmi; 
        
        // DB default Strings 
        
        public const string ResourcesPath = "Resources/LearnObjects";
        
        public const string GermanAudioFile = "ger";
        public const string EnglishAudioFile = "eng";
        public const string VimmiAudioFile = "vim";
        
        public const string VocabelTextFile = "loc";
        
        // Initializer Constants
        public const float MaxSizeThreshold = 1.0f;
        public const float RotationTolerance = 1.0f;
        public static readonly float[] RotationAngles = { 270f, 270f, 270f, 0f, 0f, 0f, 0f, 90f, 90f, 90f };

        public const int FixedRandomGroup1 = 0; 
        public const int FixedRandomGroup2 = 1; 
        public const int FixedRandomGroup3 = 2; 
        
        // LearnObject Manager Constants
        public const int Seed = 12345; //fixed Seed
        
        // Enums 
        public enum GameState
        {
            StartMenu, 
            M1,
            M2,
            M3
        }
        
        public enum SLanguage
        {
            English,
            German,
            Vimmi
        }
        
        // Resources
        public static readonly string[] LearnObjectsFolder = {
            "airplane",
            "backpack",
            "ball",
            "bandage",
            "bed",
            "bicycle",
            "bottle",
            "camera",
            "cigarette",
            "daily_newspaper",
            "drawer",
            "guitar",
            "key",
            "letter_box",
            "mask",
            "monitor",
            "painting",
            "plate",
            "present",
            "purse",
            "remote_control",
            "ring",
            "suitcase",
            "sunglasses",
            "telephone",
            "tent",
            "tire",
            "traffic_light",
            "tram",
            "window"
            
        };
    }
}
