using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class Queue
    {
        private int _count;
        private NodeList _last;
        private NodeList _top;
        //private NodeList _currentItem;

        public void Enqueue(string data)
        {
            NodeList node = new NodeList() { Data = data };
            _count++;
            if (_last == null)
            {
                _last = _top = node;
                return;
            }
            _last.Next = node;
            _last = node;
        }

        public string Dequeue()
        {
            if (_top != null)
            {
                NodeList result = _top;
                _top = _top.Next;
                _count--;
                return result.Data;
            }
            throw (new InvalidOperationException("The queue is empty"));
        }

        public int Count => _count;
        public void Clear()
        {
            _top = null;
            _last = null;
            _count = 0;
        }
    }
}
