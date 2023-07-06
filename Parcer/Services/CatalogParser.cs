using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parcer.Services
{
    public  class CatalogParser
    {
        public int Page { get; set; }
        private bool _hasMore = true;
        public bool HasMore => _hasMore;
        public List<SiteProduct> Parse(string catalogUrl)
        {
            var products = new List<SiteProduct>();
            //int iter = 1;
            //while (true)
            //{
                using (var client = new HttpClient())
                {
                    var test = TryParce1(catalogUrl + Page, client);
                    if (test.Count != 0)
                    {
                    Console.WriteLine("Page " + Page+" - " + $"{test.Count}");
                        products.AddRange(test);
                    }
                    else
                    {
                        Console.WriteLine("done!");
                        _hasMore = false;
                        //break;
                    }
                Page++;
                }
                
            //}
            
            return products;
        }
        List<SiteProduct> TryParce1(string url, HttpClient client)
        {
            var html = client.GetStringAsync(url).Result;
            var pattern = @"adobeRecords"":(.+),""topProduct";
            var matches = Regex.Matches(html, pattern);
            if (matches.Count > 0)
            {
                var json = matches[0].Groups[1].Value;
                return JsonSerializer.Deserialize<List<SiteProduct>>(json);
            }
            return null;
        }
    }
}
