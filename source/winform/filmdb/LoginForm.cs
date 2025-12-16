using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filmdb
{
    public partial class LoginForm : Form
    {
        public string LoggedUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                var data = new Dictionary<string, string>
            {
                { "username", txtUsername.Text },
                { "password", txtPassword.Text }
            };

                var response = await client.PostAsync(
                    "https://0ndra.maweb.eu/FilmDB/login_api.php",
                    new FormUrlEncodedContent(data)
                );

                var json = await response.Content.ReadAsStringAsync();
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                if (result.success == true)
                {
                    LoggedUser = result.username;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Neplatné přihlašovací údaje");
                }
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(
                "https://0ndra.maweb.eu/FilmDB/register.php"
            );
        }
    }
}
