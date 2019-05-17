using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int[,] mas = new int[3, 3] { { 2, 2, 2 }, { 2, 2, 2 }, { 2, 2, 2 } };
        int x, y, i = 0, j = 0;
        int px, py, x1, y1, x2, y2;
        bool hod = false; bool smena = true;


        public Form1()
        {
            InitializeComponent();            
        }

        private void Draw() // Рисование кр. 0
        {
            Graphics g = panel1.CreateGraphics();
            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (mas[i, j] == 0) g.DrawEllipse(new Pen(Color.Red, 5), (j + 1) * 200 - 140, (i + 1) * 200 - 140, 80, 80);
                    else if (mas[i, j] == 1)
                    {
                        g.DrawLine(new Pen(Color.Blue, 5), (j + 1) * 200 - 140, (i + 1) * 200 - 140, (j + 1) * 200 - 60, (i + 1) * 200 - 60);
                        g.DrawLine(new Pen(Color.Blue, 5), (j + 1) * 200 - 60, (i + 1) * 200 - 140, (j + 1) * 200 - 140, (i + 1) * 200 - 60);
                    }
                }            
        }
   
        private void radioButton1_Click(object sender, EventArgs e) // x ходит 1
        {            
            if (smena)
            {
                hod = false; smena = true;
                label2.Text = "Сейчас ходит X";
               
            }

        }

        private void radioButton2_Click(object sender, EventArgs e) // 0 ходит 1
        {
            if (smena)
            {
                hod = true; smena = false;
                label2.Text = "Сейчас ходит 0";
            }
        }       

        private void panel1_Paint(object sender, PaintEventArgs e) // Границы карты 3х3 
        {
            Graphics g = e.Graphics;
            g.DrawLine(new Pen(Color.Black,3), 200,0,200,600);
            g.DrawLine(new Pen(Color.Black, 3), 400, 0, 400, 600);
            g.DrawLine(new Pen(Color.Black, 3), 0, 200, 600, 200);
            g.DrawLine(new Pen(Color.Black, 3), 0, 400, 600, 400);            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)  //нажатие
        {
            x = e.Location.X;
            y = e.Location.Y;
            px = (int)x / 200;
            py = (int)y / 200;
            if (hod && (mas[py, px] == 2))
            {
                mas[py, px] = 0;
                hod = !hod;
                label2.Text = "Сейчас ходит X";
            }
            else if (mas[py, px] == 2) 
            {
                mas[py, px] = 1;
                hod = !hod;
                label2.Text = "Сейчас ходит 0";
            }
                          
            Draw();
            update_txt(py, px);
            checkwin();
            beat();
        }

        private void button2_Click(object sender, EventArgs e) // o4istka txt
        {
            File.WriteAllText("D:\\TTT.txt", string.Empty); 
        }

        void update_txt(int py, int px) // запись в блокнот (LOG)
        {
            string s;
            StreamWriter sw = File.AppendText("D:\\test.txt");
            if (hod)
            {
                s = "\nПервый игрок поставил крестик в клетку " + py + " " + px;
            }
            else
            {
                s = "\nВторой игрок поставил нолик в клетку " + py + " " + px;
            }
            sw.WriteLine(s);
            sw.Flush();
            sw.Close();
        }

        void checkwin() // check win 
        {
            
            bool check = false;
            if (mas[0, 0] == mas[0, 1] && mas[0, 1] == mas[0, 2] && mas[0, 2] != 2) { check = true; y1 = 100; x1 = 100; y2 = 100; x2 = 500; }
            if (mas[1, 0] == mas[1, 1] && mas[1, 1] == mas[1, 2] && mas[1, 2] != 2) { check = true; y1 = 300; x1 = 100; y2 = 300; x2 = 500; }
            if (mas[2, 0] == mas[2, 1] && mas[2, 1] == mas[2, 2] && mas[2, 2] != 2) { check = true; y1 = 500; x1 = 100; y2 = 500; x2 = 500; }
            if (mas[0, 0] == mas[1, 0] && mas[1, 0] == mas[2, 0] && mas[2, 0] != 2) { check = true; y1 = 100; x1 = 100; y2 = 500; x2 = 100; }
            if (mas[0, 1] == mas[1, 1] && mas[1, 1] == mas[2, 1] && mas[2, 1] != 2) { check = true; y1 = 100; x1 = 300; y2 = 500; x2 = 300; }
            if (mas[0, 2] == mas[1, 2] && mas[1, 2] == mas[2, 2] && mas[2, 2] != 2) { check = true; y1 = 100; x1 = 500; y2 = 500; x2 = 500; }
            if (mas[0, 0] == mas[1, 1] && mas[1, 1] == mas[2, 2] && mas[2, 2] != 2) { check = true; y1 = 100; x1 = 100; y2 = 500; x2 = 500; }
            if (mas[0, 2] == mas[1, 1] && mas[1, 1] == mas[2, 0] && mas[2, 0] != 2) { check = true; y1 = 100; x1 = 500; y2 = 500; x2 = 100; }
            if (check)
            {
                Graphics g = panel1.CreateGraphics();
                string s; 
                if (hod)
                {
                    g.DrawLine(new Pen(Color.Black, 3), x1, y1, x2, y2);
                    MessageBox.Show("Победили крестики"); s = "Победили крестики"; i++; label3.Text = "Побед Крестика: " + i;
                }
                else
                {
                    g.DrawLine(new Pen(Color.Black, 3), x1, y1, x2, y2);
                    MessageBox.Show("Победили нолики"); s = "Победили нолики"; j++;  label4.Text = "Побед Нолика: " + j;
                }
                StreamWriter sw = File.AppendText("D:\\TTT.txt");
                sw.WriteLine(s);
                sw.Flush();
                sw.Close();
                refresh();
                smena = true;
            }
        }

        void refresh() // refresh mapbI
        {
            panel1.Refresh(); hod = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    mas[i, j] = 2;
                }
            }
        }

        void beat() // проверка на ничью
        {
            bool check = true;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (mas[i, j] == 2) check = false;
                }
            if (check)
            {                 
                string s;
                MessageBox.Show("Ничья!"); s = "Ничья!";
                StreamWriter sw = File.AppendText("D:\\TTT.txt");
                sw.WriteLine(s);
                sw.Flush();
                sw.Close();
                refresh();
                smena = true;
            }
        }
    }
}
