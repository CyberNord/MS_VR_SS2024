namespace _Dev.Scripts
{
    public static class Constants
    {
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
