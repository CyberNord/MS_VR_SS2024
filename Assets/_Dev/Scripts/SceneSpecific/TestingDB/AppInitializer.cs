using System;
using System.Collections.Generic;
using _Dev.Scripts.db;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Dev.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        private LearnObjectManager _lm;
        
        public GameObject lmPos;
        public GameObject lmPos2;
        public GameObject lmPos3;


        // Start is called before the first frame update
        void Start()
        {

            _lm = new LearnObjectManager(); 
            new LearnObjectInitializer(_lm).InitializeDefaultLearnObjects();
            
            // Instantiate from "Resource" folder "GetLearnObjectGroups" method 
            Instantiate(_lm.GetLearnObjectGroups(1)[1][0].Asset,     // in a List of lists  (1)[1][0]  (number of Groups)[Group no-1][GameObject]
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
            
            
            //Objects are placed by their english name
            LearnObject cubeLearnObject = allLearnObjects.Find(lo => string.Equals(lo.DescEnglish, "Cube", StringComparison.OrdinalIgnoreCase));
            if(cubeLearnObject != null && cubeLearnObject.Asset != null) 
            {
                Instantiate(cubeLearnObject.Asset,      // Object
                    lmPos.transform.position,           // Position (placeholder object in scene)
                    Quaternion.identity);               // Rotation
            }
            
            LearnObject sphereLearnObject = allLearnObjects.Find(lo => string.Equals(lo.DescEnglish, "Sphere", StringComparison.OrdinalIgnoreCase));
            if(sphereLearnObject != null && sphereLearnObject.Asset != null)
            {
                Instantiate(sphereLearnObject.Asset, 
                    lmPos2.transform.position, 
                    Quaternion.identity);
            }
            
            
            
            

        }
    }
}
