// See https://aka.ms/new-console-template for more information
using Aspose.Cells;
using Parcer;
using Parcer.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var url1 = "https://www.swansonvitamins.com/ncat1/Vitamins+and+Supplements/ncat2/Multivitamins/ncat3/Multivitamins+with+Iron/q?page=";
var products = new List<SiteProduct>();
int iter = 1;

Workbook book = new Workbook();
Worksheet sheet = book.Worksheets[0];

using (var client = new HttpClient())
{
    while (true)
    {
        var test = TryParce1(url1+iter, client);
        if (test.Count != 0)
        {
            Console.WriteLine("Page "+iter);
            products.AddRange(test);
        }
        else
        {
            break;
        }
        iter++;
    }
    sheet.Cells.ImportCustomObjects((System.Collections.ICollection)products,
    new string[] { "Vendor", "Title", "Number", "Details", "Price", "Status","Url" }, // propertyNames
    true, // isPropertyNameShown
    0, // firstRow
    0, // firstColumn
    products.Count, // Number of objects to be exported
    true, // insertRows
    null, // dateFormatString
    false); // convertStringToNumber

    // Save the Excel file
    book.Save("test1.xlsx");
    Console.WriteLine("done!");
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


//string TryParce(string url, HttpClient client)
//{
//    string temp = "";
//    var html = client.GetStringAsync(url).Result;
//    var pattern = @"adobeRecords"":(.+),""topProduct";
//    var matches = Regex.Matches(html, pattern);
//    if (matches.Count > 0)
//    {
//        var json = matches[0].Groups[1].Value;
//        var products = JsonSerializer.Deserialize<List<Product>>(json);
//        products.ForEach(p =>
//        {
//            temp += iter2+". "+ p.Vendor+p.Details+p.Title+p.Price + "\n";
//            iter2++;
//        });
//        return temp;
//    }
//    return null;
//}


using (var db = new ProductContext())
{
    products.ForEach(p =>
    {
        var dbProduct = db.Products.FirstOrDefault(x => x.Code == p.Number);
        if (dbProduct == null)
        {
            dbProduct = new Product();
            dbProduct.CreateAT = DateTime.Now;
            db.Add(dbProduct);
        }
        dbProduct.Code= p.Number;
        dbProduct.Title = p.Title;
        dbProduct.Vendor = p.Vendor;
        dbProduct.Description = p.Details;
        dbProduct.Price = Decimal.Parse(p.Price, CultureInfo.InvariantCulture);
        dbProduct.UpdateAT = DateTime.Now;
        dbProduct.Available = p.Status == "In stock";
        dbProduct.FullUrl = p.FullUrl;
        dbProduct.ImgUrl = p.ImagelUrl;
    });
    db.SaveChanges();
}