using System;
using System.Collections.Generic;
using Xunit;

namespace HackerRank.Search
{
    /// <summary>
    /// https://www.hackerrank.com/challenges/pairs
    /// </summary>
    [Trait("Category", "HackerRank: Search")]
    public class Pairs
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(int k, int[] arr, int expectedPairCount)
        {
            var pairsNumber = CountPairsNumber(k, arr);
            Assert.Equal(expectedPairCount, pairsNumber);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<int, int[], int>
        {
            { 2, new[] { 1, 5, 3, 4, 2 }, 3 },
        };
        
        private static int CountPairsNumber(int k, int[] arr)
        {
            Array.Sort(arr);

            int pairsCount = 0;
            
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int first = arr[i];
                int pair = first + k;

                int pairIndex = Array.BinarySearch(arr, i + 1, arr.Length - i - 1, pair);
                if (pairIndex < 0)
                {
                    continue;
                }

                pairsCount++;
            }

            return pairsCount;
        }
        
        
    }
}