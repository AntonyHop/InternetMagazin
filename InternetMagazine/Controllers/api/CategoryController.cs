using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Infrastructure;
using InternetMagazine.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InternetMagazine.Controllers.api
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService svc;
        public CategoryController(ICategoryService _svc)
        {
            svc = _svc;
        }

        // GET: api/Category
        public IEnumerable<CategoryDTO> Get()
        {
            try
            {
                return svc.Categories();
            }catch (ValidationException ex)
            {
                throw new HttpResponseException(HttpStatusCode.ExpectationFailed);
            }
            
        }

        // GET: api/Category/5
        public CategoryDTO Get(int id)
        {
            return svc.Categories().Where(c=> c.Id== id).FirstOrDefault();
        }

        // POST: api/Category
        public void Post([FromBody]CategoryDTO value)
        {
            svc.EditCategory(value.Id,value.Name);
        }

        // PUT: api/Category/5
        public void Put(int id, [FromBody]CategoryDTO cat)
        {
            svc.AddCategory(cat.Name);
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
            svc.DeleteCategory(id);
        }
    }
}
