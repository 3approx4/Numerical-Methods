using Numerical_Methods.Algorithms.Approximation;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Numerical_Methods.Tools.Plotter2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Libs.Matrix xMatrix = new Libs.Matrix(new float[,] {
            { -3.2f, -2.1f, 0.4f, 0.7f, 2f, 2.5f, 2.777f }
        });
        private Libs.Matrix yMatrix = new Libs.Matrix(new float[,] {
            { 10f },
            { -2f },
            { 0f },
            { -7f },
            { 7f },
            { 0f },
            { 0f }
        });

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DrawMNK();
            DrawBasePoints();
        }

        private List<Point> GeneratePoints(Libs.Matrix coefficients)
        {
            var points = new List<Point>();

            for (float x = xMatrix[0, 0]; x <= xMatrix[0, xMatrix.Width - 1]; x += 0.001f)
            {
                float y = 0;
                for (int c = 0; c < coefficients.ToArray().Length; c++)
                    y += coefficients[c, 0] * (float)Math.Pow(x, c);
                points.Add(new Point(x, y));
            }

            return points;
        }

        private void DrawMNK()
        {
            var coefs = MeanSquaredErrorApproximation.Approximate(
                xMatrix,
                yMatrix,
                1
            );

            MnkK1Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(
                xMatrix,
                yMatrix,
                2
            );

            MnkK2Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(
                xMatrix,
                yMatrix,
                3
            );

            MnkK3Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(
                xMatrix,
                yMatrix,
                4
            );

            MnkK4Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(
                xMatrix,
                yMatrix,
                5
            );

            MnkK5Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(
                xMatrix,
                yMatrix,
                6
            );

            MnkK6Plotter.ItemsSource = GeneratePoints(coefs);
        }

        private void DrawBasePoints()
        {
            var points = new List<Point>();

            for (int i = 0; i < xMatrix.Width; i++)
                points.Add(new Point(xMatrix[0, i], yMatrix[i, 0]));

            RealDataPlotFor1.ItemsSource = RealDataPlotFor2.ItemsSource = RealDataPlotFor3.ItemsSource
                = RealDataPlotFor4.ItemsSource = RealDataPlotFor5.ItemsSource = RealDataPlotFor6.ItemsSource
                = points;
        }
    }
}
