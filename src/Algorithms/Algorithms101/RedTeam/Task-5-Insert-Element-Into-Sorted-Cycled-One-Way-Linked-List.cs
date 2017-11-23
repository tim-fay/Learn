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
                    node = Next;
                } while (node != start || node != None);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}