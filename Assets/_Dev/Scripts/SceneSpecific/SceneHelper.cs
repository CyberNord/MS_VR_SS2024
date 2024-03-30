using System;
using _Dev.Scripts.db;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using TMPro;
using UnityEngine.EventSystems;

namespace _Dev.Scripts.SceneSpecific
{
    /// <summary>
    /// A helper class for SceneInitializers
    /// </summary>
    public class SceneHelper : MonoBehaviour
    {
        
        public static GameObject InstantiateLearnObject(GameObject learnObject, GameObject pos,Quaternion rotation = default )
        {
            GameObject obj = null;
            if(learnObject != null && pos != null) 
            {
                obj = Instantiate(
                    NormalizeAsset(learnObject),            // Object
                    pos.transform.position,                       // Position (placeholder object in scene)
                    rotation                                      // Rotation
                );               
            }
            return obj;
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
        
        public static void PlayAudio(AudioClip clip)
        {
            if (clip == null) return;
            Debug.Assert(Camera.main != null, "Camera.main != null");
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
        
        public static Vector3 CalculateCanvasPosition(GameObject lmPosition, float rotationAngle)
        {
            Vector3 basePosition = lmPosition.transform.position + new Vector3(0, 0.5f, 0.5f);

            if (Math.Abs(rotationAngle - 270) < Constants.RotationTolerance)
            {
                return lmPosition.transform.position + new Vector3(-0.4f, 0.5f, 0);
            }

            if (Math.Abs(rotationAngle - 90) < Constants.RotationTolerance)
            {
                return lmPosition.transform.position + new Vector3(0.4f, 0.5f, 0);
            }

            return basePosition;
        }
        
        public static void SetUpTextComponents(GameObject canvas, LearnObject lo)
        {
            TextMeshProUGUI englishTextComponent = canvas.transform.Find("English_Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI germanTextComponent = canvas.transform.Find("German_Text").GetComponent<TextMeshProUGUI>();

            if (englishTextComponent != null)
            {
                englishTextComponent.text = lo.DescEnglish;
            }

            if (germanTextComponent != null)
            {
                germanTextComponent.text = lo.DescGerman;
            }
        }

        public static void SetUpEventTrigger(GameObject canvas, LearnObject lo)
        {
            EventTrigger trigger = canvas.GetComponent<EventTrigger>() ?? canvas.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
                callback = new EventTrigger.TriggerEvent()
            };
            entry.callback.AddListener(_ => PlayAudio(lo.AudioClipEnglish));
            trigger.triggers.Add(entry);
        }
        
        public static void ConvertMaterialToTransparent(GameObject obj)
        {
            Renderer renderer = obj.GetComponent<Renderer>();

            // Change material rendering mode to Transparent
            if (renderer != null && renderer.material != null)
            {
                renderer.material.SetFloat("_Surface", 1); // 1 is for Transparent mode
            }
        }
    }
}
