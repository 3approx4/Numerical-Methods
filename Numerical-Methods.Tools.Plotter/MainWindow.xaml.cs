using Numerical_Methods.Algorithms.Approximation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Numerical_Methods.Tools.Plotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var xMatrix = new Libs.Matrix(new float[,] {
                    { -3.2f, -2.1f, 0.4f, 0.7f, 2f, 2.5f, 2.777f }
                });
            var yMatrix = new Libs.Matrix(new float[,] {
                    { 10f },
                    { -2f },
                    { 0f },
                    { -7f },
                    { 7f },
                    { 0f },
                    { 0f }
                });
            var coefs = MeanSquaredErrorApproximation.Approximate(
                xMatrix,
                yMatrix,
                2
            );

            var points = new List<Point>();

            for (float x = -3.2f; x <= 2.777; x += 0.0001f)
            {
                float y = 0;
                for (int c = 0; c < coefs.ToArray().Length; c++)
                    y += coefs[c, 0] * (float)Math.Pow(x, c);
                points.Add(new Point(x, y));
            }

            using (var file = System.IO.File.CreateText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "points.csv")))
            {
                foreach (var point in points)
                {
                    file.WriteLine(string.Format("{0},{1}", point.X, point.Y));
                }
                file.Close();
            }

            DrawGraph(points, Brushes.Green);

            var realPoints = new List<Point>();
            for (int i = 0; i < xMatrix.Width; i++)
                realPoints.Add(new Point(xMatrix[0, i], yMatrix[i, 0]));

            DrawGraph(realPoints, Brushes.Orange);
        }

        protected void DrawGraph(List<Point> pointList, Brush brush)
        {
            const double margin = 10;
            double xmin = margin;
            double xmax = canGraph.Width - margin;
            double xmean = margin + (xmax - xmin) / 2;
            double ymin = margin;
            double ymax = canGraph.Height - margin;
            double ymean = margin + (ymax - ymin) / 2;
            const double step = 10;

            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymean), new Point(canGraph.Width, ymean)));
            for (double x = xmin; x <= canGraph.Width - step; x += step)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymean - margin / 2),
                    new Point(x, ymean + margin / 2)));
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            canGraph.Children.Add(xaxis_path);

            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xmean, 0), new Point(xmean, canGraph.Height)));
            for (double y = step; y <= canGraph.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmean - margin / 2, y),
                    new Point(xmean + margin / 2, y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            canGraph.Children.Add(yaxis_path);

            // Points
            if (pointList.Any())
            {
                var maxX = pointList.Max(p => Math.Abs(p.X));
                var maxY = pointList.Max(p => Math.Abs(p.Y));

                var xScale = xmax / maxX /2;
                var yScale = ymax / maxY /2;

                PointCollection points = new PointCollection();
                pointList.ForEach((p) => points.Add(new Point(
                    p.X * xScale + xmean,
                    p.Y * yScale + ymean
                    )));

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brush;
                polyline.Points = points;

                canGraph.Children.Add(polyline);
            }
        }
    }
}
