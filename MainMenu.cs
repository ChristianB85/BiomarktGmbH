using System;
using System.Linq;
using System.Windows.Forms;

namespace BiomarktGmbH
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            Products productScreen = new Products();

            productScreen.Show();
            this.Hide();

        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            Bill billScreen = new Bill();

            billScreen.Show();
            this.Hide();
        }
    }
}
