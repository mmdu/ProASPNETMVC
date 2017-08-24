using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools6.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }

    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal DiscountSize;

        public DefaultDiscountHelper(decimal discoutnParam)
        {
            DiscountSize = discoutnParam;
        }
        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (DiscountSize / 100m * totalParam));
        }
    }

}  