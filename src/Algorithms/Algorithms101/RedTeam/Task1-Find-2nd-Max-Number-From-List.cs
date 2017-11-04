using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithms101.RedTeam
{
    [Collection("RedTeam")]
    public class Task1FindSecondMaxNumberFromList
    {
        private const int DefaultShift = 1;
        private const int Min = 5;
        private const int Max = 10_000;

        [Fact]
        [Trait("Task 1", "Test input arguments")]
        public void CheckInputParameters()
        {
            Assert.Throws<ArgumentNullException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(null, DefaultShift));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(Array.Empty<int>(), DefaultShift));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(new int[Min - 1], DefaultShift));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(new int[Max + 1], DefaultShift));
        }

        public static IEnumerable<object[]> TestInputArray1() => new TheoryData<int[], int, int> { { new[] { 1, 2, 3, 4, 5 }, DefaultShift, 3 } };
        public static IEnumerable<object[]> TestInputArray2() => new TheoryData<int[], int, int> { { new[] { 6, 1, 2, 3, 4, 5 }, DefaultShift, 5 } };
        public static IEnumerable<object[]> TestInputArray3() => new TheoryData<int[], int, int> { { new[] { 1, 3, 5, 7, 9, 8, 6, 4, 2 }, DefaultShift, 6 } };

        [Theory]
        [Trait("Task 1", "Test on regular arrays")]
        [MemberData(nameof(TestInputArray1))]
        [MemberData(nameof(TestInputArray2))]
        [MemberData(nameof(TestInputArray3))]
        public void TestNormalCases(int[] input, int shift, int expected)
        {
            Assert.Equal(expected, FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(input, shift));
        }


        private int FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(int[] numbers, int shift)
        {
            if (numbers == null) throw new ArgumentNullException(nameof(numbers));
            if (numbers.Length < Min || numbers.Length > Max) throw new ArgumentException("Provided array should have values ranging from 5 to 10,000 items", nameof(numbers));

            int maxIndex = 0;

            // Search for the max element
            for (int index = 1; index < numbers.Length; index++)
            {
                if (numbers[index] > numbers[maxIndex])
                {
                    maxIndex = index;
                }
            }

            // Take sub-arrays around top max element honoring shift value around
            var first = numbers.Take(maxIndex - shift);
            var second = numbers.Skip(maxIndex + 1 + shift);
            // Find max inside combined collection
            var result = first.Concat(second).Max();

            return result;
        }
    }
}