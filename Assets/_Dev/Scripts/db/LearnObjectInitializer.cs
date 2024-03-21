using UnityEngine;

namespace _Dev.Scripts
{
    public class LearnObjectInitializer
    {
        private readonly LearnObjectManager _learnObjectManager;
        
        public LearnObjectInitializer(LearnObjectManager learnObjectManager)
        {
            _learnObjectManager = learnObjectManager;
        }

        public void InitializeDefaultLearnObjects()
        {
            _learnObjectManager.AddLearnObject(
                new LearnObject(
                Resources.Load<GameObject>("LearnObjects\\sphere\\Sphere"),
                Resources.Load<AudioClip>("LearnObjects\\sphere\\ger"),
                Resources.Load<AudioClip>("LearnObjects\\sphere\\eng"),
                Resources.Load<AudioClip>("LearnObjects\\sphere\\vim"),
                "Kugel", "Sphere", "yxc")
                );
            _learnObjectManager.AddLearnObject(
                new LearnObject(
                    Resources.Load<GameObject>("LearnObjects\\cube\\Cube"),
                    Resources.Load<AudioClip>("LearnObjects\\cube\\ger"),
                    Resources.Load<AudioClip>("LearnObjects\\cube\\eng"),
                    Resources.Load<AudioClip>("LearnObjects\\cube\\vim"),
                    "Würfel", "Cube", "xyz")
            );
        }

    }
}