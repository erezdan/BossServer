using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestClient
{
    public partial class MainForm : Form
    {
        private ApiService _apiService;

        public MainForm()
        {
            InitializeComponent();

            _apiService = new ApiService("https://localhost:44328/");
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string mail = UserMailTxt.Text;
            string password = PasswordTxt.Text;

            try
            {
                string token = await _apiService.LoginAsync(mail, password);
                MessageBox.Show("Login successful! Token: " + token);

                // Use the token for subsequent API requests
                _apiService.SetAuthorizationHeader(token);

                // Fetch chat contexts or other data and display in DataGridView
                // var chatContexts = await _apiService.GetChatContextsAsync();
                // chatContextsDataGridView.DataSource = chatContexts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
