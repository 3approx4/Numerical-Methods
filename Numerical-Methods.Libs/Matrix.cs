using System;

namespace Numerical_Methods.Libs
{
    public class Matrix
    {
        protected float[,] matrix { get; set; }

        public Matrix(int height, int width)
        {
            matrix = new float[height, width];
        }

        public Matrix(float[,] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix(float[] row)
        {
            new Matrix(0, row.Length).PushRow(row);
        }

        public float this[int x, int y]
        {
            get => matrix[y, x];
            set => matrix[y, x] = value;
        }

        public int Height => matrix.GetLength(0);

        public int Width => matrix.GetLength(1);

        public bool IsSquare => Width == Height;

        public float[,] ToArray() => matrix;

        public void PushRow(float[] row)
        {
            if (row.Length != Width)
            {
                throw new IndexOutOfRangeException("Element count in row is more than the column count of matrix");
            }
            for (var i = 0; i < row.Length; i++)
            {
                matrix[Height, i] = row[i];
            }
        }
    }
}
