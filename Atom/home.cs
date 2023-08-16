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

namespace Atom
{
    public partial class home : Form
    {
        private string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";

        public string ConnectedUserEmail { get; set; }
        public string ImageFilePath { get; set; }

        public home()
        {
            InitializeComponent();
                  }

        private void home_Load(object sender, EventArgs e)
        {
            // date ta3 lyoum
            DateTime currentDate = DateTime.Now;
            label3.Text = currentDate.ToString("yyyy-MM-dd");
            timer1.Start();
            chargerprojetlistview();

            label4.Text = "Email = ( " + ConnectedUserEmail + " )";
            pictureBox4.Image = Image.FromFile(ImageFilePath);

            PopulateListView();
            DisplayMessages();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            label2.Text = currentTime.ToString("HH:mm:ss");
        }

        private void PopulateListView()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nom_utilisateur, email_utilisateur, tel_utilisateur FROM utilisateur";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();

                    listView1.Columns.Add("Nom Membre");
                    listView1.Columns.Add("Email Membre");
                    listView1.Columns.Add("Tel Membre");

                    while (reader.Read())
                    {
                        string nomUtilisateur = reader.GetString(0);
                        string emailUtilisateur = reader.GetString(1);
                        string telUtilisateur = reader.GetString(2);

                        ListViewItem item = new ListViewItem(nomUtilisateur);
                        item.SubItems.Add(emailUtilisateur);
                        item.SubItems.Add(telUtilisateur);

                        listView1.Items.Add(item);
                    }

                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
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
            Form1 form1 = new Form1();
            form1.Show();
            this.Dispose();
        }

     

        private void gunaButton9_Click(object sender, EventArgs e)
        {
            string nomMessage = ConnectedUserEmail;
            string contenuMessage = gunaLineTextBox1.Text;
            string dateMessage = DateTime.Now.ToString("yyyy-MM-dd");

            if (contenuMessage != "")
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "INSERT INTO message (nomMessage, contenuMessage, dateMessage) VALUES (@nomMessage, @contenuMessage, @dateMessage)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@nomMessage", nomMessage);
                        command.Parameters.AddWithValue("@contenuMessage", contenuMessage);
                        command.Parameters.AddWithValue("@dateMessage", dateMessage);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Message added successfully!");

                        gunaLineTextBox3.Text = "";

                       
                        DisplayMessages();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter a message!");
            }
        }

        private void chargerprojetlistview()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nomProjet, DateProjet, nomClientProjet, dateRendreProjet, etatProjet FROM projet";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                   
                    listView3.Items.Clear();
                    listView3.Columns.Clear();

                    
                    listView3.Columns.Add("Nom Projet");
                    listView3.Columns.Add("Date Projet");
                    listView3.Columns.Add("Nom Client");
                    listView3.Columns.Add("Date à Rendre");
                    listView3.Columns.Add("État Projet");

                  
                    while (reader.Read())
                    {
                        string nomProjet = reader.GetString(0);
                        string dateProjet = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                        string comClientProjet = reader.GetString(2);
                        string dateRendreProjet = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                        string etatProjet = reader.GetString(4);

                        ListViewItem item = new ListViewItem(nomProjet);
                        item.SubItems.Add(dateProjet);
                        item.SubItems.Add(comClientProjet);
                        item.SubItems.Add(dateRendreProjet);
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
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void DisplayMessages()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nomMessage, contenuMessage FROM message";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    listView2.Items.Clear();
                    listView2.Columns.Clear();

                    listView2.Columns.Add("Nom Message");
                    listView2.Columns.Add("Contenu Message");

                    while (reader.Read())
                    {
                        string nomMessage = reader.GetString(0);
                        string contenuMessage = reader.GetString(1);

                        ListViewItem item = new ListViewItem(nomMessage);
                        item.SubItems.Add(contenuMessage);
                        item.BackColor = Color.White;

                        listView2.Items.Add(item);
                    }

                    listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void gunaRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton3.Checked)
            {
                chargerprojetlistview();
            }
        }

        private void gunaRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton1.Checked)
            {
                FilterProjetListView("En cours");
            }
        }

        private void gunaRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton2.Checked)
            {
                FilterProjetListView("Terminer");
            }
        }

        private void gunaDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            FilterProjetListViewByDate(gunaDateTimePicker1.Value);
        }

        private void FilterProjetListView(string etatProjet)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nomProjet, DateProjet, nomClientProjet, dateRendreProjet, etatProjet FROM projet WHERE etatProjet = @etatProjet";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@etatProjet", etatProjet);
                    MySqlDataReader reader = command.ExecuteReader();

                    // Clear existing items in the ListView
                    listView3.Items.Clear();
                    listView3.Columns.Clear();

                    // Create columns in the ListView
                    listView3.Columns.Add("Nom Projet");
                    listView3.Columns.Add("Date Projet");
                    listView3.Columns.Add("Nom Client");
                    listView3.Columns.Add("Date à Rendre");
                    listView3.Columns.Add("État Projet");

                    // Populate the ListView with data
                    while (reader.Read())
                    {
                        string nomProjet = reader.GetString(0);
                        string dateProjet = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                        string comClientProjet = reader.GetString(2);
                        string dateRendreProjet = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                        string etatProjetValue = reader.GetString(4);

                        ListViewItem item = new ListViewItem(nomProjet);
                        item.SubItems.Add(dateProjet);
                        item.SubItems.Add(comClientProjet);
                        item.SubItems.Add(dateRendreProjet);
                        item.SubItems.Add(etatProjetValue);

                        listView3.Items.Add(item);
                    }

                    // Auto-size the columns
                    listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void FilterProjetListViewByDate(DateTime selectedDate)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nomProjet, DateProjet, nomClientProjet, dateRendreProjet, etatProjet FROM projet WHERE DateProjet = @selectedDate";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@selectedDate", selectedDate.Date);
                    MySqlDataReader reader = command.ExecuteReader();

                    // Clear existing items in the ListView
                    listView3.Items.Clear();
                    listView3.Columns.Clear();

                    // Create columns in the ListView
                    listView3.Columns.Add("Nom Projet");
                    listView3.Columns.Add("Date Projet");
                    listView3.Columns.Add("Nom Client");
                    listView3.Columns.Add("Date à Rendre");
                    listView3.Columns.Add("État Projet");

                    // Populate the ListView with data
                    bool hasData = false;
                    while (reader.Read())
                    {
                        hasData = true;
                        string nomProjet = reader.GetString(0);
                        string dateProjet = reader.GetDateTime(1).ToString("yyyy-MM-dd");
                        string comClientProjet = reader.GetString(2);
                        string dateRendreProjet = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                        string etatProjetValue = reader.GetString(4);

                        ListViewItem item = new ListViewItem(nomProjet);
                        item.SubItems.Add(dateProjet);
                        item.SubItems.Add(comClientProjet);
                        item.SubItems.Add(dateRendreProjet);
                        item.SubItems.Add(etatProjetValue);

                        listView3.Items.Add(item);
                    }

                    // Auto-size the columns
                    listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView3.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                    reader.Close();

                    if (!hasData)
                    {
                        MessageBox.Show("No projects found for the selected date.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void gunaButton13_Click(object sender, EventArgs e)
        {

        }

       
       

        private void gunaButton13_Click_1(object sender, EventArgs e)
        {
            this.Close();
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

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            Clients Clients = new Clients();
            Clients.ConnectedUserEmail = ConnectedUserEmail;
            Clients.ImageFilePath = ImageFilePath;
            Clients.Show();
            this.Hide();
        }

        private void gunaButton5_Click_2(object sender, EventArgs e)
        {
            ressources ressources = new ressources();
            ressources.ConnectedUserEmail = ConnectedUserEmail;
            ressources.ImageFilePath = ImageFilePath;
            ressources.Show();
            this.Hide();
        }

        private void gunaButton6_Click_2(object sender, EventArgs e)
        {
            reglage reglagechef = new reglage();
            reglagechef.ConnectedUserEmail = ConnectedUserEmail;
            reglagechef.ImageFilePath = ImageFilePath;
            reglagechef.Show();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Form1 disconnect = new Form1();
            disconnect.Show();
            this.Close();
        }
    }
}
