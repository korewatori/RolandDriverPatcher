using System;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;

namespace RolandDriverPatcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //if (hasSecBoot())
            //{
            //    toolStripStatusLabel2.Text = "\"Secureboot\" detected!";
            //    toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
            //}
            //else
            //{
            //    toolStripStatusLabel2.Text = "No \"Secureboot\" detected!";
            //    toolStripStatusLabel2.ForeColor = System.Drawing.Color.Green;
            //}
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Roland Driver Patcher by Herbert Schmitt\n(Version 1.0)\nCredits for the screenshots go to \"HowToGeek.com\"", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.youtube.com/watch?v=ub9D08OGsmY");
        }

        private void modFiles(string tmpPath)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(tmpPath, "*.inf", System.IO.SearchOption.AllDirectories);
                //MessageBox.Show(files.Length.ToString());
                foreach (string file in files)
                {
                    string text = File.ReadAllText(file);
                    text = text.Replace("6.2", "10");
                    text = text.Replace("6.0", "10");
                    text = text.Replace("6.1", "10");
                    File.WriteAllText(file, text);
                }
            }
            catch (Exception ex) { }
            MessageBox.Show("Sucess! You can now install the driver like you normally would.\n\nLocation: \"" + tmpPath + "\"");
            Process.Start(tmpPath);
        }

        private void startSetup()
        {

        }

        private void extract(string tempPath)
        {
            try
            {
                string extractPath = "e";
                string zipPath = "e";

                MessageBox.Show("All the extraced files will be stored at: \"" + Path.GetDirectoryName(tempPath).ToString() + "\\RolandDriverExtracted\" for future use.");
                if (File.Exists(tempPath))
                {
                    zipPath = tempPath;
                }
                if (Directory.Exists(Path.GetDirectoryName(tempPath).ToString()))
                {
                    extractPath = Path.GetDirectoryName(tempPath).ToString() + "\\RolandDriverExtracted";
                }

                if (extractPath != "e" && zipPath != "e")
                {
                    try
                    {
                        Directory.Delete(extractPath, true);
                    }
                    catch (Exception ex) {// MessageBox.Show("Please delete the folder " + extractPath);
                    }
                    ZipFile.ExtractToDirectory(zipPath, extractPath);
                    modFiles(extractPath);
                }
                else
                {
                    MessageBox.Show("Error: Do not move or delete the Zip archive during the installation", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) {  }
        }


        private string zipLocation()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "zip file (*.zip)|*.zip*";
            openFileDialog1.Multiselect = false;
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            else
            {
                return "errornf";
            }
            return "error";
        }

        private bool Start()
        {

            if (hasSecBoot())
            {
                //It looks like your computer has \"Secureboot\" enabled which prevents this tool from installing the driver without restarting. 
                DialogResult q1 = MessageBox.Show("In order for this patcher to work, you need to manually restart your computer to a special mode. \n\nClick \"Yes\" for further instructions, \"No\" if you have already done that, \"Cancel\" to cancel this process.", "PLEASE READ!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (q1 == DialogResult.Yes)
                {
                    Inst rebootingGuide = new Inst();
                    rebootingGuide.Show();
                }
                if (q1 == DialogResult.No)
                {
                    extract(zipLocation());
                }
            }
            else
            {
                extract(zipLocation());
            }
            return false;
            //Console.WriteLine(output);
        }

        private bool hasSecBoot()
        {
            //ProcessStartInfo info = new ProcessStartInfo(@"bcdedit", " /set testsigning off");
            //info.UseShellExecute = false;
            //info.RedirectStandardOutput = true;
            //info.CreateNoWindow = true;
            //string output = Process.Start(info).StandardOutput.ReadToEnd();
            //Console.WriteLine(output);
            //if (output.Contains("The boot configuration data store could not be") || output.Contains("The value is prot"))
            //{
            //    return true;
            //}
            //else
            //{
                return true;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Start();

            //extract(zipLocation());
        }

        private void rebootingGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inst rebootingGuide = new Inst();
            rebootingGuide.Show();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.youtube.com/user/mmacke1050");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ProcessStartInfo info = new ProcessStartInfo("bcdedit ", "/set testsigning on");
            //info.UseShellExecute = false;
            //info.RedirectStandardOutput = true;
            //info.CreateNoWindow = true;
            //string output = Process.Start(info).StandardOutput.ReadToEnd();
        }
    }
}
