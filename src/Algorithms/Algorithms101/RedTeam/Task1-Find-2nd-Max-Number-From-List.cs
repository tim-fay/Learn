using System;
using System.Collections.Generic;
using Xunit;

namespace Algorithms101.RedTeam
{
    [Collection("RedTeam")]
    public class Task1FindSecondMaxNumberFromList
    {
        private const int DefaultShift = 1;
        private const int FormerSufficientMaxElementsCount = DefaultShift * 2 + 1;
        private const int Min = 5;
        private const int Max = 10_000;


        [Fact]
        [Trait("Task 1", "Test input arguments")]
        public void CheckInputParameters()
        {
            Assert.Throws<ArgumentNullException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(null, DefaultShift));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(Array.Empty<int>(), DefaultShift));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(new int[3], DefaultShift));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(new int[10_001], DefaultShift));
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

            int formerSufficientMaxElementsCount = shift * 2 + 1;
            int maxIndex = 0;
            Queue<int> formerMaxIndices = new Queue<int>(formerSufficientMaxElementsCount);

            // Looking for max values, collecting all former max values that are sufficient
            for (int index = 1; index < numbers.Length; index++)
            {
                if (numbers[index] > numbers[maxIndex])
                {
                    if (formerMaxIndices.Count == formerSufficientMaxElementsCount)
                    {
                        formerMaxIndices.Dequeue();
                    }
                    formerMaxIndices.Enqueue(maxIndex);
                    maxIndex = index;
                }
            }

            while (formerMaxIndices.TryDequeue(out int formerMaxIndex))
            {
                if (Math.Abs(maxIndex - formerMaxIndex) > shift)
                {
                    return numbers[formerMaxIndex];
                }
            }

            return 0;
        }
    }
}