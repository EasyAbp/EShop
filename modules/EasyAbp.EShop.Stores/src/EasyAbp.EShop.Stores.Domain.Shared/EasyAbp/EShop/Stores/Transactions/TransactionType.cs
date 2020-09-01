﻿using System;

 namespace EasyAbp.EShop.Stores.Transactions
{
    [Flags]
    public enum TransactionType
    {
        Debit = 1,
        Credit = 2
    }
}