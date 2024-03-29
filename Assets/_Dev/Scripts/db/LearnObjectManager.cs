using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace _Dev.Scripts.db
{
    public class LearnObjectManager
    {
        private readonly List<LearnObject> _learnObjects;

        public LearnObjectManager(List<LearnObject> initialLearnObjects = null)
        {
            _learnObjects = initialLearnObjects ?? new List<LearnObject>();
        }

        public void AddLearnObject(LearnObject learnObject)
        {
            if (_learnObjects.Any(lo => lo.Id == learnObject.Id))
            {
                throw new InvalidOperationException(
                    $"LearnObject with the same ID already exists. \nID {learnObject.Id} --> {learnObject.DescEnglish}");
            }
            _learnObjects.Add(learnObject);
        }

        public void AddLearnObject(List<LearnObject> learnObjectList)
        {
            foreach (var learnObject in learnObjectList)
            {
                AddLearnObject(learnObject);
            }
        }

        public List<LearnObject> GetAllLearnObjects()
        {
            return _learnObjects.OrderBy(lo => lo.Id).ToList();
        }

        public List<string> GetAllLearnObjectsEngDesc()
        {
            return _learnObjects.Select(learnObject => learnObject.DescEnglish).ToList();
        }
        
        public List<string> GetAllLearnObjectsGerDesc()
        {
            return _learnObjects.Select(learnObject => learnObject.DescGerman).ToList();
        }

        // flexible grouping
        public List<LearnObject> GetLearnObjectGroup(int groupSize, int groupNumber)
        {
            if (_learnObjects.Count < groupSize)
            {
                throw new InvalidOperationException($"Not enough objects to form a group of {groupSize}.");
            }

            var sortedLearnObjects = _learnObjects.OrderBy(lo => lo.DescGerman).ToList();
            int numberOfGroups = (int)Math.Ceiling((double)sortedLearnObjects.Count / groupSize);

            List<LearnObject>[] groups = new List<LearnObject>[numberOfGroups];
            for (int i = 0; i < numberOfGroups; i++)
            {
                groups[i] = sortedLearnObjects.Skip(i * groupSize).Take(groupSize).ToList();
            }

            return groups[groupNumber];
        }

        // ensures consistent fixed group
        public List<LearnObject> GetLearnObjectGroupsFixed(int groupNumber)
        {
            if (_learnObjects.Count < 30)
            {
                throw new InvalidOperationException("Not enough objects to form the required groups.");
            }
            
            const int seed = 12345;
            var rnd = new Random(seed);
            var randomizedLearnObjects = _learnObjects.OrderBy(_=> rnd.Next()).ToList();

            List<LearnObject>[] groups = new List<LearnObject>[3];
            for (int i = 0; i < groups.Length; i++)
            {
                groups[i] = randomizedLearnObjects.Skip(i * 10).Take(10).ToList();
            }

            return groups[groupNumber];
        }
    }
}