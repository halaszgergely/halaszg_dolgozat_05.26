using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using MySql.Data.MySqlClient;

namespace halaszg_dolgozat_05._26
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection kapcs = new MySqlConnection("server = localhost;database = halaszg; uid = root; password = ''");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lekerdez()
        {
            lbAdatok.Items.Clear();
            var sql = "SELECT * FROM halaszg.filmek";
            MySqlCommand parancs = new MySqlCommand(sql, kapcs);
            kapcs.Open();
            var reader = parancs.ExecuteReader();
            while (reader.Read())
            {
                lbAdatok.Items.Add($"{reader[0]};{reader[1]};{reader[2]};{reader[3]};{reader[4]};{reader[5]}");
            }
            kapcs.Close();
            reader.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lekerdez();
        }

        private void lbAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAdatok.SelectedItem != null)
            {
                var sor = lbAdatok.SelectedItem.ToString().Split(';');
                lbFilmAzon.Content = sor[0];
                tb1.Text = sor[1];
                tb2.Text = sor[2];
                tb3.Text = sor[3];
                tb4.Text = sor[4];
                tb5.Text = sor[5];
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var sql = $"UPDATE halaszg.filmek SET cim = '{tb1.Text}', ev = '{tb2.Text}', szines = '{tb3.Text}', mufaj = '{tb4.Text}', hossz = '{tb5.Text}' WHERE filmazon = '{lbFilmAzon.Content}'";
            MySqlCommand parancs = new MySqlCommand(sql, kapcs);
            kapcs.Open();
            parancs.ExecuteNonQuery();
            kapcs.Close();
            lekerdez();
            tb1.Text = "";
            tb2.Text = "";
            tb3.Text = "";
            tb4.Text = "";
            tb5.Text = "";
    }
        }
}
