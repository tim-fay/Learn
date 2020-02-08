using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Vanya and Lanterns
    /// http://codeforces.com/gym/268160/problem/C
    /// </summary>
    [Trait("Category", "Spock: Corporate Algorithmic Course: HW 1.1")]
    public class VanyaAndLanterns
    {
        [Theory]
        [MemberData(nameof(InputData1))]
        public void Test1(int streetLength, int[] lanternPositions, float expectedMinimumRadius)
        {
            var radius = CalculateMinimumLightEmittingRadius(streetLength, lanternPositions);
            Assert.Equal(expectedMinimumRadius, radius);
        }

        public static IEnumerable<object[]> InputData1 => new TheoryData<int, int[], float>
        {
            { 10, new[] { 5 }, 5f },
            { 10, new[] { 4, 4, 2, 2, 6, 6, 6 }, 4f },
            { 1, new[] { 0 }, 1f },
            { 1, new[] { 0, 1 }, 0.5f },
            { 1, new[] { 1, 1, 1, 1 }, 1f },
            { 100, new[] { 0, 0, 0 }, 100f },
            { 1000000000, new[] { 0, 1000000000 }, 500000000f },
            { 1000000000, new[] { 0, 1 }, 999999999f },
            { 5, new[] { 2, 5 }, 2f },
            { 5, new[] { 2, 3 }, 2f },
            { 5, new[] { 1, 4 }, 1.5f },
            { 15, new[] { 15, 5, 3, 7, 9, 14, 0 }, 2.5f },
        };
        
        
        /// <summary>
        /// Solving the algorithm by sorting lantern positions and calculating maximum distance between 2 adjacent lanterns. 
        /// </summary>
        private static float CalculateMinimumLightEmittingRadius(int streetLength, int[] lanternPositions)
        {
            Array.Sort(lanternPositions);
            
            // Initialize maxDistance with one of maximum length between start/end of the street and first/last lantern 
            int maximumDistanceBetweenAdjacentLanterns = 0;

            for (int i = 0; i < lanternPositions.Length - 1; i++)
            {
                var localMaxDistance = lanternPositions[i + 1] - lanternPositions[i];
                if (localMaxDistance > maximumDistanceBetweenAdjacentLanterns)
                {
                    maximumDistanceBetweenAdjacentLanterns = localMaxDistance;
                }
            }
            
            var maxRadiusForEdgeLanterns = (float)Math.Max(lanternPositions[0], streetLength - lanternPositions[lanternPositions.Length - 1]);
            var maxRadiusBetweenLanterns = (float)maximumDistanceBetweenAdjacentLanterns / 2;

            float minimumRadius = Math.Max(maxRadiusBetweenLanterns, maxRadiusForEdgeLanterns);
            return minimumRadius;
        }

        private static void Main1()
        {
            int streetLength = int.Parse(Console.ReadLine().Split(new[] {' '}).Skip(1).Take(1).Single());
            //var sequence = new int[n];
            var lanternPositions = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();

            var radius = CalculateMinimumLightEmittingRadius(streetLength, lanternPositions);
            
            Console.WriteLine(string.Format("{0:F9}", radius));
        }
    }
}