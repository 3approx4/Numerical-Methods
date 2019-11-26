using Numerical_Methods.Algorithms.Approximation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Numerical_Methods.Tools.Plotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Point> Points = new List<Point>()
        {
            new Point(1, 42),
            new Point(4, 10),
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var coefs = MeanSquaredErrorApproximation.Approximate(
                new Libs.Matrix(new float[,] {
                    { -3.2f, -2.1f, 0.4f, 0.7f, 2f, 2.5f, 2.777f }
                }),
                new Libs.Matrix(new float[,] {
                    { 10f },
                    { -2f },
                    { 0f },
                    { -7f },
                    { 7f },
                    { 0f },
                    { 0f }
                })
            );

            var points = new List<Point>();

            for (float x = -100; x <= 100; x += 0.01f)
            {
                float y = 0;
                for (int c = 0; c < coefs.ToArray().Length; c++)
                    y += coefs[c, 0] * (float)Math.Pow(x, c);
                points.Add(new Point(x, y));
            }

            DrawGraph(points);
        }

        protected void DrawGraph(List<Point> pointList)
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

                var xScale = xmax / maxX;
                var yScale = ymax / maxY;

                PointCollection points = new PointCollection();
                pointList.ForEach((p) => points.Add(new Point(
                    p.X * xScale,
                    p.Y * yScale
                    )));

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = Brushes.Green;
                polyline.Points = points;

                canGraph.Children.Add(polyline);
            }
        }
    }
}
