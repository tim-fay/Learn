using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse
{
    [Trait("Category", "Corporate Algorithmic Course")]
    public class Fence
    {
        [Theory]
        [MemberData(nameof(InputData1))]
        public void Test1(int[] fences, int piano, int expected)
        {
            var result = Solve(fences, piano);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> InputData1 => new TheoryData<int[], int, int>
        {
            { new[] { 1, 2, 6, 1, 1, 7, 1 }, 3, 3 },
        };

        private static int Solve(int[] fences, int piano)
        {
            return 0;
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