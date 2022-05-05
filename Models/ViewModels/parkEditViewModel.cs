using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cap.Models.ViewModels
{
    public class parkCreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParkAddress { get; set; }

        public IEnumerable<int> SportIds { get; set; }
    }
}