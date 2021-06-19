using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class Stack
    {
        private NodeList _top;
        private int _count;
        private NodeList _currentItem;

        public void Push(string data)
        {
            var node = new NodeList
            {
                Data = data,
                Next = _top
            };
            _top = node;
            _count++;
        }

        public string Peek()
        {
            if (_top != null)
                return _top.Data;
            throw (new InvalidOperationException("The stack is empty"));
        }

        public string Pop()
        {
            if (_top == null)
                throw (new InvalidOperationException("The stack is empty"));
            var result = _top;
            _top = _top.Next;
            _count--;
            return result.Data;
        }

        public int Count => _count;

        public void Clear()
        {
            _top = null;
            _count = 0;
        }
    }
}
