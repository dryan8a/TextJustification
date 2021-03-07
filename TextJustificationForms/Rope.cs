using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextJustificationForms
{
    public class Rope
    {
        internal class Node
        {
            public Node LeftChild;
            public Node RightChild;
            public Node Parent;
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
            
        }

        private Rope(int headWeight)
        {
            Head = new Node(headWeight, false); 
        }

        private Rope(Node head)
        {
            Head = head;
        }

        public char this[int index]
        {
            get
            {
                return Index(Head, index);
            }
        }

        private static char Index(Node node, int index)
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

        private static (Node node, int indexInNode) Query(Node node,int index)
        {
            if (node.Weight <= index && node.RightChild != null)
            {
                return Query(node.RightChild, index - node.Weight);
            }

            if (node.LeftChild != null)
            {
                return Query(node.LeftChild, index);
            }

            return (node,index);
        }

        public void Insert(string stringToInsert,int position)
        {
            if(Head == null)
            {
                Head = new Node(stringToInsert.Length, stringToInsert);
                return;
            }
            (Rope left, Rope right) = Split(this, position);
            var leftConcat = Concat(left,new Rope(new Node(stringToInsert.Length,stringToInsert)));
            Head = Concat(leftConcat, right).Head;
        }

        public void Append(string stringToInsert)
        {
            if(Head == null)
            {
                Head = new Node(stringToInsert.Length, stringToInsert);
                return;
            }
            Head = Concat(this, new Rope(new Node(stringToInsert.Length, stringToInsert))).Head;
        }

        //private static void InorderTraversal(Node startNode, List<Node> outputNodes)
        //{
        //    if (startNode == null) return;

        //    InorderTraversal(startNode.LeftChild, outputNodes);
        //    outputNodes.Add(startNode);
        //    InorderTraversal(startNode.RightChild, outputNodes);
        //}

        private static int GetTotalWeight(Node node)
        {
            if(node == null)
            {
                return 0;
            }
            if(node.LeftChild == null && node.RightChild == null)
            {
                return node.Weight;
            }

            int currentWeight = 0;
            if(node.LeftChild != null)
            {
                currentWeight += GetTotalWeight(node.LeftChild);
            }
            if(node.RightChild != null)
            {
                currentWeight += GetTotalWeight(node.RightChild);
            }

            return currentWeight;
        }

        private static Rope Concat(Rope left,Rope right)
        {
            if(left == null || left.Head == null)
            {
                return right;
            }
            if(right == null || right.Head == null)
            {
                return left;
            }

            Rope concatedRope = new Rope(GetTotalWeight(left.Head));
            concatedRope.Head.LeftChild = left.Head;
            left.Head.Parent = concatedRope.Head;
            concatedRope.Head.RightChild = right.Head;
            right.Head.Parent = concatedRope.Head;
            return concatedRope;
        }

        private static (Rope, Rope) Split(Rope rope, int position)
        {
            (Node node,int indexInNode) = Query(rope.Head, position);
    
            Rope newSplitRope = null;
            if (indexInNode != 0) //splits the split node if the split includes only part of the node's string
            {
                string left = node.StrFrag.Substring(0, indexInNode + 1);
                string right = node.StrFrag.Substring(indexInNode + 1);
                node.LeftChild = new Node(left.Length,left);
                node.LeftChild.Parent = node;
                node.StrFrag = "";
                node.Weight = left.Length;
                node.IsLeafNode = false;

                newSplitRope = new Rope(new Node(right.Length, right));
            }
            else
            {
                newSplitRope = new Rope(new Node(node.Weight, node.StrFrag));

                if(node == node.Parent.LeftChild)
                {
                    node.Parent.LeftChild = null;
                }
            }

            Node previousNode = node;
            node = node.Parent;
            while (node != null)
            {
                if(node.RightChild != previousNode)
                {
                    newSplitRope = Concat(newSplitRope, new Rope(node.RightChild));
                    node.RightChild = null;
                }

                if(node.LeftChild == null && node.RightChild == null && !node.IsLeafNode && node.Parent != null) //removes empty nodes
                {
                    if(node == node.Parent.LeftChild)
                    {
                        node.Parent.LeftChild = null;
                    }
                    else
                    {
                        node.Parent.RightChild = null;
                    }
                }

                //remove useless parent nodes and replace them with their child

                previousNode = node;
                node = node.Parent;
            }

            if(rope.Head.LeftChild == null && rope.Head.RightChild == null && !rope.Head.IsLeafNode)
            {
                rope.Head = null;
            }

            return (rope, newSplitRope);
        }
    }
}
