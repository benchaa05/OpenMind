using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataBaseProject;

namespace GesCom.Pages.ClientPageRep
{
    /// <summary>
    /// Interaction logic for FicheClient.xaml
    /// </summary>
    public partial class FicheClient : UserControl
    {
        public FicheClient()
        {
            InitializeComponent();
        }

        private readonly CLIENT _client = new CLIENT();
        private CLIENT _selected = new CLIENT();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
            // create new data context
            //GesComDBClassesDataContext dc = new GesComDBClassesDataContext();

            // set the binding source data source to the full order table
            //this.orderBindingSource.DataSource = dc.GetTable<Order>(); 
            GesComDBClassesDataContext dc = new GesComDBClassesDataContext();
            var cli = from c in dc.CLIENTs select c;
            ClientGrid.ItemsSource = cli;
            _selected = ClientGrid.SelectedItem as CLIENT;
            var telph = from p in dc.TELEPHONEs
                where p.CLIENTID == _selected.CLIENTID
                select new
                {
                    p.TELEPHONENUMBER,
                    p.TELEPHONETYPE
                };
            ClientGrid.SelectedIndex = 0;
            ClientGrid.Columns[0].Visibility = Visibility.Hidden;
            ClientGrid.Columns[1].Header = "Nom";
            ClientGrid.Columns[2].Header = "Prénom";
            ClientGrid.Columns[3].Header = "Inserer le";
            ClientGrid.Columns[4].Visibility = Visibility.Hidden;
            ClientGrid.Columns[5].Header = "Raison sociale";
            ClientGrid.Columns[6].Visibility = Visibility.Hidden;
            ClientGrid.Columns[7].Visibility = Visibility.Hidden;
            ClientGrid.Columns[8].Visibility = Visibility.Hidden;
            ClientGrid.Columns[9].Visibility = Visibility.Hidden;
            ClientGrid.Columns[10].Visibility = Visibility.Hidden;
            ClientGrid.Columns[11].Visibility = Visibility.Hidden;
            ClientGrid.Columns[12].Visibility = Visibility.Hidden;
            ClientGrid.Columns[13].Visibility = Visibility.Hidden;
            ClientGrid.Columns[14].Visibility = Visibility.Hidden;
            ClientGrid.Columns[15].Visibility = Visibility.Hidden;
            ClientGrid.Columns[16].Visibility = Visibility.Hidden;
            try
            {
                TelGrid.ItemsSource = telph;
            }
            catch (Exception er)
            {
                MessageBox.Show("erreur", "" + er);
            }
            try
            {
                TelGrid.Columns[0].Header = "Information";
                TelGrid.Columns[1].Header = "Type";
                TelGrid.Columns[0].Width = 220;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

    private void SaveBtn_Click(object sender, RoutedEventArgs e){try{
               
                using (GesComDBClassesDataContext dc = new GesComDBClassesDataContext())
                {
                    
                    _client.CLIENTADRESS = cLIENTADRESSTextBox.Text;
                    _client.CLIENTAI = cLIENTAITextBox.Text;
                    _client.CLIENTCOMPTBANC = cLIENTCOMPTBANCTextBox.Text;
                    _client.CLIENTFIRSTNAME = cLIENTFIRSTNAMETextBox.Text;
                    _client.CLIENTINSERTIONDATE = DateTime.Now;
                    _client.CLIENTLASTNAME = cLIENTLASTNAMETextBox.Text;
                    byte[] arr = cLIENTLOGOImageEdit.EditValue as byte[];
                    MemoryStream str = new MemoryStream();
                    str.Write(arr, 0, arr.Length);
                    _client.CLIENTLOGO = arr ;
                    _client.CLIENTNIF = cLIENTNIFTextBox.Text;
                    _client.CLIENTNIS = cLIENTNISTextBox.Text;_client.CLIENTRC = cLIENTRCTextBox.Text;
                    _client.CLIENTRS = cLIENTRSTextBox.Text;

                    dc.CLIENTs.InsertOnSubmit(_client);
                    dc.SubmitChanges();}
                //this.Validate();
                //orderBindingSource.EndEdit();
                //dc.SubmitChanges();
                MessageBox.Show("Client ajouter, clicker sur OK pour continuer", "Sauvegarde");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Saving");
            }
        }
        private void ClientGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int i = ClientGrid.SelectedIndex;
            if (i< ClientGrid.Items.Count-1)
            {
                GesComDBClassesDataContext dc = new GesComDBClassesDataContext();
                _selected = ClientGrid.SelectedItem as CLIENT;
                var telph = from p in dc.TELEPHONEs
                            where p.CLIENTID == _selected.CLIENTID
                            select new
                            {
                                p.TELEPHONENUMBER,
                                p.TELEPHONETYPE
                            };
                try
                {
                    TelGrid.ItemsSource = telph;
                    TelGrid.Columns[0].Header = "Information";
                    TelGrid.Columns[1].Header = "Type";TelGrid.Columns[0].Width = 220;
                }
                catch (Exception er)
                {
                    MessageBox.Show("erreur", "" + er);
                }
            }
        }

        private void previuesBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = ClientGrid.SelectedIndex;
            if (i > 0)
            {
                ClientGrid.SelectedIndex--;
            }
        }

        private void LastBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = ClientGrid.SelectedIndex;
            if (i < ClientGrid.Items.Count - 1)
            {
                ClientGrid.SelectedIndex = ClientGrid.Items.Count - 2;
            }
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Client ajouter, clicker sur OK pour continuer", "Sauvegarde");
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Client ajouter, clicker sur OK pour continuer", "Sauvegarde");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Client ajouter, clicker sur OK pour continuer", "Sauvegarde");
        }

        private void FirstBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = ClientGrid.SelectedIndex;
            if (i > 0)
            {
                ClientGrid.SelectedIndex = 0;
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = ClientGrid.SelectedIndex;
            if (i < ClientGrid.Items.Count - 2)
            {
                ClientGrid.SelectedIndex++;
            }
        }}
}
