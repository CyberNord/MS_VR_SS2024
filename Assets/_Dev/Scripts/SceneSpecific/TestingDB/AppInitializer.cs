using System;
using System.Collections.Generic;
using _Dev.Scripts.db;
using UnityEngine;

namespace _Dev.Scripts.SceneSpecific.TestingDB
{
    public class AppInitializer : MonoBehaviour
    {
        private LearnObjectManager _lm;
        
        public GameObject lmPos;
        public GameObject lmPos2;
        public GameObject lmPos3;

        private GameObject NormalizeAsset(float maxSizeThreshold, GameObject instance)
        {
            Vector3 size = instance.GetComponent<Renderer>().bounds.size; // Get the actual size of the instantiated object
            float maxDimension = Mathf.Max(size.x, size.y, size.z); // Determine the largest dimension

            if(maxDimension > maxSizeThreshold)
            {
                float scaleFactor = maxSizeThreshold / maxDimension; // Calculate the scale factor
                instance.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor); // Apply uniform scaling
            }
            
            return instance;
        }

        // Start is called before the first frame update
        void Start()
        {

            _lm = new LearnObjectManager(); 
            new LearnObjectInitializer(_lm).InitializeDefaultLearnObjects();
            
            // Instantiate from "Resource" folder "GetLearnObjectGroups" method 
            Instantiate(
                NormalizeAsset(1.0f,
                    // in a List of lists  (1)[1][0]  (number of Groups)[Group no-1][GameObject]
                    _lm.GetLearnObjectGroups(1)[0][0].Asset), 
                lmPos3.transform.position, 
                Quaternion.identity
                );

            // Instantiate from "Resource" folder "GetAllLearnObjects" method 
            List<LearnObject> allLearnObjects = _lm.GetAllLearnObjects();
            Debug.Log($"LearnObject count: {allLearnObjects.Count}");
            foreach (var lo in allLearnObjects)
            {
                Debug.Log(lo.ToString() + ", Asset: " + (lo.Asset != null ? lo.Asset.name : "NULL"));
            }

            //Hard Coded at the moment - will be replaced with a more dynamic approach 
            string[] toInstantiate = { "purse", "bottle"};
            GameObject[] go = { lmPos, lmPos2 };
            int i = 0;

            foreach (var curr in toInstantiate)
            {
                LearnObject currLearnObject = allLearnObjects.Find(
                        lo => string.Equals(lo.DescEnglish, curr, StringComparison.OrdinalIgnoreCase)
                    );
                if(currLearnObject != null && currLearnObject.Asset != null) 
                {
                    Instantiate(
                        NormalizeAsset(1.0f, currLearnObject.Asset),      // Object
                        go[i].transform.position,           // Position (placeholder object in scene)
                        Quaternion.identity                 // Rotation
                    );               
                }
                i++; 
            }
            
        }
    }
}
