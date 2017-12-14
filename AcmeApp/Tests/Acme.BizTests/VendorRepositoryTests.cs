﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
	[TestClass()]
	public class VendorRepositoryTests
	{
		#region RetrieveValue()
		[TestMethod()]
		public void RetrieveValueTest()
		{
			//-- Arrange
			var repository = new VendorRepository();
			var expected = 42;

			//-- Act
			var actual = repository.RetrieveValue<int>("Select...", 42);

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void RetrieveValueStringTest()
		{
			//-- Arrange
			var repository = new VendorRepository();
			var expected = "test";

			//-- Act
			var actual = repository.RetrieveValue<string>("Select...", "test");

			//-- Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RetrieveValueObjectTest()
		{
			//-- Arrange
			var repository = new VendorRepository();
			var vendor = new Vendor();
			var expected = vendor;

			//-- Act
			var actual = repository.RetrieveValue<Vendor>("Select...", vendor);

			//-- Assert
			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region Retrieve()
		[TestMethod()]
		public void RetrieveTest()
		{
			//-- Arrange
			var repository = new VendorRepository();

			var expected = new List<Vendor>()
			{
				new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" },
				new Vendor() { VendorId = 2, CompanyName = "XYZ Corp", Email = "xyz@xyz.com" }
			};

			//-- Act
			var actual = repository.Retrieve();

			//-- Assert
			CollectionAssert.AreEqual(expected, actual);
		}
		#endregion

		[TestMethod()]
		public void RetrieveWithKeysTest()
		{
			//-- Arrange
			var repository = new VendorRepository();
			var expected = new Dictionary<string, Vendor>()
			{
				{ "ABC Corp", new Vendor()
					{ VendorId = 5, CompanyName = "ABC Corp", Email = "abc@abc.com" } },
				{ "XYZ Corp", new Vendor()
					{ VendorId = 8, CompanyName = "XYZ Corp", Email = "xyz@xyz.com" } }
			};

			//-- Act
			var actual = repository.RetrieveWithKeys();

			//-- Assert
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}