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
    /// Interaction logic for FSControl.xaml
    /// </summary>
    public partial class FSControl : UserControl
    {
        public static ArrayList my_list;



        public class FS
        {
            public String name { get; set; }
            public int old_features { get; set; }
            public int new_features { get; set; }
            public double old_accuracy { get; set; }
            public double new_accuracy { get; set; }
        }

        public FSControl()
        {
            InitializeComponent();
            data();


        }
        public void data()
        {
            my_list = new ArrayList();
            RestClient restClient = new RestClient("http://baraadervis.pythonanywhere.com/FS/fs-results-api/", RestClient.httpVerb.GET);
            string resp = restClient.makeRequestWithToken();

            var o = JObject.Parse(resp);


            JArray a = JArray.Parse(o["results"].ToString());

            FS myObj = new FS();
            foreach (JObject obj in a.Children<JObject>())
            {
                myObj = new FS();
                foreach (JProperty objprop in obj.Properties())
                {

                    if (objprop.Name == "old_accuracy")
                        myObj.old_accuracy = Math.Round(Double.Parse(objprop.Value.ToString()), 3, MidpointRounding.ToEven);

                    else if (objprop.Name == "new_accurcay")
                        myObj.new_accuracy = Math.Round(Double.Parse(objprop.Value.ToString()), 2, MidpointRounding.ToEven); 
                    else if (objprop.Name == "name")
                        myObj.name = objprop.Value.ToString();
                    else if (objprop.Name == "old_features")
                        myObj.old_features = int.Parse(objprop.Value.ToString());
                    else if (objprop.Name == "new_features")
                        myObj.new_features = int.Parse(objprop.Value.ToString());


                }
                my_list.Add(myObj);
            }
            foreach (FS f in my_list)
            {
                fs_datagrid.Items.Add(f);

            }








        }





    }
}
