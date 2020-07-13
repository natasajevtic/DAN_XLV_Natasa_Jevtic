using System.Collections.Generic;
using System.Linq;
using Zadatak_1.Models;

namespace Zadatak_1.Validations
{
    class Validation
    {
        /// <summary>
        /// This method checks if user input for product key is unique.
        /// </summary>
        /// <param name="productKey">Key of product.</param>
        /// <returns>True if unique, else if not.</returns>
        public bool UniqueProductKey(string productKey)
        {
            Products products = new Products();
            List<vwProduct> productList = products.ViewAllProduct();
            //creating a list of product with forwarded key
            var list = productList.Where(x => x.ProductKey == productKey).ToList();
            //if exists product with forwarded key, return false
            if (list.Count() > 0)
            {
                return false;
            }            
            else
            {
                return true;
            }
        }
    }
}