using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form1 : Form

    {

        int width;
        int height;
        int offset = 25;
        int bombPercent;
        
        bool FirstClick = true;


        FieldButton[,] field;
        int Opened = 0;
        int bombs = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void GenerateField()
        {
            Random random = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    FieldButton newButton = new FieldButton();
                    newButton.Location = new Point(x * offset, y * offset);
                    newButton.Size = new Size(offset, offset);
                    newButton.isClickable = true;

                    if (random.Next(0, 100) <= bombPercent)
                    {
                        newButton.isBomb = true;
                        bombs++;
                    }
                    newButton.xPosition = x;
                    newButton.yPosition = y;
                    Controls.Add(newButton);
                    newButton.MouseUp += new MouseEventHandler(FieldButtonClick);
                    field[x, y] = newButton;
                }
            }
        }

        void FieldButtonClick(object sender, MouseEventArgs e)
        {



            FieldButton clickedButton = (FieldButton)sender;
            if (e.Button == MouseButtons.Left && clickedButton.isClickable)
            {
                if (clickedButton.isBomb)
                {
                    if (FirstClick)
                    {
                        clickedButton.isBomb = false;
                        FirstClick = false;                       // условие проверки первого хода, чтобы первое нажатие всегда было на пустую клетку

                        OpenRegion(clickedButton.xPosition, clickedButton.yPosition, clickedButton);
                    }
                    else
                    {
                        Explode();
                    }
                }
                else
                {
                    EmptyFieldButtonClick(clickedButton);
                }
                FirstClick = false;
            }

            if (e.Button == MouseButtons.Right)
            {
                clickedButton.isClickable = !clickedButton.isClickable; // Метка бомбы правой кнопкой мыши
                if (!clickedButton.isClickable)
                {
                    clickedButton.Text = "🏲";
                }
                else
                {
                    clickedButton.Text = "";
                }
            }
            CheckWin();
        }

        void Explode()
        {

            foreach (FieldButton button in field)
            {
                if (button.isBomb)
                {
                    button.Text = "💣";
                }
            }
            MessageBox.Show("Ты програв 😓");
            Application.Restart();

        }
        void EmptyFieldButtonClick(FieldButton clickedButton)
        {

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (field[x, y] == clickedButton)
                    {

                        OpenRegion(x, y, clickedButton);

                    }
                }
            }


        }
        void OpenRegion(int xPosition, int yPosition, FieldButton clickedButton)
        {
            Queue<FieldButton> queue = new Queue<FieldButton>();
            queue.Enqueue(clickedButton);
            clickedButton.wasAdded = true;

            while (queue.Count > 0)
            {
                FieldButton currentCell = queue.Dequeue();
                OpenCell(currentCell.xPosition, currentCell.yPosition, currentCell);
                Opened++;
                if (CountBombsAround(currentCell.xPosition, currentCell.yPosition) == 0)
                {
                    for (int y = currentCell.yPosition - 1; y <= currentCell.yPosition + 1; y++)
                    {
                        for (int x = currentCell.xPosition - 1; x <= currentCell.xPosition + 1; x++)  // функция автоматического раскрытия всех соседних пустых клеток
                        {
                            if (x == currentCell.xPosition && y == currentCell.yPosition)
                            {
                                continue;
                            }
                            if (x >= 0 && x < width && y < height && y >= 0)
                            {
                                if (!field[x, y].wasAdded)
                                {
                                    queue.Enqueue(field[x, y]);
                                    field[x, y].wasAdded = true;
                                }
                            }
                        }
                    }

                }
            }

        }
        void OpenCell(int x, int y, FieldButton clickedButton)
        {
            int bombsAround = CountBombsAround(x, y);
            if (bombsAround == 0)
            {
                clickedButton.Text = "";

            }
            else
            {
                clickedButton.Text = "" + bombsAround;

            }
            clickedButton.Enabled = false;

            CheckWin();
        }

        int CountBombsAround(int xCoord, int yCoord)
        {
            int bombsAround = 0;
            for (int x = xCoord - 1; x <= xCoord + 1; x++)
            {
                for (int y = yCoord - 1; y <= yCoord + 1; y++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (field[x, y].isBomb == true)
                        {
                            bombsAround++;
                        }
                    }
                }
            }
            return bombsAround;

        }

        void CheckWin()
        {
            int cells = width * height;
            int emptyCells = cells - bombs;
            if (emptyCells == Opened)
            {
                MessageBox.Show("Перемога! 🎂");
                Application.Restart();
            }
        }

        void VisibleLevel()
        {
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            label1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msb = MessageBoxButtons.YesNo;
            String message = "Ви дійсно бажаєте вийти?";
            String caption = "Вихід";
            if (MessageBox.Show(message, caption, msb) == DialogResult.Yes)
                this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {

               
                this.ClientSize = new System.Drawing.Size(300, 200);

                
                button2.Location = new Point(213, 141);
                button1.Location = new Point(213, 170);
                height = 8;
                width = 8;
                bombPercent = 15;
                field = new FieldButton[width, height];

                GenerateField();
                VisibleLevel();



            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(522, 400);
            button2.Location = new Point(419, 323);
            button1.Location = new Point(419, 352);
            height = 16;
            width = 16;
            bombPercent = 20;
            field = new FieldButton[width, height];
            GenerateField();
            VisibleLevel();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(780, 370);
            button2.Location = new Point(698, 310);
            button1.Location = new Point(698, 339);
            height = 16;
            width = 30;
            bombPercent = 30;
            field = new FieldButton[width, height];
            GenerateField();
            VisibleLevel();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

      



        public class FieldButton : Button
        {
            public bool isBomb;
            public bool isClickable;
            public bool wasAdded;
            public int xPosition;
            public int yPosition;
        }


    }
}
