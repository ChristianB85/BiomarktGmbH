using System;
using System.Linq;
using System.Windows.Forms;

namespace BiomarktGmbH
{
    public partial class LoadingScreen : Form
    {
        private int loadingbarValue; 
        public LoadingScreen()
        {
            InitializeComponent();
            
        }
        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            LoadingbarTimer.Start();
        }
        private void LoadingbarTimer_Tick(object sender, EventArgs e)
        {
            loadingbarValue += 1;
            lblProgressbarValue.Text = loadingbarValue.ToString() + "%";
            LoadingBar.Value = loadingbarValue;

            if(loadingbarValue >= LoadingBar.Maximum)
            {
                LoadingbarTimer.Stop();

                MainMenu mainMenu = new MainMenu();

                this.Hide();
                mainMenu.Show();
                
            }
        }

  
    }
}
