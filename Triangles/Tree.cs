namespace Triangles
{
    public class Tree
    {
        private TreeNode root;

        public List<Triangle> TriangleList { get; set; } = new List<Triangle>();

        public Tree()
        {
            root = new TreeNode();
        }

        // Insert a triangle to the tree
        public void Insert(Triangle triangle)
        {
            Insert(triangle, root);
        }

        // Insert a triangle to the particular node of the tree
        public void Insert(Triangle triangle, TreeNode node)
        {
            if (node.Children.Count == 0)
            {
                TreeNode childNode = new TreeNode(triangle);
                node.Children.Add(childNode);
            }
            else
            {
                bool isInside = false;
                for (int i = 0; i < node.Children.Count; i++)
                {
                    if (MathHelper.CompareTriangles(node.Children[i].Triangle, triangle) == 0)
                    {
                        Insert(triangle, node.Children[i]);
                        isInside = true;
                    }
                }

                if (!isInside)
                {
                    TreeNode childNode = new TreeNode(triangle);
                    node.Children.Insert(0, childNode);
                    for (int i = node.Children.Count - 1; i > 0; i--)
                    {
                        if (MathHelper.CompareTriangles(node.Children[i].Triangle, triangle) == 1)
                        {
                            node.Children[0].Children.Add(node.Children[i]);
                            node.Children.RemoveAt(i);
                        }
                    }
                }
            }
        }

        // Making a list of triangles from the tree
        public void Flatten()
        {
            Flatten(root);
        }

        // Adding to the list of triangles beginning from the particular node
        public void Flatten(TreeNode node)
        {
            if (node.Triangle is not null)
                TriangleList.Add(node.Triangle);

            foreach (var childNode in node.Children)
            {
                if (node.Triangle is not null)
                    childNode.Triangle.Rank = node.Triangle.Rank + 1;
                else childNode.Triangle.Rank = 1;
                Flatten(childNode);
            }
        }
    }
}
