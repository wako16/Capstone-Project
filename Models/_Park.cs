using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cap.Models
{
    public partial class Park
    {
        public double amentiesAverage
        {
            get
            {
                var reviewCount = this.Reviews.Count;
                if (reviewCount > 0)
                {
                    return this.Reviews.Average(r => r.AmentiesRating ?? 0.0);
                }
                else
                {
                    return 2.5;
                }
            }
        }
        public double PriceAverage
        {
            get
            {
                var reviewCount = this.Reviews.Count;
                if (reviewCount > 0)
                {
                    return this.Reviews.Average(r => r.PriceRating ?? 0.0);
                }
                else
                {
                    return 2.5;
                }
            }
        }

    }
}
