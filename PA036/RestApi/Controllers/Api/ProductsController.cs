using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Service.Data;
using Service.DTO;
using Shared.Modifiers;

namespace RestApi.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController()
        {
            _productService = new ProductService();
        }

        // GET: api/Products
        public IEnumerable<ProductDTO> Get(int page = 0, int pageSize = 10)
        {
            var totalCount = _productService.TotalCount();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(Request);
            var prevLink = page > 0 ? urlHelper.Link("Products", new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link("Products", new { page = page + 1, pageSize = pageSize }) : "";

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            var modifier = new ProductModifier()
            {
                Skip = page,
                Take = pageSize
            };

            var result = _productService.Get(modifier);
            return result;
        }

        // GET: api/Products/5
        public ProductDTO GetProduct(int id)
        {
            var result = _productService.Get(id);
            return result;
        }

        // POST: api/Products
        public void Post([FromBody]ProductDTO product)
        {
            _productService.Create(product);
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]ProductDTO product)
        {
            product.id = id;
            _productService.Update(product);
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
            _productService.Delete(id);
        }
    }
}
