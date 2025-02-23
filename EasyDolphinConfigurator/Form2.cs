using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyDolphinConfigurator
{
    public partial class Form2 : Form
    {
        private List<string> allLines = new List<string>(); // Stores all lines from the file
        private string selectedLine = ""; // Stores the selected line
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadFile("C:\\Users\\danie\\Downloads\\wiitdb.txt"); // Replace with your actual file path
        }
        private void LoadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                allLines = File.ReadAllLines(filePath).ToList();
                listBox1.DataSource = new List<string>(allLines); // Efficiently binds the list
            }
            else
            {
                MessageBox.Show("File not found!");
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            listBox1.DataSource = allLines.Where(line => line.ToLower().Contains(searchText)).ToList();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                selectedLine = listBox1.SelectedItem.ToString();
            }
        }
    }
}
