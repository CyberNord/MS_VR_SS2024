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
        public static readonly float[] rotationAngles = { 270f, 270f, 270f, 0f, 0f, 0f, 0f, 90f, 90f, 90f };
    }
}
