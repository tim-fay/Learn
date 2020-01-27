using System;

namespace CorpAlgoCourse.Spock
{
    
    /// <summary>
    /// Merge K sorted arrays into one array; K=[1..100]  
    /// </summary>
    public class MergeSortedArrays
    {
        private static int[] Merge(int[][] arraysToMerge)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}