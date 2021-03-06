﻿using Service.DTO;
using Shared.Filters;

namespace Service.Data
{
    public interface IProductService: IService<ProductDTO, ProductFilter>
    {
    }
}
