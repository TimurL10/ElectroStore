using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Services
{
    interface IExcelOperation
    {
        public List<string> ReadArticlesFromExcel();

    }
}
