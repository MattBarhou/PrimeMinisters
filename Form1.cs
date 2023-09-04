using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Web.Script.Serialization;





namespace PrimeMinisters
{
    public partial class frmPrimeMinisters : Form
    {
        private Dictionary<string, PrimeMinister> primeMinisters = new Dictionary<string, PrimeMinister>();
        public frmPrimeMinisters()
        {
            InitializeComponent();
        }

        private void frmPrimeMinisters_Load(object sender, EventArgs e)
        {
            string file = "PrimeMinisters.json";

            LoadPrimeMinistersFromFile(file);
            PopulatePrimeMinistersList();
        }

        private void LoadPrimeMinistersFromFile(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string jsonString = streamReader.ReadToEnd();
                primeMinisters = JsonConvert.DeserializeObject<Dictionary<string, PrimeMinister>>(jsonString);
            }
        }

        private void PopulatePrimeMinistersList()
        {
            lstPrimeMinisters.DataSource = null;
            lstPrimeMinisters.Items.Clear();
            lstPrimeMinisters.DataSource = primeMinisters.Keys.ToList();
        }

        private void lstPrimeMinisters_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstPrimeMinisters.SelectedValue != null)
            {
                string selectedLastName = lstPrimeMinisters.SelectedValue.ToString();
                PrimeMinister selectedPrimeMinister = primeMinisters[selectedLastName];
                
                lblName.Text = $"{selectedPrimeMinister.FirstName} {selectedPrimeMinister.LastName}";
                lblTerm.Text = selectedPrimeMinister.Term;
                lblParty.Text = selectedPrimeMinister.Party;
                picPhoto.ImageLocation = $"Images/{selectedLastName}.jpg";

            }
        }
    }
}
