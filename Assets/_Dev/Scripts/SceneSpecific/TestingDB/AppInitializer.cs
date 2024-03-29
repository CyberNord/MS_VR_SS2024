using System;
using System.Collections.Generic;
using System.Linq;
using _Dev.Scripts.db;
using UnityEngine;

namespace _Dev.Scripts.SceneSpecific.TestingDB
{
    /// <summary>
    /// The template class for each scene placing the LearnObjects on their place on the shelves
    /// </summary>
    public class AppInitializer : MonoBehaviour
    {
        private LearnObjectManager _lm;
        private List<LearnObject> _allLearnObjects;
        private readonly Dictionary<string, GameObject> _posToInstantiate = new();
        private Dictionary<string, LearnObject> _allLearnObjectsDict;
        
        [Header("Spawn-points for LearnObjects")]
        [SerializeField] private List<GameObject> loPositions;
        
        
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
            PopulateIdentifiers(_lm.GetAllLearnObjectsEngDesc());
            
            // Instantiate the LearnObjects to positions 
            foreach (var identifier in _posToInstantiate)
            {
                LearnObject currLearnObject;
                if (_allLearnObjectsDict.TryGetValue(identifier.Key, out currLearnObject))
                {
                    SceneHelper.InstantiateLearnObject(currLearnObject.Asset, identifier.Value);
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
    }
}
