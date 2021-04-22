using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SignUpWin.xaml
    /// </summary>
    public partial class SignUpWin : Window
    {
        private SqlConnection sqlCon;
        public SignUpWin()
        {
            InitializeComponent();
            sqlCon = new SqlConnection("Data Source=DESKTOP-T2S26JP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Show();

        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(nameTextBox.Text == string.Empty || EmailTextBox.Text == string.Empty || passTextBox.Text == string.Empty))
            {
                if (! (Validation.ValUserEmail(EmailTextBox.Text)))
                {
                    MessageBox.Show($"Enter correct Email format");
                    return;
                }
                else if (!(Validation.ValUserPass(passTextBox.Text)))
                {
                    MessageBox.Show("Minimum length of password is 8 char and Maximum lenght is 50 char");
                    return;
                }
                bool indicator = false;
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand cmd = new SqlCommand("Select Fullname, Email  from UsersDatabase.dbo.Users", sqlCon);
                List<string> info = new List<string>();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        info.Add($"{rdr[0]},{rdr[1]}");
                    }
                }
                sqlCon.Close();

                for (int i = 0; i < info.Count; i++)
                {
                    string[] strArray = null;
                    strArray = info[i].Split(",");
                    string name = strArray[0];
                    string email = strArray[1];
                    if(name == nameTextBox.Text ||email == EmailTextBox.Text) 
                    {
                        indicator = true;
                        break;
                    }
                }
                if (indicator == true) MessageBox.Show("this user name or Email is already taken");

                else if (indicator == false)
                {

                    sqlCon.Open();

                    string query = "INSERT INTO UsersDatabase.dbo.Users (Fullname,Email,Password) VALUES (@Fullname,@Email,@Password)";
                    SqlCommand cmd2 = new(query, sqlCon);
                    cmd2.CommandText = query;
                    cmd2.Parameters.AddWithValue("@Fullname", nameTextBox.Text);
                    cmd2.Parameters.AddWithValue("@Email", EmailTextBox.Text);
                    cmd2.Parameters.AddWithValue("@Password", passTextBox.Text);
                    cmd2.ExecuteNonQuery();

                    Window productionwin = new production();
                    Window.GetWindow(this).Close();
                    Owner.Close();
                    productionwin.Show();
                    indicator = false;
                    sqlCon.Close();
                }


            }
            else MessageBox.Show("Pleas fill all Fields");
        }
    }
}
