using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Atom
{
    public partial class reglage : Form
    {
        private string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";

        public string ConnectedUserEmail { get; set; }
        public string ImageFilePath { get; set; }

        public reglage()
        {
            InitializeComponent();
        }

        private void reglage_Load(object sender, EventArgs e)
        {

            gunaLineTextBox4.Text = ConnectedUserEmail;
            gunaPictureBox1.Image = Image.FromFile(ImageFilePath);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Fetch the 'chef' information based on the ConnectedUserEmail
                MySqlCommand command = new MySqlCommand("SELECT nom_chef, prenom_chef, email_chef, pass_chef, tel_chef FROM chef WHERE email_chef = @email", connection);
                command.Parameters.AddWithValue("@email", ConnectedUserEmail);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string nomChef = reader.GetString(0);
                    string prenomChef = reader.GetString(1);
                    string emailChef = reader.GetString(2);
                    string passChef = reader.GetString(3);
                    string telChef = reader.GetString(4);

                    // Populate the text boxes with the chef's information
                    gunaLineTextBox1.Text = nomChef;
                    gunaLineTextBox2.Text = prenomChef;
                    gunaLineTextBox4.Text = emailChef;
                    gunaLineTextBox3.Text = passChef;
                    gunaLineTextBox6.Text = telChef;
                }

                reader.Close();
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Fetch the 'utilisateur' information based on the ConnectedUserEmail
                MySqlCommand command = new MySqlCommand("SELECT nom_utilisateur, prenom_utilisateur, email_utilisateur, pass_utilisateur, tel_utilisateur FROM utilisateur WHERE email_utilisateur = @email", connection);
                command.Parameters.AddWithValue("@email", ConnectedUserEmail);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string nomUtilisateur = reader.GetString(0);
                    string prenomUtilisateur = reader.GetString(1);
                    string emailUtilisateur = reader.GetString(2);
                    string passUtilisateur = reader.GetString(3);
                    string telUtilisateur = reader.GetString(4);

                    // Populate the text boxes with the utilisateur's information
                    gunaLineTextBox1.Text = nomUtilisateur;
                    gunaLineTextBox2.Text = prenomUtilisateur;
                    gunaLineTextBox4.Text = emailUtilisateur;
                    gunaLineTextBox3.Text = passUtilisateur;
                    gunaLineTextBox6.Text = telUtilisateur;
                }

                reader.Close();
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            // Ask for confirmation
            DialogResult result = MessageBox.Show("Etes vous sure de vouloir modifier vos informations ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Check if the new email already exists in the 'chef' table
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand checkEmailCommand = new MySqlCommand("SELECT COUNT(*) FROM chef WHERE email_chef = @email", connection);
                    checkEmailCommand.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);
                    int count = Convert.ToInt32(checkEmailCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Le nouvel email existe déjà dans la table.");
                        return;
                    }
                }

                // Check if the new email already exists in the 'utilisateur' table
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand checkEmailCommand = new MySqlCommand("SELECT COUNT(*) FROM utilisateur WHERE email_utilisateur = @email", connection);
                    checkEmailCommand.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);
                    int count = Convert.ToInt32(checkEmailCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Le nouvel email existe déjà dans la table.");
                        return;
                    }
                }

                // Update the 'chef' information
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand updateCommand = new MySqlCommand("UPDATE chef SET nom_chef = @nom, prenom_chef = @prenom, email_chef = @email, pass_chef = @pass, tel_chef = @tel WHERE email_chef = @oldEmail", connection);
                    updateCommand.Parameters.AddWithValue("@nom", gunaLineTextBox1.Text);
                    updateCommand.Parameters.AddWithValue("@prenom", gunaLineTextBox2.Text);
                    updateCommand.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);
                    updateCommand.Parameters.AddWithValue("@pass", gunaLineTextBox3.Text);
                    updateCommand.Parameters.AddWithValue("@tel", gunaLineTextBox6.Text);
                    updateCommand.Parameters.AddWithValue("@oldEmail", ConnectedUserEmail);
                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Les informations ont été modifiées avec succès.");
                }

                // Update the 'utilisateur' information
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand updateCommand = new MySqlCommand("UPDATE utilisateur SET nom_utilisateur = @nom, prenom_utilisateur = @prenom, email_utilisateur = @email, pass_utilisateur = @pass, tel_utilisateur = @tel WHERE email_utilisateur = @oldEmail", connection);
                    updateCommand.Parameters.AddWithValue("@nom", gunaLineTextBox1.Text);
                    updateCommand.Parameters.AddWithValue("@prenom", gunaLineTextBox2.Text);
                    updateCommand.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);
                    updateCommand.Parameters.AddWithValue("@pass", gunaLineTextBox3.Text);
                    updateCommand.Parameters.AddWithValue("@tel", gunaLineTextBox6.Text);
                    updateCommand.Parameters.AddWithValue("@oldEmail", ConnectedUserEmail);
                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Les informations ont été modifiées avec succès.");
                }
            }
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand restoreCommand = new MySqlCommand("SELECT nom_chef, prenom_chef, email_chef, pass_chef, tel_chef FROM chef WHERE email_chef = @email", connection);
                restoreCommand.Parameters.AddWithValue("@oldEmail", ConnectedUserEmail);
                MySqlDataReader reader = restoreCommand.ExecuteReader();

                if (reader.Read())
                {
                    string nomChef = reader.GetString(0);
                    string prenomChef = reader.GetString(1);
                    string emailChef = reader.GetString(2);
                    string passChef = reader.GetString(3);
                    string telChef = reader.GetString(4);

                    // Restore the original 'chef' information in the text boxes
                    gunaLineTextBox1.Text = nomChef;
                    gunaLineTextBox2.Text = prenomChef;
                    gunaLineTextBox4.Text = emailChef;
                    gunaLineTextBox3.Text = passChef;
                    gunaLineTextBox6.Text = telChef;

                    MessageBox.Show("Les informations ont été restaurées avec succès.");
                }

                reader.Close();
            }

            // Restore the original 'utilisateur' information
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand restoreCommand = new MySqlCommand("SELECT nom_utilisateur, prenom_utilisateur, email_utilisateur, pass_utilisateur, tel_utilisateur FROM utilisateur WHERE email_utilisateur = @oldEmail", connection);
                restoreCommand.Parameters.AddWithValue("@oldEmail", ConnectedUserEmail);
                MySqlDataReader reader = restoreCommand.ExecuteReader();

                if (reader.Read())
                {
                    string nomUtilisateur = reader.GetString(0);
                    string prenomUtilisateur = reader.GetString(1);
                    string emailUtilisateur = reader.GetString(2);
                    string passUtilisateur = reader.GetString(3);
                    string telUtilisateur = reader.GetString(4);

                    // Restore the original 'utilisateur' information in the text boxes
                    gunaLineTextBox1.Text = nomUtilisateur;
                    gunaLineTextBox2.Text = prenomUtilisateur;
                    gunaLineTextBox4.Text = emailUtilisateur;
                    gunaLineTextBox3.Text = passUtilisateur;
                    gunaLineTextBox6.Text = telUtilisateur;

                    MessageBox.Show("Les informations ont été restaurées avec succès.");
                }

                reader.Close();
            }
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            // Ask for confirmation
            DialogResult result = MessageBox.Show("Votre compte sera supprimé définitivement. Êtes-vous sûr de vouloir continuer ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Delete the 'chef' record from the table
                    MySqlCommand deleteChefCommand = new MySqlCommand("DELETE FROM chef WHERE email_chef = @oldEmail", connection);
                    deleteChefCommand.Parameters.AddWithValue("@oldEmail", ConnectedUserEmail);
                    deleteChefCommand.ExecuteNonQuery();

                    // Delete the 'utilisateur' record from the table
                    MySqlCommand deleteUtilisateurCommand = new MySqlCommand("DELETE FROM utilisateur WHERE email_utilisateur = @oldEmail", connection);
                    deleteUtilisateurCommand.Parameters.AddWithValue("@oldEmail", ConnectedUserEmail);
                    deleteUtilisateurCommand.ExecuteNonQuery();

                    MessageBox.Show("Votre compte a été supprimé définitivement.");
                }

                // Close the form or perform any necessary actions
                this.Close();
            }
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
