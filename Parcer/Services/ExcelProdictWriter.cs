﻿using Aspose.Cells;
using Parcer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcer.Services
{
    public class ExcelProdictWriter : IProductWriter
    {
        public void SaveAs(string file, IList<SiteProduct> products)
        {
            using (var book = new Workbook())
            {

                Worksheet sheet = book.Worksheets[0];
                sheet.Cells.ImportCustomObjects((System.Collections.ICollection)products,
                new string[] { "Vendor", "Title", "Number", "Details", "Price", "Status", "Url" }, // propertyNames
                true, // isPropertyNameShown
                0, // firstRow
                0, // firstColumn
                products.Count, // Number of objects to be exported
                true, // insertRows
                null, // dateFormatString
                false); // convertStringToNumber

                // Save the Excel file
                book.Save(file);
            }
        }
        public void SaveAs(string file, IList<Product> products)
        {
            using (var book = new Workbook())
            {

                Worksheet sheet = book.Worksheets[0];
                sheet.Cells.ImportCustomObjects((System.Collections.ICollection)products,
                new string[] { "Code", "Title", "Price", "Available", "Description", "Vendor", "FullUrl" }, // propertyNames
                true, // isPropertyNameShown
                0, // firstRow
                0, // firstColumn
                products.Count, // Number of objects to be exported
                true, // insertRows
                null, // dateFormatString
                false); // convertStringToNumber
                
                // Save the Excel file
                book.Save(file);
            }
        }

        public void SaveOrderList(string file, IList<Product> products)
        {
            using (var book = new Workbook())
            {

                Worksheet sheet = book.Worksheets[0];
                sheet.Cells.ImportCustomObjects((System.Collections.ICollection)products,
                new string[] { "Code", "Title", "Price", "Available", "Description", "Vendor", "FullUrl" }, // propertyNames
                true, // isPropertyNameShown
                0, // firstRow
                0, // firstColumn
                products.Count, // Number of objects to be exported
                true, // insertRows
                null, // dateFormatString
                false); // convertStringToNumber

                int lastRow = products.Count + 2; // Визначте номер останнього рядка з продуктами

                // Обчисліть суму усіх цін продуктів
                decimal totalPrice = products.Sum(p => p.Price);

                // Додайте суму в кінці стовбця "Price"
                sheet.Cells[lastRow, 1].PutValue("TotalPrice:");
                sheet.Cells[lastRow, 2].PutValue(totalPrice);
                // Save the Excel file
                book.Save(file);
            }
        }
    }
}
