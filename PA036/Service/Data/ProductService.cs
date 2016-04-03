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
        private readonly IProducts _instance;

        public ProductService()
        {
            _instance = new Products();
        }

        public ProductDTO Get(int id)
        {
            return ToProductDto(_instance.Get(id));
        }

        public IList<ProductDTO> GetAll()
        {
            return Get(null);
        }

        public IList<ProductDTO> Get(ProductModifier modifier)
        {
            return _instance.Get(modifier).Select(ToProductDto).ToList();
        }

        // TODO ?? should return newly created product?
        public ProductDTO Create(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Create(product);
            return null; 
        }

        // TODO ?? should return newly created product?
        public ProductDTO Update(int id, ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            product.Id = id;
            _instance.Update(product);
            return null; 
        }

        public void Delete(int id)
        {
            _instance.Delete(id);
        }

        public int TotalCount()
        {
            return _instance.Count();
        }

        private static ProductDTO ToProductDto(Product product)
        {
            return new ProductDTO(product);
        }

        private static Product ToProduct(ProductDTO dto)
        {
            var result = new Product()
            {
                Id = dto.id,
                Name = dto.Name,
                StockCount = dto.StockCount,
                UnitCost = dto.UnitCost
            };

            return result;
        }
    }
}