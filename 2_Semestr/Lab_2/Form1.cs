using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Timer1.Interval = 200;
            this.Timer1.Tick += Timer1_Tick;
            this.Timer1.Start();
        }

        string text = "12345";
        bool on_or_off = true;
        int i = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
