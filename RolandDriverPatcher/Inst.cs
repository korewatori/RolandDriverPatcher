using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RolandDriverPatcher
{
    public partial class Inst : Form
    {
        private int picCounter = 1;

        public Inst()
        {
            InitializeComponent();
        }

        private void Inst_Load(object sender, EventArgs e)
        {
            loadPic(picCounter);
        }

        private void fwd(object sender, EventArgs e)
        {
            button2.Enabled = true;
            if (picCounter < 6)
            {
                picCounter++;
                try
                {
                    loadPic(picCounter);
                }
                catch (Exception ex) { MessageBox.Show("Error: For this guide to work you need to extract the archive this application came in"); }
            }
            else
            {
                DialogResult q1 = MessageBox.Show("Ready to start?\n(The settings app will automatically open at the starting point of this guide)", "Ready?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (q1 == DialogResult.Yes)
                {
                    Process.Start("ms-settings:recovery");
                }
            }
            newBtnText();
        }

        private void newBtnText()
        {
            if (picCounter != 6)
            {
                button1.Text = "To Step " + (picCounter + 1) + " >";
            }
            else
            {
                button1.Text = "Finished";
            }
        }

        private void bwd(object sender, EventArgs e)
        {
            if (picCounter > 1)
            {
                picCounter--;
                loadPic(picCounter);
            }
            if (picCounter == 1)
            {
                button2.Enabled = false;
            }
            newBtnText();
        }

        private void loadPic(int num)
        {
            try
            {
                pictureBox1.Image = Image.FromFile("Screenshots\\Screen" + num + ".png");
            }
            catch (Exception ex) { MessageBox.Show("Error: For this guide to work you need to extract the archive this application came in"); }
        }
    }
}
