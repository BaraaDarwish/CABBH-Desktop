using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CABBH.ViewModels
{
    /// <summary>
    /// Interaction logic for DiabetesControl.xaml
    /// </summary>
    public partial class DiabetesControl : UserControl
    {
        public static ArrayList my_list;



     

        public DiabetesControl()
        {
            InitializeComponent();
            data();


        }
        public void data()
        {
            my_list = new ArrayList();
            RestClient restClient = new RestClient("http://baraadervis.pythonanywhere.com/CAC/diabetes-results-api/", RestClient.httpVerb.GET);
            string resp = restClient.makeRequestWithToken();

            var o = JObject.Parse(resp);


            JArray a = JArray.Parse(o["results"].ToString());

            DiabetesResult myObj = new DiabetesResult();
            foreach (JObject obj in a.Children<JObject>())
            {
                myObj = new DiabetesResult();
                foreach (JProperty objprop in obj.Properties())
                {

                    if (objprop.Name == "BMI")
                        myObj.BMI = Math.Round(Double.Parse(objprop.Value.ToString()), 3, MidpointRounding.ToEven);

                    else if (objprop.Name == "DiabetesPedigreeFunction")
                        myObj.DiabetesPedigreeFunction = Math.Round(Double.Parse(objprop.Value.ToString()), 2, MidpointRounding.ToEven);
                    else if (objprop.Name == "name")
                        myObj.Name = objprop.Value.ToString();
                    else if (objprop.Name == "Glucose")
                        myObj.Glucose = int.Parse(objprop.Value.ToString());
                    else if (objprop.Name == "BloodPressure")
                        myObj.BloodPressure = int.Parse(objprop.Value.ToString());
                    else if (objprop.Name == "SkinThickness")
                        myObj.SkinThickness = int.Parse(objprop.Value.ToString());
                    else if (objprop.Name == "Insulin")
                        myObj.Insulin = int.Parse(objprop.Value.ToString());
                    else if (objprop.Name == "Age")
                        myObj.Age = int.Parse(objprop.Value.ToString());
                    else if (objprop.Name == "result")
                        myObj.result = (objprop.Value.ToString());
                    


                }
                my_list.Add(myObj);
            }
            foreach (DiabetesResult f in my_list)
            {
                diabetes_datagrid.Items.Add(f);

            }








        }


    }
}
