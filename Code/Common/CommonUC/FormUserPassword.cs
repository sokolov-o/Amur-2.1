using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOV.Common
{
    public partial class FormUserPassword : Form
    {
        Dictionary<string, string> _connectionStrings;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStrings">Наименование БД и её строка подключения</param>
        /// <param name="user">Пользователь. Если отсутствует, то берётся из строки подключения.</param>
        public FormUserPassword(Dictionary<string, string> connectionStrings, User user = null)
        {
            InitializeComponent();

            _connectionStrings = connectionStrings;

            if (connectionStrings.Count > 0)
            {
                if (user != null)
                    User = user;
                else
                    User = ADbNpgsql.ConnectionStringGetUser(connectionStrings.ElementAt(0).Value);

                foreach (var item in connectionStrings)
                {
                    cnnsListBox.Items.Add(item.Key);
                }
                cnnsListBox.SelectedIndex = 0;
            }
        }
        public FormUserPassword(User user)
        {
            InitializeComponent();

            splitContainer1.Panel1Collapsed = true;

            _connectionStrings = null;
            User = user;
        }
        public FormUserPassword(string connectionString, User user = null)
        {
            InitializeComponent();
            var conStr = new Dictionary<string, string>();
            conStr.Add(connectionString, connectionString);
            _connectionStrings = conStr;
            cnnsListBox.Items.Add(ADbNpgsql.ConnectionStringWithoutUser(connectionString));
            cnnsListBox.SelectedIndex = 0;

            if (user != null)
                User = user;
            else
                User = ADbNpgsql.ConnectionStringGetUser(connectionString);
        }
        /// <summary>
        /// Selected connection string
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (cnnsListBox.SelectedItem == null)
                    return null;
                return ADbNpgsql.ConnectionStringUpdateUser(_connectionStrings.ElementAt(cnnsListBox.SelectedIndex).Value, User);
                //return ADbNpgsql.ConnectionStringUpdateUser(cnnsListBox.SelectedItem.ToString(), User);
            }
        }
        public User User
        {
            get
            {
                return (string.IsNullOrEmpty(userNameTextBox.Text) || string.IsNullOrEmpty(pwdTextBox.Text))
                    ? null
                    : new User(userNameTextBox.Text, pwdTextBox.Text, checkBox1.Checked);
            }
            set
            {
                userNameTextBox.Text = (value == null) ? "" : value.Name;
                pwdTextBox.Text = (value == null) ? "" : value.Password;
                checkBox1.Checked = value.IsIntegritySecurity;
                checkBox1_Click(null, null);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            userNameTextBox.Enabled = !checkBox1.Checked;
            pwdTextBox.Enabled = !checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (User == null)
                MessageBox.Show("Укажите и логин, и пароль.", "Ошибка ввода информации о пользователе", MessageBoxButtons.OK);
            else
                Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cnnsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(User.Name))
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                button1_Click(null, null);
            }
        }
    }
}
