/**
 * Definition for a binary tree node. */
 public class TreeNode {
     public int val;
     public TreeNode? left;
     public TreeNode? right;
     public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null) {
         this.val = val;
         this.left = left;
         this.right = right;
     }
 }
 
public class Solution
{
    public int MaxDepth(TreeNode root)
    {
        if (root == null)
            return 0;

        Queue<TreeNode> queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int maxDepth = 0;

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            maxDepth++;

            for (int i = 0; i < levelSize; i++)
            {
                TreeNode node = queue.Dequeue();
                if (node.left != null)
                    queue.Enqueue(node.left);
                if (node.right != null)
                    queue.Enqueue(node.right);
            }
        }

        return maxDepth;
    }

    private void countDepth(TreeNode root, ref int maxDepth, ref int currentDepth)
    {
        if (root == null)
            return;

        currentDepth++;

        if (root.left != null)
        { 
            countDepth(root.left, ref maxDepth, ref currentDepth);
            currentDepth--;
        }
        
        maxDepth = Math.Max(maxDepth, currentDepth);

        if (root.right != null)
        {
            countDepth(root.right, ref maxDepth, ref currentDepth);
            currentDepth--;
        }

        return;
    }

    public int MaxDepthRecursive(TreeNode root)
    {
        int maxDepth = 0;
        int currentDepth = 0;
        countDepth(root, ref maxDepth, ref currentDepth);
        return maxDepth;
    }
}

public class Program
{
    public static void Main()
    {
        Solution s = new Solution();
        // easy
        // TreeNode tree = new(3, new TreeNode(9), new TreeNode(20, new TreeNode(15), new TreeNode(7)));
        // hard
        TreeNode tree = new(3, 
            /*3,L*/ new(9,
            /*9,L*/new(57), /*9, R*/new(20, 
                           /*20,L*/new(15, 
                          /*15,L*/new(14), /*15,R*/new(16)), 
                              /*20,R*/new(7, 
                             /* 7,L*/new(88, 
                            /*88,L*/new(66)), 
                           /* 7,r*/new(99)))), /*3,R*/new(71, 
                                            /*71,L*/new(10, 
                                           /*10,L*/new(6), /*10,R*/new(19)), /*71,R*/new(11, 
                                                                            /*11,L*/new(4), /*11,R*/new(5))));

        Console.WriteLine($"Your max depth is: {s.MaxDepth(tree)}");
    }
}