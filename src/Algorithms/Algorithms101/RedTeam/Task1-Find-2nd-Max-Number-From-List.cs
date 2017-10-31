using System;
using System.Collections.Generic;
using Xunit;

namespace Algorithms101.RedTeam
{
    [Collection("RedTeam")]
    public class Task1FindSecondMaxNumberFromList
    {
        [Fact]
        [Trait("Task 1", "Test input arguments")]
        public void CheckInputParameters()
        {
            Assert.Throws<ArgumentNullException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(null));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(Array.Empty<int>()));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(new int[3]));
            Assert.Throws<ArgumentException>(() => FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(new int[10_001]));
        }

        private int FindSecondMaxNumberThatIsNotCloserToMaxValueThanThreePositions(int[] numbers)
        {
            if (numbers == null) throw new ArgumentNullException(nameof(numbers));
            if (numbers.Length < 5 || numbers.Length > 10_000) throw new ArgumentException("Provided array should have values ranging from 5 to 10,000 items", nameof(numbers));

            int maxIndex1 = 0, maxIndex2;

            for (int index = 1; index < numbers.Length; index++)
            {
                if (numbers[index] > numbers[maxIndex1])
                {
                    maxIndex1 = index;
                }

            }


            return 0;
        }
    }
}