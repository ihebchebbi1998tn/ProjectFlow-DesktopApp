using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Atom
{
    public partial class ressources : Form
    {
        private string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";

        public string ConnectedUserEmail { get; set; }
        public string ImageFilePath { get; set; }

        private bool isButtonsEnabled = false;

        public ressources()
        {
            InitializeComponent();
           

        }

        private void ressources_Load(object sender, EventArgs e)
        {

            DateTime currentDate = DateTime.Now;
            label6.Text = currentDate.ToString("yyyy-MM-dd");
            timer1.Start();


            label4.Text = "Email = ( " + ConnectedUserEmail + " )";
            pictureBox4.Image = Image.FromFile(ImageFilePath);

            listView1.Columns.Add("Description", 200);
            listView1.Columns.Add("Date Achat", 150);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("Marque", 100);

            LoadData(); 
            LoadComboBoxData(); 
            LoadDataAffectation();
        }

        private void LoadData()
        {
            listView1.Items.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT descriptionRessource, dateAchatRessource, typeRessource, marqueRessource FROM ressource", connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string description = reader.GetString(0);
                    DateTime dateAchat = reader.GetDateTime(1);
                    string type = reader.GetString(2);
                    string marque = reader.GetString(3);

                    ListViewItem item = new ListViewItem(description);
                    item.SubItems.Add(dateAchat.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(type);
                    item.SubItems.Add(marque);
                    listView1.Items.Add(item);

                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }

                reader.Close();
            }
        }

        private void LoadComboBoxData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand emailCommand = new MySqlCommand("SELECT email_utilisateur FROM utilisateur", connection);
                MySqlDataReader emailReader = emailCommand.ExecuteReader();

                while (emailReader.Read())
                {
                    string email = emailReader.GetString(0);
                    gunaComboBox2.Items.Add(email);
                }

                emailReader.Close();

                MySqlCommand descriptionCommand = new MySqlCommand("SELECT descriptionRessource FROM ressource", connection);
                MySqlDataReader descriptionReader = descriptionCommand.ExecuteReader();

                while (descriptionReader.Read())
                {
                    string description = descriptionReader.GetString(0);
                    gunaComboBox5.Items.Add(description);
                }

                descriptionReader.Close();
            }
        }

        private void gunaButton10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gunaLineTextBox4.Text) ||
                gunaDateTimePicker1.Value == null ||
                gunaComboBox3.SelectedIndex == -1 ||
                gunaComboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("merci de remplir tous les champs obligatoires.");
                return;
            }

            string description = gunaLineTextBox4.Text;
            DateTime dateAchat = gunaDateTimePicker1.Value;
            string type = gunaComboBox3.SelectedItem.ToString();
            string marque = gunaComboBox4.SelectedItem.ToString();

            byte[] photo = null;
            if (gunaPictureBox1.Image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    gunaPictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    photo = ms.ToArray();
                }
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("INSERT INTO ressource (descriptionRessource, dateAchatRessource, typeRessource, marqueRessource, photoRessource) VALUES (@description, @dateAchat, @type, @marque, @photo)", connection);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@dateAchat", dateAchat);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@marque", marque);
                command.Parameters.AddWithValue("@photo", photo);
                command.ExecuteNonQuery();

                LoadData();
                LoadComboBoxData(); 

            }
        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                gunaPictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void gunaButton9_Click(object sender, EventArgs e)
        {
            if (gunaComboBox2.SelectedItem == null ||
             gunaComboBox5.SelectedItem == null ||
             gunaDateTimePicker1.Value == null ||
             gunaDateTimePicker2.Value == null)
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }


            string membreAffectation = gunaComboBox2.SelectedItem.ToString();
            string ressourceAffectation = gunaComboBox5.SelectedItem.ToString();



            DateTime dateAffecatation = gunaDateTimePicker1.Value.Date;
            DateTime rendreAffectation = gunaDateTimePicker2.Value.Date;


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand checkCommand = new MySqlCommand("SELECT COUNT(*) FROM affectation WHERE membreAffectation = @membreAffectation AND ressourceAffectation = @ressourceAffectation AND dateAffecatation = @dateAffecatation AND rendreAffectation = @rendreAffectation", connection);
                checkCommand.Parameters.AddWithValue("@membreAffectation", membreAffectation);
                checkCommand.Parameters.AddWithValue("@ressourceAffectation", ressourceAffectation);
                checkCommand.Parameters.AddWithValue("@dateAffecatation", dateAffecatation);
                checkCommand.Parameters.AddWithValue("@rendreAffectation", rendreAffectation);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("La même combinaison de données existe déjà dans la table 'affectation'.");
                    return;
                }

                MySqlCommand insertCommand = new MySqlCommand("INSERT INTO affectation (membreAffectation, ressourceAffectation, dateAffecatation, rendreAffectation) VALUES (@membreAffectation, @ressourceAffectation, @dateAffecatation, @rendreAffectation)", connection);
                insertCommand.Parameters.AddWithValue("@membreAffectation", membreAffectation);
                insertCommand.Parameters.AddWithValue("@ressourceAffectation", ressourceAffectation);
                insertCommand.Parameters.AddWithValue("@dateAffecatation", dateAffecatation);
                insertCommand.Parameters.AddWithValue("@rendreAffectation", rendreAffectation);
                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Affectation ajoutée avec succès.");

                gunaComboBox2.SelectedItem = null;
                gunaComboBox5.SelectedItem = null;
                gunaDateTimePicker1.Value = DateTime.Now;
                gunaDateTimePicker2.Value = DateTime.Now;

                LoadDataAffectation();
            }
        }

        private void LoadDataAffectation()
        {
            listView2.Items.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT membreAffectation, ressourceAffectation, dateAffecatation, statut FROM affectation", connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string membreAffectation = reader.GetString(0);
                    string ressourceAffectation = reader.GetString(1);
                    DateTime dateAffecatation = reader.GetDateTime(2);
                    string statut = reader.GetString(3);

                    ListViewItem item = new ListViewItem(membreAffectation);
                    item.SubItems.Add(ressourceAffectation);
                    item.SubItems.Add(dateAffecatation.ToString("yyyy-MM-dd"));
                    item.SubItems.Add(statut);
                    listView2.Items.Add(item);

                    listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }

                reader.Close();
            }
        }

        private void gunaButton6_Click(object sender, EventArgs e)
        {
            reglage reglagechef = new reglage();
            reglagechef.ConnectedUserEmail = ConnectedUserEmail;
            reglagechef.ImageFilePath = ImageFilePath;
            reglagechef.Show();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            Clients Clients = new Clients();
            Clients.ConnectedUserEmail = ConnectedUserEmail;
            Clients.ImageFilePath = ImageFilePath;
            Clients.Show();
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

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            projectchefadd projectchefadd = new projectchefadd();
            projectchefadd.ConnectedUserEmail = ConnectedUserEmail;
            projectchefadd.ImageFilePath = ImageFilePath;
            projectchefadd.Show();
            this.Hide();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            home Equipe = new home();
            Equipe.ConnectedUserEmail = ConnectedUserEmail;
            Equipe.ImageFilePath = ImageFilePath;
            Equipe.Show();
            this.Hide();
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

        private void gunaButton18_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ressource ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ListViewItem selectedItem = listView1.SelectedItems[0];
                    string description = selectedItem.SubItems[0].Text;

                    listView1.Items.Remove(selectedItem);

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        MySqlCommand deleteCommand = new MySqlCommand("DELETE FROM ressource WHERE descriptionRessource = @description", connection);
                        deleteCommand.Parameters.AddWithValue("@description", description);
                        deleteCommand.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ressource à supprimer.", "Aucune ressource sélectionnée", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaButton17_Click(object sender, EventArgs e)
        {

            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = listView1.SelectedItems[0];

                    string newDescription = gunaLineTextBox4.Text;
                    DateTime newDateAchat = gunaDateTimePicker1.Value;
                    string newType = gunaComboBox3.SelectedItem.ToString();
                    string newMarque = gunaComboBox4.SelectedItem.ToString();

                    selectedItem.SubItems[0].Text = newDescription;
                    selectedItem.SubItems[1].Text = newDateAchat.ToString("yyyy-MM-dd");
                    selectedItem.SubItems[2].Text = newType;
                    selectedItem.SubItems[3].Text = newMarque;

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        MySqlCommand updateCommand = new MySqlCommand("UPDATE ressource SET descriptionRessource = @newDescription, dateAchatRessource = @newDateAchat, typeRessource = @newType, marqueRessource = @newMarque WHERE descriptionRessource = @description", connection);
                        updateCommand.Parameters.AddWithValue("@newDescription", newDescription);
                        updateCommand.Parameters.AddWithValue("@newDateAchat", newDateAchat);
                        updateCommand.Parameters.AddWithValue("@newType", newType);
                        updateCommand.Parameters.AddWithValue("@newMarque", newMarque);
                        updateCommand.Parameters.AddWithValue("@description", selectedItem.SubItems[0].Text);
                        updateCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a resource to update.", "No resource selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];

                string description = selectedItem.SubItems[0].Text;
                DateTime dateAchat = DateTime.ParseExact(selectedItem.SubItems[1].Text, "yyyy-MM-dd", null);
                string type = selectedItem.SubItems[2].Text;
                string marque = selectedItem.SubItems[3].Text;

                gunaLineTextBox4.Text = description;
                gunaDateTimePicker1.Value = dateAchat;
                gunaComboBox3.SelectedItem = type;
                gunaComboBox4.SelectedItem = marque;
            }

        }

        private void gunaButton11_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView2.SelectedItems[0];

                string membreAffectation = selectedItem.SubItems[0].Text;
                string ressourceAffectation = selectedItem.SubItems[1].Text;
                DateTime dateAffecatation = DateTime.ParseExact(selectedItem.SubItems[2].Text, "yyyy-MM-dd", null);
                string statut = selectedItem.SubItems[3].Text;

                if ((statut == "en cours") || (statut == "Pas Remis"))
                {
                    selectedItem.SubItems[3].Text = "Remis";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        MySqlCommand updateCommand = new MySqlCommand("UPDATE affectation SET statut = @newStatut WHERE membreAffectation = @membreAffectation AND ressourceAffectation = @ressourceAffectation AND dateAffecatation = @dateAffecatation", connection);
                        updateCommand.Parameters.AddWithValue("@newStatut", "Remis");
                        updateCommand.Parameters.AddWithValue("@membreAffectation", membreAffectation);
                        updateCommand.Parameters.AddWithValue("@ressourceAffectation", ressourceAffectation);
                        updateCommand.Parameters.AddWithValue("@dateAffecatation", dateAffecatation);
                        updateCommand.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("L'élément sélectionné n'a pas le statut 'en cours'.", "Statut invalide", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un élément dans la liste.", "Aucun élément sélectionné", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaButton12_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView2.SelectedItems[0];
                string membreAffectation = selectedItem.SubItems[0].Text;
                string ressourceAffectation = selectedItem.SubItems[1].Text;

                selectedItem.SubItems[3].Text = "Pas Remis";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand updateCommand = new MySqlCommand("UPDATE affectation SET statut = 'Pas Remis' WHERE membreAffectation = @membreAffectation AND ressourceAffectation = @ressourceAffectation", connection);
                    updateCommand.Parameters.AddWithValue("@membreAffectation", membreAffectation);
                    updateCommand.Parameters.AddWithValue("@ressourceAffectation", ressourceAffectation);
                    updateCommand.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une affectation à mettre à jour.", "Aucune affectation sélectionnée.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void gunaButton13_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = gunaDateTimePicker4.Value.Date;

            listView2.Items.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand selectCommand = new MySqlCommand("SELECT * FROM affectation WHERE dateAffecatation = @selectedDate", connection);
                selectCommand.Parameters.AddWithValue("@selectedDate", selectedDate);

                using (MySqlDataReader reader = selectCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string membreAffectation = reader.GetString("membreAffectation");
                            string ressourceAffectation = reader.GetString("ressourceAffectation");
                            DateTime dateEmprunt = reader.GetDateTime("dateAffecatation");
                            string statut = reader.GetString("statut");

                            ListViewItem item = new ListViewItem(membreAffectation);
                            item.SubItems.Add(ressourceAffectation);
                            item.SubItems.Add(dateEmprunt.ToString("yyyy-MM-dd"));
                            item.SubItems.Add(statut);

                            listView2.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Aucune affectation à cette date.", "Aucune Affectation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void gunaButton19_Click(object sender, EventArgs e)
        {
            if (gunaComboBox1.SelectedItem == null || string.IsNullOrWhiteSpace(gunaLineTextBox2.Text))
            {
                MessageBox.Show("Please select a search criteria and enter a search value.");
                return;
            }

            string searchCriteria = gunaComboBox1.SelectedItem.ToString();
            string searchValue = gunaLineTextBox2.Text;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT descriptionRessource, dateAchatRessource, typeRessource, marqueRessource FROM ressource";
                string condition = string.Empty;

                switch (searchCriteria)
                {
                    case "Description":
                        condition = "descriptionRessource = '" + searchValue + "'";
                        break;
                    case "Date Achat":
                        condition = "dateAchatRessource = '" + searchValue + "'";
                        break;
                    case "Type":
                        condition = "typeRessource = '" + searchValue + "'";
                        break;
                    case "Marque":
                        condition = "marqueRessource = '" + searchValue + "'";
                        break;
                }

                if (!string.IsNullOrEmpty(condition))
                {
                    query += " WHERE " + condition;
                }

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                listView1.Items.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(0);
                        DateTime dateAchat = reader.GetDateTime(1);
                        string type = reader.GetString(2);
                        string marque = reader.GetString(3);

                        ListViewItem item = new ListViewItem(description);
                        item.SubItems.Add(dateAchat.ToString("yyyy-MM-dd"));
                        item.SubItems.Add(type);
                        item.SubItems.Add(marque);
                        listView1.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Pas de ressource trouvée.");
                }

                reader.Close();
            }

        }




        private void timer1_Tick_1(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            label7.Text = currentTime.ToString("HH:mm:ss");
        }

        private void gunaButton8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            Form1 deconnection = new Form1();
            deconnection.Show();
            this.Close();
        }


    }
}
       



