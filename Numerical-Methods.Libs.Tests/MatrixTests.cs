using NUnit.Framework;
using Numerical_Methods.Libs;
using System;

namespace Numerical_Methods.Libs.Tests
{
	public class MatrixTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void MatrixHeightTest()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.AreEqual(matrixHeight, matrix.Height, "Matrix height is incorrect");
		}

		[Test]
		public void MatrixWidthTest()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.AreEqual(matrixWidth, matrix.Width, "Matrix width is incorrect");
		}

		[Test]
		public void MatrixIsSquareTest()
		{
			var matrixHeight = 4;
			var matrixWidth = 4;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.IsTrue(matrix.IsSquare, $"Matrix with height: {matrix.Height} and width: {matrix.Width} is considered as not square");
		}

		[Test]
		public void MatrixIsNotSquareTest()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.IsFalse(matrix.IsSquare, $"Matrix with height: {matrix.Height} and width: {matrix.Width} is considered as square");
		}

		[Test]
		public void ZeroMatrixHeightTest()
		{
			var matrixHeight = 0;
			var matrixWidth = 0;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.AreEqual(matrixHeight, matrix.Height, "Matrix height is incorrect");
		}

		[Test]
		public void ZeroMatrixWidthTest()
		{
			var matrixHeight = 0;
			var matrixWidth = 0;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.AreEqual(matrixWidth, matrix.Width, "Matrix width is incorrect");
		}

		[Test]
		public void IsZeroMatrixSquareTest()
		{
			var matrixHeight = 0;
			var matrixWidth = 0;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.IsTrue(matrix.IsSquare, $"Matrix with height: {matrix.Height} and width: {matrix.Width} is considered as not square");
		}

		[Test]
		public void GetMatrixValueTest()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var targetRow = 0;
			var targetColumn = 2;
			var targetValue = 5;
			var matrix = new Matrix(matrixHeight, matrixWidth) { [targetRow, targetColumn] = targetValue };
			Assert.AreEqual(targetValue,
				matrix[targetRow, targetColumn],
				"Stored value is incorrect. " +
				$"Saved {targetValue} at [{targetRow}, {targetColumn}], " +
				$"but received {matrix[targetRow, targetColumn]}");
		}

		[Test]
		public void StoreValueOutOfRowRangeTest()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var targetRow = 10;
			var targetColumn = 2;
			var targetValue = 5;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.Catch<IndexOutOfRangeException>(() => matrix[targetColumn, targetRow] = targetValue, $"Value is stored in non-existing row. Available rows: {matrixHeight}, but accessed {targetRow}");
		}

		[Test]
		public void StoreValueOutOfColumnRangeTest()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var targetRow = 2;
			var targetColumn = 10;
			var targetValue = 5;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.Catch<IndexOutOfRangeException>(() => matrix[targetColumn, targetRow] = targetValue, $"Value is stored in non-existing column. Available columns: {matrixWidth}, but accessed {targetColumn}");
		}

		[Test]
		public void StoreValueOutOfAllRangesTest()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var targetRow = 2;
			var targetColumn = 10;
			var targetValue = 5;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.Catch<IndexOutOfRangeException>(() => matrix[targetRow, targetColumn] = targetValue, $"Value is stored in non-existing row and column. Available columns: {matrixWidth}, but accessed {targetColumn}. Available rows: {matrixHeight}, but accessed {targetRow}.");
		}

		[Test]
		public void ConvertMatrixToArrayTest()
		{
			var array = new float[,]
			{
				{ 0, 1, 2, 3 },
				{ 4, 5, 6, 7 },
				{ 8, 9, 10, 11 },
				{ 12, 13, 14, 15 }
			};
			var matrix = new Matrix(array);
			Assert.AreEqual(array, matrix.ToArray());
		}

		[Test]
		public void CreateMatrixFromArrayTest()
		{
			var array = new float[,]
			{
				{ 0, 1, 2, 3 },
				{ 4, 5, 6, 7 },
				{ 8, 9, 10, 11 },
				{ 12, 13, 14, 15 }
			};
			var matrix = new Matrix(array);
			Assert.AreEqual(array, matrix.ToArray());
		}

        [Test]
        public void MatrixMultiplicationTest()
        {
            Matrix matrix1 = new Matrix(new float[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                });
            Matrix matrix2 = new Matrix(new float[,]
                {
                    { 2, 0 },
                    { 1, 2 }
                });

            Matrix result = new Matrix(new float[,]
                {
                    { 4, 4 },
                    { 10, 8 }
                });

            Assert.AreEqual(result, matrix1 * matrix2);
        }

        [Test]
        public void GetRowValidIndexTest()
        {
	        int targetRowIndex = 1;
	        
	        Matrix matrix = new Matrix(new float[,]
	        {
		        { 1, 2, 3, 4 },
		        { 5, 6, 7, 8 },
		        { 9, 10, 11, 12 }
	        });
	        
	        float[] expectedRowData = new float[]
	        {
		        5, 6, 7, 8
	        };
	        
	        Assert.AreEqual(expectedRowData, matrix.GetRow(targetRowIndex));
        }

        [Test]
        public void GetRowIndexWithMaxAbsValueNegativeNumberTest()
        {
	        int expectedRowIndex = 1;
	        int columnIndex = 0;
	        Matrix matrix = new Matrix(new float[,]
	        {
		        { 1, 2, 3, 4 },
		        { -15, 6, 7, 8 },
		        { 9, 10, 11, 12 }
	        });
	        Assert.AreEqual(expectedRowIndex, matrix.GetRowIndexWithMaxAbsValue(columnIndex));
        }
        
        [Test]
        public void GetRowIndexWithMaxAbsValuePositiveNumberTest()
        {
	        int expectedRowIndex = 2;
	        int columnIndex = 0;
	        Matrix matrix = new Matrix(new float[,]
	        {
		        { 1, 2, 3, 4 },
		        { 5, 6, 7, 8 },
		        { 9, 10, 11, 12 }
	        });
	        Assert.AreEqual(expectedRowIndex, matrix.GetRowIndexWithMaxAbsValue(columnIndex));
        }

        [Test]
        public void GetRowIndexWithMaxAbsValueColumnCheckTest()
        {
	        int expectedRowIndex = 1;
	        int columnIndex = 2;
	        Matrix matrix = new Matrix(new float[,]
	        {
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 }
	        });
	        Assert.AreEqual(expectedRowIndex, matrix.GetRowIndexWithMaxAbsValue(columnIndex));
	        
        }

        [Test]
        public void SliceMatrixValidIndexesTest()
        {
	        Matrix matrix = new Matrix(new float[,]
	        {
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 },
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 },
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 },
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 },
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 },
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 }
	        });
	        
	        Matrix expectedMatrix = new Matrix(new float[,]
	        {
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 },
	        });
	        
	        Assert.AreEqual(expectedMatrix, matrix.RowSlice(0, 2));
        }

        [Test]
        public void ExcludeRowTest()
        {
	        int rowIndexToExclude = 0;
	        Matrix matrix = new Matrix(new float[,]
	        {
		        { 1, 2, 3, 4 },
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 },
	        });
	        
	        Matrix expectedResult = new Matrix(new float[,]
	        {
		        { 5, 11, 11, 11 },
		        { 9, 10, 11, 12 },
	        });
	        
	        Assert.AreEqual(expectedResult, matrix.ExcludeRow(rowIndexToExclude));

        }

        [Test]
        public void CombineRowWithValuesTest()
        {
	        int rowIndex = 0;
	        var aMatrix = new Matrix(new float[,]
	        {
		        { 1, 2, 3 }
	        });
	        var bMatrix = new Matrix(new float[,]
	        {
		        { 5 }
	        });
	        var xValues = new Matrix(new float[,]
	        {
		        { 0.5f },
		        { 1 },
		        { 3 }
	        });
	        // (b - a2 * x2 - a3 * x3) / a1
	        float expectedResult = (5f - 2 * 1 - 3 * 3) / 1.0f;

	        var actualResult = aMatrix.CombineValues(rowIndex, xValues, bMatrix);
	        
	        Assert.AreEqual(expectedResult, actualResult, 
		        $"Values are not equal:\nExpected{expectedResult}\nActual:{actualResult}");
        }
    }
}