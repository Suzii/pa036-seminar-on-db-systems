﻿using DataAccess.Model;
using Shared.Filters;

namespace DataAccess.Data
{
    public interface IProducts : ICrud<Product, ProductFilter>
    {
    }
}
