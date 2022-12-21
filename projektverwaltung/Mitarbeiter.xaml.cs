using System.Windows;
using System.Windows.Controls;
using System.Data.OleDb;
using System.Configuration;
using System.Data;

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
            grid.Columns[0].Visibility = Visibility.Hidden;
            grid.Columns[1].Visibility = Visibility.Visible;
            grid.Columns[2].Visibility = Visibility.Visible;
            grid.Columns[3].Visibility = Visibility.Hidden;
            grid.Columns[4].Visibility = Visibility.Hidden;
            grid.Columns[5].Visibility = Visibility.Hidden;
            grid.Columns[6].Visibility = Visibility.Hidden;
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
            projektverwaltung.AnnaKaiDataSet annaKaiDataSet = ((projektverwaltung.AnnaKaiDataSet)(this.FindResource("annaKaiDataSet")));
            // Load data into the table Mitarbeiter. You can modify this code as needed.
            projektverwaltung.AnnaKaiDataSetTableAdapters.MitarbeiterTableAdapter annaKaiDataSetMitarbeiterTableAdapter = new projektverwaltung.AnnaKaiDataSetTableAdapters.MitarbeiterTableAdapter();
            annaKaiDataSetMitarbeiterTableAdapter.Fill(annaKaiDataSet.Mitarbeiter);
            System.Windows.Data.CollectionViewSource mitarbeiterViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("mitarbeiterViewSource")));
            nachname.Text = "";
            vorname.Text = "";
            adresse.Text = "";
            telefon.Text = "";
            email.Text = "";
        }

        private void grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                nachname.Text = row_selected[1].ToString();
                vorname.Text = row_selected[2].ToString();
                adresse.Text = row_selected[3].ToString();
                telefon.Text = row_selected[4].ToString();
                email.Text = row_selected[5].ToString();
                //nachname.Text = row_selected[6].ToString();
                delete.IsEnabled = true;
                update.IsEnabled = true;
            }
            
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if(grid.SelectedItem != null)
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["projektverwaltung.Properties.Settings.AnnaKaiConnectionString"].ToString();
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "DELETE FROM [Mitarbeiter] WHERE ID = @id";
                DataRowView dr = grid.SelectedItem as DataRowView;
                cmd.Parameters.AddWithValue("@id", dr[0]);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                projektverwaltung.AnnaKaiDataSet annaKaiDataSet = ((projektverwaltung.AnnaKaiDataSet)(this.FindResource("annaKaiDataSet")));
                // Load data into the table Mitarbeiter. You can modify this code as needed.
                projektverwaltung.AnnaKaiDataSetTableAdapters.MitarbeiterTableAdapter annaKaiDataSetMitarbeiterTableAdapter = new projektverwaltung.AnnaKaiDataSetTableAdapters.MitarbeiterTableAdapter();
                annaKaiDataSetMitarbeiterTableAdapter.Fill(annaKaiDataSet.Mitarbeiter);
                System.Windows.Data.CollectionViewSource mitarbeiterViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("mitarbeiterViewSource")));
                nachname.Text = "";
                vorname.Text = "";
                adresse.Text = "";
                telefon.Text = "";
                email.Text = "";
            }
            
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem != null)
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["projektverwaltung.Properties.Settings.AnnaKaiConnectionString"].ToString();
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "UPDATE [Mitarbeiter] SET Nachname = @nachname, Vorname = @vorname, Adresse = @adresse, Telefon = @telefon, email = @email, bild = @bild WHERE ID = @id;";
                cmd.Parameters.AddWithValue("@nachname", nachname.Text);
                cmd.Parameters.AddWithValue("@vorname", vorname.Text);
                cmd.Parameters.AddWithValue("@adresse", adresse.Text);
                cmd.Parameters.AddWithValue("@telefon", telefon.Text);
                cmd.Parameters.AddWithValue("@email", email.Text);
                cmd.Parameters.AddWithValue("@bild", "/lol");
                DataRowView dr = grid.SelectedItem as DataRowView;
                cmd.Parameters.AddWithValue("@id", dr[0]);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                projektverwaltung.AnnaKaiDataSet annaKaiDataSet = ((projektverwaltung.AnnaKaiDataSet)(this.FindResource("annaKaiDataSet")));
                // Load data into the table Mitarbeiter. You can modify this code as needed.
                projektverwaltung.AnnaKaiDataSetTableAdapters.MitarbeiterTableAdapter annaKaiDataSetMitarbeiterTableAdapter = new projektverwaltung.AnnaKaiDataSetTableAdapters.MitarbeiterTableAdapter();
                annaKaiDataSetMitarbeiterTableAdapter.Fill(annaKaiDataSet.Mitarbeiter);
                System.Windows.Data.CollectionViewSource mitarbeiterViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("mitarbeiterViewSource")));
                //mitarbeiterViewSource.View.MoveCurrentToFirst();
            }

        }
    }
}
