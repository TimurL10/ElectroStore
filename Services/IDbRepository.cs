﻿using ElectroStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroStore.Services
{
    public interface IDbRepository
    {
        //public void InsertRemain(Remains remains);
       // public List<GetIdByArticles> GetRemainsForPrices();
        public void Dal_UpdateWithNewElevelids(string elevel_ids);
        public List<string> GetArticles();

    }
}
