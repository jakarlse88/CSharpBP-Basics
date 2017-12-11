﻿using Acme.Common;
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
	/// <summary>
	/// Manages products carried in inventory. 
	/// </summary>
	public class Product
	{
		public const double InchesPerMeter = 39.37;
		public readonly decimal MinimumPrice;

		#region Constructors
		public Product()
		{
			//this.ProductVendor = new Vendor();
			this.MinimumPrice = .96m;
			Console.WriteLine("Product instance created.");
		}

		public Product(int productId, 
						string productName,
						string description) : this() 
		{
			this.ProductId = productId;
			this.ProductName = productName;
			this.Description = description;
			if (this.ProductName.StartsWith("Bulk"))
			{
				this.MinimumPrice = 9.99m;
			}

			Console.WriteLine("Product instance has a name: " + ProductName);
		}
		#endregion

		#region Properties
		private string productName;

		public string ProductName
		{
			get { return productName; }
			set { productName = value; }
		}

		private string description;

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		private int productId;

		public int ProductId
		{
			get { return productId; }
			set { productId = value; }
		}

		private Vendor productVendor;

		public Vendor ProductVendor
		{
			get
			{
				if (this.productVendor == null)
				{
					productVendor = new Vendor();
				}
				return productVendor;
			}
			set { productVendor = value; }
		}

		private DateTime? availabilityDate;

		public DateTime? AvailabilityDate
		{
			get { return availabilityDate; }
			set { availabilityDate = value; }
		}

		#endregion

		#region Methods
		public string SayHello()
		{
			//var vendor = new Vendor();
			//vendor.SendWelcomeEmail("Message from Product");
			

			var emailService = new EmailService();
			var confirmation = emailService.SendMessage("New Product",
														this.productName,
														"sales@abc.com");

			var result = LogAction("Saying hello");

			return "Hello " + ProductName +
					" (" + ProductId + "): " +
					Description +
					" Available on: " +
					AvailabilityDate?.ToShortDateString();
		}

		#endregion
	}
}
