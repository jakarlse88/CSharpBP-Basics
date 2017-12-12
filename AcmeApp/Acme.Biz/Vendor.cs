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
		public OperationResult PlaceOrder(Product product, int quantity,
										DateTimeOffset? deliverBy = null, 
										string instructions = "standard delivery")
		{
			if (product == null) throw new ArgumentNullException(nameof(product));
			if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
			if (deliverBy <= DateTimeOffset.Now) throw new ArgumentOutOfRangeException(nameof(deliverBy));

			var success = false;

			var orderText = "Order from Acme, Inc." + System.Environment.NewLine +
							"Product: " + product.ProductCode + System.Environment.NewLine +
							"Quantity: " + quantity;
			if (deliverBy.HasValue)
			{
				orderText += System.Environment.NewLine +
							"Deliver by: " + deliverBy.Value.ToString("d");
			}

			if (!String.IsNullOrWhiteSpace(instructions))
			{
				orderText += System.Environment.NewLine +
							"Instructions: " + instructions;
			}

			var emailService = new EmailService();
			var confirmation = emailService.SendMessage("New Order", orderText, this.Email);

			if (confirmation.StartsWith("Message sent:"))
			{
				success = true;
			}

			return new OperationResult(success, orderText);
		}

		/// <summary>
		/// Places a single product order with the vendor. 
		/// </summary>
		/// <param name="product">Product to be ordered.</param>
		/// <param name="quantity">Quantity of products to be ordered.</param>
		/// <param name="includeAddress">True to include the shipping address address.</param>
		/// <param name="sendCopy">True to send a copy of the email to the current customer.</param>
		/// <returns>Success flag and order text.</returns>
		public OperationResult PlaceOrder(Product product, int quantity,
											IncludeAddress includeAddress, SendCopy sendCopy)
		{
			var orderText = "Test";
			if (includeAddress == IncludeAddress.Yes) orderText += " with address";
			if (sendCopy == SendCopy.Yes) orderText += " with copy";

			return new OperationResult(true, orderText);
		}

		#endregion
	}
}
