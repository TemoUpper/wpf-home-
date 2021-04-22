using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            var SignUpWindow = new SignUpWin()
            {
                
                Owner = Window.GetWindow(this),
                SizeToContent=SizeToContent.WidthAndHeight,
                
            };
            Window.GetWindow(this).Hide();
            SignUpWindow.Show();
            
        }

        private void signinBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!((emailTextBox.Text == string.Empty) || (passTextBox.Text == string.Empty)))
            {

                if (!(Validation.ValUserEmail(emailTextBox.Text)))
                {
                    MessageBox.Show($"Enter correct Email format");
                    return;
                }
                else if (!(Validation.ValUserPass(passTextBox.Text)))
                {
                    MessageBox.Show("Minimum length of password is 8 char and Maximum lenght is 50 char");
                    return;
                }
                /// do log in

                List<string> info = new List<string>();

                SqlConnection sqlcon = new SqlConnection("Data Source=DESKTOP-T2S26JP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                sqlcon.Open();

                SqlCommand cmd = new SqlCommand("Select Email, Password  from UsersDatabase.dbo.Users", sqlcon);

                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        info.Add($"{rdr[0]},{rdr[1]}");
                    }
                }
                sqlcon.Close();
                bool indicator = false;

                
                for (int i = 0; i < info.Count; i++)
                {
                    string[] strArray = null;
                    strArray = info[i].Split(",");
                    string Email = strArray[0];
                    string Password = strArray[1];
                    if ((Email == emailTextBox.Text) && (Password == passTextBox.Text))
                    {
                        indicator = true;
                        break;
                    }
                }
                if (indicator == false) MessageBox.Show("Email or password is incorrect");
                else if(indicator==true) 
                {
                    Window productionwin = new production();
                    Window.GetWindow(this).Close();
                    
                    productionwin.Show();
                    this.Close();
                    indicator = false;
                }

            }
            else MessageBox.Show("Pleas fill all fields");
        }
    }
}
