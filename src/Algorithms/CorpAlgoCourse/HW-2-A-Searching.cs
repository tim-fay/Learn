using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse
{
    [Trait("Category", "Corporate Algorithmic Course")]
    public class Searching
    {
        [Theory]
        [MemberData(nameof(InputData1))]
        public void Test1(int[] sequence, int[] queries, int[] expected)
        {
            var result = Solve(sequence, queries);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> InputData1 => new TheoryData<int[], int[], int[]>
        {
            { new[] { 7, 4, 1, 3, 9, 2, 4, 5, 9, 8 }, new[] { 6, 1, 9, 4 }, new[] { 6, 0, 8, 3 } },
            { new[] { 7 }, new[] { 6 }, new[] { 0 } },
            { new[] { 5 }, new[] { 7 }, new[] { 1 } },
            { new[] { 1 }, new[] { 1 }, new[] { 0 } },
        };

        private static int[] Solve(int[] sequence, int[] queries)
        {
            Array.Sort(sequence);
            var result = new int[queries.Length];

            for (int i = 0; i < queries.Length; i++)
            {
                var query = queries[i];

                if (query == 1)
                {
                    result[i] = 0;
                    continue;
                }

                result[i] = CountNumberOfElementsLessThanNumber(query, sequence);
            }

            return result;
        }

        private static int CountNumberOfElementsLessThanNumber(int number, int[] sequence)
        {
            var count = 0;
            while (count < sequence.Length && sequence[count] < number)
            {
                count++;
            }

            return count;
        }

        private static void Main1()
        {
            int n = int.Parse(Console.ReadLine());
            //var sequence = new int[n];
            var sequence = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();
            int m = int.Parse(Console.ReadLine());
            var queries = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();

            var result = Solve(sequence, queries);

            var output = string.Join(" ", result.Select(val => val.ToString()));
            Console.WriteLine(output);
        }
    }
}