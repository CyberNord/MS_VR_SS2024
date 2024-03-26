using UnityEngine;

namespace _Dev.Scripts.db
{
    public class LearnObject
    {
        private static int _nextId = 1;
        private readonly GameObject _asset;
        private readonly int _id;
        private readonly AudioClip _audioClipGerman; 
        private readonly AudioClip _audioClipEnglish; 
        private readonly AudioClip _audioClipVimmi;
        private readonly string _descGerman;
        private readonly string _descEnglish;
        private readonly string _descVimmi;
        

        public GameObject Asset => _asset;
        public int Id => _id;
        public AudioClip AudioClipEnglish => _audioClipEnglish;
        public AudioClip AudioClipGerman => _audioClipGerman;
        public AudioClip AudioClipVimmi => _audioClipVimmi;
        public string DescGerman => _descGerman;
        public string DescEnglish => _descEnglish;
        public string DescVimmi => _descVimmi;

        public LearnObject(GameObject asset, AudioClip audioClipGerman, AudioClip audioClipEnglish,
            AudioClip audioClipVimmi, string descGerman, string descEnglish, string descVimmi)
        {
            _asset = asset;
            _id = _nextId++;
            _audioClipEnglish = audioClipEnglish;
            _audioClipGerman = audioClipGerman;
            _audioClipVimmi = audioClipVimmi;
            _descGerman = descGerman;
            _descEnglish = descEnglish;
            _descVimmi = descVimmi;
        }

        public override string ToString()
        {
            return "ID = " + Id + "\nDescEng = " + DescEnglish ;
        }
    }
}