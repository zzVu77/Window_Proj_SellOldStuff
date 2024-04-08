﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class PurchasedItem_DAO
    {
        new DB_Connection db = new DB_Connection();
        public List<purchasedItem> Load () // More descriptive method name
        {
            return db.LoadData<purchasedItem>("PurchasedProducts");
        }

        public void AddItem(purchasedItem item) // Using PascalCase for method name
        {
            string sqlQuery = @"
            INSERT INTO [dbo].[PurchasedProducts] 
                (Id_user, Item_Id)
            VALUES
                (@userId, @itemId)";
            new DB_Connection().ExecuteNonQuery(sqlQuery,
                new SqlParameter("@userId", item.Id_user),
                new SqlParameter("@itemId", item.Item_Id)
                );
        }
    }
}
