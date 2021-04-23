using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Interaction logic for production.xaml
    /// </summary>
    public partial class production : Window
    {
        private SqlConnection sqlCon;
        public production()
        {
            InitializeComponent();
             sqlCon = new SqlConnection("Data Source=DESKTOP-T2S26JP;Initial Catalog=UsersDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!(prodBrandTextBox.Text == string.Empty && prodCodeTextBox.Text == string.Empty || prodNameTextBox.Text == string.Empty || prodQuantityTextBox.Text == string.Empty))
            {
                
                 if (Validation.IsUserInputNumber(prodCodeTextBox.Text) || Validation.IsUserInputNumber(prodQuantityTextBox.Text))
                {
                    MessageBox.Show("Code  and Quantity must be numeric value not string");
                    return;
                }
                sqlCon.Open();
                string query = "INSERT INTO UsersDatabase.dbo.Warehouse (Code,Name,Quantity,Brand) VALUES(@Code,@Name,@Quantity,@Brand)";
                SqlCommand cmd = new(query, sqlCon);
                cmd.Parameters.AddWithValue("@Code", prodCodeTextBox.Text);
                cmd.Parameters.AddWithValue("@Name", prodNameTextBox.Text);
                cmd.Parameters.AddWithValue("@Quantity", prodQuantityTextBox.Text);
                cmd.Parameters.AddWithValue("@Brand", prodBrandTextBox.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("საქონელი დამატებულია");
                sqlCon.Close();
            }
            else MessageBox.Show("Please fill all Field");
        }

        private void showGoodsBtn_Click(object sender, RoutedEventArgs e)
        {
            var addedProdWin = new AddedProduction();
            
            addedProdWin.Show();

        }
    }
}
