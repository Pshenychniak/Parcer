// See https://aka.ms/new-console-template for more information
using Aspose.Cells;
using Parcer;
using Parcer.Models;
using Parcer.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");

var url1 = "https://www.swansonvitamins.com/q?page=";
var parser = new CatalogParser() { Page=1};
//var products = parser.Parse(url1);

//IProductWriter writer = new ExcelProdictWriter();
//writer.SaveAs("products123.xlsx", products);

using (var db = new ProductContext())
{

    var storage = new ProductStorage(db);
    while (parser.HasMore)
    {
        var products=parser.Parse(url1);
        storage.Update(products);
        Thread.Sleep(3000);
    }

    
}