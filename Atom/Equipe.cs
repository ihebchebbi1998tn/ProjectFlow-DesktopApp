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
using System.Net.Mail;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;


namespace Atom
{
    public partial class Equipe : Form
    {
        public string ConnectedUserEmail { get; set; }
        public string ImageFilePath { get; set; }

        private string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";
        private string selectedEmail = string.Empty;

        public Equipe()
        {
            InitializeComponent();
        }

        private void Equipe_Load(object sender, EventArgs e)
        {
            // date ta3 lyoum
            DateTime currentDate = DateTime.Now;
            label6.Text = currentDate.ToString("yyyy-MM-dd");
            timer1.Start();

            label4.Text = "Email = ( " + ConnectedUserEmail + " )";
            pictureBox4.Image = Image.FromFile(ImageFilePath);

            PopulateListView();
            LoadProjetNames();
            Loademailclients();

            chart1.Series.Add("Performance");
            chart1.Series["Performance"].ChartType = SeriesChartType.Column;
            LoadChartData();
        }

        private void LoadChartData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT s.membreSession, s.tempSession
                             FROM Session s
                             INNER JOIN
                             (
                                 SELECT membreSession, MAX(tempSession) AS maxSession
                                 FROM Session
                                 GROUP BY membreSession
                             ) t ON s.membreSession = t.membreSession AND s.tempSession = t.maxSession
                             ORDER BY s.tempSession DESC";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    chart1.Series["Performance"].Points.Clear();

                    while (reader.Read())
                    {
                        string memberName = reader.GetString(0);
                        TimeSpan totalTime = TimeSpan.Parse(reader.GetString(1));

                        chart1.Series["Performance"].Points.AddXY(memberName, totalTime.TotalMinutes);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }






        private void Loademailclients()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT email_utilisateur FROM utilisateur";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string emailUtilisateur = reader.GetString(0);
                        gunaComboBox2.Items.Add(emailUtilisateur);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void gunaComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gunaComboBox2.SelectedIndex != -1)
            {
                gunaLineTextBox1.Text = gunaComboBox2.SelectedItem.ToString();
            }
        }
        private void LoadProjetNames()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nomProjet FROM projet";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    gunaComboBox1.Items.Clear();

                    while (reader.Read())
                    {
                        string nomProjet = reader.GetString(0);
                        gunaComboBox1.Items.Add(nomProjet);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void gunaRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton1.Checked)
            {
                SortListViewByDisponibilite("disponible");
            }
        }

        private void gunaRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton2.Checked)
            {
                SortListViewByDisponibilite("occupe");
            }
        }

        private void gunaRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (gunaRadioButton3.Checked)
            {
                PopulateListView();
            }
        }

        private void SortListViewByDisponibilite(string disponibilite)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nom_utilisateur, email_utilisateur, tel_utilisateur, dispo_utilisateur FROM utilisateur WHERE dispo_utilisateur = @dispo";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@dispo", disponibilite);
                    MySqlDataReader reader = command.ExecuteReader();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();

                    listView1.Columns.Add("Nom Membre");
                    listView1.Columns.Add("Email Membre");
                    listView1.Columns.Add("Tel Membre");
                    listView1.Columns.Add("Disponibilité");

                    if (!reader.HasRows)
                    {
                        MessageBox.Show("No records found.");
                        reader.Close();
                        return;
                    }

                    while (reader.Read())
                    {
                        string nomUtilisateur = reader.GetString(0);
                        string emailUtilisateur = reader.GetString(1);
                        string telUtilisateur = reader.GetString(2);
                        string Disponibilite = reader.GetString(3);

                        ListViewItem item = new ListViewItem(nomUtilisateur);
                        item.SubItems.Add(emailUtilisateur);
                        item.SubItems.Add(telUtilisateur);
                        item.SubItems.Add(Disponibilite);
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

        private void PopulateListView()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nom_utilisateur, email_utilisateur, tel_utilisateur, dispo_utilisateur FROM utilisateur";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    listView1.Items.Clear();
                    listView1.Columns.Clear();

                    listView1.Columns.Add("Nom Membre");
                    listView1.Columns.Add("Email Membre");
                    listView1.Columns.Add("Tel Membre");
                    listView1.Columns.Add("Disponibilité");

                    while (reader.Read())
                    {
                        string nomUtilisateur = reader.GetString(0);
                        string emailUtilisateur = reader.GetString(1);
                        string telUtilisateur = reader.GetString(2);
                        string Disponibilite = reader.GetString(3);

                        ListViewItem item = new ListViewItem(nomUtilisateur);
                        item.SubItems.Add(emailUtilisateur);
                        item.SubItems.Add(telUtilisateur);
                        item.SubItems.Add(Disponibilite);
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                selectedEmail = selectedItem.SubItems[1].Text; 

                GetUserInfo(selectedEmail);
            }
        }

        private void GetUserInfo(string email)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT nom_utilisateur, prenom_utilisateur, email_utilisateur, tel_utilisateur, photo_utilisateur FROM utilisateur WHERE email_utilisateur = @email";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@email", email);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string nomUtilisateur = reader.GetString(0);
                        string prenomUtilisateur = reader.GetString(1);
                        string emailUtilisateur = reader.GetString(2);
                        string telUtilisateur = reader.GetString(3);
                        string photoUtilisateur = reader.GetString(4);

                        gunaLabel14.Text = nomUtilisateur;
                        gunaLabel13.Text = prenomUtilisateur;
                        gunaLabel12.Text = emailUtilisateur;
                        gunaLabel11.Text = telUtilisateur;

                        pictureBox1.ImageLocation = photoUtilisateur;
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            label7.Text = currentTime.ToString("HH:mm:ss");
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

        private void gunaButton10_Click(object sender, EventArgs e)
        {
            string selectedProjet = gunaComboBox1.Text;
            string selectedMembre = gunaLabel12.Text;

            if (string.IsNullOrEmpty(selectedProjet))
            {
                MessageBox.Show("Veuillez sélectionner un projet.");
                return;
            }

            if (selectedMembre == "-")
            {
                MessageBox.Show("Veuillez sélectionner un membre dans la liste.");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT membreProjet FROM projet WHERE nomProjet = @nomProjet";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nomProjet", selectedProjet);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        string existingMembre = result.ToString();

                        query = "UPDATE projet SET membreProjet = @membre WHERE nomProjet = @nomProjet";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@membre", selectedMembre);
                        command.Parameters.AddWithValue("@nomProjet", selectedProjet);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update 'dispo_utilisateur' column in the 'utilisateur' table
                            query = "UPDATE utilisateur SET dispo_utilisateur = 'occupe' WHERE email_utilisateur = @membre";
                            command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@membre", selectedMembre);
                            int usersUpdated = command.ExecuteNonQuery();

                            if (usersUpdated > 0)
                            {
                                MessageBox.Show("Membre affecté au projet avec succès. La disponibilité de l'utilisateur a été mise à jour.");
                                PopulateListView();
                            }
                            else
                            {
                                MessageBox.Show("Membre affecté au projet avec succès. La mise à jour de la disponibilité de l'utilisateur a échoué.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gunaButton13_Click(object sender, EventArgs e)
        {
            LoadChartData();
        }
    }
}
