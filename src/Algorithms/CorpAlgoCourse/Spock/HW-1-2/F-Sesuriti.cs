using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Order Book
    /// https://codeforces.com/gym/269631/problem/F
    /// </summary>
    [Trait("Category", "Corporate Algorithmic Course: Spock, HW 1.2")]
    public class Sesuriti
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(int[,] region, int expectedMinerals)
        {
            var result = CalculateMaxMinerals(region);
            Assert.Equal(expectedMinerals, result);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<int[,], int>
        {
            { new[,] { { 0, 1 }, { 0, 0 } }, 1 },
            { new[,] { { 1 } }, 1 },
            { new[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 0, 1 } }, 3 }
        };
        
        private static int CalculateMaxMinerals(int[,] region)
        {
            int n = region.GetLength(0);
            int[,] minerals = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int fromLeft = i - 1 >= 0 ? minerals[i - 1, j] : 0;
                    int fromTop = j - 1 >= 0 ? minerals[i, j - 1] : 0;

                    minerals[i, j] = Math.Max(fromLeft, fromTop) + region[i, j];
                }
            }

            return minerals[n - 1, n - 1];
        }

        private static void Main1()
        {
            int n = int.Parse(Console.ReadLine());
            int[,] region = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                var row = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse((string) str)).ToArray();

                for (int j = 0; j < n; j++)
                {
                    region[i, j] = row[j];
                }
            }

            var result = CalculateMaxMinerals(region);
            
            Console.WriteLine(result);
        }
    }
}