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
    public partial class motdepassoublie : Form
    {
        public motdepassoublie()
        {
            InitializeComponent();
        }

        private void gunaLineTextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void motdepassoublie_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
        string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM utilisateur WHERE nom_utilisateur=@nom AND email_utilisateur=@email AND tel_utilisateur=@tel";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@nom", gunaLineTextBox1.Text);
            command.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);
            command.Parameters.AddWithValue("@tel", gunaLineTextBox6.Text);

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                if (reader.GetString("nom_utilisateur") == gunaLineTextBox1.Text &&
                    reader.GetString("email_utilisateur") == gunaLineTextBox4.Text &&
                    reader.GetString("tel_utilisateur") == gunaLineTextBox6.Text)
                {
                    string password = reader.GetString("pass_utilisateur");
                    MessageBox.Show("Votre mot de passe est: " + password);
                }
                else
                {
                    MessageBox.Show("Utilisateur non trouvé");
                }
            }
            else
            {
                reader.Close();
                query = "SELECT * FROM chef WHERE nom_chef=@nom AND email_chef=@email AND tel_chef=@tel";
                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nom", gunaLineTextBox1.Text);
                command.Parameters.AddWithValue("@email", gunaLineTextBox4.Text);
                command.Parameters.AddWithValue("@tel", gunaLineTextBox6.Text);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (reader.GetString("nom_chef") == gunaLineTextBox1.Text &&
                        reader.GetString("email_chef") == gunaLineTextBox4.Text &&
                        reader.GetString("tel_chef") == gunaLineTextBox6.Text)
                    {
                        string password = reader.GetString("pass_chef");
                        MessageBox.Show("Votre mot de passe est: " + password);
                    }
                    else
                    {
                        MessageBox.Show("Utilisateur non trouvé");
                    }
                }
                else
                {
                    MessageBox.Show("Utilisateur non trouvé");
                }
            }

            reader.Close();
            connection.Close();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            Form1 backtohome = new Form1();
            backtohome.Show();
            this.Hide();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
