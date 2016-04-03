﻿using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Linq;

namespace Service.Data
{
    public class ProductService: IProductService
    {
        public IList<ProductDTO> GetAll()
        {
            return Products.GetProducts().Select(ToProductDto).ToList();
        }

        public IList<ProductDTO> Get(ProductModifier modifier)
        {
            return Products.GetProducts(modifier).Select(ToProductDto).ToList();
        }

        public ProductDTO Create(ProductDTO product)
        {
            throw new System.NotImplementedException();
        }

        public ProductDTO Update(int id, ProductDTO product)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        private static ProductDTO ToProductDto(Product product)
        {
            return new ProductDTO(product);
        }
    }
}