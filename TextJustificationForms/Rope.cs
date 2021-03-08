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

        /// <summary>
        /// Gets the whole string stored in the Rope
        /// </summary>
        /// <returns></returns>
        public string ReportAll()
        {
            StringBuilder output = new StringBuilder();

            var nodeStack = new Stack<Node>();
            var currentNode = Head;
            while (currentNode != null || nodeStack.Count != 0)
            {
                while (currentNode != null)
                {
                    nodeStack.Push(currentNode);
                    currentNode = currentNode.LeftChild;
                }

                currentNode = nodeStack.Pop();

                if (currentNode.IsLeafNode)
                {
                    output.Append(currentNode.StrFrag);
                }

                currentNode = currentNode.RightChild;
            }

            return output.ToString();
        }

        /// <summary>
        /// Gets a substring from the Rope
        /// </summary>
        /// <param name="startIndex">The starting index of the substring in the Rope</param>
        /// <param name="length">The length of the substring</param>
        /// <returns></returns>
        public string Report(int startIndex, int length)
        {
            (Node startNode, int index) = Query(Head, startIndex);
            StringBuilder output = new StringBuilder(startNode.StrFrag.Substring(index));

            var nodeStack = new Stack<Node>();
            var currentNode = Head;
            bool didBeginReporting = false;
            while(currentNode != null || nodeStack.Count != 0) //inorder traversal through the tree
            {
                while(currentNode != null)
                {
                    nodeStack.Push(currentNode);
                    currentNode = currentNode.LeftChild;
                }

                currentNode = nodeStack.Pop();

                if(didBeginReporting && currentNode.IsLeafNode)
                {
                    if(currentNode.StrFrag.Length > length - output.Length)
                    {
                        output.Append(currentNode.StrFrag.Substring(0, length - output.Length));
                    }
                    else
                    {
                        output.Append(currentNode.StrFrag);
                    }
                }

                if (output.Length == length) break;

                didBeginReporting = currentNode == startNode && !didBeginReporting; //begins reporting after first reaching startNode in the traversal

                currentNode = currentNode.RightChild;
            }

            return output.ToString();
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

        /// <summary>
        /// Gets the total combined weight of the leaves in a subtree (the length of the string that the tree holds)
        /// </summary>
        /// <param name="node">The node to recurse from</param>
        /// <returns></returns>
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
            while (node != null) //recurses up the tree finding all nodes that are further right than the original node
            {
                if(node.RightChild != previousNode) //adds the node's right child to the new rope assuming that it didn't just recurse from the right child
                {
                    newSplitRope = Concat(newSplitRope, new Rope(node.RightChild));
                    node.RightChild = null;
                }

                if (node.LeftChild == null ^ node.RightChild == null) //removes unnecessary parent nodes (ones that only contain one subtree)
                {
                    if(node.Parent != null)
                    {
                        ChangeBasedOnSide(node, node.LeftChild != null ? node.LeftChild : node.RightChild);
                        if (node.LeftChild != null)
                        {
                            node = node.LeftChild;
                        }
                        else
                        {
                            node = node.RightChild;
                        }
                    }
                    else
                    {
                        rope.Head = node.LeftChild != null ? node.LeftChild : node.RightChild;
                    }
                }

                if (node.LeftChild == null && node.RightChild == null && !node.IsLeafNode) //removes empty nodes that both have no children and is not considered a leaf node (meaning it has no string value)
                {
                    if(node.Parent != null)
                    {
                        ChangeBasedOnSide(node, null);
                    }
                    else
                    {
                        rope.Head = null;
                    }

                }

                previousNode = node;
                node = node.Parent;
            }

            return (rope, newSplitRope);
        }

        /// <summary>
        /// Changes the parents connection to a given node based on whether the node is the left child or right child of its parent
        /// </summary>
        /// <param name="nodeToChange">The node whose parent's connection will change</param>
        /// <param name="value">The value to swap in place of the node</param>
        private static void ChangeBasedOnSide(Node nodeToChange, Node value)
        {
            if(nodeToChange == nodeToChange.Parent.LeftChild)
            {
                nodeToChange.Parent.LeftChild = value;
            }
            else
            {
                nodeToChange.Parent.RightChild = value;
            }
        }
    }
}
