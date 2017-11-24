using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithms101.RedTeam
{
    [Trait("Category", "Red Team")]
    public class Task5InsertElementIntoSortedCycledOneWayLinkedList
    {
        [Theory]
        [InlineData(42, 41)]
        [InlineData(42, 43)]
        public void TestSingleNodeList(int listItem, int insertedValue)
        {
            var singleNodeList = new Node<int>(listItem);
            singleNodeList.AddNext(singleNodeList);
            InsertNodeIntoSortedLinkedList(singleNodeList, insertedValue);

            var expected = new[] { listItem, insertedValue };
            var actual = singleNodeList.ToArray();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new[] { 1, 2, 3, 4, 6, 7, 8 }, 5, new[] { 1, 2, 3, 4, 5, 6, 7, 8 })]
        public void TestMultipleNodeList(int[] inputItems, int insertedValue, int[] expected)
        {
            var root = new Node<int>(inputItems[0]);
            var previousNode = root;

            for (int i = 1; i < inputItems.Length; i++)
            {
                var node = new Node<int>(inputItems[i]);
                previousNode.AddNext(node);
                previousNode = node;
            }
            // Make a cycle
            previousNode.AddNext(root);

            InsertNodeIntoSortedLinkedList(root, insertedValue);

            var actual = root.ToArray();
            Assert.Equal(expected, actual);
        }

        private void InsertNodeIntoSortedLinkedList<T>(Node<T> nodeWithMinimalValue, T valueToInsert) where T : IComparable<T>
        {
            if (nodeWithMinimalValue.Next == Node<T>.None || nodeWithMinimalValue == nodeWithMinimalValue.Next)
            {
                nodeWithMinimalValue.AddNext(valueToInsert);
                return;
            }

            var currentNode = nodeWithMinimalValue;
            while (true)
            {
                if (currentNode.Next.Value.CompareTo(valueToInsert) >= 0)
                {
                    currentNode.AddNext(valueToInsert);
                    return;
                }
                currentNode = currentNode.Next;
            }

        }

        private class Node<T> : IEnumerable<T>
        {
            public static readonly Node<T> None = new Node<T>(default);

            public T Value { get; }
            public Node<T> Next { get; private set; }

            public Node(T value)
            {
                Value = value;
                Next = None;
            }

            public void AddNext(Node<T> node)
            {
                if (Next == None || node == this)
                {
                    Next = node;
                }
                else
                {
                    var tempNode = Next;
                    Next = node;
                    node.Next = tempNode;
                }
            }

            public void AddNext(T value)
            {
                AddNext(new Node<T>(value));
            }

            public IEnumerator<T> GetEnumerator()
            {
                var start = this;
                var node = start;
                do
                {
                    yield return node.Value;
                    node = node.Next;
                } while (node != start && node != None);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}