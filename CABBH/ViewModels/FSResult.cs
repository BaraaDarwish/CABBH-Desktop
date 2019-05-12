using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABBH.ViewModels
{
    public class FSResult
    {
       
            public String name { get; set; }
            public int old_features { get; set; }
            public int new_features { get; set; }
            public double old_accuracy { get; set; }
            public double new_accuracy { get; set; }
        
    }
}
