using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Merge K sorted arrays into one array; K=[1..100]  
    /// </summary>
    public class MergeSortedArrays
    {
        [Theory]
        [MemberData(nameof(InputData1))]
        public void Test1(long[][] sortedArrays, int arrayCount, long[] expected)
        {
            var result = Merge(sortedArrays, arrayCount);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestOnBigRandomDataRange()
        {
            var (arrays, arrayCount, expected) = InputRandomData();

            var result = Merge(arrays, arrayCount);
            
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> InputData1 => new TheoryData<long[][], int, long[]>
        {
            {
                new long[][]
                {
                    new long[] { 1, 3, 5, 7, 9 },
                    new long[] { 0, 2, 4, 6 },
                    new long[] { 11, 22 }
                },
                3,
                new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 9, 11, 22 }
            },
        };

        private static (long[][] Arrays, int ArrayCount, long[] Expected) InputRandomData()
        {
            var random = new Random(42);

            const int maxArrays = 100;
            long[][] arrays = new long[maxArrays][];

            for (int i = 0; i < maxArrays; i++)
            {
                var currentArrayLength = random.Next(100, 100000);
                arrays[i] = new long[currentArrayLength];
                for (int j = 0; j < currentArrayLength; j++)
                {
                    arrays[i][j] = random.Next();
                }
                Array.Sort(arrays[i]);
            }

            var expectedSortedSet = new List<long>();
            for (int i = 0; i < maxArrays; i++)
            {
                expectedSortedSet.AddRange(arrays[i]);
            }
            expectedSortedSet.Sort();

            return (arrays, maxArrays, expectedSortedSet.ToArray());
        }

        private static long[] Merge(long[][] arraysToMerge, int arrayCount)
        {
            var minElements = new SortedDictionary<long, (int ArrayNumber, int Index)>();

            for (var arrayNumber = 0; arrayNumber < arrayCount; arrayNumber++)
            {
                minElements.Add(arraysToMerge[arrayNumber][0], (arrayNumber, 0));
            }

            var resultLength = 0;
            Array.ForEach(arraysToMerge, array => resultLength += array.Length);
            var resultArray = new long[resultLength];
            int insertingIndex = 0;

            while (minElements.Count > 0)
            {
                var minElement = minElements.Keys.First();
                resultArray[insertingIndex] = minElement;
                insertingIndex++;
                minElements.Remove(minElement, out var value);
                if (value.Index < arraysToMerge[value.ArrayNumber].Length - 1)
                {
                    var nextArrayMinElement = arraysToMerge[value.ArrayNumber][value.Index + 1];
                    minElements.Add(nextArrayMinElement, (value.ArrayNumber, value.Index + 1));
                }
            }

            return resultArray;
        }
    }

    public class MinHeap
    {
        private readonly int _capacity;
        private int _size;
        private readonly int[] _heapArray;

        public MinHeap(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException(nameof(capacity));
            }

            _capacity = capacity;
            _heapArray = new int[_capacity];
        }

        public static MinHeap CreateFromArray(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException(nameof(array));
            }

            var heap = new MinHeap(array.Length);

            foreach (var t in array)
            {
                heap.Insert(t);
            }

            return heap;
        }


        public static MinHeap CreateFromSingleItem(int item, int capacity)
        {
            var heap = new MinHeap(capacity);
            heap.Insert(item);
            return heap;
        }

        public void Insert(int item)
        {
            if (_size == _capacity)
            {
                throw new InvalidOperationException("Heap is full");
            }

            _size++;
            int index = _size;
            _heapArray[index] = item;
            BubbleUp(index);
        }

        private void BubbleUp(int index)
        {
            if (index == 0)
            {
            }
        }
    }
}