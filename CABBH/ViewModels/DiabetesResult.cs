using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CABBH.ViewModels
{
    public class DiabetesResult
    {
        
            public String Name { get; set; }
            public int Pregnancies { get; set; }
            public int Glucose { get; set; }

            public int BloodPressure { get; set; }

            public int SkinThickness { get; set; }

            public  int Insulin { get; set; }
            public Double BMI { get; set; }
            public  Double DiabetesPedigreeFunction { get; set; }
            public  int Age { get; set; }
            public  String result { get; set; }
    }
}
