using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;
using MySql.Data.MySqlClient;

namespace Atom
{
    public partial class membretableau : Form
    {
        public string ConnectedUserName { get; set; }
        public string ConnectedUserEmail { get; set; }
        public string ImageFilePath { get; set; }

        private string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";
        private MySqlConnection connection;

        public membretableau()
        {
            InitializeComponent();
         
        }


        private void RetrieveAndDisplayTotalSessionTime()
        {
            try
            {
                connection.Open();

                // Retrieve all sessions for the selected member
                string selectQuery = "SELECT tempSession FROM Session WHERE membreSession = @membreSession";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@membreSession", gunaLabel5.Text);
                MySqlDataReader reader = command.ExecuteReader();

                TimeSpan totalTime = TimeSpan.Zero;

                // Calculate the total time for all sessions
                while (reader.Read())
                {
                    TimeSpan sessionTime = TimeSpan.Parse(reader.GetString("tempSession"));
                    totalTime = totalTime.Add(sessionTime);
                }

                reader.Close();

                // Display the total time in hh:mm:ss format
                gunaLabel15.Text = totalTime.ToString(@"hh\:mm\:ss");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void gunaAdvenceButton3_Click(object sender, EventArgs e)
        {
            string nomTache = gunaLineTextBox1.Text;
            string descriTache = gunaLineTextBox2.Text;
            DateTime dateEchanTache = gunaDateTimePicker2.Value;
            string membreTache = gunaLabel5.Text;

            // Check if any field is empty
            if (string.IsNullOrEmpty(nomTache) || string.IsNullOrEmpty(descriTache) || string.IsNullOrEmpty(membreTache))
            {
                MessageBox.Show("Veuillez remplir tous les formulaires.");
                return;
            }

            // Check if the selected date is at least one day later than the current date
            if (dateEchanTache.Date <= DateTime.Today)
            {
                MessageBox.Show("Veuillez sélectionner une date postérieure d'au moins un jour à la date actuelle.");
                return;
            }

            try
            {
                connection.Open();

                // Insert the new task into the 'tache' table
                string insertQuery = "INSERT INTO tache (nomTache, descriTache, dateEchanTache, membreTache) " +
                                     "VALUES (@nomTache, @descriTache, @dateEchanTache, @membreTache)";
                MySqlCommand command = new MySqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@nomTache", nomTache);
                command.Parameters.AddWithValue("@descriTache", descriTache);
                command.Parameters.AddWithValue("@dateEchanTache", dateEchanTache);
                command.Parameters.AddWithValue("@membreTache", membreTache);
                command.ExecuteNonQuery();

                // Update the ListView with the new task
                UpdateListView();

                // Clear the input fields
                gunaLineTextBox1.Text = string.Empty;
                gunaLineTextBox2.Text = string.Empty;
                gunaDateTimePicker2.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void UpdateListView()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                // Clear existing items in ListView
                listView3.Items.Clear();

                // Retrieve all tasks for the selected member
                string selectQuery = "SELECT nomTache, descriTache, dateEchanTache, statutTache FROM tache WHERE membreTache = @membreTache";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                command.Parameters.AddWithValue("@membreTache", gunaLabel5.Text);
                MySqlDataReader reader = command.ExecuteReader();

                int enCoursCount = 0; // Counter for tasks with statutTache = "En cours"

                // Add tasks to ListView
                while (reader.Read())
                {
                    string nomTache = reader.GetString("nomTache");
                    string descriTache = reader.GetString("descriTache");
                    DateTime dateEchanTache = reader.GetDateTime("dateEchanTache");
                    string statutTache = reader.GetString("statutTache");

                    ListViewItem item = new ListViewItem(nomTache);
                    item.SubItems.Add(descriTache);
                    item.SubItems.Add(dateEchanTache.ToString());
                    item.SubItems.Add(statutTache);
                    listView3.Items.Add(item);

                    if (statutTache == "En cours")
                    {
                        enCoursCount++;
                    }
                }

                reader.Close();

                // Update the label with the count of "En cours" tasks
                gunaLabel16.Text = enCoursCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void gunaButton15_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une tâche dans la liste.");
                return;
            }

            ListViewItem selectedItem = listView3.SelectedItems[0];
            string selectedNomTache = selectedItem.SubItems[0].Text;
            string selectedStatutTache = selectedItem.SubItems[3].Text;

            // Check if the selected task has a status of "En cours"
            if (selectedStatutTache == "En cours")
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string updateQuery = "UPDATE tache SET statutTache = 'Terminer' WHERE nomTache = @nomTache";
                    MySqlCommand command = new MySqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@nomTache", selectedNomTache);
                    command.ExecuteNonQuery();

                    UpdateListView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
            else if (selectedStatutTache == "Terminer")
            {
                MessageBox.Show("Cette tâche a déjà été marquée comme 'Terminer' ");
            }
            else
            {
                MessageBox.Show("Seules les tâches avec le statut 'En cours' peuvent être modifiées.");
            }

           
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            gunaDateTimePicker1.Value = DateTime.Today;
            gunaAdvenceButton2.Visible = true; 
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Increment the current time by 1 second
            TimeSpan currentTime = TimeSpan.Parse(gunaLabel15.Text);
            currentTime = currentTime.Add(TimeSpan.FromSeconds(1));

            // Display the updated time in hh:mm:ss format
            gunaLabel15.Text = currentTime.ToString(@"hh\:mm\:ss");
        }
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {
            // Check if the timer is running
            if (timer1.Enabled)
            {
                timer1.Stop();

                string tempSession = gunaLabel15.Text;
                DateTime dateSession = gunaDateTimePicker1.Value.Date;
                string membreSession = gunaLabel5.Text;

                // Check if the selected date is earlier than or equal to the current date
                if (dateSession < DateTime.Today)
                {
                    MessageBox.Show("Veuillez sélectionner une date égale ou postérieure à la date actuelle.");
                    return;
                }

                try
                {
                    connection.Open();

                    // Insert the session data into the 'Session' table
                    string insertQuery = "INSERT INTO Session (tempSession, dateSession, membreSession) " +
                                         "VALUES (@tempSession, @dateSession, @membreSession)";
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@tempSession", tempSession);
                    command.Parameters.AddWithValue("@dateSession", dateSession);
                    command.Parameters.AddWithValue("@membreSession", membreSession);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Veuillez démarrer le chronomètre avant de sauvegarder la session.");
            }

            gunaAdvenceButton2.Visible = false;
        }


        private void gunaButton8_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gunaButton7_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show(); 
                this.Dispose();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            reglage reglagechef = new reglage();
            reglagechef.ConnectedUserEmail = ConnectedUserEmail;
            reglagechef.ImageFilePath = ImageFilePath;
            reglagechef.Show();
        }

        private void gunaPictureBox2_Click(object sender, EventArgs e)
        {
            chatmembre newchat = new chatmembre() ;
            newchat.ConnectedUserEmail = ConnectedUserEmail;

            newchat.Show();
        }

       

        private void membretableau_Load(object sender, EventArgs e)
        {
            gunaLabel4.Text = ConnectedUserName;
            gunaLabel5.Text = ConnectedUserEmail;
            gunaCirclePictureBox1.Image = Image.FromFile(ImageFilePath);

            connection = new MySqlConnection(connectionString);

            // Set the view to details
            listView3.View = View.Details;

            // Add columns to the ListView
            listView3.Columns.Add("Task Name", 200);
            listView3.Columns.Add("Description", 200);
            listView3.Columns.Add("Due Date", 100);
            listView3.Columns.Add("Status", 100);

            UpdateListView();

         
            RetrieveAndDisplayTotalSessionTime();

            int projetencours = calculprojetencours();
            gunaLabel17.Text = projetencours.ToString();

            int projetTerminer = calculprojettermiber();
            gunaLabel14.Text = projetTerminer.ToString();
        }

        private int calculprojetencours()
          {
    int rowCount = 0;

    try
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT COUNT(*) FROM projet WHERE etatProjet = 'En cours' AND membreProjet = @connectedUserEmail";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@connectedUserEmail", ConnectedUserEmail);
            rowCount = Convert.ToInt32(command.ExecuteScalar());
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message);
    }

    return rowCount;
           }


        private int calculprojettermiber()
        {
            int rowCount = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM projet WHERE etatProjet = 'Terminer' AND membreProjet = @connectedUserEmail";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@connectedUserEmail", ConnectedUserEmail);
                    rowCount = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return rowCount;
        }


    }
}
