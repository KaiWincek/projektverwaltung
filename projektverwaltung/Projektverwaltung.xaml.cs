using System.Windows;
using System.Windows.Controls;
using System.Data.OleDb;
using System.Configuration;
using System.Data;


namespace projektverwaltung
{
    /// <summary>
    /// Interaction logic for Projektverwaltung.xaml
    /// </summary>
    public partial class Projektverwaltung : Window
    {
        public Projektverwaltung()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            projektverwaltung.AnnaKaiDataSet annaKaiDataSet = ((projektverwaltung.AnnaKaiDataSet)(this.FindResource("annaKaiDataSet")));
            projektverwaltung.AnnaKaiDataSetTableAdapters.ProjekteTableAdapter annaKaiDataSetProjekteTableAdapter = new projektverwaltung.AnnaKaiDataSetTableAdapters.ProjekteTableAdapter();
            annaKaiDataSetProjekteTableAdapter.Fill(annaKaiDataSet.Projekte);
            System.Windows.Data.CollectionViewSource projekteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("projekteViewSource")));
                
            grid.Columns[0].Visibility = Visibility.Hidden;
            grid.Columns[1].Visibility = Visibility.Visible;
            grid.Columns[2].Visibility = Visibility.Hidden;
            grid.Columns[3].Visibility = Visibility.Hidden;
            grid.Columns[4].Visibility = Visibility.Hidden;
            grid.Columns[5].Visibility = Visibility.Hidden;
            grid.Columns[6].Visibility = Visibility.Hidden;
            grid.Columns[7].Visibility = Visibility.Hidden;
        }

        private void grid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                titel.Text = row_selected[1].ToString();
                startdatum.Text = row_selected[2].ToString();
                enddatum.Text = row_selected[3].ToString();
                prioritaet.Text = row_selected[4].ToString();
                //projektbeteiligung.Text = row_selected[5].ToString();
                ressourcen.Text = row_selected[5].ToString();
                beschreibung.Text = row_selected[6].ToString();
                erledigt.IsChecked = (bool?)row_selected[7];
                delete.IsEnabled = true;
                update.IsEnabled = true;
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
                cmd.CommandText = "UPDATE [Projekte] SET Titel = @titel, Startdatum = @startdarum, Enddatum = @enddatum, Priorität = @prioritaet, Ressourcen = @ressourcen, Beschreibung = @beschreibung, Erledigt = @erledigt WHERE ID = @id;";
                cmd.Parameters.AddWithValue("@titel", titel.Text);
                cmd.Parameters.AddWithValue("@startdatum", startdatum.Text);
                cmd.Parameters.AddWithValue("@enddatum", enddatum.Text);
                cmd.Parameters.AddWithValue("@prioritaet", prioritaet.Text);
                cmd.Parameters.AddWithValue("@ressourcen", ressourcen.Text);
                cmd.Parameters.AddWithValue("@beschreibung", beschreibung.Text);
                cmd.Parameters.AddWithValue("@erledigt", erledigt.IsChecked);
                DataRowView dr = grid.SelectedItem as DataRowView;
                cmd.Parameters.AddWithValue("@id", dr[0]);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                projektverwaltung.AnnaKaiDataSet annaKaiDataSet = ((projektverwaltung.AnnaKaiDataSet)(this.FindResource("annaKaiDataSet")));
                projektverwaltung.AnnaKaiDataSetTableAdapters.ProjekteTableAdapter annaKaiDataSetProjekteTableAdapter = new projektverwaltung.AnnaKaiDataSetTableAdapters.ProjekteTableAdapter();
                annaKaiDataSetProjekteTableAdapter.Fill(annaKaiDataSet.Projekte);
                System.Windows.Data.CollectionViewSource projekteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("projekteViewSource")));

            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem != null)
            {
                OleDbConnection con = new OleDbConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["projektverwaltung.Properties.Settings.AnnaKaiConnectionString"].ToString();
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "DELETE FROM [Projekte] WHERE ID = @id";
                DataRowView dr = grid.SelectedItem as DataRowView;
                cmd.Parameters.AddWithValue("@id", dr[0]);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                projektverwaltung.AnnaKaiDataSet annaKaiDataSet = ((projektverwaltung.AnnaKaiDataSet)(this.FindResource("annaKaiDataSet")));
                projektverwaltung.AnnaKaiDataSetTableAdapters.ProjekteTableAdapter annaKaiDataSetProjekteTableAdapter = new projektverwaltung.AnnaKaiDataSetTableAdapters.ProjekteTableAdapter();
                annaKaiDataSetProjekteTableAdapter.Fill(annaKaiDataSet.Projekte);
                System.Windows.Data.CollectionViewSource projekteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("projekteViewSource")));
                titel.Text = "";
                startdatum.Text = "";
                enddatum.Text = "";
                prioritaet.Text = "";
                projektbeteiligte.Text = "";
                ressourcen.Text = "";
                beschreibung.Text = "";
                erledigt.IsChecked = false;
            }
        }

        private void neu_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["projektverwaltung.Properties.Settings.AnnaKaiConnectionString"].ToString();
            con.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "insert into [Projekte] (Titel, Startdatum, Enddatum, Priorität, Ressourcen, Beschreibung, Erledigt) Values (@titel, @startdatum, @enddatum, @prioritaet, @ressourcen, @beschreibung, @erledigt);";
            cmd.Parameters.AddWithValue("@titel", titel.Text);
            cmd.Parameters.AddWithValue("@startdatum", startdatum.Text);
            cmd.Parameters.AddWithValue("@enddatum", enddatum.Text);
            cmd.Parameters.AddWithValue("@prioritaet", prioritaet.Text);
            cmd.Parameters.AddWithValue("@ressourcen", ressourcen.Text);
            cmd.Parameters.AddWithValue("@beschreibung", beschreibung.Text);
            cmd.Parameters.AddWithValue("@erledigt", erledigt.IsChecked);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            projektverwaltung.AnnaKaiDataSet annaKaiDataSet = ((projektverwaltung.AnnaKaiDataSet)(this.FindResource("annaKaiDataSet")));
            projektverwaltung.AnnaKaiDataSetTableAdapters.ProjekteTableAdapter annaKaiDataSetProjekteTableAdapter = new projektverwaltung.AnnaKaiDataSetTableAdapters.ProjekteTableAdapter();
            annaKaiDataSetProjekteTableAdapter.Fill(annaKaiDataSet.Projekte);
            System.Windows.Data.CollectionViewSource projekteViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("projekteViewSource")));
            titel.Text = "";
            startdatum.Text = "";
            enddatum.Text = "";
            prioritaet.Text = "";
            projektbeteiligte.Text = "";
            ressourcen.Text = "";
            beschreibung.Text = "";
            erledigt.IsChecked = false;
        }
    }
}
