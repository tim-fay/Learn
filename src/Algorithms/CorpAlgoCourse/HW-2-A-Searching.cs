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
            { new[] { 3, 3, 3, 3, 3, 3, 3, 3, 4 }, new[] { 3, 4, 5 }, new[] { 0, 8, 9 } },
            { new[] { 1000000000, 1000000000 }, new[] { 1000000000 }, new[] { 0 } },
            { new[] { 1000000000, 999999999 }, new[] { 999999999, 1000000000 }, new[] { 0, 1 } },
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
                
                if (sequence.Length == 1)
                {
                    result[i] = sequence[0] < query ? 1 : 0;
                }

                var index = BinarySearchEx(query, sequence);
                if (index < 0)
                {
                    result[i] = ~index;
                }
                else
                {
                    result[i] = index;
                }

            }

            return result;
        }

        private static int BinarySearchEx(int target, int[] sequence, bool firstOccurence = true)
        {
            if (sequence.Length == 0)
            {
                return -1;
            }
            if (target < sequence[0])
            {
                return -1;
            }
            if (sequence[sequence.Length - 1] < target)
            {
                return ~sequence.Length;
            }
            
            int low = 0;
            int high = sequence.Length - 1;
            int resultIndex = -1;
            int mid = 0;

            while (low <= high)
            {
                mid = (low + high) / 2;
                if (sequence[mid] == target)
                {
                    resultIndex = mid;
                    if (firstOccurence)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }
                else if (target < sequence[mid])
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            if (resultIndex == -1)
            {
                return ~mid;
            }
            
            return resultIndex;
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
            Console.Out.WriteLine();
        }
    }
}