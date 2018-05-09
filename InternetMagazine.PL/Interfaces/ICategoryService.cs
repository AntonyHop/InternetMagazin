using System;
using InternetMagazine.PL.DTO;
using System.Collections.Generic;

namespace InternetMagazine.PL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<ProductDTO> LoadProductsCategory(int? catId);
        IEnumerable<ProductDTO> Products();
        IEnumerable<CategoryDTO> Categories();
        ProductDTO GetOneProduct(int id);
        void EditCategory(int id, string name);
        void AddCategory(string name);
        void DeleteCategory(int id);

        void AddProduct(ProductDTO pr);
        void DeleteProduct(int? id);

        void Dispose();
    }
}
