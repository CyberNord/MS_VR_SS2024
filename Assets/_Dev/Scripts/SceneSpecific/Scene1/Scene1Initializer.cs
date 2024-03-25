using _Dev.Scripts.db;
using UnityEngine;

namespace _Dev.Scripts
{
    public class Scene1Initializer : MonoBehaviour
    {
        private LearnObjectManager lm;

        public GameObject lm_pos;
        public GameObject lm_pos2;
        public GameObject lm_pos3;
    
    
        // Start is called before the first frame update
        void Start()
        {

            lm = new LearnObjectManager(); 
            new LearnObjectInitializer(lm).InitializeDefaultLearnObjects();
            
            // Instanciate from Resource Folder 1
            Instantiate(lm.GetLearnObjectGroups(1)[1][0].Asset,     // in a List of lists ... Zukunftsrudi simplify pls
                lm_pos.transform.position,                                  // Position (a placeholder object in scene
                Quaternion.identity                                         // Rotation
                );                   
            
            Instantiate(lm.GetLearnObjectGroups(1)[0][0].Asset,lm_pos2.transform.position,Quaternion.identity); // Instanciate from Resource Folder 1

            Instantiate(lm.GetLearnObjectGroups(1)[2][0].Asset, lm_pos3.transform.position, Quaternion.identity); // Instanciate from Resource Folder 1
        }
    }
}
