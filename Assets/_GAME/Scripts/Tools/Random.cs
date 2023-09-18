using System;
using System.Collections.Generic;
using System.Linq;

namespace _GAME.Scripts.Tools
{
    public static class Random
    {
        private static readonly System.Random _systemRandom = new System.Random();

        public static float value => _systemRandom.Next(0, int.MaxValue) / (float)int.MaxValue;

        public static float Range(float min, float max)
        {
            return min + (max - min) * value;
        }

        public static int Range(int min, int max)
        {
            return _systemRandom.Next(min, max);
        }

        public static T RandomValue<T>(this List<T> list)
        {
            return list == null || list.Count == 0 ? default : list[Range(0, list.Count)];
        }

        public static T RandomValueWithExclusion<T>(this List<T> list, T excludedValue)
        {
            if (list == null || list.Count == 0) return default;

            var filteredList = new List<T>();
            for (int i = 0; i < list.Count; i++)
            { 
                if (list[i].Equals(excludedValue))
                {
                    continue;
                }
                filteredList.Add(list[i]);
            }

            if (filteredList.Count == 0) return default;
            var randomIndex = Range(0, filteredList.Count);
            return filteredList[randomIndex];
        }

        public static T RandomValue<T>(this IEnumerable<T> collection)
        {
            return collection.ToList().RandomValue();
        }

        public static T GetRandomItem<T>(this IEnumerable<T> items) where T : IRandomizedByWeight
        {
            var itemsArray = items.ToArray();
            var randomIndex = GetRandomItemIndex(itemsArray);
            return randomIndex < 0 ? default : itemsArray[randomIndex];
        }

        public static int GetRandomItemIndex<T>(this IEnumerable<T> items) where T : IRandomizedByWeight
        {
            var itemsArray = items.ToArray();
            if (itemsArray.Length == 0) return -1;

            var weightSum = itemsArray.Sum(i => i.RandomWeight);
            var randomResult = Range(0, weightSum);
            var prevWeights = 0f;
            var itemIndex = 0;

            foreach (var item in itemsArray)
            {
                if (randomResult - prevWeights <= item.RandomWeight) return itemIndex;
                prevWeights += item.RandomWeight;
                itemIndex++;
            }

            return itemIndex;
        }

        public static int GetRandomItemIndex(this IEnumerable<float> weights)
        {
            return weights.Select(w => new RandomizedByWeightWrapper { RandomWeight = w })
                .GetRandomItemIndex();
        }
    }

    public class RandomItem : IRandomizedByWeight
    {
        public Enum Type { get; set; }
        public float RandomWeight { get; set; }
    }

    public interface IRandomizedByWeight
    {
        float RandomWeight { get; }
    }

    public struct RandomizedByWeightWrapper : IRandomizedByWeight
    {
        public float RandomWeight { get; set; }
    }
}