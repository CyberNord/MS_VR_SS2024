using System;
using System.Collections.Generic;
using _Dev.Scripts.db;
using UnityEngine;

namespace _Dev.Scripts.SceneSpecific.TestingDB
{
    public class AppInitializer : MonoBehaviour
    {
        private LearnObjectManager _lm;
        private List<LearnObject> _allLearnObjects;
        private readonly Dictionary<string, GameObject> _toInstantiate = new();
        
        [Header("Spawn-points for LearnObjects")]
        [SerializeField] private List<GameObject> positions;
        
        
        // Start is called before the first frame update
        private void Start()
        {
            _lm = new LearnObjectManager(); 
            new LearnObjectInitializer(_lm).InitializeDefaultLearnObjects();

            // Instantiate from "Resource" folder "GetAllLearnObjects" method 
            _allLearnObjects = _lm.GetAllLearnObjects();
            PopulateIdentifiers(_lm.GetAllLearnObjectsEngDesc());

            foreach (var item in _toInstantiate)
            {
                var currLearnObject = _allLearnObjects.Find(
                    lo => string.Equals(
                        lo.DescEnglish, 
                        item.Key, 
                        StringComparison.OrdinalIgnoreCase
                        )
                    );
                    
                if (item.Value != null && currLearnObject.Asset != null)
                {
                    InstantiateLearnObject(item.Key, item.Value);
                }
            }
        }
        
        private void PopulateIdentifiers(List<string> identifiers)
        {
            int minCount = Mathf.Min(identifiers.Count, positions.Count);
            for (var i = 0; i < minCount; i++)
            {
                if (!_toInstantiate.ContainsKey(identifiers[i]))
                {
                    _toInstantiate.Add(identifiers[i], positions[i]);
                }
            }
        }
        private static GameObject NormalizeAsset(GameObject instance)
        {
            Vector3 size = instance.GetComponent<Renderer>().bounds.size; 
            float maxDimension = Mathf.Max(size.x, size.y, size.z); 

            if(maxDimension > Constants.MaxSizeThreshold)
            {
                float scaleFactor = Constants.MaxSizeThreshold / maxDimension; 
                instance.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor); 
            }
            
            return instance;
        }
        private void InstantiateLearnObject(string descEnglish, GameObject instance)
        {
            var currLearnObject = _allLearnObjects.Find(
                lo => string.Equals(lo.DescEnglish, descEnglish, StringComparison.OrdinalIgnoreCase)
            );
            
            if(currLearnObject != null && currLearnObject.Asset != null) 
            {
                Instantiate(
                    NormalizeAsset(currLearnObject.Asset),      // Object
                    instance.transform.position,           // Position (placeholder object in scene)
                    Quaternion.identity                 // Rotation
                );               
            }
        }
    }
}
