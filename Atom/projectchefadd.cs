using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atom
{
    public partial class projectchefadd : Form
    {
        public string ConnectedUserEmail { get; set; }
        public string ImageFilePath { get; set; }
        private string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";

        public projectchefadd()
        {
            InitializeComponent();
        }

        private void projectchef_Load(object sender, EventArgs e)
        {

            label4.Text = "Email = ( " + ConnectedUserEmail + " )";
            pictureBox4.Image = Image.FromFile(ImageFilePath);

            // date ta3 lyoum
            DateTime currentDate = DateTime.Now;
            label6.Text = currentDate.ToString("yyyy-MM-dd");
            timer1.Start();

            LoadNomClientValues();
            LoadProjects();


            listView3.Columns.Add("Nom Projet");
            listView3.Columns.Add("Date Projet");
            listView3.Columns.Add("Description Projet");
            listView3.Columns.Add("Nom Client");
            listView3.Columns.Add("Date à Rendre");
            listView3.Columns.Add("Type Projet");
            listView3.Columns.Add("État");

            gunaRadioButton1.CheckedChanged += GunaRadioButton1_CheckedChanged;
            gunaRadioButton2.CheckedChanged += GunaRadioButton2_CheckedChanged;
            gunaRadioButton3.CheckedChanged += GunaRadioButton3_CheckedChanged;




        }


        private void GunaRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton1.Checked)
            {
                SortProjectsByEtat("Terminer");
            }
        }

        private void GunaRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton2.Checked)
            {
                SortProjectsByEtat("En cours");
            }
        }

        private void GunaRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton3.Checked)
            {
                SortProjectsByEtat("Arrêté");
            }
        }

        private void SortProjectsByEtat(string etat)
        {
            foreach (ListViewItem item in listView3.Items)
            {
                if (item.SubItems[6].Text == etat)
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                }
                else
                {
                    item.BackColor = Color.LightGray;
                    item.ForeColor = Color.Gray;
                }
            }
        }

        private void SortProjectsByType(string type)
        {
            foreach (ListViewItem item in listView3.Items)
            {
                if (item.SubItems[4].Text == type)
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                }
                else
                {
                    item.BackColor = Color.LightGray;
                    item.ForeColor = Color.Gray;
                }
            }
        }

        private void SortProjectsByDate(string date)
        {
            foreach (ListViewItem item in listView3.Items)
            {
                if (item.SubItems[3].Text == date)
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                }
                else
                {
                    item.BackColor = Color.LightGray;
                    item.ForeColor = Color.Gray;
                }
            }
        }
        private void LoadNomClientValues()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                  
                    string query = "SELECT nomClient FROM client";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    
                    while (reader.Read())
                    {
                        string nomClient = reader.GetString("nomClient");
                        gunaComboBox1.Items.Add(nomClient);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void LoadProjects()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nomProjet, dateProjet, descriptionProjet, nomClientProjet, typeProjet, dateRendreProjet, etatProjet FROM projet";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    listView3.Items.Clear();

                 
                    while (reader.Read())
                    {
                        string nomProjet = reader.GetString("nomProjet");
                        DateTime dateProjet = reader.GetDateTime("dateProjet");
                        string descriptionProjet = reader.GetString("descriptionProjet");
                        string nomClientProjet = reader.GetString("nomClientProjet");
                        DateTime dateRendreProjet = reader.GetDateTime("dateRendreProjet");
                        string typeProjet = reader.GetString("typeProjet");
                         string etatProjet = reader.GetString("etatProjet");

                        ListViewItem item = new ListViewItem(nomProjet);
                        item.SubItems.Add(dateProjet.ToString());
                        item.SubItems.Add(descriptionProjet);
                        item.SubItems.Add(nomClientProjet);

                        item.SubItems.Add(dateRendreProjet.ToString());
                        item.SubItems.Add(typeProjet);

                        item.SubItems.Add(etatProjet);

                        listView3.Items.Add(item);

                        
                    }

                    listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            home home = new home();
            home.ConnectedUserEmail = ConnectedUserEmail;
            home.ImageFilePath = ImageFilePath;
            home.Show();
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

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            Clients Clients = new Clients();
            Clients.ConnectedUserEmail = ConnectedUserEmail;
            Clients.ImageFilePath = ImageFilePath; 
            Clients.Show();
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

        private void gunaLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clients Clients = new Clients();
            Clients.ConnectedUserEmail = ConnectedUserEmail;
            Clients.ImageFilePath = ImageFilePath;
            Clients.Show();
            this.Hide();
        }

        private void gunaButton9_Click(object sender, EventArgs e)
        {
            
            string nomProjet = gunaLineTextBox1.Text.Trim();
            DateTime dateProjet = gunaDateTimePicker1.Value;
            string descriptionProjet = gunaLineTextBox2.Text.Trim();
            string nomClientProjet = gunaComboBox1.SelectedItem != null ? gunaComboBox1.SelectedItem.ToString() : string.Empty;
            string typeProjet = gunaComboBox3.SelectedItem != null ? gunaComboBox3.SelectedItem.ToString() : string.Empty;

            DateTime dateRendreProjet = gunaDateTimePicker2.Value;

            if (string.IsNullOrEmpty(nomProjet) || string.IsNullOrEmpty(descriptionProjet) ||
                string.IsNullOrEmpty(nomClientProjet) || string.IsNullOrEmpty(typeProjet))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO projet (nomProjet, dateProjet, descriptionProjet, nomClientProjet, typeProjet, dateRendreProjet) " +
                                   "VALUES (@nomProjet, @dateProjet, @descriptionProjet, @nomClientProjet, @typeProjet, @dateRendreProjet)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nomProjet", nomProjet);
                    command.Parameters.AddWithValue("@dateProjet", dateProjet);
                    command.Parameters.AddWithValue("@descriptionProjet", descriptionProjet);
                    command.Parameters.AddWithValue("@nomClientProjet", nomClientProjet);
                    command.Parameters.AddWithValue("@typeProjet", typeProjet);
                    command.Parameters.AddWithValue("@dateRendreProjet", dateRendreProjet);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Projet ajouté avec succès.");
                        LoadProjects();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Échec de l'ajout du projet.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message);
            }
        }

        private void gunaButton10_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            gunaLineTextBox1.Clear();
            gunaDateTimePicker1.Value = DateTime.Now;
            gunaLineTextBox2.Clear();
            gunaComboBox1.SelectedIndex = -1;
            gunaComboBox3.SelectedIndex = -1;
            gunaDateTimePicker2.Value = DateTime.Now;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            label7.Text = currentTime.ToString("HH:mm:ss");
        }



        private void UpdateProjectStatus(string newStatus)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView3.SelectedItems[0];
                string currentStatus = selectedItem.SubItems[5].Text;

                if (currentStatus == newStatus)
                {
                    MessageBox.Show("Le projet est déjà dans l'état spécifié.");
                }
                else
                {
                    DialogResult result;

                    if (newStatus == "Terminer")
                    {
                        result = MessageBox.Show("Êtes-vous sûr de vouloir terminer le projet ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    }
                    else if (newStatus == "Arrêté")
                    {
                        result = MessageBox.Show("Êtes-vous sûr de vouloir arrêter le projet ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    }
                    else
                    {
                        MessageBox.Show("État de projet non valide.");
                        return;
                    }

                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (MySqlConnection connection = new MySqlConnection(connectionString))
                            {
                                connection.Open();

                                string query = "UPDATE projet SET etatProjet = @newStatus WHERE nomProjet = @nomProjet";
                                MySqlCommand command = new MySqlCommand(query, connection);
                                command.Parameters.AddWithValue("@newStatus", newStatus);
                                command.Parameters.AddWithValue("@nomProjet", selectedItem.Text);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Le projet a été mis à jour avec succès.");
                                    LoadProjects();
                                }
                                else
                                {
                                    MessageBox.Show("Échec de la mise à jour du projet.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un projet.");
            }
        }

        private void buttonTerminer_Click(object sender, EventArgs e)
        {
            UpdateProjectStatus("Terminer");
        }

        private void buttonArreter_Click(object sender, EventArgs e)
        {
            UpdateProjectStatus("Arrêté");
        }


        private void gunaButton12_Click(object sender, EventArgs e)
        {
            gunaButton11.Enabled = !gunaButton11.Enabled;
            gunaButton13.Enabled = !gunaButton13.Enabled;
            gunaLineTextBox1.Enabled = !gunaLineTextBox1.Enabled;
        }

        private void gunaButton13_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                var selectedItem = listView3.SelectedItems[0];

                
                DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer ce projet ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "DELETE FROM projet WHERE nomProjet = @nomProjet";
                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@nomProjet", selectedItem.Text);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Le projet a été supprimé avec succès.");
                                LoadProjects(); 
                            }
                            else
                            {
                                MessageBox.Show("Échec de la suppression du projet.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un projet.");
            }
        }







private void ListView3_SelectedIndexChanged(object sender, EventArgs e)
{
    if (listView3.SelectedItems.Count > 0)
    {
        ListViewItem selectedItem = listView3.SelectedItems[0];
        
        string nomProjet = selectedItem.SubItems[0].Text;
        string dateProjet = selectedItem.SubItems[1].Text;
        string descriptionProjet = selectedItem.SubItems[2].Text;
        string nomClientProjet = selectedItem.SubItems[3].Text;
        string dateRendreProjet = selectedItem.SubItems[4].Text;
        string selectedTypeProjet = selectedItem.SubItems[5].Text;

        gunaComboBox3.SelectedItem = selectedTypeProjet;

        gunaLineTextBox1.Text = nomProjet;
        gunaDateTimePicker1.Value = DateTime.Parse(dateProjet);
        gunaLineTextBox2.Text = descriptionProjet;
        gunaDateTimePicker1.Value = DateTime.Parse(dateRendreProjet);
        gunaComboBox1.SelectedItem = nomClientProjet;
     }
}

private void gunaButton11_Click(object sender, EventArgs e)
{
     if (listView3.SelectedItems.Count > 0)
    {
        string nomProjet = listView3.SelectedItems[0].SubItems[0].Text;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "UPDATE projet SET nomProjet = @nomProjet, descriptionProjet = @descriptionProjet, nomClientProjet = @nomClientProjet, typeProjet = @typeProjet,  dateProjet = @dateProjet, dateRendreProjet = @dateRendreProjet WHERE nomProjet = @nomProjet";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nomProjet", nomProjet);
            command.Parameters.AddWithValue("@descriptionProjet", gunaLineTextBox2.Text);
            command.Parameters.AddWithValue("@nomClientProjet", gunaComboBox1.SelectedItem.ToString());
            command.Parameters.AddWithValue("@typeProjet", gunaComboBox3.SelectedItem.ToString());
            command.Parameters.AddWithValue("@dateProjet", gunaDateTimePicker1.Value);
            command.Parameters.AddWithValue("@dateRendreProjet", gunaDateTimePicker2.Value);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Projet mis à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadProjects();
            }
            else
            {
                MessageBox.Show("Échec de la mise à jour du projet.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    else
    {
        MessageBox.Show("Veuillez sélectionner un projet.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
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
        

        
        
    


