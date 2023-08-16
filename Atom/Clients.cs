using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms.DataVisualization.Charting;
namespace Atom
{
    public partial class Clients : Form
    {
        private string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";
        private Dictionary<string, List<string>> regionsByCountry;
        public string ConnectedUserEmail { get; set; }
        public string ImageFilePath { get; set; }

        private bool isButtonsEnabled = false;
        public Clients()
        {
            InitializeComponent();

            

        }

        private void InitializeRegionsByCountry()
        {
            regionsByCountry = new Dictionary<string, List<string>>();

            regionsByCountry = new Dictionary<string, List<string>>();

            regionsByCountry.Add("Tunisie", new List<string>
    {
        "Ariana",
        "Béja",
        "Ben Arous",
        "Bizerte",
        "Gabès",
        "Gafsa",
        "Jendouba",
        "Kairouan",
        "Kasserine",
        "Kébili",
        "Le Kef",
        "Mahdia",
        "La Manouba",
        "Médenine",
        "Monastir",
        "Nabeul",
        "Sfax",
        "Sidi Bouzid",
        "Siliana",
        "Sousse",
        "Tataouine",
        "Tozeur",
        "Tunis",
        "Zaghouan"
    });

            regionsByCountry.Add("France", new List<string>
    {
        "Auvergne-Rhône-Alpes",
        "Bourgogne-Franche-Comté",
        "Bretagne",
        "Centre-Val de Loire",
        "Corse",
        "Grand Est",
        "Hauts-de-France",
        "Île-de-France",
        "Normandie",
        "Nouvelle-Aquitaine",
        "Occitanie",
        "Pays de la Loire",
        "Provence-Alpes-Côte d'Azur"
    });

            regionsByCountry.Add("États-Unis", new List<string>
    {
        "Alabama",
        "Alaska",
        "Arizona",
        "Arkansas",
        "Californie",
        "Caroline du Nord",
        "Caroline du Sud",
        "Colorado",
        "Connecticut",
        "Dakota du Nord",
        "Dakota du Sud",
        "Delaware",
        "Floride",
        "Géorgie",
        "Hawaï",
        "Idaho",
        "Illinois",
        "Indiana",
        "Iowa",
        "Kansas",
        "Kentucky",
        "Louisiane",
        "Maine",
        "Maryland",
        "Massachusetts",
        "Michigan",
        "Minnesota",
        "Mississippi",
        "Missouri",
        "Montana",
        "Nebraska",
        "Nevada",
        "New Hampshire",
        "New Jersey",
        "New York",
        "Nouveau-Mexique",
        "Ohio",
        "Oklahoma",
        "Oregon",
        "Pennsylvanie",
        "Rhode Island",
        "Tennessee",
        "Texas",
        "Utah",
        "Vermont",
        "Virginie",
        "Virginie-Occidentale",
        "Washington",
        "Wisconsin",
        "Wyoming"
    });
            regionsByCountry.Add("Maroc", new List<string>
{
    "Tanger-Tétouan-Al Hoceïma",
    "Oriental",
    "Fès-Meknès",
    "Rabat-Salé-Kénitra",
    "Béni Mellal-Khénifra",
    "Casablanca-Settat",
    "Marrakech-Safi",
    "Drâa-Tafilalet",
    "Souss-Massa",
    "Guelmim-Oued Noun",
    "Laâyoune-Sakia El Hamra",
    "Dakhla-Oued Ed-Dahab"
});

            regionsByCountry.Add("Algérie", new List<string>
{
    "Adrar",
    "Chlef",
    "Laghouat",
    "Oum El Bouaghi",
    "Batna",
    "Béjaïa",
    "Biskra",
    "Béchar",
    "Blida",
    "Bouira",
    "Tamanrasset",
    "Tébessa",
    "Tlemcen",
    "Tiaret",
    "Tizi Ouzou",
    "Alger",
    "Djelfa",
    "Jijel",
    "Sétif",
    "Saïda",
    "Skikda",
    "Sidi Bel Abbès",
    "Annaba",
    "Guelma",
    "Constantine",
    "Médéa",
    "Mostaganem",
    "M'Sila",
    "Mascara",
    "Ouargla",
    "Oran",
    "El Bayadh",
    "Illizi",
    "Bordj Bou Arreridj",
    "Boumerdès",
    "El Tarf",
    "Tindouf",
    "Tissemsilt",
    "El Oued",
    "Khenchela",
    "Souk Ahras",
    "Tipaza",
    "Mila",
    "Aïn Defla",
    "Naâma",
    "Aïn Témouchent",
    "Ghardaïa",
    "Relizane"
});
          
        }

   

        private void gunaButton10_Click(object sender, EventArgs e)
        {
            string nomClient = gunaLineTextBox4.Text;
            string emailClient = gunaLineTextBox5.Text;
            string telClient = gunaLineTextBox6.Text;
            string paysClient = gunaComboBox3.SelectedItem != null ? gunaComboBox3.SelectedItem.ToString() : null;
            string regionClient = gunaComboBox4.SelectedItem != null ? gunaComboBox4.SelectedItem.ToString() : null;
            string adressClient = gunaLineTextBox7.Text;
            string typeClient = gunaComboBox5.SelectedItem != null ? gunaComboBox5.SelectedItem.ToString() : null;

            if (!string.IsNullOrWhiteSpace(nomClient) &&
                !string.IsNullOrWhiteSpace(emailClient) &&
                !string.IsNullOrWhiteSpace(telClient) &&
                !string.IsNullOrWhiteSpace(paysClient) &&
                !string.IsNullOrWhiteSpace(regionClient) &&
                !string.IsNullOrWhiteSpace(adressClient) &&
                !string.IsNullOrWhiteSpace(typeClient))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string checkDuplicateQuery = "SELECT COUNT(*) FROM client WHERE emailClient = @email";
                    MySqlCommand checkDuplicateCommand = new MySqlCommand(checkDuplicateQuery, connection);
                    checkDuplicateCommand.Parameters.AddWithValue("@email", emailClient);
                    int duplicateCount = Convert.ToInt32(checkDuplicateCommand.ExecuteScalar());

                    if (duplicateCount == 0)
                    {
                        string insertQuery = "INSERT INTO client (nomClient, emailClient, telClient, paysClient, regionClient, adressClient, typeClient) " +
                            "VALUES (@nomClient, @email, @telClient, @paysClient, @regionClient, @adressClient, @typeClient)";
                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@nomClient", nomClient);
                        insertCommand.Parameters.AddWithValue("@email", emailClient);
                        insertCommand.Parameters.AddWithValue("@telClient", telClient);
                        insertCommand.Parameters.AddWithValue("@paysClient", paysClient);
                        insertCommand.Parameters.AddWithValue("@regionClient", regionClient);
                        insertCommand.Parameters.AddWithValue("@adressClient", adressClient);
                        insertCommand.Parameters.AddWithValue("@typeClient", typeClient);
                        insertCommand.ExecuteNonQuery();


                        LoadClients();
                        MessageBox.Show("Les informations sur le client ont été enregistrées avec succès dans la base de données.");
                    }
                    else
                    {
                        MessageBox.Show("Un client avec le même email existe déjà. Veuillez fournir un autre e-mail.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs d'informations sur le client.");
            }


        }


        private void gunaComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCountry = gunaComboBox3.SelectedItem.ToString();

            if (regionsByCountry.ContainsKey(selectedCountry))
            {
                gunaComboBox4.Items.Clear();
                foreach (string region in regionsByCountry[selectedCountry])
                {
                    gunaComboBox4.Items.Add(region);
                }
            }
        }

        private void LoadClients()
        {
            listView1.Items.Clear();
            gunaComboBox2.Items.Clear();
            chart1.Series.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT nomClient, emailClient, telClient, paysClient, typeClient FROM client";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                Dictionary<string, int> clientCountByCountry = new Dictionary<string, int>();

                while (reader.Read())
                {
                    string nomClient = reader.GetString("nomClient");
                    string emailClient = reader.GetString("emailClient");
                    string telClient = reader.GetString("telClient");
                    string paysClient = reader.GetString("paysClient");
                    string typeClient = reader.GetString("typeClient");

                    ListViewItem item = new ListViewItem(nomClient);
                    item.SubItems.Add(emailClient);
                    item.SubItems.Add(telClient);
                    item.SubItems.Add(paysClient);
                    item.SubItems.Add(typeClient);

                    listView1.Items.Add(item);

                    gunaComboBox2.Items.Add(nomClient);

                    if (clientCountByCountry.ContainsKey(paysClient))
                    {
                        clientCountByCountry[paysClient]++;
                    }
                    else
                    {
                        clientCountByCountry[paysClient] = 1;
                    }
                }

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                reader.Close();

                Series series = chart1.Series.Add("Nbr Client /Pays");
                series.ChartType = SeriesChartType.Column;

                foreach (var entry in clientCountByCountry)
                {
                    series.Points.AddXY(entry.Key, entry.Value);
                }
            }
        }

        private void Clients_Load(object sender, EventArgs e)
        {

            label4.Text = "Email = ( " + ConnectedUserEmail + " )";
            pictureBox4.Image = Image.FromFile(ImageFilePath);

            DateTime currentDate = DateTime.Now;
             label6.Text = currentDate.ToString("yyyy-MM-dd");
            timer1.Start();


            InitializeRegionsByCountry();

            gunaComboBox3.SelectedIndexChanged += gunaComboBox3_SelectedIndexChanged;
            gunaComboBox2.SelectedIndexChanged += gunaComboBox2_SelectedIndexChanged;

            LoadClients();
        }

        private void gunaButton15_Click(object sender, EventArgs e)
        {
            gunaLineTextBox4.Text = string.Empty;  
            gunaLineTextBox5.Text = string.Empty;  
            gunaLineTextBox6.Text = string.Empty;  
            gunaLineTextBox7.Text = string.Empty;  
          
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];


                string emailClient = selectedItem.SubItems[1].Text;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM client WHERE emailClient = @email";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@email", emailClient);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string nomClient = reader.GetString("nomClient");
                        string email = reader.GetString("emailClient");
                        string telClient = reader.GetString("telClient");
                        string paysClient = reader.GetString("paysClient");
                        string regionClient = reader.GetString("regionClient");
                        string adressClient = reader.GetString("adressClient");
                        string typeClient = reader.GetString("typeClient");

                        gunaLineTextBox4.Text = nomClient;
                        gunaLineTextBox5.Text = email;
                        gunaLineTextBox6.Text = telClient;
                        gunaComboBox3.SelectedItem = paysClient;
                        gunaComboBox4.SelectedItem = regionClient;
                        gunaLineTextBox7.Text = adressClient;
                        gunaComboBox5.SelectedItem = typeClient;
                    }

                    reader.Close();
                }
            }
        }

        private void gunaButton18_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer le client sélectionné ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    ListViewItem selectedItem = listView1.SelectedItems[0];

                    string emailClient = selectedItem.SubItems[1].Text;

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM client WHERE emailClient = @email";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@email", emailClient);
                        deleteCommand.ExecuteNonQuery();

                        listView1.Items.Remove(selectedItem);

                        MessageBox.Show("Le client a été supprimé avec succès.");

                        LoadClients();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à supprimer.", "Aucun client sélectionné", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaButton16_Click(object sender, EventArgs e)
        {
            if (isButtonsEnabled)
            {
                gunaButton18.Enabled = false;
                gunaButton17.Enabled = false;
                isButtonsEnabled = false;
            }
            else
            {
                gunaButton18.Enabled = true;
                gunaButton17.Enabled = true;
                isButtonsEnabled = true;


            }
        }

        private void gunaComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gunaComboBox2.SelectedIndex >= 0)
            {
                string selectedClient = gunaComboBox2.SelectedItem.ToString();

                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.SubItems[0].Text == selectedClient)
                    {
                        string emailClient = item.SubItems[1].Text;
                        gunaLineTextBox1.Text = emailClient;
                        break;
                    }
                }
            }
        }

        private void gunaButton9_Click(object sender, EventArgs e)
        {
            string recipient = gunaLineTextBox1.Text;
            string subject = gunaLineTextBox3.Text;
            string body = gunaTextBox1.Text;
            

            SendEmail(recipient, subject, body);
        }
        private void SendEmail(string recipient, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(ConnectedUserEmail);
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtpClient = new SmtpClient("smtp-relay.sendinblue.com", 587); 
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("erzerino2@gmail.com", "bzPyJIF5t3Mkfcwn");

                smtpClient.Send(mail);

                MessageBox.Show("E-mail envoyé avec succès!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while sending the email: " + ex.Message);
            }
        }

        private void gunaButton12_Click(object sender, EventArgs e)
        {
            gunaLineTextBox1.Text = string.Empty;
            gunaLineTextBox3.Text = string.Empty;
            gunaTextBox1.Text = string.Empty;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            home home = new home();
            home.ConnectedUserEmail = ConnectedUserEmail;
            home.ImageFilePath = ImageFilePath;
            home.Show();
            this.Hide();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            projectchefadd projectchefadd = new projectchefadd();
            projectchefadd.ConnectedUserEmail = ConnectedUserEmail;
            projectchefadd.ImageFilePath = ImageFilePath;
            projectchefadd.Show();
            this.Hide();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            Equipe Equipe = new Equipe();
            Equipe.ConnectedUserEmail = ConnectedUserEmail;
            Equipe.ImageFilePath = ImageFilePath;
            Equipe.Show();
            this.Hide();
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            ressources ressources = new ressources();
            ressources.ConnectedUserEmail = ConnectedUserEmail;
            ressources.ImageFilePath = ImageFilePath;
            ressources.Show();
            this.Hide();
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            reglage reglagechef = new reglage();
            reglagechef.ConnectedUserEmail = ConnectedUserEmail;
            reglagechef.ImageFilePath = ImageFilePath;
            reglagechef.Show();
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            Form1 deconnection = new Form1();
            deconnection.Show();
            this.Close();
        }

        private void gunaButton17_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];

                string emailClient = selectedItem.SubItems[1].Text;

                string nomClient = gunaLineTextBox4.Text;
                string email = gunaLineTextBox5.Text;
                string telClient = gunaLineTextBox6.Text;
                string paysClient = gunaComboBox3.SelectedItem != null ? gunaComboBox3.SelectedItem.ToString() : null;
                string regionClient = gunaComboBox4.SelectedItem != null ? gunaComboBox4.SelectedItem.ToString() : null;
                string adressClient = gunaLineTextBox7.Text;
                string typeClient = gunaComboBox5.SelectedItem != null ? gunaComboBox5.SelectedItem.ToString() : null;

                if (string.IsNullOrEmpty(nomClient) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(telClient)
                    || string.IsNullOrEmpty(paysClient) || string.IsNullOrEmpty(regionClient)
                    || string.IsNullOrEmpty(adressClient) || string.IsNullOrEmpty(typeClient))
                {
                    MessageBox.Show("Veuillez remplir tous les champs.", "Données manquantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE client SET nomClient = @nomClient, emailClient = @email,"
                                         + "telClient = @telClient, paysClient = @paysClient, "
                                         + "regionClient = @regionClient, adressClient = @adressClient, "
                                         + "typeClient = @typeClient WHERE emailClient = @emailClient";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@nomClient", nomClient);
                    updateCommand.Parameters.AddWithValue("@email", email);
                    updateCommand.Parameters.AddWithValue("@telClient", telClient);
                    updateCommand.Parameters.AddWithValue("@paysClient", paysClient);
                    updateCommand.Parameters.AddWithValue("@regionClient", regionClient);
                    updateCommand.Parameters.AddWithValue("@adressClient", adressClient);
                    updateCommand.Parameters.AddWithValue("@typeClient", typeClient);
                    updateCommand.Parameters.AddWithValue("@emailClient", emailClient);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        LoadClients();
                        MessageBox.Show("Les données du client ont été mises à jour avec succès.", "Mise à jour réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Impossible de mettre à jour les données du client.", "Échec de la mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à mettre à jour.", "Aucun client sélectionné", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void gunaButton19_Click(object sender, EventArgs e)
{
    string searchValue = gunaComboBox1.SelectedItem.ToString();
    string searchText = gunaLineTextBox2.Text;

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        connection.Open();

        string columnName;
        switch (searchValue)
        {
            case "Nom":
                columnName = "nomClient";
                break;
            case "Pays":
                columnName = "paysClient";
                break;
            case "Region":
                columnName = "regionClient";
                break;
            case "Type":
                columnName = "typeClient";
                break;
            default:
                MessageBox.Show("Invalid search option");
                return;
        }

        string query = "SELECT * FROM client WHERE " + columnName + " = @searchText";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@searchText", searchText);

        MySqlDataReader reader = command.ExecuteReader();

        listView1.Items.Clear();
        bool found = false;

        while (reader.Read())
        {
            string nomClient = reader.GetString("nomClient");
            string emailClient = reader.GetString("emailClient");
            string telClient = reader.GetString("telClient");
            string paysClient = reader.GetString("paysClient");
            string typeClient = reader.GetString("typeClient");

            ListViewItem item = new ListViewItem(nomClient);
            item.SubItems.Add(emailClient);
            item.SubItems.Add(telClient);
            item.SubItems.Add(paysClient);
            item.SubItems.Add(typeClient);

            listView1.Items.Add(item);
            found = true;
        }

        reader.Close();

        if (!found)
        {
            MessageBox.Show("Pas de clients trouvés");
        }
    }
}

        private void gunaButton13_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
 label7.Text = currentTime.ToString("HH:mm:ss");
        }

        private void gunaButton8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


    }
}
