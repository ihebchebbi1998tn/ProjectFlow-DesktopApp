using System;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace Atom
{
    public partial class creecompte : Form
    {
        public creecompte()
        {
            InitializeComponent();
        }

        private void creecompte_Load(object sender, EventArgs e)
        {
            // Establish database connection
          string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "";
            MySqlCommand command = new MySqlCommand(query, connection);

            if (gunaComboBox1.Text == "Chef de projet")
            {
                query = "INSERT INTO chef (nom_chef, prenom_chef, email_chef, pass_chef, tel_chef, photo_chef) VALUES (@nom, @prenom, @email, @pass, @tel, @photo)";
                command = new MySqlCommand(query, connection);

                // Check if email already exists
                string emailQuery = "SELECT COUNT(*) FROM chef WHERE email_chef = @email";
                MySqlCommand emailCommand = new MySqlCommand(emailQuery, connection);
                emailCommand.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);

                int emailCount = Convert.ToInt32(emailCommand.ExecuteScalar());
                if (emailCount > 0)
                {
                    MessageBox.Show("Cet email est déjà utilisé pour un compte chef.");
                    return;
                }
            }
            else if (gunaComboBox1.Text == "Membre de projet")
            {
                query = "INSERT INTO utilisateur (nom_utilisateur, prenom_utilisateur, email_utilisateur, pass_utilisateur, tel_utilisateur, photo_utilisateur) VALUES (@nom, @prenom, @email, @pass, @tel, @photo)";
                command = new MySqlCommand(query, connection);

                // Check if email already exists
                string emailQuery = "SELECT COUNT(*) FROM utilisateur WHERE email_utilisateur = @email";
                MySqlCommand emailCommand = new MySqlCommand(emailQuery, connection);
                emailCommand.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);

                int emailCount = Convert.ToInt32(emailCommand.ExecuteScalar());
                if (emailCount > 0)
                {
                    MessageBox.Show("Cet email est déjà utilisé pour un compte utilisateur.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un type de compte.");
                return;
            }

            // Check if textboxes are empty
            if (string.IsNullOrEmpty(gunaLineTextBox1.Text) || string.IsNullOrEmpty(gunaLineTextBox2.Text) || string.IsNullOrEmpty(gunaLineTextBox3.Text) || string.IsNullOrEmpty(gunaLineTextBox4.Text) || string.IsNullOrEmpty(gunaLineTextBox6.Text))
            {
                MessageBox.Show("Tous les champs sont obligatoires.");
                return;
            }

            command.Parameters.AddWithValue("@nom", gunaLineTextBox1.Text);
            command.Parameters.AddWithValue("@prenom", gunaLineTextBox2.Text);
            command.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);
            command.Parameters.AddWithValue("@pass", gunaLineTextBox3.Text);
            command.Parameters.AddWithValue("@tel", gunaLineTextBox6.Text);

            if (gunaPictureBox1.Image != null)
            {
                // Save image to file
                string imageName = Guid.NewGuid().ToString();
                string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
                string imagePath = Path.Combine(directoryPath, imageName + ".png");

                // Create directory if it does not exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                gunaPictureBox1.Image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

                // Store image location in database
                command.Parameters.AddWithValue("@photo", imagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@photo", "");
            }

            command.ExecuteNonQuery();

            MessageBox.Show("Utilisateur ajouté avec succès!");

            connection.Close();
            Form1 login = new Form1();
            login.Show();
            this.Close();
        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                gunaPictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }


        private void gunaButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
    }
}

