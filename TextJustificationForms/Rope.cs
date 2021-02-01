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

            public Node(int Weight)
            {
                this.Weight = Weight;
            }
            public Node(int Weight, string StrFrag)
            {
                this.Weight = Weight;
                this.StrFrag = StrFrag;
            }
        }

        Node Head;
        
        public Rope()
        {
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

        public string Report(int startIndex, int endIndex)
        {
            Node startNode = Head;
            int currentIndex = startIndex;
            while(true)
            {
                if (startNode.Weight <= currentIndex && startNode.RightChild != null)
                {
                    startNode = startNode.RightChild;
                    startIndex -= startNode.Weight;
                    continue;
                }

                if (startNode.LeftChild != null)
                {
                    startNode = startNode.LeftChild;
                    continue;
                }

                break;
            }

            //Do Inorder Traversal here starting at startNode
            throw new NotImplementedException();
        }

        public void Insert(string stringToInsert,int position)
        {
            throw new NotImplementedException();
        }

        public void Append(string stringToInsert)
        {
            throw new NotImplementedException();
        }

        private Node Concat(Node left,Node right)
        {
            throw new NotImplementedException();
        }

        private (Node,Node) Split(Node node, int position)
        {
            throw new NotImplementedException();
        }
    }
}
