using System;

public class Program
{
    public class AVLTree
    {
        private class Node
        {
            public int data;
            public Node left;
            public Node right;
            public int height;

            public Node(int data)
            {
                this.data = data;
                left = null;
                right = null;
                height = 1;
            }
        }

        private Node root;

        public AVLTree()
        {
            root = null;
        }

        private int Height(Node node)
        {
            if (node == null)
                return 0;
            return node.height;
        }

        private int BalanceFactor(Node node)
        {
            if (node == null)
                return 0;
            return Height(node.left) - Height(node.right);
        }

        private Node RightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            x.right = y;
            y.left = T2;

            y.height = Math.Max(Height(y.left), Height(y.right)) + 1;
            x.height = Math.Max(Height(x.left), Height(x.right)) + 1;

            return x;
        }

        private Node LeftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            y.left = x;
            x.right = T2;

            x.height = Math.Max(Height(x.left), Height(x.right)) + 1;
            y.height = Math.Max(Height(y.left), Height(y.right)) + 1;

            return y;
        }

        public void Insert(int data)
        {
            root = InsertRec(root, data);
        }

        private Node InsertRec(Node root, int data)
        {
            if (root == null)
                return new Node(data);

            if (data < root.data)
                root.left = InsertRec(root.left, data);
            else if (data > root.data)
                root.right = InsertRec(root.right, data);
            else // Duplicate keys not allowed
                return root;

            root.height = 1 + Math.Max(Height(root.left), Height(root.right));

            int balance = BalanceFactor(root);

            // Left Left Case
            if (balance > 1 && data < root.left.data)
                return RightRotate(root);

            // Right Right Case
            if (balance < -1 && data > root.right.data)
                return LeftRotate(root);

            // Left Right Case
            if (balance > 1 && data > root.left.data)
            {
                root.left = LeftRotate(root.left);
                return RightRotate(root);
            }

            // Right Left Case
            if (balance < -1 && data < root.right.data)
            {
                root.right = RightRotate(root.right);
                return LeftRotate(root);
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
                // Go all the way left
                InorderTraversalRec(root.left);
                // Print the middle
                Console.Write(root.data + " ");
                // Go Right
                InorderTraversalRec(root.right);
            }
        }
    }

    public static void Main(string[] args)
    {
        AVLTree avl = new AVLTree();

        avl.Insert(10);
        avl.Insert(20);
        avl.Insert(30);
        avl.Insert(40);
        avl.Insert(50);
        avl.Insert(25);

        Console.WriteLine("Inorder Traversal of AVL Tree:");
        avl.InorderTraversal();
    }
}
