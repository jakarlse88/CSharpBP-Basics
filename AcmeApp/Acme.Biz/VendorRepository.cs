using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    public class VendorRepository
    {
		private List<Vendor> vendors;
        /// <summary>
		/// Retrieves one vendor.
		/// </summary>
		/// <param name="vendorId">ID of the vendor to retrieve.</param>
		/// <returns>Returns the specified vendor.</returns>
        public Vendor Retrieve(int vendorId)
        {
            // Create the instance of the Vendor class
            Vendor vendor = new Vendor();

            // Code that retrieves the defined customer

            // Temporary hard coded values to return 
            if (vendorId == 1)
            {
                vendor.VendorId = 1;
                vendor.CompanyName = "ABC Corp";
                vendor.Email = "abc@abc.com";
            }
            return vendor;
        }

		public List<Vendor> Retrieve()
		{
			if (vendors == null)
			{
				vendors = new List<Vendor>()
				{
				new Vendor() { VendorId = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" },
				new Vendor() { VendorId = 2, CompanyName = "XYZ Corp", Email = "xyz@xyz.com" }
				};
			}
			Console.WriteLine(vendors[0]);

			return vendors;
		}

		/// <summary>
		/// Retrieves all approved vendors. 
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, Vendor> RetrieveWithKeys()
		{
			var vendors = new Dictionary<string, Vendor>()
			{
				{ "ABC Corp", new Vendor()
					{ VendorId = 5, CompanyName = "ABC Corp", Email = "abc@abc.com" } },
				{ "XYZ Corp", new Vendor()
					{ VendorId = 8, CompanyName = "XYZ Corp", Email = "xyz@xyz.com" } }
			};

			foreach (var element in vendors)
			{
				var vendor = element.Value;
				var key = element.Key;
				Console.WriteLine($"Key: {key} Value: {vendor}");
			}
			
			return vendors;
		}

        public bool Save(Vendor vendor)
        {
            var success = true;

            // Code that saves the vendor

            return success;
        }

		public T RetrieveValue<T>(string sql, T defaultValue)
		{
			// Call the database to retrieve the value
			// If no value is returned, return the default value
			T value = defaultValue;
			return value;
		}
    }
}
