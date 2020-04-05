using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class Form1 : Form
    {
        List<Button> butons_list = new List<Button>();
        TextBox tb = new TextBox();
        public Form1()
        {
            InitializeComponent();
            Mix();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Add(textBox1.Text);
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.Items.Count > 0)
            {
                comboBox1.Items.RemoveAt(comboBox1.Items.Count - 1);
            }
        }

        private void Mix()
        {
            tb.BackColor = Color.LawnGreen;
            tb.Location = new Point(137, 205);
            tb.Size = new Size(153, 20);
            tb.TextAlign = HorizontalAlignment.Center;
            tb.ReadOnly = true;
            this.tabPage2.Controls.Add(tb);
            string[] a = new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16"};

            Random rand = new Random();
            int rnd_nom;
            for (int n = 0; n < 16; n++)
            {
                rnd_nom = rand.Next(0, 15);
                string temp = a[rnd_nom];
                a[rnd_nom] = a[n];
                a[n] = temp;
            }


            int n_16 = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button b = new Button();
                    b.Size = new System.Drawing.Size(39, 20);
                    b.Location = new System.Drawing.Point(90 + j * 70, 30 + i * 25);
                    b.Text = a[n_16];
                    this.tabPage2.Controls.Add(b);
                    n_16++;
                    b.MouseClick += Button_MouseClick;
                    butons_list.Add(b);
                }
            }

            
        }

        int index_list = 15;
        int min = 0;
        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            Random rand = new Random();
            int rnd_nom;
            int numb_but = Convert.ToInt32((sender as Button).Text);


            if (numb_but == (min + 1))
            {
                int numb_elem = 0;
                for (int n = 0; n < butons_list.Count; n++)
                {
                    if(butons_list[n].Text == (sender as Button).Text)
                    {
                        numb_elem = n;
                        break;
                    }
                    
                }
                butons_list[numb_elem].Visible = false;
                butons_list.RemoveAt(numb_elem);
                index_list--;
                for (int n = 0; n < butons_list.Count; n++)
                {
                    rnd_nom = rand.Next(0, index_list);
                    string temp = butons_list[rnd_nom].Text;
                    butons_list[rnd_nom].Text = butons_list[n].Text;
                    butons_list[n].Text = temp;
                }
                min++;
            }
            else
            {
                min = 0;
                this.tabPage2.Controls.Clear();
                butons_list.Clear();
                Mix();
            }

            if(min == 16)
            {
                tb.Text = "Молодець!!!";
            }
        }
    }
}
