using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class TreeNode
    {
        public TreeNode()
        {
            Left = null;
            Right = null;
        }

        public string Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
    }


    public class Tree
    {
        TreeNode _root;
        TreeNode _currentNode;
        int _count;

        public TreeNode Root => _root;

        public void Add(string data)
        {
            var treenode = new TreeNode { Data = data };
            if (_root == null)
            {
                _root = treenode;
                _count++;
                return;
            }
            _currentNode = _root;
            while (true)
            {
                var comparison = treenode.Data.CompareTo(_currentNode.Data);
                if (comparison == 0)
                    throw new InvalidOperationException("Data already in the tree");
                if (comparison < 0 )
                {
                    if (_currentNode.Left != null)
                        _currentNode = _currentNode.Left;
                    else
                    {
                        _currentNode.Left = treenode;
                        _count++;
                        break;
                    }
                }
                else
                {
                    if (_currentNode.Right != null)
                        _currentNode = _currentNode.Right;
                    else
                    {
                        _currentNode.Right = treenode;
                        _count++;
                        break;
                    }
                }
            }
        }

        public bool Exists(string data)
        {
            if (_root == null)
                return false;
            TreeNode currentNode = _root;
            while (true)
            {
                if (currentNode.Data == data)
                    return true;
                TreeNode nodeToFollow;
                if(Convert.ToInt32(data) < Convert.ToInt32(currentNode.Data))
                {
                    nodeToFollow = currentNode.Left;
                }
                else
                {
                    nodeToFollow = currentNode.Right;
                }

                if (nodeToFollow != null)
                    currentNode = nodeToFollow;
                else
                    return false;
            }
        }

        public string TraverseTree()
        {
            if (_root == null)
                return "";
            var sb = new StringBuilder();
            TraverseNode(_root, sb);
            return sb.ToString();
        }

        private void TraverseNode(TreeNode node, StringBuilder sb)
        {
            sb.AppendLine(node.Data.ToString());
            if (node.Left != null)
                TraverseNode(node.Left, sb);
            if (node.Right != null)
                TraverseNode(node.Right, sb);
        }

        public Tuple<TreeNode, TreeNode> FindParentAndNode(string data)
        {
            TreeNode currentNode = _root;
            TreeNode parentNode = null;
            while (true)
            {
                if (currentNode.Data == data)
                    return Tuple.Create(parentNode, currentNode);
                TreeNode nodeToFollow;
                if (Convert.ToInt32(data) < Convert.ToInt32(currentNode.Data))
                {
                    nodeToFollow = currentNode.Left;
                }
                else
                {
                    nodeToFollow = currentNode.Right;
                }
                if (nodeToFollow != null)
                {
                    parentNode = currentNode;
                    currentNode = nodeToFollow;
                }
                else
                    return null;
            }
        }

        public int Count => _count;

        public bool Remove(string data)
        {
            var nodeAndParent = FindParentAndNode(data);
            if (nodeAndParent == null)
                return false;
            var parent = nodeAndParent.Item1;
            var currentNode = nodeAndParent.Item2;

            if (currentNode.Left == null || currentNode.Right == null)
            {
                var nodeToReplace = currentNode.Left == null && currentNode.Right == null
                    ? null : currentNode.Left == null ? currentNode.Right : currentNode.Left;
                if (parent == null)
                    _root = nodeToReplace;
                else if (parent.Left == currentNode)
                    parent.Left = nodeToReplace;
                else
                    parent.Right = nodeToReplace;
                _count--;
                return true;
            }

            var parentAndPredecessor = FindParentAndPredecessor(currentNode);
            var predParent = parentAndPredecessor.Item1;
            var pred = parentAndPredecessor.Item2;
            if (predParent == currentNode)
            {
                if (parent.Left == currentNode)
                {
                    parent.Left = pred;
                    pred.Right = currentNode.Right;
                }
                else
                {
                    parent.Right = pred;
                    pred.Right = currentNode.Right;
                }
            }
            else
            {
                predParent.Right = pred.Left;
                currentNode.Data = pred.Data;
            }
            return false;
        }

        private Tuple<TreeNode, TreeNode> FindParentAndPredecessor(TreeNode currentNode)
        {
            var parent = currentNode;
            var pred = currentNode.Left;
            while (pred.Right != null)
            {
                parent = pred;
                pred = parent.Right;
            }
            return Tuple.Create(parent, pred);
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }
    }
}
