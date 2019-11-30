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

        private float a = -2.0f;
        private float b = 2.0f;
        private float step = 0.5f;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
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

        private List<Point> GeneratePoints(float[] coefficients, float a, float b)
        {
            var points = new List<Point>();

            for (float i = a; i <= b; i += 0.001f)
            {
                float x = ChebyshevApproximation.Squish(i, a, b);
                float y = ChebyshevApproximation.Evaluate(coefficients, x);
                points.Add(new Point(i, y));
            }

            return points;
        }

        double ApproximatedFunction(double x)
        {
            return Math.Sin(5 * x) * Math.Exp(x);
        }

        private void DrawMSE_Func()
        {
            var rank = ChebyshevApproximation.CalculateRank(a, b, step);
            var xValues = new Libs.Matrix(ChebyshevApproximation.GenerateX(a, b, step));
            var yValues = new Libs.Matrix(new float[rank, 1]);
            for (int j = 0; j < yValues.Height; j++)
            {
                yValues[j, 0] = (float)ApproximatedFunction(xValues[0, j]);
            }

            var coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 1);
            MnkK1Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 2);
            MnkK2Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 3);
            MnkK3Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 4);
            MnkK4Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 5);
            MnkK5Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 6);
            MnkK6Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 7);
            MnkK7Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 8);
            MnkK8Plotter.ItemsSource = GeneratePoints(coefs);

            coefs = MeanSquaredErrorApproximation.Approximate(xValues, yValues, 9);
            MnkK9Plotter.ItemsSource = GeneratePoints(coefs);
        }

        private void DrawChebyshevFunc()
        {
            var coefs = ChebyshevApproximation.Approximate(a, b, 1, ApproximatedFunction);
            MnkK1Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(a, b, 2, ApproximatedFunction);
            MnkK2Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(a, b, 3, ApproximatedFunction);
            MnkK3Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(a, b, 4, ApproximatedFunction);
            MnkK4Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(a, b, 5, ApproximatedFunction);
            MnkK5Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(a, b, 6, ApproximatedFunction);
            MnkK6Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(a, b, 7, ApproximatedFunction);
            MnkK7Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(a, b, 8, ApproximatedFunction);
            MnkK8Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(a, b, 9, ApproximatedFunction);
            MnkK9Plotter.ItemsSource = GeneratePoints(coefs, a, b);
        }

        private void DrawChebyshevTable()
        {

            float[] xValues = new float[]
            {
                -3.2f, -2.1f, 0.4f, 0.7f, 2.0f, 2.5f, 2.777f
            };
            float[] yValues = new float[]
            {
                10.0f, -2.0f, 0, -7.0f, 7.0f, 0, 0
            };

            float a = xValues[0];
            float b = xValues[xValues.Length - 1];

            var coefs = ChebyshevApproximation.Approximate(xValues, yValues, 0);

            coefs = ChebyshevApproximation.Approximate(xValues, yValues, 1);
            MnkK1Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(xValues, yValues, 2);
            MnkK2Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(xValues, yValues, 3);
            MnkK3Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(xValues, yValues, 4);
            MnkK4Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(xValues, yValues, 5);
            MnkK5Plotter.ItemsSource = GeneratePoints(coefs, a, b);

            coefs = ChebyshevApproximation.Approximate(xValues, yValues, 6);
            MnkK6Plotter.ItemsSource = GeneratePoints(coefs, a, b);
        }

        private void DrawMNKTable()
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

        private void DrawBasePoints(float[] xValues, float[] yValues)
        {
            var points = new List<Point>();

            for (int i = 0; i < xValues.Length; i++)
                points.Add(new Point(xValues[i], yValues[i]));

            RealDataPlotFor1.ItemsSource = RealDataPlotFor2.ItemsSource = RealDataPlotFor3.ItemsSource
                = RealDataPlotFor4.ItemsSource = RealDataPlotFor5.ItemsSource = RealDataPlotFor6.ItemsSource
                = points;
        }

        private void DrawBasePoints(FittingFunction fittingFunction)
        {
            var points = new List<Point>();
            var xValues = ChebyshevApproximation.GenerateX(a, b, step);
            for (int i = 0; i < xValues.Length; i++)
            {
                points.Add(new Point(xValues[i], fittingFunction(xValues[i])));
            }

            RealDataPlotFor1.ItemsSource = RealDataPlotFor2.ItemsSource = RealDataPlotFor3.ItemsSource
                = RealDataPlotFor4.ItemsSource = RealDataPlotFor5.ItemsSource = RealDataPlotFor6.ItemsSource
                = RealDataPlotFor7.ItemsSource = RealDataPlotFor8.ItemsSource = RealDataPlotFor9.ItemsSource
                = points;
        }

        private void MSE_Table_Click(object sender, RoutedEventArgs e)
        {
            CleanDataSources();
            DrawMNKTable();
            DrawBasePoints();
        }

        private void ChebyshevTable_Click(object sender, RoutedEventArgs e)
        {
            CleanDataSources();
            DrawChebyshevTable();
            DrawBasePoints();
        }

        private void MSE_Func_Click(object sender, RoutedEventArgs e)
        {
            CleanDataSources();
            DrawMSE_Func();
            DrawBasePoints(ApproximatedFunction);
        }

        private void ChebyshevFunc_Click(object sender, RoutedEventArgs e)
        {
            CleanDataSources();
            DrawChebyshevFunc();
            DrawBasePoints(ApproximatedFunction);
        }

        private void CleanDataSources()
        {
            MnkK1Plotter.ItemsSource = MnkK2Plotter.ItemsSource = MnkK3Plotter.ItemsSource
                = MnkK4Plotter.ItemsSource = MnkK5Plotter.ItemsSource = MnkK6Plotter.ItemsSource
                = MnkK7Plotter.ItemsSource = MnkK8Plotter.ItemsSource = MnkK9Plotter.ItemsSource
                = RealDataPlotFor1.ItemsSource = RealDataPlotFor2.ItemsSource = RealDataPlotFor3.ItemsSource
                = RealDataPlotFor4.ItemsSource = RealDataPlotFor5.ItemsSource = RealDataPlotFor6.ItemsSource
                = RealDataPlotFor7.ItemsSource = RealDataPlotFor8.ItemsSource = RealDataPlotFor9.ItemsSource = new List<Point>();

        }

        private void DemoTable_MSE_Click(object sender, RoutedEventArgs e)
        {
            CleanDataSources();
            float[] xValues = new float[]
            {
                0, 3.3f, 6.6f, 9.9f
            };
            float[] yValues = new float[]
            {
                2.1f, 5.9f, 2.4f, 3.4f
            };

            var xMatrix = new Libs.Matrix(new float[,] {
            { 0, 3.3f, 6.6f, 9.9f }
            });
            var yMatrix = new Libs.Matrix(new float[,] {
            { 2.1f },
            { 5.9f },
            { 2.4f },
            { 3.4f }
            });
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

            DrawBasePoints(xValues, yValues);
        }

        private void DemoTable_Chebyshev_Click(object sender, RoutedEventArgs e)
        {
            CleanDataSources();
            float[] xValues = new float[]
            {
                0, 3.3f, 6.6f, 9.9f
            };
            float[] yValues = new float[]
            {
                2.1f, 5.9f, 2.4f, 3.4f
            };


            var coefs = ChebyshevApproximation.Approximate(
                    xValues,
                    yValues,
                    1
                );

            MnkK1Plotter.ItemsSource = GeneratePoints(coefs, xValues[0], xValues[xValues.Length - 1]);

            coefs = ChebyshevApproximation.Approximate(
                    xValues,
                    yValues,
                    2
            );

            MnkK2Plotter.ItemsSource = GeneratePoints(coefs, xValues[0], xValues[xValues.Length - 1]);

            coefs = ChebyshevApproximation.Approximate(
                    xValues,
                    yValues,
                    3
            );

            MnkK3Plotter.ItemsSource = GeneratePoints(coefs, xValues[0], xValues[xValues.Length - 1]);


            DrawBasePoints(xValues, yValues);
        }
    }
}
