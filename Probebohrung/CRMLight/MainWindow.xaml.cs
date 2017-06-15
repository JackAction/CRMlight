using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.listViewPendenzen.DataContext = CreateDataTable();
        }

        DataTable CreateDataTable()
        {
            DataTable tbl = new DataTable("Customers");

            tbl.Columns.Add("Titel", typeof(string));
            tbl.Columns.Add("Quelle", typeof(string));
            tbl.Columns.Add("Mitarbeiter", typeof(string));
            tbl.Columns.Add("Termin", typeof(DateTime));
            tbl.Columns.Add("Erledigt", typeof(int));
            tbl.Columns.Add("Beschreibung", typeof(string));

            tbl.Rows.Add("testTitel1", "Johnny BeeGood", "Mitarbeiter1", new DateTime(2017, 09,9),0,  "testBeschreibung1");
            tbl.Rows.Add("testTitel2", "Jane Dorkenheimer", "Mitarbeiter2", new DateTime(2018, 09, 9), 0, "testBeschreibung2");
            tbl.Rows.Add("testTitel1", "John Doe", "Mitarbeiter3", new DateTime(1999, 09, 9), 0, "testBeschreibung3");
            tbl.Rows.Add("testTitel2", "Jane Dorkenheimer", "Mitarbeiter4", new DateTime(2018, 09, 9), 1, "testBeschreibung4");
            tbl.Rows.Add("testTitel1", "John Doep", "Mitarbeiter5", new DateTime(2000, 09, 9), 0, "testBeschreibung5");
            tbl.Rows.Add("testTitel2", "Jane Dorkenheimer", "Mitarbeiter6", new DateTime(2018, 09, 9), 1, "testBeschreibung6");
            tbl.Rows.Add("testTitel1", "John Doep", "Mitarbeiter7", new DateTime(2017, 12, 9), 0, "testBeschreibung7");
            tbl.Rows.Add("testTitel2", "Jane Smith", "Mitarbeiter8", new DateTime(2018, 11, 9), 0, "testBeschreibung8");


            return tbl;
        }

        private void tabPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Verwerfen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hinzufügen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listViewPendenzen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tabPanel_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
