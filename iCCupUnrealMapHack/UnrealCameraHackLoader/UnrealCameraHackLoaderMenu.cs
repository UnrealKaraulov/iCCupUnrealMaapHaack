using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace UnrealCameraHackLoader
{
    public partial class UnrealCameraHackLoaderMenu : Form
    {
        public UnrealCameraHackLoaderMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Syringe.Injector war3inject = new Syringe.Injector(Process.GetProcessesByName(textBox2.Text)[0]);
            string path = Directory.GetCurrentDirectory() + "\\" + textBox3.Text;
            try
            {
                if (File.Exists(".\\Game.dll"))
                {
                    MessageBox.Show("Warning. Game.dll file found in CameraHack folder. Remove this file.");
                }
                else
                {
                    war3inject.InjectLibraryW(path);
                }
            }
            catch
            {

            }
            Thread.Sleep(2000);
            try
            {
                int height = int.Parse(textBox1.Text);
                war3inject.CallExport<float>(textBox3.Text, "SetCameraDistance", (float)height);
            }
            catch
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
