using System;
using System.Collections.Generic;
using System.Linq;  
using System.Web;

namespace EssentialTools6.Models
{
    public class ShoppingCart
    {
        private  IValueCalculator calc;
                    
        public ShoppingCart( IValueCalculator calcPara)  // Encapsul ,only need interface here , no need  linqValueCalculator
        {
            calc = calcPara;
        }

        public IEnumerable<Product> Products { get; set; }

        public decimal CalculateProductTotal() // only interface here , 
        {
            return calc.ValueProducts(Products);
        }
    }
}