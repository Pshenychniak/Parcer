using Parcer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcer.Services
{
    public  interface IProductWriter
    {
        public void SaveAs(string file, IList<Product> products);
        public void SaveAs(string file,IList<SiteProduct> products);
        

    }
}
