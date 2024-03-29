using UnityEngine;

namespace _Dev.Scripts.SceneSpecific
{
    /// <summary>
    /// A helper class for SceneInitializers
    /// </summary>
    public class SceneHelper : MonoBehaviour
    {
        
        public static void InstantiateLearnObject(GameObject learnObject, GameObject pos,Quaternion rotation = default )
        {
            if(learnObject != null && pos != null) 
            {
                Instantiate(
                    NormalizeAsset(learnObject),            // Object
                    pos.transform.position,                       // Position (placeholder object in scene)
                    rotation                                      // Rotation
                );               
            }
        }

        private static GameObject NormalizeAsset(GameObject instance)
        {
            Vector3 size = instance.GetComponent<Renderer>().bounds.size; 
            float maxDimension = Mathf.Max(size.x, size.y, size.z);

            if (!(maxDimension > Constants.MaxSizeThreshold)) return instance;
            float scaleFactor = Constants.MaxSizeThreshold / maxDimension; 
            instance.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            return instance;
        }
    }
}
