using ElectroStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Services
{
    interface IExcelOperation
    {
        public List<string> ReadArticlesFromExcel();
        public void BuildExcelFileWithPrices(List<Remains> remains);

    }
}
