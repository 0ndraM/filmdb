using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filmdb
{
    public partial class AddFilmForm : Form
    {
        private readonly string _autor;

        public AddFilmForm(string autor)
        {
            InitializeComponent();
            _autor = autor;
        }


      
        private async void btnSave_Click(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(txtNazev.Text), "nazev");
                form.Add(new StringContent(txtRok.Text), "rok");
                form.Add(new StringContent(txtZanr.Text), "zanr");
                form.Add(new StringContent(txtReziser.Text), "reziser");
                form.Add(new StringContent(txtHodnoceni.Text), "hodnoceni");
                form.Add(new StringContent(txtPopis.Text), "popis");
                form.Add(new StringContent(_autor), "autor");

                if (!string.IsNullOrEmpty(selectedPosterPath))
                {
                    var bytes = File.ReadAllBytes(selectedPosterPath);
                    form.Add(new ByteArrayContent(bytes), "plakat", "poster.jpg");
                }

                var response = await client.PostAsync(
                    "https://0ndra.maweb.eu/FilmDB/add_film_api.php",
                    form
                );

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Film odeslán ke schválení");
                    Close();
                }
                else
                {
                    MessageBox.Show("Chyba při odeslání filmu");
                }
            }
        }
    }
}