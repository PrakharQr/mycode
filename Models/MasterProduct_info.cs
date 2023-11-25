using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace QuarTest.Models
{
    public class MasterProduct_info
    {
        public string Brandid { get; set; }

        public string BrandName { get; set; }
        public string Id { get; set; }
        public string PrdName { get; set; }
        public string Prdcolor { get; set; }
        public DataTable Prddt { get; set; }
        public string Flag { get; set; }
        public string CreatedDate { get; set; }
    }
}