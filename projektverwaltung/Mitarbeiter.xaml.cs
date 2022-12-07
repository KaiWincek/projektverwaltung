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
using System.Data.OleDb;
using System.Configuration;

namespace projektverwaltung
{
    /// <summary>
    /// Interaction logic for Mitarbeiter.xaml
    /// </summary>
    public partial class Mitarbeiter : Window
    {
        public Mitarbeiter()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            projektverwaltung.AnnaKaiDataSet annaKaiDataSet = ((projektverwaltung.AnnaKaiDataSet)(this.FindResource("annaKaiDataSet")));
            // Load data into the table Mitarbeiter. You can modify this code as needed.
            projektverwaltung.AnnaKaiDataSetTableAdapters.MitarbeiterTableAdapter annaKaiDataSetMitarbeiterTableAdapter = new projektverwaltung.AnnaKaiDataSetTableAdapters.MitarbeiterTableAdapter();
            annaKaiDataSetMitarbeiterTableAdapter.Fill(annaKaiDataSet.Mitarbeiter);
            System.Windows.Data.CollectionViewSource mitarbeiterViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("mitarbeiterViewSource")));
            mitarbeiterViewSource.View.MoveCurrentToFirst();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["projektverwaltung.Properties.Settings.AnnaKaiConnectionString"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "insert into [Mitarbeiter] (Nachname, Vorname, Adresse, Telefon, Email, Bild) Values (@nachname, @vorname, @adresse, @telefon, @email, @bild);";
            cmd.Parameters.AddWithValue("@nachname", nachname.Text);
            cmd.Parameters.AddWithValue("@vorname", vorname.Text);
            cmd.Parameters.AddWithValue("@adresse", adresse.Text);
            cmd.Parameters.AddWithValue("@telefon", telefon.Text);
            cmd.Parameters.AddWithValue("@email", email.Text);
            cmd.Parameters.AddWithValue("@bild", "/lol");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
