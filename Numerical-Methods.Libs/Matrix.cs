using System;
using System.Linq;

namespace Numerical_Methods.Libs
{
    public class Matrix
    {
        protected float[,] matrix { get; set; }

        /// <summary>
        /// Constructor for creation emty matrix by height and width
        /// </summary>
        /// <param name="height">Matrix height</param>
        /// <param name="width">Matrix width</param>
        public Matrix(int height, int width)
        {
            matrix = new float[height, width];
        }

        /// <summary>
        /// Constructor for creation matrix from two-dimensional float array
        /// </summary>
        /// <param name="matrix">Two-dimensional float array</param>
        public Matrix(float[,] matrix)
        {
            this.matrix = matrix;
        }

        /// <summary>
        /// Constructor for creation matrix from one-dimensional float array
        /// Vector-like matrix
        /// </summary>
        /// <param name="array">One-dimensional float array</param>
        public Matrix(float[] array)
        {
            this.matrix = new float[1, array.Length];
            for (int i = 0; i < array.Length; i++)
                matrix[1, i] = array[i];
        }

        /// <summary>
        /// Get value of matrix
        /// </summary>
        /// <param name="x">Horizontal position</param>
        /// <param name="y">Vertical position</param>
        /// <returns></returns>
        public float this[int y, int x]
        {
            get => matrix[y, x];
            set => matrix[y, x] = value;
        }

        /// <summary>
        /// Matrix height - vertical length
        /// </summary>
        public int Height => matrix.GetLength(0);

        /// <summary>
        /// Matrix width - horizontal length
        /// </summary>
        public int Width => matrix.GetLength(1);

        /// <summary>
        /// Returns is matrix square
        /// Width and height should be equal
        /// </summary>
        public bool IsSquare => Width == Height;

        /// <summary>
        /// Extracts from matrix two-dimensional float array
        /// </summary>
        /// <returns>Two-dimensional float array</returns>;
        public float[,] ToArray() => matrix;

        /// <summary>
        /// Overwrites matrix values by sub-matrix values
        /// </summary>
        /// <param name="subMatrix">Sub-matrix, which contains values to be writed</param>
        /// <param name="verticalPosition">Vertical offset</param>
        /// <param name="horizonatalPosition">Horizontal offset</param>
        public void Write(float[,] subMatrix, int verticalPosition = 0, int horizonatalPosition = 0)
        {
            Write(new Matrix(subMatrix), verticalPosition, horizonatalPosition);
        }
        /// <summary>
        /// Overwrites matrix values by sub-matrix values
        /// </summary>
        /// <param name="subMatrix">Sub-matrix, which contains values to be writed</param>
        /// <param name="verticalPosition">Vertical offset</param>
        /// <param name="horizonatalPosition">Horizontal offset</param>
        public void Write(Matrix subMatrix, int verticalPosition = 0, int horizonatalPosition = 0)
        {
            if ((subMatrix.Width + horizonatalPosition) > Width ||
                (subMatrix.Height + verticalPosition) > Height)
                throw new Exception("Can't write matrix, it is not fit");

            for (int y = 0; y < subMatrix.Height; y++)
                for (int x = 0; x < subMatrix.Width; x++)
                    matrix[y + verticalPosition, x + horizonatalPosition] = subMatrix[y, x];
        }

        /// <summary>
        /// Push row in the end of matrix
        /// </summary>
        /// <param name="row">Row, that will be pushed</param>
        public void PushRow(float[] row)
        {
            if (row.Length != Width)
                throw new IndexOutOfRangeException("Element count in row is more than the column count of matrix");

            float[,] oldMatrix = matrix;
            matrix = new float[Height + 1, Width];
            Write(oldMatrix);
            for (var i = 0; i < row.Length; i++)
                matrix[Height - 1 , i] = row[i];
        }

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <returns>Result of multiplication</returns>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Height != matrix2.Width)
                throw new Exception("Matrix are not compatible");

            Matrix newMatrix = new Matrix(matrix1.Height, matrix2.Width);
            for (int y = 0; y < matrix1.Height; y++)
                for (int x = 0; x < matrix2.Width; x++)
                {
                    float[] mult = new float[matrix1.Width];
                    for (int i = 0; i < matrix1.Width; i++)
                        mult[i] = matrix1[y, i] * matrix2[i, x];
                    newMatrix[y, x] = mult.Sum();
                }
            return newMatrix;
        }

        /// <summary>
        /// Matrix value by value check
        /// </summary>
        /// <param name="obj">Other matrix object</param>
        /// <returns>Boolean result of checking</returns>
        public override bool Equals(object obj)
        {
            Matrix compareMatrix = obj as Matrix;
            if (compareMatrix == null ||
                Width != compareMatrix.Width ||
                Height != compareMatrix.Height)
                return false;
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    if (matrix[y, x] != compareMatrix[y, x])
                        return false;
            return true;
        }

        /// <summary>
        /// Get hash code of matrix:
        /// Sum of all matrix value hashs
        /// </summary>
        /// <returns>Hash</returns>
        public override int GetHashCode()
        {
            int sum = 0;
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    sum += matrix[y, x].GetHashCode();
            return sum;
        }
    }
}
