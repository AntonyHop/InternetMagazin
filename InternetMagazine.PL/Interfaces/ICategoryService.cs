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
        ProductDTO GetOneProduct(int? id);
        void EditCategory(int id, string name);
        void AddCategory(string name);
        void DeleteCategory(int id);
        IEnumerable<ProductDTO> Search(string s);

        void AddProduct(ProductDTO pr);
        void UpdateOneProduct(ProductDTO p);
        void DeleteProduct(int? id);

        void Dispose();
    }
}
