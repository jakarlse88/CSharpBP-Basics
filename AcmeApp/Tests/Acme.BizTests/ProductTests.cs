using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
	[TestClass()]
	public class ProductTests
	{
		#region SayHello
		[TestMethod()]
		public void SayHelloTest()
		{
			//-- Arrange
			var currentProduct = new Product();
			currentProduct.ProductName = "Saw";
			currentProduct.ProductId = 1;
			currentProduct.Description = "15-inch steel blade hand saw";
			currentProduct.ProductVendor.CompanyName = "ABC Corp.";

			var expected = "Hello Saw (1): 15-inch steel blade hand saw" +
							" Available on: ";

			//-- Act
			var actual = currentProduct.SayHello();

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void SayHelloTest_ParameterisedConstructor()
		{
			//-- Arrange
			var currentProduct = new Product(1, "Saw",
			"15-inch steel blade hand saw");

			var expected = "Hello Saw (1): 15-inch steel blade hand saw" +
							" Available on: ";

			//-- Act
			var actual = currentProduct.SayHello();

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void SayHelloTest_ObjectInitialiser()
		{
			//-- Arrange
			var currentProduct = new Product
			{
				ProductId = 1,
				ProductName = "Saw",
				Description = "15-inch steel blade hand saw"
			};

			var expected = "Hello Saw (1): 15-inch steel blade hand saw" +
							" Available on: ";

			//-- Act
			var actual = currentProduct.SayHello();

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region Null-conditional operator
		[TestMethod]
		public void Product_Null()
		{
			//-- Arrange
			Product currentProduct = null;
			var companyName = currentProduct?.ProductVendor?.CompanyName;

			string expected = null;

			//-- Act
			var actual = companyName;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region Constant field
		[TestMethod]
		public void ConvertMetersToInchesTest()
		{
			//-- Arrange
			var expected = 78.74;

			//-- Act
			var actual = 2 * Product.InchesPerMeter;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region Read-only field
		[TestMethod]
		public void MinimumPriceTest_Default()
		{
			//-- Arrange
			var currentProduct = new Product();
			var expected = .96m;

			//-- Act
			var actual = currentProduct.MinimumPrice;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void MinimumPriceTest_Bulk()
		{
			//-- Arrange
			var currentProduct = new Product(1, "Bulk Tools", "");
			var expected = 9.99m;

			//-- Act
			var actual = currentProduct.MinimumPrice;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region ProductName
		[TestMethod]
		public void ProductName_Format()
		{
			//-- Arrange
			var currentProduct = new Product();
			currentProduct.ProductName = "  Steel Hammer  ";

			var expected = "Steel Hammer";

			//-- Act
			var actual = currentProduct.ProductName;

			//-- Assert 
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ProductName_TooShort()
		{
			//-- Arrange
			var currentProduct = new Product();
			currentProduct.ProductName = "aw";

			string expected = null;
			string expectedMessage = "Product name must be at least 3 characters long";

			//-- Act
			var actual = currentProduct.ProductName;
			var actualMessage = currentProduct.ValidationMessage;

			Assert.AreEqual(expected, actual);
			Assert.AreEqual(expectedMessage, actualMessage);
		}

		[TestMethod]
		public void ProductName_TooLong()
		{
			//-- Arrange
			var currentProduct = new Product();
			currentProduct.ProductName = "awawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawawaw";

			string expected = null;
			string expectedMessage = "Product name cannot exceed 20 characters";

			//-- Act
			var actual = currentProduct.ProductName;
			var actualMessage = currentProduct.ValidationMessage;

			Assert.AreEqual(expected, actual);
			Assert.AreEqual(expectedMessage, actualMessage);
		}

		[TestMethod]
		public void ProductName_JustRight()
		{
			//-- Arrange
			var currentProduct = new Product();
			currentProduct.ProductName = "Saw";

			string expected = "Saw";
			string expectedMessage = null;

			//-- Act
			var actual = currentProduct.ProductName;
			var actualMessage = currentProduct.ValidationMessage;

			Assert.AreEqual(expected, actual);
			Assert.AreEqual(expectedMessage, actualMessage);
		}
		#endregion

		#region Auto-implemented properties/Prop. initialisation
		[TestMethod]
		public void Category_DefaultValue()
		{
			//-- Arrange
			var currentProduct = new Product();
			var expected = "Tools";

			//-- Act
			var actual = currentProduct.Category;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Category_NewValue()
		{
			//-- Arrange
			var currentProduct = new Product();
			currentProduct.Category = "Garden";

			var expected = "Garden";

			//-- Act
			var actual = currentProduct.Category;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Sequence_DefaultValue()
		{
			//-- Arrange
			var currentProduct = new Product();

			var expected = 1;

			//-- Act
			var actual = currentProduct.SequenceNumber;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Sequence_NewValue()
		{
			//-- Arrange
			var currentProduct = new Product();
			currentProduct.SequenceNumber = 2;

			var expected = 2;

			//-- Act
			var actual = currentProduct.SequenceNumber;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region Expression-bodied property
		[TestMethod]
		public void ProductCode_DefaultValue()
		{
			//-- Arrange
			var currentProduct = new Product();
			var expected = "Tools-1";

			//-- Act
			var actual = currentProduct.ProductCode;

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		[TestMethod()]
		public void CalculateSuggestedPriceTest()
		{
			//-- Arrange
			var currentProduct = new Product(1, "Saw", "");
			currentProduct.Cost = 50m;
			var expected = 55m;

			//-- Act
			var actual = currentProduct.CalculateSuggestedPrice(10m);

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
	}
}