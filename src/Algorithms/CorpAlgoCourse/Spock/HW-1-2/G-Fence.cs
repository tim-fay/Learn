using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Fence
    /// https://codeforces.com/gym/269631/problem/G
    /// </summary>
    [Trait("Category", "Corporate Algorithmic Course: Spock, HW 1.2")]
    public class Fence
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(int[] fences, int piano, int expected)
        {
            var result = Solve(fences, piano);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<int[], int, int>
        {
            { new[] { 1, 2, 6, 1, 1, 7, 1 }, 3, 3 },
            { new[] { 3, 1, 4, 1, 4, 6, 2, 1, 4, 6 }, 2, 7 },
            { new[] { 1, 2, 6, 1, 1, 7, 1 }, 3, 3 },
            { new[] { 1, 1, 1 }, 3, 1 },
            { new[] { 20, 1 }, 1, 2 },
        };

        private static int Solve(int[] fences, int piano)
        {
            if (fences.Length == piano)
            {
                return 1;
            }

            int currentFences = 0;
            int minimumFences = 0;

            for (int i = 0; i < piano; i++)
            {
                currentFences += fences[i];
            }

            minimumFences = currentFences;
            int minimumIndex = 0;

            for (int i = 1; i < fences.Length - piano + 1; i++)
            {
                currentFences = currentFences - fences[i - 1] + fences[i + piano - 1];
                if (currentFences < minimumFences)
                {
                    minimumFences = currentFences;
                    minimumIndex = i;
                }
            }
            
            return minimumIndex + 1;
        }

        private static void Main1()
        {
            int piano = int.Parse(Console.ReadLine().Split(new[] {' '}).Skip(1).Take(1).Single());
            //var sequence = new int[n];
            var fences = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();

            var result = Solve(fences, piano);

            Console.WriteLine(result);
        }
    }
}