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
        [MemberData(nameof(InputData))]
        public void Test(int streetLength, int[] lanternPositions, double expectedMinimumRadius)
        {
            var radius = CalculateMinimumLightEmittingRadius(streetLength, lanternPositions);
            Assert.Equal(expectedMinimumRadius, radius);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<int, int[], double>
        {
            { 10, new[] { 5 }, 5f },
            { 10, new[] { 4, 4, 2, 2, 6, 6, 6 }, 4f },
            { 1, new[] { 0 }, 1f },
            { 1, new[] { 0, 1 }, 0.5f },
            { 1, new[] { 1, 1, 1, 1 }, 1f },
            { 100, new[] { 0, 0, 0 }, 100f },
            { 1000000000, new[] { 0, 1000000000 }, 500000000 },
            { 1000000000, new[] { 0, 1 }, 999999999 },
            { 5, new[] { 2, 5 }, 2f },
            { 5, new[] { 2, 3 }, 2f },
            { 5, new[] { 1, 4 }, 1.5f },
            { 15, new[] { 15, 5, 3, 7, 9, 14, 0 }, 2.5 },
            { 615683844, new[] { 431749087, 271781274, 274974690, 324606253, 480870261, 401650581, 13285442, 478090364, 266585394, 425024433, 588791449, 492057200, 391293435, 563090494, 317950, 173675329, 473068378, 356306865, 311731938, 192959832, 321180686, 141984626, 578985584, 512026637, 175885185, 590844074, 47103801, 212211134, 330150, 509886963, 565955809, 315640375, 612907074, 500474373, 524310737, 568681652, 315339618, 478782781, 518873818, 271322031, 74600969, 539099112, 85129347, 222068995, 106014720, 77282307 }, 22258199.5000000000}
        };
        
        
        /// <summary>
        /// Solving the algorithm by sorting lantern positions and calculating maximum distance between 2 adjacent lanterns. 
        /// </summary>
        private static double CalculateMinimumLightEmittingRadius(int streetLength, int[] lanternPositions)
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
            
            var maxRadiusForEdgeLanterns = (double)Math.Max(lanternPositions[0], streetLength - lanternPositions[lanternPositions.Length - 1]);
            var maxRadiusBetweenLanterns = (double)maximumDistanceBetweenAdjacentLanterns / 2;

            var minimumRadius = Math.Max(maxRadiusBetweenLanterns, maxRadiusForEdgeLanterns);
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