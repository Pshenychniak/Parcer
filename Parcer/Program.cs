// See https://aka.ms/new-console-template for more information
using Aspose.Cells;
using Parcer;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var url = "https://www.swansonvitamins.com/ncat1/Vitamins+and+Supplements/ncat2/Multivitamins/ncat3/Multivitamins+with+Iron/q";

var url1 = "https://www.swansonvitamins.com/ncat1/Vitamins+and+Supplements/ncat2/Multivitamins/ncat3/Multivitamins+with+Iron/q?page=";
string prodList = "";
var products = new List<Product>();
int iter = 1;
int iter2 = 1;

Workbook book = new Workbook();
Worksheet sheet = book.Worksheets[0];

using (var client = new HttpClient())
{
    while (true)
    {
        var test = TryParce1(url1+iter, client);
        if (test.Count != 0)
        {
            products.AddRange(TryParce1(url1 + iter, client));
        }
        else
        {
            break;
        }
        iter++;
    }
    sheet.Cells.ImportCustomObjects((System.Collections.ICollection)products,
    new string[] { "Vendor", "Title", "Number", "Details", "Price" }, // propertyNames
    true, // isPropertyNameShown
    0, // firstRow
    0, // firstColumn
    products.Count, // Number of objects to be exported
    true, // insertRows
    null, // dateFormatString
    false); // convertStringToNumber

    // Save the Excel file
    book.Save("ExportedCustomObjects.xlsx");
    Console.WriteLine("done!");
}

List<Product> TryParce1(string url, HttpClient client)
{
    var html = client.GetStringAsync(url).Result;
    var pattern = @"adobeRecords"":(.+),""topProduct";
    var matches = Regex.Matches(html, pattern);
    if (matches.Count > 0)
    {
        var json = matches[0].Groups[1].Value; 
        return JsonSerializer.Deserialize<List<Product>>(json);
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