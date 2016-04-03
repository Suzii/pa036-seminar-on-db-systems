using DataAccess.Model;
using Shared.Modifiers;
using System.Collections.Generic;

namespace DataAccess.Data
{
    public interface IProducts
    {
        IList<Product> Get(ProductModifier modifier);

        Product Get(int id);

        int Count();

        void Create(Product product);

        void Delete(int id);

        void Update(Product product);
    }
}
