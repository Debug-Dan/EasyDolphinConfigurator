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
        public string confirmedGame = ""; // Next 3 lines store the game info to be passed to form1
        public string confirmedGameID = "";
        public string confirmedGameRegion = "";
        private string selectedGame = ""; // Next 3 lines store the select game info
        private string selectedGameID = "";
        private string selectedGameRegion = "";
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadFile("C:\\Users\\danie\\Downloads\\wiitdb.txt"); // UPDATE THIS LATER WITH FILE DOWNLOAD
            button1.Enabled = false;
        }
        private void LoadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                allLines = File.ReadAllLines(filePath).ToList();
                listBox1.DataSource = new List<string>(allLines); // Fills the list with all entries from the txt file
            }
            else
            {
                MessageBox.Show("File not found!");
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.ToLower(); // This code performs the search and updates the list live
            var filteredList = allLines.Where(line => line.ToLower().Contains(searchText)).ToList();
            listBox1.DataSource = null;
            listBox1.DataSource = filteredList;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                selectedLine = listBox1.SelectedItem.ToString();
                if (selectedLine.StartsWith("TITLES"))
                {
                    button1.Enabled = false;
                    selectedGame = "No Game Selected";
                    selectedGameID = "";
                    selectedGameRegion = "";
                }
                else
                {
                    button1.Enabled = true;
                    string[] parts = selectedLine.Split(new[] { " = " }, StringSplitOptions.None);
                    selectedGameID = parts[0];
                    selectedGame = parts.Length > 1 ? parts[1] : "";

                    char regionChar = selectedGameID[3];
                    selectedGameRegion = regionChar switch
                    {
                        'P' or 'D' or 'F' or 'H' or 'I' or 'X' or 'Y' or 'Z' => "PAL",
                        'E' => "USA",
                        'J' => "Japan",
                        'K' => "Korea",
                        'R' => "Russia",
                        'W' => "Taiwan",
                        'A' => "Region Free",
                        _ => "Unknown"
                    };
                }
                label1.Text = "Game: " + selectedGame;
                label2.Text = "Game ID: " + selectedGameID;
                label3.Text = "Game Region: " + selectedGameRegion;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            confirmedGame = selectedGame;
            confirmedGameID = selectedGameID;
            confirmedGameRegion = selectedGameRegion;
            this.Close();
        }
    }
}
