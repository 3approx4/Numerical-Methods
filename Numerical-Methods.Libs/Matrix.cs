﻿using System;
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
                matrix[0, i] = array[i];
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
        /// Get and sets matrix row
        /// </summary>
        /// <param name="y">Vartical position</param>
        /// <returns></returns>
        public float[] this[int y]
        {
            get => GetRow(y);
            set => WriteRow(value, y);
        }

        /// <summary>
        /// Matrix height - vertical (column) length
        /// </summary>
        public int Height => matrix.GetLength(0);

        /// <summary>
        /// Matrix width - horizontal (row) length
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
        /// <param name="subMatrix">Sub-matrix, which contains values to be written</param>
        /// <param name="verticalPosition">Vertical offset</param>
        /// <param name="horizontalPosition">Horizontal offset</param>
        public void Write(float[,] subMatrix, int verticalPosition = 0, int horizontalPosition = 0)
        {
            Write(new Matrix(subMatrix), verticalPosition, horizontalPosition);
        }
        /// <summary>
        /// Overwrites matrix values by sub-matrix values
        /// </summary>
        /// <param name="subMatrix">Sub-matrix, which contains values to be written</param>
        /// <param name="verticalPosition">Vertical offset</param>
        /// <param name="horizontalPosition">Horizontal offset</param>
        public void Write(Matrix subMatrix, int verticalPosition = 0, int horizontalPosition = 0)
        {
            if ((subMatrix.Width + horizontalPosition) > Width ||
                (subMatrix.Height + verticalPosition) > Height)
                throw new Exception("Can't write matrix, it does not fit the dimensions");

            for (int y = 0; y < subMatrix.Height; y++)
                for (int x = 0; x < subMatrix.Width; x++)
                    matrix[y + verticalPosition, x + horizontalPosition] = subMatrix[y, x];
        }

        /// <summary>
        /// Overwrites matrix row by the provided row
        /// </summary>
        /// <param name="row">Array of new values to be written</param>
        /// <param name="verticalOffset">Updated row index</param>
        /// <exception cref="Exception"></exception>
        public void WriteRow(float[] row, int verticalOffset = 0)
        {
            if (row.Length > Width ||
               verticalOffset > Height)
                throw new Exception("Can't write matrix, it does not fit the row");

            for (int i = 0; i < row.Length; i++)
            {
                matrix[verticalOffset, i] = row[i];
            }
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
            this[Height - 1] = row;
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
                throw new Exception("Matrices are not compatible");

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
        /// Get the row of the matrix
        /// </summary>
        /// <param name="index">Row index</param>
        /// <returns>Array of the row elements</returns>
        public float[] GetRow(int index)
        {
            if (index > Height || index < 0)
                throw new IndexOutOfRangeException("No row with such index available");

            float[] row = new float[Width];
            for (int i = 0; i < Width; i++)
                row[i] = matrix[index, i];
            return row;
        }

        /// <summary>
        /// Get the row index, where the absolute value of the specified column is maximal.
        /// Row index is used to determine the leading row for Gauss method
        /// </summary>
        /// <param name="columnIndex">The column index of the candidates for comparison</param>
        /// <returns></returns>
        public int GetRowIndexWithMaxAbsValue(int columnIndex)
        {
            if (columnIndex > Width || columnIndex < 0)
                throw new IndexOutOfRangeException("No row with such index available");

            int maxElementRow = 0;
            for (int i = 0; i < Height; i++)
            {
                maxElementRow = Math.Abs(matrix[i, columnIndex]) > Math.Abs(matrix[maxElementRow, columnIndex]) ? i : maxElementRow;
            }
            return maxElementRow;
        }

        /// <summary>
        /// Get rows between the indexes
        /// </summary>
        /// <param name="startIndex">Starting row</param>
        /// <param name="endIndex">Ending row</param>
        /// <returns>Matrix with the same width than the sliced and with row count of endIndex-startIndex</returns>
        public Matrix RowSlice(int startIndex, int endIndex)
        {
            Matrix result = new Matrix(endIndex - startIndex + 1, Width);

            for (int i = 0; i <= (endIndex - startIndex); i++)
                result[i] = this[i + startIndex];

            return result;
        }

        /// <summary>
        /// Get matrix excluding the row specified
        /// </summary>
        /// <param name="index">Index of the row to exclude</param>
        /// <returns>Matrix with the rows, except the specified one</returns>
        public Matrix ExcludeRow(int index)
        {
            Matrix result = new Matrix(Height - 1, Width);
            result.Write(RowSlice(0, index));
            result.Write(RowSlice(index + 1, Height - 1), index);
            return result;
        }

        /// <summary>
        /// Multiply the row with the value
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <param name="value">Multiplier</param>
        /// <returns>Multiplied row values</returns>
        public float[] MultiplyRow(int rowIndex, float value)
        {
            float[] row = GetRow(rowIndex);
            for (int i = 0; i < Width; i++)
                row[i] *= value;
            return row;
        }

        /// <summary>
        /// Swap the rows inside the matrix
        /// </summary>
        /// <param name="firstRow">First row to swap</param>
        /// <param name="secondRow">Second row to swap</param>
        public void SwapRow(int firstRow, int secondRow)
        {
            float[] tmp = this[firstRow];
            this[firstRow] = this[secondRow];
            this[secondRow] = tmp;
        }

        /// <summary>
        /// Normalize the row, by dividing it with the leading variables' coefficient
        /// </summary>
        /// <param name="rowNumber">Number of row to normalize</param>
        public void NormalizeRow(int rowNumber)
        {
            float divider = this[rowNumber, rowNumber];
            for (int i = 0; i < Width; i++)
            {
                this[rowNumber, i] /= divider;
            }
        }

        /// <summary>
        /// Get the maximal distance between the matrices
        /// </summary>
        /// <param name="compareMatrix">Matrix for comparison</param>
        /// <returns>Maximal absolute distance</returns>
        public float MaxDelta(Matrix compareMatrix)
        {
            if (compareMatrix == null ||
                Width != compareMatrix.Width ||
                Height != compareMatrix.Height) return float.NaN;
            float maxDelta = 0;
            for (int i = 0; i < Height; i++)
            {
                float newDelta = Math.Abs(this[i, 0] - compareMatrix[i, 0]);
                maxDelta = newDelta > maxDelta ? newDelta : maxDelta;
            }

            return maxDelta;
        }

        /// <summary>
        /// Calculate the row value using the specified x values from xValues
        /// </summary>
        /// <param name="rowIndex">Number of row, where to substitute x with xValues</param>
        /// <param name="xValues">X values to use</param>
        /// <param name="bMatrix">Left part values</param>
        /// <returns>b - ai1*x1 - ai2*x2 - ai3*x3 ...</returns>
        public float CombineValues(int rowIndex, Matrix xValues, Matrix bMatrix)
        {
            float result = bMatrix[rowIndex, 0];
            for (int i = 0; i < xValues.Height; i++)
            {
                if(i != rowIndex)
                    result -= this[rowIndex, i] * xValues[i, 0];
            }
            result /= this[rowIndex, rowIndex];
            return result;
        }

        /// <summary>
        /// Check for the |A[i, i]| >= sum(j!=i)|Aij|  
        /// </summary>
        /// <returns>True, if A[i, i] is GoE than other elements</returns>
        public bool IsDiagonallyDominant()
        {
            bool dominant = true;
            for (int i = 0; i < Height; i++)
            {
                dominant &= CheckRowDominance(i);
            }

            return dominant;
        }
        
        /// <summary>
        /// Check row for the dominance of the rowIndex located value
        /// </summary>
        /// <param name="rowIndex">Row number</param>
        /// <returns>True, if A[i, i] is GoE than other elements in the row</returns>
        private bool CheckRowDominance(int rowIndex)
        {
            float diagValue = Math.Abs(this[rowIndex, rowIndex]);
            float sum = 0;
            for (int j = 0; j < this.Width; j++)
            {
                if (j != rowIndex)
                    sum += Math.Abs(this[rowIndex, j]);
            }

            return diagValue >= sum;
        }
        

        /// <summary>
        /// Get Manhattan distance value of matrix
        /// </summary>
        /// <returns>Manhattan distance value of matrix</returns>
        public float GetManhattanDistance()
        {
            float maxNorma = float.MinValue;
            for (int x = 0; x < Width; x++)
            {
                float localMax = 0;
                for (int y = 0; y < Height; y++)
                    localMax += this[y, x];
                if (localMax > maxNorma)
                    maxNorma = localMax;
            }

            return maxNorma;
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
        /// Matrix value by value approximately check
        /// </summary>
        /// <param name="compareMatrix">Other matrix</param>
        /// <param name="delta">Epsilon</param>
        /// <returns></returns>
        public bool NearEquals(Matrix compareMatrix, float delta = 0.0001f)
        {
            if (compareMatrix == null ||
                Width != compareMatrix.Width ||
                Height != compareMatrix.Height)
                return false;
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    if (Math.Abs(matrix[y, x] - compareMatrix[y, x]) > delta)
                        return false;
            return true;
        }

        /// <summary>
        /// Get hash code of matrix:
        /// Sum of all matrix value hashes
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

        /// <summary>
        /// To string override
        /// </summary>
        /// <returns>Matrix as string</returns>
        public override string ToString()
        {
            string mString = "[\n";
            for (int y = 0; y < Height; y++)
            {
                mString += " { ";
                for (int x = 0; x < Width; x++)
                {
                    mString += this[y, x] + " ";
                }
                mString += "}\n";
            }
            mString += "]";
            return mString;
        }

        /// <summary>
        /// Get clone of an existing matrix
        /// </summary>
        /// <param name="otherMatrix">Matrix to clone</param>
        /// <returns>New matrix with same values</returns>
        public static Matrix Clone(Matrix otherMatrix)
        {
            var newMatrix = new Matrix(otherMatrix.Height, otherMatrix.Width);
            for (int j = 0; j < otherMatrix.Height; j++)
            {
                newMatrix[j] = (float[]) otherMatrix[j].Clone();
            }

            return newMatrix;
        }
    }
}
