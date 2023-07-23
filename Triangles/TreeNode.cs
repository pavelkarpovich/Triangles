namespace Triangles
{
    public class TreeNode
    {
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        public Triangle Triangle { get; set; }

        public TreeNode()
        {
        }

        public TreeNode(Triangle triangle)
        {
            Triangle = triangle;
        }
    }
}
