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
		public void IsZeroMatrixSquare_Test()
		{
			var matrixHeight = 0;
			var matrixWidth = 0;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.IsTrue(matrix.IsSquare, $"Matrix with height: {matrix.Height} and width: {matrix.Width} is considered as not square");
		}

		[Test]
		public void GetMatrixValue_Test()
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
		public void StoreValueOutOfRowRange_Test()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var targetRow = 10;
			var targetColumn = 2;
			var targetValue = 5;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.Catch<IndexOutOfRangeException>(() => matrix[targetRow, targetColumn] = targetValue, $"Value is stored in non-existing row. Available rows: {matrixHeight}, but accessed {targetRow}");
		}

		[Test]
		public void StoreValueOutOfColumnRange_Test()
		{
			var matrixHeight = 4;
			var matrixWidth = 3;
			var targetRow = 2;
			var targetColumn = 10;
			var targetValue = 5;
			var matrix = new Matrix(matrixHeight, matrixWidth);
			Assert.Catch<IndexOutOfRangeException>(() => matrix[targetRow, targetColumn] = targetValue, $"Value is stored in non-existing column. Available columns: {matrixWidth}, but accessed {targetColumn}");
		}

		[Test]
		public void StoreValueOutOfAllRanges_Test()
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
		public void ConvertMatrixToArray_Test()
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
		public void CreateMatrixFromArray_Test()
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
	}
}