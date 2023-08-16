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
    public partial class chatmembre : Form
    {
        public string ConnectedUserEmail { get; set; }

        private string connectionString = "server=sql7.freemysqlhosting.net;port=3306;database=sql7621945;uid=sql7621945;password=PnJ5h9F2sC;";
        private GroupBox groupBox2;
        private Guna.UI.WinForms.GunaButton gunaButton11;
        private Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox3;
        private Guna.UI.WinForms.GunaButton gunaButton9;
        private ListView listView2;
        private Guna.UI.WinForms.GunaButton gunaButton12;
        private Guna.UI.WinForms.GunaComboBox gunaComboBox2;
        private Guna.UI.WinForms.GunaButton gunaButton4;
        private MySqlConnection connection;

        public chatmembre()
        {
            InitializeComponent();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chatmembre));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gunaButton11 = new Guna.UI.WinForms.GunaButton();
            this.gunaLineTextBox3 = new Guna.UI.WinForms.GunaLineTextBox();
            this.gunaButton9 = new Guna.UI.WinForms.GunaButton();
            this.listView2 = new System.Windows.Forms.ListView();
            this.gunaButton12 = new Guna.UI.WinForms.GunaButton();
            this.gunaComboBox2 = new Guna.UI.WinForms.GunaComboBox();
            this.gunaButton4 = new Guna.UI.WinForms.GunaButton();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gunaButton11);
            this.groupBox2.Controls.Add(this.gunaLineTextBox3);
            this.groupBox2.Controls.Add(this.gunaButton9);
            this.groupBox2.Controls.Add(this.listView2);
            this.groupBox2.Controls.Add(this.gunaButton12);
            this.groupBox2.Controls.Add(this.gunaComboBox2);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 605);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chat equipe ";
            // 
            // gunaButton11
            // 
            this.gunaButton11.AnimationHoverSpeed = 0.07F;
            this.gunaButton11.AnimationSpeed = 0.03F;
            this.gunaButton11.BaseColor = System.Drawing.Color.MediumSlateBlue;
            this.gunaButton11.BorderColor = System.Drawing.Color.Black;
            this.gunaButton11.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton11.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaButton11.ForeColor = System.Drawing.Color.White;
            this.gunaButton11.Image = null;
            this.gunaButton11.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton11.Location = new System.Drawing.Point(179, 556);
            this.gunaButton11.Name = "gunaButton11";
            this.gunaButton11.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton11.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton11.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton11.OnHoverImage = null;
            this.gunaButton11.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton11.Size = new System.Drawing.Size(108, 38);
            this.gunaButton11.TabIndex = 61;
            this.gunaButton11.Text = "Envoyer";
            this.gunaButton11.Click += new System.EventHandler(this.gunaButton11_Click);
            // 
            // gunaLineTextBox3
            // 
            this.gunaLineTextBox3.BackColor = System.Drawing.Color.White;
            this.gunaLineTextBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gunaLineTextBox3.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaLineTextBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLineTextBox3.LineColor = System.Drawing.Color.MediumSlateBlue;
            this.gunaLineTextBox3.Location = new System.Drawing.Point(6, 556);
            this.gunaLineTextBox3.Name = "gunaLineTextBox3";
            this.gunaLineTextBox3.PasswordChar = '\0';
            this.gunaLineTextBox3.SelectedText = "";
            this.gunaLineTextBox3.Size = new System.Drawing.Size(167, 38);
            this.gunaLineTextBox3.TabIndex = 61;
            // 
            // gunaButton9
            // 
            this.gunaButton9.AnimationHoverSpeed = 0.07F;
            this.gunaButton9.AnimationSpeed = 0.03F;
            this.gunaButton9.BaseColor = System.Drawing.Color.MediumSlateBlue;
            this.gunaButton9.BorderColor = System.Drawing.Color.Black;
            this.gunaButton9.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton9.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaButton9.ForeColor = System.Drawing.Color.White;
            this.gunaButton9.Image = null;
            this.gunaButton9.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton9.Location = new System.Drawing.Point(365, 599);
            this.gunaButton9.Name = "gunaButton9";
            this.gunaButton9.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton9.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton9.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton9.OnHoverImage = null;
            this.gunaButton9.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton9.Size = new System.Drawing.Size(101, 40);
            this.gunaButton9.TabIndex = 61;
            this.gunaButton9.Text = "Envoyer";
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.Color.GhostWhite;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listView2.Location = new System.Drawing.Point(20, 28);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(267, 522);
            this.listView2.TabIndex = 61;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // gunaButton12
            // 
            this.gunaButton12.AnimationHoverSpeed = 0.07F;
            this.gunaButton12.AnimationSpeed = 0.03F;
            this.gunaButton12.BaseColor = System.Drawing.Color.MediumSlateBlue;
            this.gunaButton12.BorderColor = System.Drawing.Color.Black;
            this.gunaButton12.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton12.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaButton12.ForeColor = System.Drawing.Color.Violet;
            this.gunaButton12.Image = null;
            this.gunaButton12.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton12.Location = new System.Drawing.Point(714, 278);
            this.gunaButton12.Name = "gunaButton12";
            this.gunaButton12.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton12.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton12.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton12.OnHoverImage = null;
            this.gunaButton12.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton12.Size = new System.Drawing.Size(46, 40);
            this.gunaButton12.TabIndex = 57;
            // 
            // gunaComboBox2
            // 
            this.gunaComboBox2.BackColor = System.Drawing.Color.Transparent;
            this.gunaComboBox2.BaseColor = System.Drawing.Color.White;
            this.gunaComboBox2.BorderColor = System.Drawing.Color.Silver;
            this.gunaComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.gunaComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gunaComboBox2.FocusedColor = System.Drawing.Color.Empty;
            this.gunaComboBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gunaComboBox2.ForeColor = System.Drawing.Color.Black;
            this.gunaComboBox2.FormattingEnabled = true;
            this.gunaComboBox2.Location = new System.Drawing.Point(558, 282);
            this.gunaComboBox2.Name = "gunaComboBox2";
            this.gunaComboBox2.OnHoverItemBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaComboBox2.OnHoverItemForeColor = System.Drawing.Color.White;
            this.gunaComboBox2.Size = new System.Drawing.Size(150, 26);
            this.gunaComboBox2.TabIndex = 54;
            // 
            // gunaButton4
            // 
            this.gunaButton4.AnimationHoverSpeed = 0.07F;
            this.gunaButton4.AnimationSpeed = 0.03F;
            this.gunaButton4.BaseColor = System.Drawing.Color.MediumSlateBlue;
            this.gunaButton4.BorderColor = System.Drawing.Color.Black;
            this.gunaButton4.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton4.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaButton4.ForeColor = System.Drawing.Color.White;
            this.gunaButton4.Image = null;
            this.gunaButton4.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton4.Location = new System.Drawing.Point(295, 0);
            this.gunaButton4.Name = "gunaButton4";
            this.gunaButton4.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton4.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton4.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton4.OnHoverImage = null;
            this.gunaButton4.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton4.Size = new System.Drawing.Size(27, 31);
            this.gunaButton4.TabIndex = 70;
            this.gunaButton4.Text = "X";
            this.gunaButton4.Click += new System.EventHandler(this.gunaButton4_Click_1);
            // 
            // chatmembre
            // 
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(321, 652);
            this.Controls.Add(this.gunaButton4);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "chatmembre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.chatmembre_Load_1);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void gunaButton4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
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

                    // Clear existing items in the ListView
                    listView2.Items.Clear();
                    listView2.Columns.Clear();

                    // Create columns in the ListView
                    listView2.Columns.Add("Nom Message");
                    listView2.Columns.Add("Contenu Message");

                    // Populate the ListView with data
                    while (reader.Read())
                    {
                        string nomMessage = reader.GetString(0);
                        string contenuMessage = reader.GetString(1);

                        ListViewItem item = new ListViewItem(nomMessage);
                        item.SubItems.Add(contenuMessage);
                        item.BackColor = Color.White;

                        listView2.Items.Add(item);
                    }

                    // Auto-size the columns
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

        private void gunaButton11_Click(object sender, EventArgs e)
        {
            string nomMessage = ConnectedUserEmail;
            string contenuMessage = gunaLineTextBox3.Text.Trim();
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

                        // Clear the text box
                        gunaLineTextBox3.Text = "";

                        // Refresh the chat ListView
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

        private void chatmembre_Load_1(object sender, EventArgs e)
        {
            DisplayMessages();


        }
    }
}
