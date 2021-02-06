using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextJustificationForms
{
    public class Rope
    {
        class Node
        {
            public Node LeftChild;
            public Node RightChild;
            public string StrFrag;
            public int Weight;
            public bool IsLeafNode { get; internal set; }

            public Node(int Weight, bool IsLeafNode)
            {
                this.Weight = Weight;
                this.IsLeafNode = IsLeafNode;
            }
            public Node(int Weight, string StrFrag)
            {
                this.Weight = Weight;
                this.StrFrag = StrFrag;
                IsLeafNode = true;
            }
        }

        Node Head;
        
        public Rope()
        {
            Head = new Node(0,false);
        }

        internal Rope(int headWeight)
        {
            Head = new Node(headWeight, false); 
        }

        public char this[int index]
        {
            get
            {
                return Index(Head, index);
            }
        }

        private char Index(Node node, int index)
        {
            if(node.Weight <= index && node.RightChild != null)
            {
                return Index(node.RightChild, index - node.Weight);
            }

            if(node.LeftChild != null)
            {
                return Index(node.LeftChild, index);
            }

            return node.StrFrag[index];
        }

        //public string Report(int startIndex, int endIndex)
        //{
        //    Node currentNode = Head;
        //    int currentIndex = startIndex;
        //    while(true)
        //    {
        //        if (currentNode.Weight >= endIndex && currentNode.Weight <= currentIndex && currentNode.RightChild != null)
        //        {
        //            currentNode = currentNode.RightChild;
        //            startIndex -= currentNode.Weight;
        //            continue;
        //        }

        //        if (currentNode.Weight >= endIndex && currentNode.LeftChild != null)
        //        {
        //            currentNode = currentNode.LeftChild;
        //            continue;
        //        }
        //        break;
        //    }

        //    Stack<Node> stack = new Stack<Node>();
        //    while(currentNode != null || stack.Count > 0)
        //    {
        //        while(currentNode != null)
        //        {
        //            stack.Push(currentNode);
        //            currentNode = currentNode.LeftChild;
        //        }

        //        currentNode = stack.Pop();
        //        if(currentNode.StrFrag != null )

        //        currentNode = currentNode.RightChild;
        //    }
        //}

        public void Insert(string stringToInsert,int position)
        {
            throw new NotImplementedException();  
        }

        public void Append(string stringToInsert)
        {
            Insert(stringToInsert, Head.Weight);
        }

        private static void InorderTraversal(Node startNode, List<Node> nodes)
        {
            if (startNode == null) return;

            InorderTraversal(startNode.LeftChild, nodes);
            nodes.Add(startNode);
            InorderTraversal(startNode.RightChild, nodes);
        }

        private static Rope Concat(Rope left,Rope right)
        {
            var inorderNodes = new List<Node>();
            InorderTraversal(left.Head,inorderNodes);
            int weight = 0;
            foreach(var node in inorderNodes)
            {
                if (!node.IsLeafNode) continue;
                weight += node.StrFrag.Length;
            }
            Rope concatedRope = new Rope(weight);
            concatedRope.Head.LeftChild = left.Head;
            concatedRope.Head.RightChild = right.Head;
            return concatedRope;
        }

        private (Node,Node) Split(Node node, int position)
        {
            throw new NotImplementedException();
        }
    }
}
