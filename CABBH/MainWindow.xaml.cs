using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace CABBH
{

    public class Token
    {
        public String token { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if(Properties.Settings.Default.AuthToken.ToString() != "")
            {
                HomeWindow register = new HomeWindow();
                register.Show();
                this.Close();
            }
            InitializeComponent();
         
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (username_login.Text == null || username_login.Text == "")
            {
                warning.Text = "Username Required";

            }
            else if (username_login.Text == null || username_login.Text == "")
            {
                warning.Text = "Password Required";
            }
            else
            {
                RestClient restClient = new RestClient("http://baraadervis.pythonanywhere.com/FS/token/", RestClient.httpVerb.POST);
                restClient.username = username_login.Text;
                restClient.password = password_login.Password;
                string resp = restClient.LoginRequest();
                try
                {
                    var obj = JsonConvert.DeserializeObject<Token>(resp);
                    jsonText.Text = obj.token;

                    Properties.Settings.Default.AuthToken = obj.token;
                    Properties.Settings.Default.Save();
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.Show();



                }
                catch (Exception ex)
                {
                    warning.Text = "Invalid Credentials";
                }
            }
        }

   
    }
}
