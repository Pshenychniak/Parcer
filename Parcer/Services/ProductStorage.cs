using Parcer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcer.Services
{
    public class ProductStorage
    {
        private ProductContext _context;
        public ProductStorage(ProductContext context)
        {
            _context = context;
        }
        public Product? FindByCode(string code)
        {
            return _context.Products.FirstOrDefault(x => x.Code == code);
        }
        public void Update(List<SiteProduct> products)
        {
            products.ForEach(p =>
            {
                var dbProduct = FindByCode( p.Number);
                if (dbProduct == null)
                {
                    dbProduct = new Product();
                    dbProduct.CreateAT = DateTime.Now;
                    _context.Add(dbProduct);
                }
                dbProduct.Code = p.Number;
                dbProduct.Title = p.Title;
                dbProduct.Vendor = p.Vendor;
                dbProduct.Description = p.Details;
                dbProduct.Price = Decimal.Parse(p.Price, CultureInfo.InvariantCulture);
                dbProduct.UpdateAT = DateTime.Now;
                dbProduct.Available = p.Status == "In stock";
                dbProduct.FullUrl = p.FullUrl;
                dbProduct.ImgUrl = p.ImagelUrl;
            });
            _context.SaveChanges();
        }
    }
}
