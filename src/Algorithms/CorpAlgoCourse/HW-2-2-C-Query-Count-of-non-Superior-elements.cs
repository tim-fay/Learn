using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse
{
    [Trait("Category", "Corporate Algorithmic Course")]
    public class QueryCountOfNonSuperiorElements
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
            { new[] { 1, 3, 5, 7, 9 }, new[] { 6, 4, 2, 8 }, new[] { 3, 2, 1, 4 } },
            { new[] { 1, 2, 1, 2, 5 }, new[] { 3, 1, 4, 1, 5 }, new[] { 4, 2, 4, 2, 5 } },
            { new[] { -1, -2, -1, 2, 5 }, new[] { 3, 1, 4, 1, 5 }, new[] { 4, 3, 4, 3, 5 } },
        };

        private static int[] Solve(int[] sequence, int[] queries)
        {
            Array.Sort(sequence);
            var result = new int[queries.Length];

            for (int i = 0; i < queries.Length; i++)
            {
                var query = queries[i];

                if (sequence.Length == 1)
                {
                    result[i] = sequence[0] <= query ? 1 : 0;
                }

                var index = BinarySearchEx(query, sequence, false);
                if (index < 0)
                {
                    result[i] = ~index;
                }
                else
                {
                    result[i] = index + 1;
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

            while (low <= high)
            {
                var mid = low + (high - low >> 1);
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
                return ~low;
            }
            
            return resultIndex;
        }

        private static void Main1()
        {
            var nM = Console.ReadLine(); // Do nothing with n & m
            //int n = int.Parse(Console.ReadLine());
            //var sequence = new int[n];
            var sequence = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();
            var queries = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();

            var result = Solve(sequence, queries);

            var output = string.Join(" ", result.Select(val => val.ToString()));
            Console.WriteLine(output);
        }
    }
}