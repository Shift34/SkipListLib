using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipListLib
{
    public class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Right,
                            Up,
                            Down;
        public Node()
        { }
        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Right = null;
            Up = null;
            Down = null;
        }
    }
    public class SkipList<TKey, TValue> :
        IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        Node<TKey, TValue>[] _head;
        readonly double _probability;
        readonly int _maxLevel;
        int _curLevel;
        Random _rd;
        public int Count { get; private set; }
        public SkipList(int maxLevel = 10, double p = 0.5)
        {
            _maxLevel = maxLevel;
            _probability = p;
            _head = new Node<TKey, TValue>[_maxLevel];
            for (int i = 0; i < maxLevel; i++)
            {
                _head[i] = new Node<TKey, TValue>();
                if (i == 0) continue;
                _head[i - 1].Up = _head[i];
                _head[i].Down = _head[i - 1];
            }

            _curLevel = 0;
            _rd = new Random(DateTime.Now.Millisecond);
        }
        public TValue this[TKey key]
        {
            get
            {
                var pair = Find(key);
                if (pair != null)
                    return pair.Value;
                throw new KeyNotFoundException();
            }
            set
            {
                var pair = Find(key) ?? throw new KeyNotFoundException();
                pair.Value = value;
            }
        }
        public void Add(TKey key, TValue value)
        {
            var prevNode = new Node<TKey, TValue>[_maxLevel];
            var currentNode = _head[_curLevel];
            for (int i = _curLevel; i >= 0; i--)
            {
                while (currentNode.Right != null &&
                    currentNode.Right.Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Right;
                }
                prevNode[i] = currentNode;
                if (currentNode.Down == null)
                    break;
                currentNode = currentNode.Down;
            }
            int level = 0;
            while (_rd.NextDouble() < _probability && level < _maxLevel - 1)
            {
                level++;
            }
            while (_curLevel < level)
            {
                _curLevel++;
                prevNode[_curLevel] = _head[_curLevel];
            }
            for (int i = 0; i <= level; i++)
            {
                var node = new Node<TKey, TValue>(key, value) { Right = prevNode[i].Right };
                prevNode[i].Right = node;
                if (i == 0) continue;
                node.Down = prevNode[i - 1].Right;
                prevNode[i - 1].Right.Up = node;
            }
            Count++;
        }

        public bool Contains(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException();
            return Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException();
            var currentNode = _head[_curLevel];
            for (int i = _curLevel; i >= 0; i--)
            {
                while (currentNode.Right != null &&
                    currentNode.Right.Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Right;
                }
                if (currentNode.Right != null &&
                    currentNode.Right.Key.CompareTo(key) == 0)
                {
                    break;
                }
                if (currentNode.Down == null)
                {
                    return false;
                }
                currentNode = currentNode.Down;
            }
            currentNode.Right = currentNode.Right.Right;
            currentNode = currentNode.Down;
            while (currentNode != null)
            {
                if (currentNode.Right != null &&
                    currentNode.Right.Up != null)
                {
                    currentNode.Right = currentNode.Right.Right;
                    currentNode = currentNode.Down;
                }
                else currentNode = currentNode.Right;
            }
            Count--;
            return true;
        }
        public Node<TKey,TValue> Find(TKey key)
        {
            var currentNode = _head[_curLevel];
            for (int i = _curLevel; i >= 0; i--)
            {
                while (currentNode.Right != null &&
                    currentNode.Right.Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Right;
                }
                if (currentNode.Right != null &&
                    currentNode.Right.Key.CompareTo(key) == 0)
                {
                    return currentNode.Right;
                }
                if (currentNode.Down == null)
                    break;
                currentNode = currentNode.Down;
            }
            return null;
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (var node = _head[0].Right; node != null; node = node.Right)
            {
                yield return new KeyValuePair<TKey, TValue>(node.Key, node.Value);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
