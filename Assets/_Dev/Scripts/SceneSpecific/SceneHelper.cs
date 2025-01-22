using System;
using System.Collections;
using _Dev.Scripts.db;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using TMPro;
using UnityEngine.EventSystems;
using _Dev.Scripts.ObjectBehaviour;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Transactions;

namespace _Dev.Scripts.SceneSpecific
{
    /// <summary>
    /// A helper class for SceneInitializers
    /// </summary>
    public class SceneHelper : MonoBehaviour
    {
        
        public static GameObject InstantiateLearnObject(GameObject learnObject, GameObject pos, Quaternion rotation = default)
        {
            GameObject obj = null;
            if(learnObject != null && pos != null) 
            {
                obj = Instantiate(
                    NormalizeAsset(learnObject),            // Object
                    pos.transform.position,                 // Position (placeholder object in scene)
                    rotation                           // Rotation
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
        
        // This class sets the translation on the Canvas
        public static void SetUpTextComponents(GameObject canvas, LearnObject lo)
        {
            // Yea that's Confusing, however it is way too integrated at this point to change it.
            // "English Text" = fat Text --> to Language
            // "German_Text" = normal Text --> from Language
            TextMeshProUGUI englishTextComponent = canvas.transform.Find("English_Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI germanTextComponent = canvas.transform.Find("German_Text").GetComponent<TextMeshProUGUI>();
            
            if (englishTextComponent != null)
            {
                englishTextComponent.text = Constants.ToLanguage switch
                {
                    Constants.SLanguage.English => lo.DescEnglish,
                    Constants.SLanguage.German => lo.DescGerman,
                    Constants.SLanguage.Vimmi => lo.DescVimmi,
                    _ => lo.DescVimmi
                };
            }

            if (germanTextComponent != null)
            {
                germanTextComponent.text = Constants.FromLanguage switch
                {
                    Constants.SLanguage.English => lo.DescEnglish,
                    Constants.SLanguage.German => lo.DescGerman,
                    Constants.SLanguage.Vimmi => lo.DescVimmi,
                    _ => lo.DescGerman
                };
            }
        }

        public static void SetUpEventTrigger(GameObject canvas, LearnObject lo)
        {
            bool isAudioPlaying = false;
            var clip = Constants.ToLanguage switch
            {
                Constants.SLanguage.English => lo.AudioClipEnglish,
                Constants.SLanguage.German => lo.AudioClipGerman,
                Constants.SLanguage.Vimmi => lo.AudioClipVimmi,
                _ => lo.AudioClipVimmi
            };
            
            EventTrigger trigger = canvas.GetComponent<EventTrigger>() ?? canvas.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
                callback = new EventTrigger.TriggerEvent()
            };
            entry.callback.AddListener(_ =>
            {
                if(!isAudioPlaying)
                {
                    PlayAudio(clip);
                    isAudioPlaying = true;
                    canvas.GetComponent<MonoBehaviour>().StartCoroutine(TriggerAudioDelay(clip.length, () => { isAudioPlaying = false;  }));
                }
            });
            trigger.triggers.Add(entry);
        }

        private static IEnumerator TriggerAudioDelay(float clipLength, Action onComplete)
        {
            yield return new WaitForSeconds(clipLength);
            onComplete?.Invoke();
        }

        public static void ConvertMaterialToTransparent(GameObject obj)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>(true);

            foreach (Renderer renderer in renderers)
            {
                foreach (Material material in renderer.materials)
                {
                    material.SetFloat("_Surface", 1); // 1 is for Transparent mode
                }
            }

        }

        public static void ActivateComponents(GameObject learnObject)
        {
            learnObject.AddComponent<DestroyObject>();
            learnObject.GetComponent<Grabbable>().enabled = true;
            learnObject.GetComponent<GrabInteractable>().enabled = true;
            learnObject.GetComponent<HandGrabInteractable>().enabled = true;
            learnObject.GetComponent<PhysicsGrabbable>().enabled = true;
        }
    }
}
