using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CABBH
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        public  bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
             if (username_reg.Text == null || username_reg.Text == "")
            {
                warn.Content = "Username Cant be empty";
            }
            else if (password_reg.Password == null || password_reg.Password == "")
            {
                warn.Content = "Password Cant be empty";
            }
            else if (password_reg.Password != cpassword.Password)
            {
                warn.Content = "Passwords dont match";
            }
            else if (password_reg.Password.Length < 8)
            {
                warn.Content = "Password Too short , should at least 8";
            }
             else if (!IsValidEmailAddress(email_reg.Text))
            {
                warn.Content = "Enter a valid Email Addess";
            }
            else
            {
                RestClient restClient = new RestClient("http://baraadervis.pythonanywhere.com/FS/create-user/", RestClient.httpVerb.POST);
                restClient.username = username_reg.Text;
                restClient.password = password_reg.Password;
                restClient.firstname = firstname_reg.Text;
                restClient.lastname = lastname_reg.Text;
                restClient.email = email_reg.Text;
                string resp = restClient.RegisterRequest();
               
               

                if(resp == "User name already exists")
                {
                    warn.Content = resp.ToString();
                    
                }

                else
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }





                
            }
   
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
