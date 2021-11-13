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

        public void BuildExcelFileWithPrices(List<Remains> remains)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                worksheet.Cells["A1"].Value = "ID товара";
                worksheet.Cells["B1"].Value = "Название товара";
                worksheet.Cells["C1"].Value = "Название товара в URL";
                worksheet.Cells["D1"].Value = "URL";
                worksheet.Cells["E1"].Value = "Краткое описание";
                worksheet.Cells["F1"].Value = "Полное описание";
                worksheet.Cells["G1"].Value = "Видимость на витрине";
                worksheet.Cells["H1"].Value = "Применять скидки";
                worksheet.Cells["I1"].Value = "Тег title";
                worksheet.Cells["J1"].Value = "Мета-тег keywords";
                worksheet.Cells["K1"].Value = "Мета-тег description";
                worksheet.Cells["L1"].Value = "Размещение на сайте";
                worksheet.Cells["M1"].Value = "Весовой коэффициент";
                worksheet.Cells["N1"].Value = "Валюта склада";
                worksheet.Cells["O1"].Value = "НДС";
                worksheet.Cells["P1"].Value = "Единица измерения";
                worksheet.Cells["Q1"].Value = "Габариты"; //17
                worksheet.Cells["R1"].Value = "Изображения";
                worksheet.Cells["S1"].Value = "Свойство: Размер";
                worksheet.Cells["T1"].Value = "ID варианта";
                worksheet.Cells["U1"].Value = "Артикул";
                worksheet.Cells["V1"].Value = "Штрих-код";
                worksheet.Cells["W1"].Value = "Габариты варианта";
                worksheet.Cells["X1"].Value = "Цена продажи";
                worksheet.Cells["Y1"].Value = "Старая цена";
                worksheet.Cells["Z1"].Value = "Цена закупки";
                worksheet.Cells["AA1"].Value = "Остаток";
                worksheet.Cells["AB1"].Value = "Вес";
                worksheet.Cells["AC1"].Value = "Изображения варианта";
                worksheet.Cells["AD1"].Value = "Параметр: Метка";
                worksheet.Cells["AE1"].Value = "Параметр: Характеристика 1";
                worksheet.Cells["AF1"].Value = "Параметр: Характеристика 3";
                worksheet.Cells["AG1"].Value = "Параметр: Категория Яндекс Маркета";
                worksheet.Cells["AH1"].Value = "Параметр: Категория товара в Google";
                worksheet.Cells["AI1"].Value = "Параметр: Категория товара в OZON";

                //worksheet.Cells["A1"].LoadFromCollection(remains);

                for (int i = 0, k = 2; i <= remains.Count-1; i ++, k ++)
                {
                    for (int j = 1; j <= 1; j++)
                    {
                        worksheet.Cells[k, 1].Value = remains[i].Id;
                        worksheet.Cells[k, 2].Value = remains[i].ItemName;
                        worksheet.Cells[k, 3].Value = remains[i].ItemNameInURL;
                        worksheet.Cells[k, 4].Value = remains[i].URL;
                        worksheet.Cells[k, 5].Value = remains[i].ShortDescription;
                        worksheet.Cells[k, 6].Value = remains[i].FullDescription;
                        worksheet.Cells[k, 7].Value = remains[i].Visibility;
                        worksheet.Cells[k, 8].Value = remains[i].Discount;
                        worksheet.Cells[k, 9].Value = remains[i].TegTitle;
                        worksheet.Cells[k, 10].Value = remains[i].MetaTegKeywords;
                        worksheet.Cells[k, 11].Value = remains[i].MetaTegDescription;
                        worksheet.Cells[k, 12].Value = remains[i].CategoryInSite;
                        worksheet.Cells[k, 13].Value = remains[i].WeightCoefficient;
                        worksheet.Cells[k, 14].Value = remains[i].Currancy;
                        worksheet.Cells[k, 15].Value = remains[i].NDS;
                        worksheet.Cells[k, 16].Value = "";
                        worksheet.Cells[k, 17].Value = remains[i].Gabarit;
                        worksheet.Cells[k, 18].Value = remains[i].ImageURL;
                        worksheet.Cells[k, 19].Value = "";
                        worksheet.Cells[k, 20].Value = "";
                        worksheet.Cells[k, 21].Value = remains[i].VendorCode;
                        worksheet.Cells[k, 22].Value = "";
                        worksheet.Cells[k, 23].Value = "";
                        worksheet.Cells[k, 24].Value = remains[i].PriceRetail; 
                        worksheet.Cells[k, 25].Value = "";
                        worksheet.Cells[k, 26].Value = "";
                        worksheet.Cells[k, 27].Value = remains[i].Remain;
                        worksheet.Cells[k, 28].Value = remains[i].Weight;
                        worksheet.Cells[k, 28].Value = "";
                        worksheet.Cells[k, 29].Value = "";
                        worksheet.Cells[k, 30].Value = remains[i].Param1;
                        worksheet.Cells[k, 31].Value = remains[i].Param2; 
                        worksheet.Cells[k, 32].Value = remains[i].Param3; 
                        worksheet.Cells[k, 33].Value = "";
                        worksheet.Cells[k, 34].Value = "";
                        worksheet.Cells[k, 35].Value = "";
                    }
                }

                FileInfo excelFile = new FileInfo($@"C:\Users\Timur\source\repos\ElectroStore\Prices\Prices2021.xlsx");
                excelPackage.SaveAs(excelFile);
            }
        }
    }

}

