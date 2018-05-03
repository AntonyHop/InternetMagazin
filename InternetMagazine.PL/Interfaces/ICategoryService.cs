﻿using System;
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
        void Dispose();
    }
}