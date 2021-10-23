using ElectroStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.DAL
{
    public interface IDbRepository
    {
        public void InsertNomenclature(string nomenclatureObj);
        public void InsertPrice(string price);
    }
}
