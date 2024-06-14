using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace DAI
{
    public partial class Form1 : Form
    {
        private XmlDataManager xmlDataManager;
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            xmlDataManager = new XmlDataManager("vehicles.xml");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VehicleForm.AddVehicle();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            XmlDataManager.ShowVehicles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XmlDataManager.RemoveVehicle();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            XmlDataManager.ShowOverdueVehicles();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            XmlDataManager.ShowVehiclesByTemplate();
        }

    }
}

