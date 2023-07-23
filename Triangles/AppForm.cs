namespace Triangles
{
    public partial class AppForm : Form
    {
        private Tree tree;
        private int maxRank;

        public AppForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            int formSizeWidth = Screen.GetWorkingArea(this).Width;
            int formSizeHeight = Screen.GetWorkingArea(this).Height;
            panel.Size = new Size(formSizeWidth - 300, formSizeHeight - 100);   // panel size fits window size
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(dialog.FileName);

                int numberOfLines;
                List<Triangle> triangles = new List<Triangle>(); ;

                try
                {
                    numberOfLines = int.Parse(lines[0]);
                    for (int i = 0; i < lines.Length - 1; i++)
                    {
                        string[] pointsString = lines[i + 1].Split(' ');
                        var points = pointsString.Select(int.Parse).ToArray();
                        triangles.Add(new Triangle(new Point(points[0], points[1]), new Point(points[2], points[3]), new Point(points[4], points[5])));
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Data is not in a correct format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panel.Refresh();
                    rankLabel.Text = string.Empty;
                    return;
                }

                if (numberOfLines < 0 || numberOfLines > 1000)
                {
                    MessageBox.Show("Number of triangles must be within range 0 <= n <= 1000", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (numberOfLines != lines.Length - 1)
                {
                    MessageBox.Show("Number of triangles is incorrect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MathHelper.AnyCollinear(triangles))
                {
                    MessageBox.Show("Collinear points!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DisplayTriangles(triangles, false);
                    return;
                }

                if (MathHelper.AreIntersected(triangles))
                {
                    MessageBox.Show("Intersected triangles!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DisplayTriangles(triangles, false);
                    return;
                }

                tree = new Tree();

                for (int i = 0; i < triangles.Count; i++)
                {
                    tree.Insert(triangles[i]);
                }

                tree.Flatten();
                triangles = tree.TriangleList;
                maxRank = triangles.Max(x => x.Rank);
                rankLabel.Text = maxRank.ToString();

                DisplayTriangles(triangles, true);
            }
        }

        private void DisplayTriangles(List<Triangle> triangles, bool isOk)
        {
            panel.Refresh();
            Graphics graphics = panel.CreateGraphics();
            Pen pen = new Pen(Color.Black, 3);

            // Adjust triangles coordinates to panel size
            int offset = 5;
            int maxX = new[] { triangles.Max(x => x.PointA.X), triangles.Max(x => x.PointB.X), triangles.Max(x => x.PointC.X) }.Max();
            int maxY = new[] { triangles.Max(x => x.PointA.Y), triangles.Max(x => x.PointB.Y), triangles.Max(x => x.PointC.Y) }.Max();
            double ratioWidth = (double)(panel.Width - 2 * offset) / maxX;
            double ratioHeight = (double)(panel.Height - 2 * offset) / maxY;
            double ratio = ratioWidth < ratioHeight ? ratioWidth : ratioHeight;

            foreach (var t in triangles)
            {
                Point[] points1 = {
                    new Point(offset + Convert.ToInt32(t.PointA.X * ratio), offset + Convert.ToInt32(t.PointA.Y * ratio)),
                    new Point(offset + Convert.ToInt32(t.PointB.X * ratio), offset + Convert.ToInt32(t.PointB.Y * ratio)),
                    new Point(offset + Convert.ToInt32(t.PointC.X * ratio), offset + Convert.ToInt32(t.PointC.Y * ratio))
                };
                graphics.DrawPolygon(pen, points1);
                if (isOk)
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(150, 0, 255 - 180 / maxRank * t.Rank, 0));
                    graphics.FillPolygon(brush, points1);
                }
            }
        }
    }
}