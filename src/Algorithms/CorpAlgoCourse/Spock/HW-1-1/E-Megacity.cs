using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: MegaCity
    /// http://codeforces.com/gym/268160/problem/E
    /// </summary>
    [Trait("Category", "Spock: Corporate Algorithmic Course: HW 1.1")]
    public class MegaCity
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(int tomskPopulation, Town[] nearbyTowns, float expectedExpansionRadius)
        {
            var radius = CalculateMinimumRadiusToExpandForBecomingMegaCity(tomskPopulation, nearbyTowns);
            double epsilon = 0.0000001;
            Assert.InRange(radius, expectedExpansionRadius - epsilon, expectedExpansionRadius + epsilon);
            
        }

        public static IEnumerable<object[]> InputData => new TheoryData<int, Town[], double>
        {
            {
                999998,
                new[]
                {
                    new Town(1, 1, 1),
                    new Town(2, 2, 1),
                    new Town(3, 3, 1),
                    new Town(2, -2, 1)
                },
                2.8284271
            },
            {
                999998,
                new[]
                {
                    new Town(1, 1, 2),
                    new Town(2, 2, 1),
                    new Town(3, 3, 1),
                    new Town(2, -2, 1)
                },
                1.4142136
            },
            {
                1,
                new[]
                {
                    new Town(1, 1, 999997),
                    new Town(2, 2, 1),
                },
                -1
            },
        };


        private static double CalculateMinimumRadiusToExpandForBecomingMegaCity(int tomskPopulation, Town[] nearbyTowns)
        {
            var nearbyTownsOrderedByDistance = new SortedDictionary<float, int>();

            foreach (var town in nearbyTowns)
            {
                var currentRadius = town.Position.LengthSquared();
                if (nearbyTownsOrderedByDistance.ContainsKey(currentRadius))
                {
                    nearbyTownsOrderedByDistance[currentRadius] += town.Population;
                }
                else
                {
                    nearbyTownsOrderedByDistance.Add(town.Position.LengthSquared(), town.Population);
                }
            }

            const int requiredPopulation = 1000000;
            int currentPopulation = tomskPopulation;

            foreach (var radiusCandidate in nearbyTownsOrderedByDistance)
            {
                var currentRadius = radiusCandidate.Key;
                currentPopulation += radiusCandidate.Value;

                if (currentPopulation >= requiredPopulation)
                {
                    return Math.Sqrt(currentRadius);
                }
            }

            return -1; // No required towns nearby
        }

        public struct Town
        {
            public Vector2 Position { get; }
            public int Population { get; }

            public Town(int x, int y, int population)
            {
                Position = new Vector2(x, y);
                Population = population;
            }
        }

        private static void Main1()
        {
            var n_s = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();
            var n = n_s[0];
            var s = n_s[1];

            var towns = new Town[n];
            for (int i = 0; i < n; i++)
            {
                var x_y_p = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();
                towns[i] = new Town(x_y_p[0], x_y_p[1], x_y_p[2]);
            }

            var radius = CalculateMinimumRadiusToExpandForBecomingMegaCity(s, towns);
            
            Console.WriteLine(string.Format("{0:F6}", radius));
        }
    }
}