using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {
        double left_operand = 0;
        double right_operand = 0;
        Operation operation;
        public Calculator()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Number_Click(object sender, EventArgs e)
        {
            this.textBox1.Text += (sender as Button).Text;
        }

        private void Clean_Button_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        private void Plus_Button_Click(object sender, EventArgs e)
        {
            this.operation = Operation.Plus;
            this.left_operand = double.Parse(this.textBox1.Text);
            this.textBox1.Text = "";
        }
        private void Minus_Button_Click(object sender, EventArgs e)
        {
            this.operation = Operation.Minus;
            this.left_operand = double.Parse(this.textBox1.Text);
            this.textBox1.Text = "";
        }
        private void Multiply_Button_Click(object sender, EventArgs e)
        {
            this.operation = Operation.Multiply;
            this.left_operand = double.Parse(this.textBox1.Text);
            this.textBox1.Text = "";
        }
        private void Devide_Button_Click(object sender, EventArgs e)
        {
            this.operation = Operation.Devide;
            this.left_operand = double.Parse(this.textBox1.Text);
            this.textBox1.Text = "";
        }

        private void Equal_Button_Click(object sender, EventArgs e)
        {
            this.right_operand = double.Parse(this.textBox1.Text);
            switch (this.operation)
            {
                case Operation.Plus:
                    this.textBox1.Text = (left_operand + right_operand).ToString();
                    break;
                case Operation.Minus:
                    this.textBox1.Text = (left_operand - right_operand).ToString();
                    break;
                case Operation.Multiply:
                    this.textBox1.Text = (left_operand * right_operand).ToString();
                    break;
                case Operation.Devide:
                    this.textBox1.Text = (left_operand / right_operand).ToString();
                    break;
                default:
                    break;
            }
        }

        enum Operation
        {
            Plus,
            Minus,
            Multiply,
            Devide
        }
    }
}
