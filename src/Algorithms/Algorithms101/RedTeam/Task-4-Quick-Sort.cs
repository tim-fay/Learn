using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithms101.RedTeam
{
    [Trait("Category", "Red Team")]
    public class Task4QuickSort
    {
        public static IEnumerable<object[]> TestInputArray1() => new TheoryData<int[]> { new[] { 9, 3, 4, 5, 6, 7, 8, 1, 2, 0 } };
        public static IEnumerable<object[]> TestInputArray2() => new TheoryData<int[]> { new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 } };
        public static IEnumerable<object[]> TestInputArray3() => new TheoryData<int[]> { new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 } };
        public static IEnumerable<object[]> TestInputArray4() => new TheoryData<int[]> { new[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 } };
        public static IEnumerable<object[]> TestInputArray5() => new TheoryData<int[]> { new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
        public static IEnumerable<object[]> TestInputArray6() => new TheoryData<int[]> { new[] { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 } };

        [Theory]
        [MemberData(nameof(TestInputArray1))]
        [MemberData(nameof(TestInputArray2))]
        [MemberData(nameof(TestInputArray3))]
        [MemberData(nameof(TestInputArray4))]
        [MemberData(nameof(TestInputArray5))]
        [MemberData(nameof(TestInputArray6))]
        public void SortTest(int[] input)
        {
            var expected = input.ToArray();
            Array.Sort(expected);

            QuickSort(input);

            Assert.Equal(expected, input);

        }

        private void QuickSort<T>(T[] array) where T : IComparable<T>
        {
            QuickSort(array, array.GetLowerBound(0), array.GetUpperBound(0));
        }

        private void QuickSort<T>(T[] array, int startIndex, int endIndex) where T : IComparable<T>
        {
            if (startIndex >= endIndex) return;

            var partitionIndex = Partition(array, startIndex, endIndex);
            QuickSort(array, startIndex, partitionIndex);
            QuickSort(array, partitionIndex + 1, endIndex);
        }

        private int Partition<T>(T[] array, int start, int end) where T : IComparable<T>
        {
            T pivotValue = array[start];

            int left = start - 1;
            int right = end + 1;

            while (true)
            {

                do
                {
                    left++;
                } while (array[left].CompareTo(pivotValue) < 0);

                do
                {
                    right--;
                } while (array[right].CompareTo(pivotValue) > 0);

                if (left < right)
                {
                    var temp = array[left];
                    array[left] = array[right];
                    array[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}