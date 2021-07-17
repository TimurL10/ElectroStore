using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ElectroStore.Services;


namespace ElectroStore.Models
{
    public class ExcelOperations : IExcelOperation
    {
        public List<string> ReadArticlesFromExcel()
        {
            List<string> excelData = new List<string>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DirectoryInfo di = new DirectoryInfo(@"C:\Users\Timur\source\repos\ElectroStore\Files\");
            FileInfo[] files = di.GetFiles("*.xlsx");

            byte[] bin = System.IO.File.ReadAllBytes(files.First().FullName);

            //create a new Excel package in a memorystream
            using (MemoryStream stream = new MemoryStream(bin))
            using (ExcelPackage excelPackage = new ExcelPackage(stream))
            {
                //loop all worksheets
                foreach (ExcelWorksheet worksheet in excelPackage.Workbook.Worksheets)
                {                    
                    //loop all rows
                    for (int i = worksheet.Dimension.Start.Row; i <= worksheet.Dimension.End.Row; i++)
                    {
                        //loop all columns in a row
                        for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
                        {
                            //add the cell data to the List
                            if (worksheet.Cells != null)
                            {
                                //if (worksheet.Cells["A"])
                                excelData.Add(worksheet.Cells[i, j].Value.ToString());
                            }
                        }
                    }
                }
            }
            return excelData;
        }

        public struct ArticlesJson
        {
            public List<string> articles;
            public string typeOfSearch;
        }
    }
}
