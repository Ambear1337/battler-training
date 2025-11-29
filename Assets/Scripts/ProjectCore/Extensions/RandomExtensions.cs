using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCore.Extensions
{
    public static class RandomExtensions
    {
        public static void Shuffle<T>(this System.Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }

        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> ts)
        {
            // https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
            var count = ts.Count;
            var last = count - 1;

            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }

        public static T TakeRandom<T>(this T[] array)
        {
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static string TakeRandom(this string str)
        {
            return str[UnityEngine.Random.Range(0, str.Length)].ToString();
        }

        public static T TakeRandom<T>(this List<T> array)
        {
            return array[UnityEngine.Random.Range(0, array.Count)];
        }

        public static T TakeRandom<T>(this IReadOnlyList<T> array)
        {
            return array[UnityEngine.Random.Range(0, array.Count)];
        }

        public static T TakeRandom<T>(this List<T> array, out int index)
        {
            index = UnityEngine.Random.Range(0, array.Count);
            return array[index];
        }

        public static T TakeRandom<T>(this IReadOnlyList<T> array, out int index)
        {
            index = UnityEngine.Random.Range(0, array.Count);
            return array[index];
        }

        public static T[] RandomExclude<T>(this T[] array, T[] exclude, int count = 1)
        {
            List<T> tmp = new List<T>(array);
            List<T> final = new List<T>();

            foreach (var ex in exclude)
                tmp.Remove(ex);

            for (int i = 0; i < count; i++)
            {
                if (tmp.Count <= 0)
                    break;

                T value = TakeRandom(tmp.ToArray());

                final.Add(value);
                tmp.Remove(value);
            }

            return final.ToArray();
        }

        public static bool CalculateRandomChance(int chance)
        {
            if (chance == 0)
                return false;
            else if (chance == 100)
                return true;

            return UnityEngine.Random.Range(0, 100) < chance ? true : false;
        }

        private class Node
        {
            public float Weight { get; set; }
            public int ID { get; set; }
            public float TotalWeight { get; set; }

            public Node(float weight, int id, float totalWeight)
            {
                Weight = weight;
                ID = id;
                TotalWeight = totalWeight;
            }
        }

        public static int WeightedRandomIndex(float[] weights)
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(null);

            for (int n = 0; n < weights.Length; n++)
                nodes.Add(new Node(weights[n], n, weights[n]));


            for (int n = nodes.Count - 1; n > 1; n--)
                nodes[n >> 1].TotalWeight += nodes[n].TotalWeight;

            List<int> picked = new List<int>();


            float gas = (float)UnityEngine.Random.Range(0, nodes[1].TotalWeight);
            int i = 1;

            try
            {
                //in some case where is IndexOutOfRange, IDK why.
                while (gas >= nodes[i].Weight)
                {
                    gas -= nodes[i].Weight;
                    i <<= 1;

                    if (gas >= nodes[i].TotalWeight)
                    {
                        gas -= nodes[i].TotalWeight;
                        i += 1;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            float weight = nodes[i].Weight;
            return nodes[i].ID;
        }

        public static int GetWeightedRandomIndex(List<float> weights)
        {
            if (weights == null || weights.Count == 0)
            {
                Debug.LogError("Invalid input data.");
                return -1;
            }

            float[] cumulativeWeights = new float[weights.Count];
            float totalWeight = 0;

            for (int i = 0; i < weights.Count; i++)
            {
                if (weights[i] < 0)
                {
                    Debug.LogError("Weights must be non-negative.");
                    return -1;
                }

                totalWeight += weights[i];
                cumulativeWeights[i] = totalWeight;
            }

            if (totalWeight <= 0)
            {
                Debug.LogError("Total weight must be greater than zero.");
                return -1;
            }

            double randomNumber = UnityEngine.Random.value * cumulativeWeights[^1];

            for (int i = 0; i < cumulativeWeights.Length; i++)
            {
                if (randomNumber <= cumulativeWeights[i])
                    return i;
            }

            return weights.Count - 1;
        }


        public static TWeightedElement TakeWeightedRandomElement<TWeightedElement>(
            this TWeightedElement[] weightedArray) where TWeightedElement : IWeight
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(null);

            for (int n = 0; n < weightedArray.Length; n++)
                nodes.Add(new Node(weightedArray[n].Weight, n, weightedArray[n].Weight));


            for (int n = nodes.Count - 1; n > 1; n--)
                nodes[n >> 1].TotalWeight += nodes[n].TotalWeight;

            List<int> picked = new List<int>();


            float gas = (float)UnityEngine.Random.Range(0, nodes[1].TotalWeight);
            int i = 1;

            while (gas >= nodes[i].Weight)
            {
                gas -= nodes[i].Weight;
                i <<= 1;

                if (gas >= nodes[i].TotalWeight)
                {
                    gas -= nodes[i].TotalWeight;
                    i += 1;
                }
            }

            float weight = nodes[i].Weight;
            return weightedArray[nodes[i].ID];
        }
        
        public interface IWeight
        {
            float Weight { get; }
        }
    }
}