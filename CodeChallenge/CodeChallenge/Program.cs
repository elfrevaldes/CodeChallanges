using System;

public class Program
{
    public class BinarySearchTree
    {

        private class Node
        {
            public int data;
            public Node left;
            public Node right;

            public Node(int data)
            {
                this.data = data;
                left = null;
                right = null;
            }
        }

        private Node root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Insert(int data)
        {
            root = InsertRec(root, data);
        }

        private Node InsertRec(Node root, int data)
        {
            if (root == null)
            {
                root = new Node(data);
                return root;
            }

            if (data < root.data)
            {
                root.left = InsertRec(root.left, data);
            }
            else if (data > root.data)
            {
                root.right = InsertRec(root.right, data);
            }

            return root;
        }

        public void InorderTraversal()
        {
            InorderTraversalRec(root);
            Console.WriteLine();
        }

        private void InorderTraversalRec(Node root)
        {
            if (root != null)
            {
                // Go to the farthest left side
                InorderTraversalRec(root.left);
                // Middle
                Console.Write(root.data + " ");
                // Lets go right now
                InorderTraversalRec(root.right);
            }
        }
    }

    public static void Main(string[] args)
    {
        BinarySearchTree bst = new BinarySearchTree();

        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(20);
        bst.Insert(25);
        bst.Insert(40);
        bst.Insert(70);
        bst.Insert(92);
        bst.Insert(60);
        bst.Insert(80);

        Console.WriteLine("Inorder Traversal of Binary Search Tree:");
        bst.InorderTraversal();
    }
}