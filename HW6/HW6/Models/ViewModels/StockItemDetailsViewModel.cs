using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW6.DAL;

namespace HW6.Models.ViewModels
{
    public class Purchasers
    {
        public Purchasers (string name, int qty)
        {
            PurchaserName = name;
            PurchaseQty = qty;
        }
        public string PurchaserName { get; private set; }
        public int PurchaseQty { get; private set; }
    }
    public class StockItemDetailsViewModel
    {
        private WorldWideImportersContext db = new WorldWideImportersContext();
        public StockItemDetailsViewModel(StockItem stockItem)
        {
            Supplier ItemSupplier = db.Suppliers.Find(stockItem.SupplierID);
            Person SupplierContact = db.People.Find(ItemSupplier.PrimaryContactPersonID);
            IEnumerable<InvoiceLine> ItemInvoiceLines = db.InvoiceLines.Where(x => x.StockItemID == stockItem.StockItemID).ToList();
            IEnumerable<Customer> ItemCustomers = new List<Customer>();
            foreach (var invoice in ItemInvoiceLines)
            {
                Customer cust = db.Customers.Find(invoice.Invoice.CustomerID);
                ItemCustomers.Append(cust);
            }

            ItemID = stockItem.StockItemID;
            Name = stockItem.StockItemName;
            Size = stockItem.Size;
            Price = stockItem.UnitPrice;
            Weight = stockItem.TypicalWeightPerUnit;
            LeadTime = stockItem.LeadTimeDays;
            ValidSince = stockItem.ValidFrom;
            Origin = stockItem.CustomFields;
            //parse Origin
            Tags = stockItem.Tags;
            
            SupplierName = ItemSupplier.SupplierName;
            SupplierPhone = ItemSupplier.PhoneNumber;
            SupplierFax = ItemSupplier.FaxNumber;
            SupplierWebsite = ItemSupplier.WebsiteURL;
            SupplierContactName = SupplierContact.FullName;

            foreach (var invoice in ItemInvoiceLines)
            {
                Orders += 1;
                GrossSales += invoice.ExtendedPrice;
                GrossProfits += invoice.LineProfit;
            }

            TopPurchasers = ItemCustomers.Take(10); 
        }
        public int ItemID { get; private set; }
        public string Name { get; private set; }
        public string Size { get; private set; }
        public decimal Price { get; private set; }
        public decimal Weight { get; private set; }
        public int LeadTime { get; private set; }
        public DateTime ValidSince { get; private set; }
        public string Origin { get; private set; }
        public string Tags { get; private set; }

        //supplier Info
        public int SupplierID { get; private set; }
        public Supplier ItemSupplier { get; private set; }
        public string SupplierName { get; private set; }
        public string SupplierPhone { get; private set; }
        public string SupplierFax { get; private set; }
        public string SupplierWebsite { get; private set; }
        public int SupplierContactID { get; private set; }
        public Person SupplierContact { get; private set; }
        public string SupplierContactName { get; private set; }

        //Sales History Summary
        public int Orders { get; private set; }
        public decimal GrossSales { get; private set; }
        public decimal GrossProfits { get; private set; }

        //Top Purchasers
        public IEnumerable<Customer> TopPurchasers {get; private set;}
    }
}