using System;
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
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddedProduction.xaml
    /// </summary>
    public partial class AddedProduction : Window
    {
        private SqlConnection con;

        public AddedProduction()
        {
            InitializeComponent();
            FillDatagrid();
        }

        private void FillDatagrid()
        {

            string ConnectionString = "Data Source=DESKTOP-T2S26JP;Initial Catalog=UsersDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                CmdString = "SELECT Code,Name,Quantity,Brand FROM UsersDatabase.dbo.Warehouse";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("AddedProduct");
                sda.Fill(dt);
                grdProducts.ItemsSource = dt.DefaultView;
            }

        }
    }

}
