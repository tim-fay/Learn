using System;
using System.Collections.Generic;
using Xunit;

namespace UniLecs
{
    [Collection("UniLecs")]
    public class Task2MinElemInShiftedSortedArray
    {
        [Fact]
        [Trait("Task 2", "Test input arguments")]
        public void CheckCornerCaseArguments()
        {
            Assert.Throws<ArgumentNullException>(() => FindMinElementInShiftedSortedArray(null));
            Assert.Throws<ArgumentException>(() => FindMinElementInShiftedSortedArray(Array.Empty<int>()));
        }

        public static IEnumerable<object[]> TestInputArray1() => new TheoryData<int[], int> { { new[] { 3, 4, 5, 6, 7, 8, 1, 2 }, 1 } };
        public static IEnumerable<object[]> TestInputArray2() => new TheoryData<int[], int> { { new[] { 2, 3, 4, 5, 6, 7, 8, 1 }, 1 } };
        public static IEnumerable<object[]> TestInputArray3() => new TheoryData<int[], int> { { new[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 1 } };
        public static IEnumerable<object[]> TestInputArray4() => new TheoryData<int[], int> { { new[] { 1, 2 }, 1 } };
        public static IEnumerable<object[]> TestInputArray5() => new TheoryData<int[], int> { { new[] { 42 }, 42 } };
        public static IEnumerable<object[]> TestInputArray6() => new TheoryData<int[], int> { { new[] { 3, 4, 5, 6, 7, 8, 9, 10, 2 }, 2 } };

        [Theory]
        [Trait("Task 2", "Test on regular array")]
        [MemberData(nameof(TestInputArray1))]
        [MemberData(nameof(TestInputArray2))]
        [MemberData(nameof(TestInputArray3))]
        [MemberData(nameof(TestInputArray4))]
        [MemberData(nameof(TestInputArray5))]
        [MemberData(nameof(TestInputArray6))]
        public void CheckOnRegularInput(int[] input, int expected)
        {
            Assert.Equal(expected, FindMinElementInShiftedSortedArray(input));
        }

        private int FindMinElementInShiftedSortedArray(int[] array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (array.Length == 0) throw new ArgumentException();

            int start = 0;
            int end = array.Length - 1;

            // Check if array sorted without shift
            if (array.Length == 1 || array[start] < array[end])
            {
                return array[start];
            }

            while (end - start > 1)
            {
                int middle = (start + end) / 2;

                if (array[start] < array[middle])
                {
                    start = middle;
                }

                if (array[middle] < array[end])
                {
                    end = middle;
                }
            }

            return array[start] < array[end] ? array[start] : array[end];
        }
    }
}