using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderService.DataAccess.Validators
{
    public interface IProductValidator
    {
        List<KeyValuePair<string, string>> CanAddProduct(Product product);
        List<KeyValuePair<string, string>> CanUpdateProduct(Product product);
    }

    public class ProductValidator : IProductValidator
    {
        public List<KeyValuePair<string, string>> CanAddProduct(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException("Product can not be null");
            }

            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(product.Name))
            {
                errors.Add(new KeyValuePair<string, string>("Name", "Name is required"));
            }
            if (string.IsNullOrEmpty(product.Description))
            {
                errors.Add(new KeyValuePair<string, string>("Description", "Description is required"));
            }
            if (product.Price <= 0)
            {
                errors.Add(new KeyValuePair<string, string>("Price", "Price must be positive number"));
            }
            if (product.Rates != null)
            {
                errors.Add(new KeyValuePair<string, string>("", "Rates must not be added here"));
            }

            return errors;
        }

        public List<KeyValuePair<string, string>> CanUpdateProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product can not be null");
            }

            List<KeyValuePair<string, string>> errors = new List<KeyValuePair<string, string>>();
            if (string.IsNullOrEmpty(product.Name))
            {
                errors.Add(new KeyValuePair<string, string>("Name", "Name is required"));
            }
            if (string.IsNullOrEmpty(product.Description))
            {
                errors.Add(new KeyValuePair<string, string>("Description", "Description is required"));
            }
            if (product.Price <= 0)
            {
                errors.Add(new KeyValuePair<string, string>("Price", "Price must be positive number"));
            }

            return errors;
        }
    }
}