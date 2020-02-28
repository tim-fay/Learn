using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Shower Line
    /// https://codeforces.com/gym/269631/problem/C
    /// </summary>
    [Trait("Category", "Corporate Algorithmic Course: Spock, HW 1.2")]
    public class ShowerLine
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(int[,] hapiness, int expectedMaxHapiness)
        {
            var result = CalculateMaxTotalHappinessValue(hapiness);
            Assert.Equal(expectedMaxHapiness, result);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<int[,], int>
        {
            { new[,] { { 0, 0, 0, 0, 9 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 7, 0, 0, 0, 0 } }, 32 },
            { new[,] { { 0, 43, 21, 18, 2 }, { 3, 0, 21, 11, 65 }, { 5, 2, 0, 1, 4 }, { 54, 62, 12, 0, 99 }, { 87, 64, 81, 33, 0 } }, 620 }
        };

        private static int CalculateMaxTotalHappinessValue(int[,] happinessValues)
        {
            var permutations = GetPermutations(new[] { 0, 1, 2, 3, 4 });

            int maxHappiness = -1;

            foreach (var students in permutations)
            {
                int tempHappiness;
                
                // 01234
                tempHappiness = happinessValues[students[0], students[1]] + happinessValues[students[1], students[0]];
                tempHappiness += happinessValues[students[2], students[3]] + happinessValues[students[3], students[2]];

                // 1234
                tempHappiness += happinessValues[students[1], students[2]] + happinessValues[students[2], students[1]];
                tempHappiness += happinessValues[students[3], students[4]] + happinessValues[students[4], students[3]];

                // 234
                tempHappiness += happinessValues[students[2], students[3]] + happinessValues[students[3], students[2]];

                // 34
                tempHappiness += happinessValues[students[3], students[4]] + happinessValues[students[4], students[3]];

                if (tempHappiness > maxHappiness)
                {
                    maxHappiness = tempHappiness;
                }
            }

            return maxHappiness;
        }

        private static IEnumerable<T[]> GetPermutations<T>(T[] values)
        {
            if (values.Length == 1)
                return new[] { values };

            return values.SelectMany(v => GetPermutations(values.Except(new[] { v }).ToArray()),
                (v, p) => new[] { v }.Concat(p).ToArray());
        }

        private static void Main1()
        {
            const int n = 5;
            int[,] happiness = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                var row = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse((string) str)).ToArray();
                for (int j = 0; j < n; j++)
                {
                    happiness[i, j] = row[j];
                }
            }

            var result = CalculateMaxTotalHappinessValue(happiness);
            Console.WriteLine(result);
        }
    }
}