using System;
using System.Linq;
using Xunit;

namespace Algorithms101
{
    [Trait("Category", "Interview")]
    public class ThreeNumbersSumInArrayEqualsZero
    {
        [Fact]
        public void OneCombinationShouldBeFound()
        {
            var array = new[] { 1, 2, 3, 4, -3 };
            var actual = FindThreeNumbersSumInArrayEqualsZero(array);
            var expected = new[] { 1, 2, -3 };
            Assert.Contains(actual, item => expected.Contains(item));
        }

        [Fact]
        public void NoCombinationFound()
        {
            var array = new[] { 1, 2, 3, 4, 5 };
            var actual = FindThreeNumbersSumInArrayEqualsZero(array);
            Assert.True(actual.Length == 0);
        }

        private int[] FindThreeNumbersSumInArrayEqualsZero(int[] array)
        {
            if (array.Length < 3)
            {
                throw new ArgumentException("Array has fewer number of elements than expected", nameof(array));
            }

            Array.Sort(array);

            const int reminderNumbersCount = 2;

            for (int i = 0; i < array.Length - reminderNumbersCount; i++)
            {
                var firstNumber = array[i];
                var sumToFind = -firstNumber;
                var secondAndThirdNumbers = FindTwoNumbersSumInSortedArrayEqualsSpecificNumber(array, i + 1, array.Length - 1, sumToFind);

                if (secondAndThirdNumbers != null)
                {
                    return new[] { firstNumber, secondAndThirdNumbers.Item1, secondAndThirdNumbers.Item2 };
                }
            }

            return new int[0];
        }

        private Tuple<int, int> FindTwoNumbersSumInSortedArrayEqualsSpecificNumber(int[] array, int startIndex, int endIndex, int sumToFind)
        {
            int lowIndex = startIndex;
            int hiIndex = endIndex;

            while (hiIndex - lowIndex > 0)
            {
                var sum = array[lowIndex] + array[hiIndex];
                if (sum == sumToFind)
                {
                    return Tuple.Create(array[lowIndex], array[hiIndex]);
                }
                if (sum < sumToFind)
                {
                    lowIndex++;
                }
                if (sum > sumToFind)
                {
                    hiIndex--;
                }
            }

            return null;
        }
    }
}