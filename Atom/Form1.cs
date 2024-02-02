using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Atom;
using System;

namespace Atom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaLineTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            string email = gunaLineTextBox1.Text;
            string password = gunaLineTextBox2.Text;
            string imageFile = "";

            // Connect to the database
         string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // Check the "utilisateur" table for a matching email and password
            string utilisateurQuery = "SELECT COUNT(*) FROM utilisateur WHERE email_utilisateur = @email AND pass_utilisateur = @password";
            MySqlCommand utilisateurCommand = new MySqlCommand(utilisateurQuery, connection);
            utilisateurCommand.Parameters.AddWithValue("@email", email);
            utilisateurCommand.Parameters.AddWithValue("@password", password);
            int utilisateurCount = Convert.ToInt32(utilisateurCommand.ExecuteScalar());

            // Check the "chef" table for a matching email and password
            string chefQuery = "SELECT COUNT(*) FROM chef WHERE email_chef = @email AND pass_chef = @password";
            MySqlCommand chefCommand = new MySqlCommand(chefQuery, connection);
            chefCommand.Parameters.AddWithValue("@email", email);
            chefCommand.Parameters.AddWithValue("@password", password);
            int chefCount = Convert.ToInt32(chefCommand.ExecuteScalar());

            if (utilisateurCount > 0)
            {
                // Email and password exist in the "utilisateur" table
                string nomUtilisateurQuery = "SELECT nom_utilisateur, email_utilisateur, photo_utilisateur FROM utilisateur WHERE email_utilisateur = @email";
                MySqlCommand nomUtilisateurCommand = new MySqlCommand(nomUtilisateurQuery, connection);
                nomUtilisateurCommand.Parameters.AddWithValue("@email", email);
                MySqlDataReader utilisateurReader = nomUtilisateurCommand.ExecuteReader();
                if (utilisateurReader.Read())
                {
                    membretableau membreTableau = new membretableau();
                    membreTableau.ConnectedUserName = utilisateurReader.GetString("nom_utilisateur");
                    membreTableau.ConnectedUserEmail = utilisateurReader.GetString("email_utilisateur");
                    membreTableau.ImageFilePath = utilisateurReader.GetString("photo_utilisateur");
                    utilisateurReader.Close();



                    MessageBox.Show("Connexion réussie !");
                    membreTableau.Show();
                    this.Hide();
                }
            }
            else if (chefCount > 0)
            {
                // Email and password exist in the "chef" table
                string emailChefQuery = "SELECT email_chef, photo_chef FROM chef WHERE email_chef = @email";
                MySqlCommand emailChefCommand = new MySqlCommand(emailChefQuery, connection);
                emailChefCommand.Parameters.AddWithValue("@email", email);
                MySqlDataReader chefReader = emailChefCommand.ExecuteReader();
                if (chefReader.Read())
                {
                    imageFile = chefReader.GetString("photo_chef");
                    chefReader.Close();

                    home home = new home();
                    Clients Clientpage = new Clients();
                    home.ConnectedUserEmail = email;
                    Clientpage.ConnectedUserEmail = email;
                    home.ImageFilePath = imageFile;
                    MessageBox.Show("Connexion réussie !");
                    home.Show();
                    this.Hide();
                }
            }
            else
            {
                // Email and password do not exist in either table
                MessageBox.Show("L'email ou le mot de passe est incorrect.");
            }

            connection.Close();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            creecompte cree = new creecompte();
            cree.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            motdepassoublie pass = new motdepassoublie();
            pass.Show();
            this.Hide();
        }



        private void gunaButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
