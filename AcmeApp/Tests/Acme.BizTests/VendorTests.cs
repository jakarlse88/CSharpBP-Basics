﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
	[TestClass()]
	public class VendorTests
	{
		#region SendWelcomeEmail
		[TestMethod()]
		public void SendWelcomeEmail_ValidCompany_Success()
		{
			// Arrange
			var vendor = new Vendor();
			vendor.CompanyName = "ABC Corp";
			var expected = "Message sent: Hello ABC Corp";

			// Act
			var actual = vendor.SendWelcomeEmail("Test Message");

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void SendWelcomeEmail_EmptyCompany_Success()
		{
			// Arrange
			var vendor = new Vendor();
			vendor.CompanyName = "";
			var expected = "Message sent: Hello";

			// Act
			var actual = vendor.SendWelcomeEmail("Test Message");

			// Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void SendWelcomeEmail_NullCompany_Success()
		{
			// Arrange
			var vendor = new Vendor();
			vendor.CompanyName = null;
			var expected = "Message sent: Hello";

			// Act
			var actual = vendor.SendWelcomeEmail("Test Message");

			// Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region PlaceOrder
		[TestMethod()]
		public void PlaceOrderTest()
		{
			//-- Arrange
			var vendor = new Vendor();
			var product = new Product(1, "Saw", "");
			var expected = new OperationResult<bool>(true,
							"Order from Acme, Inc.\r\nProduct: Tools-1\r\nQuantity: 12" +
							"\r\nInstructions: standard delivery");

			//-- Act
			var actual = vendor.PlaceOrder(product, 12);

			//-- Assert
			Assert.AreEqual(expected.Result, actual.Result);
			Assert.AreEqual(expected.Message, actual.Message);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void PlaceOrder_NullProduct_Exception()
		{
			//-- Arrange
			var vendor = new Vendor();

			//-- Act
			var actual = vendor.PlaceOrder(null, 12);

			//-- Assert
			// Expected exception
		}

		[TestMethod]
		public void PlaceOrder_3Parameters()
		{
			//-- Arrange
			var vendor = new Vendor();
			var product = new Product(1, "Saw", "");
			var expected = new OperationResult<bool>(true,
							"Order from Acme, Inc.\r\nProduct: Tools-1\r\nQuantity: 12" +
							"\r\nDeliver by: 25-Oct-18" +
							"\r\nInstructions: standard delivery");

			//-- Act
			var actual = vendor.PlaceOrder(product, 12,
						new DateTimeOffset(2018, 10, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));

			//-- Assert
			Assert.AreEqual(expected.Result, actual.Result);
			Assert.AreEqual(expected.Message, actual.Message);
		}

		[TestMethod]
		public void PlaceOrder_NoDeliveryDate()
		{
			//-- Arrange
			var vendor = new Vendor();
			var product = new Product(1, "Saw", "");
			var expected = new OperationResult<bool>(true,
							"Order from Acme, Inc.\r\nProduct: Tools-1\r\nQuantity: 12" +
							"\r\nInstructions: Deliver to suite 42");

			//-- Act
			var actual = vendor.PlaceOrder(product, 12, instructions: "Deliver to suite 42");

			//-- Assert
			Assert.AreEqual(expected.Result, actual.Result);
			Assert.AreEqual(expected.Message, actual.Message);
		}

		#endregion

		#region Specifying clear method parameters
		[TestMethod()]
		public void PlaceOrderTest_WithAddress()
		{
			//-- Arrange
			var vendor = new Vendor();
			var product = new Product(1, "Saw", "");
			var expected = new OperationResult<bool>(true, "Test with address");

			//-- Act
			var actual = vendor.PlaceOrder(product, 12,
											Vendor.IncludeAddress.Yes,
											Vendor.SendCopy.No);

			//-- Assert
			Assert.AreEqual(expected.Result, actual.Result);
			Assert.AreEqual(expected.Message, actual.Message);
		}

		#endregion

		#region Handling strings

		[TestMethod()]
		public void ToStringTest()
		{
			//-- Arrange
			var vendor = new Vendor();
			vendor.VendorId = 1;
			vendor.CompanyName = "ABC Corp";
			var expected = "Vendor: ABC Corp (1)";

			//-- Act
			var actual = vendor.ToString();

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void PrepareDirectionsTest()
		{
			//-- Arrange
			var vendor = new Vendor();
			var expected = @"Insert \r\n to define a new line";

			//-- Act
			var actual = vendor.PrepareDirections();
			Console.WriteLine(actual);

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void PrepareDirectionsTwoLinesTest()
		{
			//-- Arrange
			var vendor = new Vendor();
			var expected = "First do this\r\nThen do that";

			//-- Act
			var actual = vendor.PrepareDirectionsTwoLines();
			Console.WriteLine(actual);

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region Generic Collection Interfaces
		[TestMethod()]
		public void SendEmailTest()
		{
			//-- Arrange
			var vendorRepository = new VendorRepository();
			var vendorsCollection = vendorRepository.Retrieve();
		
			var vendorsMaster = vendorRepository.Retrieve();

			var expected = new List<string>()
				{ "Message sent: Important message for: ABC Corp",
				"Message sent: Important message for: XYZ Corp" };
			var vendors = vendorsCollection.ToList();

			//-- Act
			var actual = Vendor.SendEmail(vendors, "Test Message");

			//-- Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void SendEmailTestAdd()
		{
			//-- Arrange
			var vendorRepository = new VendorRepository();
			var vendorsCollection = vendorRepository.Retrieve();
			var expected = new List<string>()
				{ "Message sent: Important message for: ABC Corp",
				"Message sent: Important message for: XYZ Corp" };
			var vendors = vendorsCollection.ToList();

			//-- Act
			var actual = Vendor.SendEmail(vendors, "Test Message");

			//-- Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void SendEmailTestArray()
		{
			//-- Arrange
			var vendorRepository = new VendorRepository();
			var vendorsCollection = vendorRepository.Retrieve();
			var expected = new List<string>()
				{ "Message sent: Important message for: ABC Corp",
				"Message sent: Important message for: XYZ Corp" };
			var vendors = vendorsCollection.ToArray();

			//-- Act
			var actual = Vendor.SendEmail(vendors, "Test Message");

			//-- Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void SendEmailTestDictionary()
		{
			//-- Arrange
			var vendorRepository = new VendorRepository();
			var vendorsCollection = vendorRepository.Retrieve();
			var expected = new List<string>()
				{ "Message sent: Important message for: ABC Corp",
				"Message sent: Important message for: XYZ Corp" };
			var vendors = vendorsCollection.ToDictionary(v => v.CompanyName);

			//-- Act
			var actual = Vendor.SendEmail(vendors.Values, "Test Message");

			//-- Assert
			CollectionAssert.AreEqual(expected, actual);
		}
		#endregion
	}
}