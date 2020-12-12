using dbsqlbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsBrowsing_backend.Models
{
    public class MDProduct : TBModelBase
    {
        [MDTBProperty(IsdbReadWrite = true)]
        public string Name { get; set; }

        [MDTBProperty(IsdbReadWrite = true)]
        public double Price { get; set; }

        [MDTBProperty(IsdbReadWrite = true)]
        public string Imagepath { get; set; }

        [MDTBProperty(IsdbReadWrite = true)]
        public DateTime? Lastupdated { get; set; }


        public MDProduct() { }


        public MDProduct(double price, string name)
        {
            Name = name;
            Price = price;
        }

        public static MDProduct Add(double price,string name)
        {
            MDProduct newproduct = new MDProduct(price, name);
            newproduct.Lastupdated = DateTime.Now;
            newproduct.Imagepath = "";
            MDProduct added= newproduct.Add<MDProduct>(out string msg);
            return added;
        }


        public static MDProduct Update(MDProduct product)
        {
            product.Lastupdated = DateTime.Now;
            try
            {
                product.Update<MDProduct>(out string msg);
                return product;

            }
            catch
            {
                
                return null;
            }
            
        }
        public static bool Delete(MDProduct product)
        {
            bool flag = product.Delete();
            return flag;
        }
        public static MDProduct  GetById(int id)
        {
            MDProduct product = new MDProduct().GetByParameter<MDProduct>("Id", id.ToString(),out string msg).FirstOrDefault();
            return product;
        }

        public static MDProduct UploadImage(HttpRequest request,int id)
        {
            var image = request.Files["Image"];
            MDProduct product = GetById(id);
            if (product == null) { return null; }
            try
            {
                string ImagName = $"{product.Id}{System.IO.Path.GetExtension(image.FileName)}";
                string ImagePath = HttpContext.Current.Server.MapPath($"~/ProductImages/{ImagName}");
                if (System.IO.File.Exists(ImagePath))
                    System.IO.File.Delete(ImagePath);
                image.SaveAs(ImagePath);
                product.Imagepath = ImagName;
                MDProduct Updatedproduct = Update(product);
                return Updatedproduct;
            }
            catch
            {
                return null;
            }

        }
        public static List<MDProduct> GetAll()
        {
            List<MDProduct> products = new MDProduct().GetAll<MDProduct>("Id", out string msg);
            return products;
        }
        public override string GetTableName()
        {
            return "TBProducts";
        }
    }
}