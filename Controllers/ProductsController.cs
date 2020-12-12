using ProductsBrowsing_backend.Models;
using ProductsBrowsing_backend.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProductsBrowsing_backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Auth]
        public IHttpActionResult GetAll()
        {

            List<MDProduct> products = MDProduct.GetAll();
            return Ok(products);
        }

        [HttpPost]
        [Auth]
        public IHttpActionResult Add(MDProduct product) 
        {

            MDProduct added = MDProduct.Add(product.Price, product.Name);
            if (added == null) { return BadRequest(); }
            return Ok(added);
        }

        [HttpPost]
        [Auth]
        public IHttpActionResult Update(MDProduct product)
        {

            MDProduct updated = MDProduct.Update(product);
            if (updated == null) { return BadRequest(); }
            return Ok(updated);
        }

        [HttpPost]
        [Auth]
        public IHttpActionResult Delete(MDProduct product)
        {

            bool deleted = MDProduct.Delete(product);
            if (!deleted) { return BadRequest(); }
            return Ok();
        }

        [HttpPost]
        [Auth]
        public IHttpActionResult UploadImage(int id) 
        {
            MDProduct product = MDProduct.UploadImage(HttpContext.Current.Request, id);
            if (product == null) { return BadRequest(); }
            return Ok(product);
        }


    }
}
