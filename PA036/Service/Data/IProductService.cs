﻿using System.Collections.Generic;
using System.Dynamic;
using Service.DTO;
using Shared.Modifiers;

namespace Service.Data
{
    public interface IProductService
    {
        ProductDTO Get(int id);

        IList<ProductDTO> GetAll();

        IList<ProductDTO> Get(ProductModifier modifier);

        ProductDTO Create(ProductDTO product);

        ProductDTO Update(ProductDTO product);

        void Delete(int id);

        int TotalCount();
    }
}