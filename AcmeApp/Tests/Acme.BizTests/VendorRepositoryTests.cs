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
			CollectionAssert.AreEqual(expected, actual.ToList());
		}

		[TestMethod()]
		public void RetrieveWithIteratorTest()
		{
			//-- Arrange
			var repository = new VendorRepository();
			var expected = new List<Vendor>()
			{
				{ new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" } },
				{ new Vendor() { VendorId = 2, CompanyName = "XYZ Corp", Email = "xyz@xyz.com"} }
			};

			//-- Act
			var vendorIterator = repository.RetrieveWithIterator();
			foreach (var item in vendorIterator)
			{
				Console.WriteLine(item);
			}

			var actual = vendorIterator.ToList();

			//-- Assert
			CollectionAssert.AreEqual(expected, actual);
		}
		#endregion

		#region LINQ
		[TestMethod()]
		public void RetrieveAllTest()
		{
			//-- Arrange
			var repository = new VendorRepository();
			var expected = new List<Vendor>()
			{
				{ new Vendor() { VendorId = 22, CompanyName = "Amalgamated Toys", Email = "a@abc.com"} },
				{ new Vendor() { VendorId = 35, CompanyName = "Car Toys", Email = "car@abc.com"} },
				{ new Vendor() { VendorId = 28, CompanyName = "Toy Blocks Inc", Email = "blocks@abc.com"} },
				{ new Vendor() { VendorId = 42, CompanyName = "Toys for Fun", Email = "fun@abc.com"} }
			};

			//-- Act
			var vendors = repository.RetrieveAll();
			var vendorQuery = vendors
								.Where(v => v.CompanyName.Contains("Toy"))
								.OrderBy(v => v.CompanyName);
			//-- Assert
			CollectionAssert.AreEqual(expected, vendorQuery.ToList()); 
		}
		#endregion
	}
}