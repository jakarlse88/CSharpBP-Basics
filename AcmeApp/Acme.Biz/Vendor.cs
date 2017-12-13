using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
		#region Enums
		public enum IncludeAddress { Yes, No }
		public enum SendCopy { Yes, No }
		#endregion

		#region Properties
		public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
		#endregion

		#region Methods

		/// <summary>
		/// Sends an email to welcome a new vendor.
		/// </summary>
		/// <returns></returns>
		public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }

		/// <summary>
		/// Places a single product order with the vendor.
		/// </summary>
		/// <param name="product">Product to be ordered.</param>
		/// <param name="quantity">Quantity of products to be ordered.</param>
		/// <param name="deliverBy">Requested delivery dates.</param>
		/// <param name="instructions">Additional delivery instructions.</param>
		/// <returns></returns>
		public OperationResult<bool> PlaceOrder(Product product, int quantity,
										DateTimeOffset? deliverBy = null, 
										string instructions = "standard delivery")
		{
			if (product == null) throw new ArgumentNullException(nameof(product));
			if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
			if (deliverBy <= DateTimeOffset.Now) throw new ArgumentOutOfRangeException(nameof(deliverBy));

			var success = false;

			var orderTextBuilder = new StringBuilder("Order from Acme, Inc." + System.Environment.NewLine +
							"Product: " + product.ProductCode + System.Environment.NewLine +
							"Quantity: " + quantity);

			if (deliverBy.HasValue)
			{
				orderTextBuilder.Append(System.Environment.NewLine +
							"Deliver by: " + deliverBy.Value.ToString("d"));
			}

			if (!String.IsNullOrWhiteSpace(instructions))
			{
				orderTextBuilder.Append(System.Environment.NewLine +
							"Instructions: " + instructions);
			}

			var orderText = orderTextBuilder.ToString();
			var emailService = new EmailService();
			var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

			if (confirmation.StartsWith("Message sent:"))
			{
				success = true;
			}

			return new OperationResult<bool>(success, orderText);
		}

		/// <summary>
		/// Places a single product order with the vendor. 
		/// </summary>
		/// <param name="product">Product to be ordered.</param>
		/// <param name="quantity">Quantity of products to be ordered.</param>
		/// <param name="includeAddress">True to include the shipping address address.</param>
		/// <param name="sendCopy">True to send a copy of the email to the current customer.</param>
		/// <returns>Success flag and order text.</returns>
		public OperationResult<bool> PlaceOrder(Product product, int quantity,
											IncludeAddress includeAddress, SendCopy sendCopy)
		{
			var orderText = "Test";
			if (includeAddress == IncludeAddress.Yes) orderText += " with address";
			if (sendCopy == SendCopy.Yes) orderText += " with copy";

			return new OperationResult<bool>(true, orderText);
		}
		
		public override string ToString()
		{
			return $"Vendor: {this.CompanyName} ({this.VendorId})";
		}

		public string PrepareDirections()
		{
			var directions = @"Insert \r\n to define a new line";
			return directions;
		}

		public string PrepareDirectionsTwoLines()
		{
			var directions = "First do this" + Environment.NewLine +
							"Then do that";
			return directions;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || this.GetType() != obj.GetType())
			{
				return false;
			}

			Vendor compareVendor = obj as Vendor;
			if (compareVendor!= null && 
				this.VendorId == compareVendor.VendorId &&
				this.CompanyName == compareVendor.CompanyName &&
				this.Email == compareVendor.Email)
			{
				return true;
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return this.VendorId.GetHashCode() ^ this.CompanyName.GetHashCode() ^ this.Email.GetHashCode();
		}
		#endregion
	}
}
