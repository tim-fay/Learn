using System;
using System.Collections.Generic;

namespace LinkedLists
{
    public class StackWithLinkedList
    {
        public void Run()
        {
            var linkedListStack = new LinkedListStack<int>();

            linkedListStack.Push(0);
            linkedListStack.Push(1);
            linkedListStack.Push(2);
            linkedListStack.Push(3);
            linkedListStack.Push(4);

            Console.WriteLine(linkedListStack.Pop());
            Console.WriteLine(linkedListStack.Pop());
            Console.WriteLine(linkedListStack.Pop());
            Console.WriteLine(linkedListStack.Pop());
            Console.WriteLine(linkedListStack.Pop());
        }

        class LinkedListStack<T>
        {
            private readonly LinkedList<T> _list = new LinkedList<T>();

            public void Push(T item)
            {
                _list.AddFirst(item);
            }

            public T Pop()
            {
                if (_list.Count == 0)
                {
                    throw new InvalidOperationException();
                }

                var result = _list.First.Value;
                _list.RemoveFirst();
                return result;
            }
        }
    }
}