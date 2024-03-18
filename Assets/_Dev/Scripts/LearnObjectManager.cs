using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Dev.Scripts
{
    public class Database
    {
        private readonly List<LearnObject> _learnObjects;

        public Database()
        {
            _learnObjects = new List<LearnObject>();
        }

        public void AddLearnObject(LearnObject learnObject)
        {
            _learnObjects.Add(learnObject);
        }
        
        
        public List<LearnObject>[] GetLearnObjectGroups()
        {
            if (_learnObjects.Count < 30)
            {
                return null;
            }
            // Sort by german to ensure consistent Sorting
            var sortedLearnObjects = _learnObjects.OrderBy(lo => lo.DescGerman).ToList();

            List<LearnObject>[] groups = new List<LearnObject>[3];
            for (int i = 0; i < groups.Length; i++)
            {
                // Skipping ensures each group gets a unique set of LearnObjects
                groups[i] = sortedLearnObjects.Skip(i * 10).Take(10).ToList();
            }

            return groups;
        }
    }
}