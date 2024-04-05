using System;
using System.Collections.Generic;
using System.Linq;
using _Dev.Scripts.db;
using UnityEngine;

namespace _Dev.Scripts.SceneSpecific.Scene3
{
    public class Scene3Init : MonoBehaviour
    {
        private LearnObjectManager _lm;
        private List<LearnObject> _allLearnObjects;
        private readonly Dictionary<string, GameObject> _posToInstantiate = new();
        private Dictionary<string, LearnObject> _allLearnObjectsDict;

        [Header("Spawn-points for LearnObjects")] [SerializeField]
        private List<GameObject> loPositions;

        [SerializeField] private GameObject canvasPrefab;

        // Start is called before the first frame update
        private void Start()
        {
            _lm = new LearnObjectManager();
            new LearnObjectInitializer(_lm).InitializeDefaultLearnObjects();

            // Create Dictionary (Key = DescEnglish, Value = LearnObject) ==> Objects to Spawn
            _allLearnObjectsDict = _lm.GetAllLearnObjects()
                .ToDictionary(
                    lo => lo.DescEnglish, lo => lo,
                    StringComparer.OrdinalIgnoreCase
                );

            // Create Dictionary (Key = DescEnglish, Value = Position Object) ==> Positions to Spawn
            PopulateIdentifiers(
                _lm.GetLearnObjectGroupsFixed(Constants.FixedRandomGroup3) // Set the fixed random group
                    .Select(x => x.DescEnglish)
                    .ToList()
            );


            // Instantiate the LearnObjects to positions 
            int i = -1;
            foreach (var posPair in _posToInstantiate)
            {
                LearnObject currLearnObject;
                i++;
                if (_allLearnObjectsDict.TryGetValue(posPair.Key, out currLearnObject))
                {
                    Debug.Log("Instantiate LearnObject: " + currLearnObject.DescEnglish);
                    Quaternion rotation = currLearnObject.Asset.transform.rotation * Quaternion.Euler(0, Constants.RotationAngles[i], 0);
                    GameObject obj = SceneHelper.InstantiateLearnObject(currLearnObject.Asset, posPair.Value, rotation);
                    SceneHelper.ConvertMaterialToTransparent(obj);
                    SceneHelper.ActivateComponents(obj);

                    InstantiateObjectWithCanvas(currLearnObject, posPair.Value, Constants.RotationAngles[i]);
                }
            }
        }

        private void PopulateIdentifiers(List<string> identifiers)
        {
            int minCount = Mathf.Min(identifiers.Count, loPositions.Count);
            for (var i = 0; i < minCount; i++)
            {
                if (!_posToInstantiate.ContainsKey(identifiers[i]))
                {
                    _posToInstantiate.Add(identifiers[i], loPositions[i]);
                }
            }
        }

        private void InstantiateObjectWithCanvas(LearnObject lo, GameObject lmPosition, float rotationAngle)
        {
            Vector3 canvasPosition = SceneHelper.CalculateCanvasPosition(lmPosition, rotationAngle);
            GameObject canvas = Instantiate(canvasPrefab, canvasPosition, Quaternion.identity);
            canvas.transform.Rotate(Vector3.up, rotationAngle);

            SceneHelper.SetUpTextComponents(canvas, lo);
            SceneHelper.SetUpEventTrigger(canvas, lo);
        }
    }
}