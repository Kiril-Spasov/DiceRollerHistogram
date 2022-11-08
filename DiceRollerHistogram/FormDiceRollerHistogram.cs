using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DiceRollerHistogram
{
    public partial class FormDiceRollerHistogram : Form
    {
        public FormDiceRollerHistogram()
        {
            InitializeComponent();
        }

        Graphics g;

        private void BtnHorizontal_Click(object sender, EventArgs e)
        {        
            int[] frequency = new int[13];
            DiceRollSimulation(frequency);
            DisplayHorizontalHistogram(frequency);
        }

        private void DiceRollSimulation(int[] f)
        {
            ClearArray(f);
            Random r = new Random();
            int dice1, dice2, sum;

            //We start from 2, because there is no sum of 2 dice = 1.
            for (int i = 2; i <= 1000; i++)
            {
                dice1 = r.Next(1, 7);
                dice2 = r.Next(1, 7);
                sum = dice1 + dice2;
                f[sum] += 1;
            }
        }

        //We take the largest number from our data and divide it by the size of the screen.
        //We use the result to calculate later how many pixels for each chart.
        private double FindScalingFactor(int[] f)
        {
            int biggest = f[1];
            double scalingFactor = 0;

            for (int i = 2; i <= f.Count() - 1; i++)
            {
                if (f[i] > biggest)
                    biggest = f[i];
            }

            scalingFactor = Convert.ToDouble(biggest) / (panel1.Width - 200);

            return scalingFactor;
        }

        private void DisplayHorizontalHistogram(int[] f)
        {
            g.Clear(panel1.BackColor);

            //We use Random for random colors.
            Random r = new Random();

            double sf = FindScalingFactor(f);

            Font font = new Font("Verdana", 8);
            SolidBrush brush = new SolidBrush(Color.Blue);
            

            g.DrawString("Dice Roller Histogram", font, brush, panel1.Width / 2, 10);
            g.DrawString("Roll", font, brush, 1, 50);
            g.DrawString("Frequency", font, brush, 40, 50);

            for (int i = 2; i <= 12; i++)
            {
                g.DrawString(i.ToString(), font, brush, 1, 50 + i * 20);
                g.DrawString(f[i].ToString(), font, brush, 40, 50 + i * 20);

                Color randomColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                SolidBrush brushRandomColor = new SolidBrush(randomColor);

                //We use the scaling factor to calculate how many pixels for the width of each rectangle.
                int rectangleWidth = (int)(f[i] / sf);

                g.FillRectangle(brushRandomColor, 100, 50 + i * 20, rectangleWidth, 15);
            }
        }

        private void BtnVertical_Click(object sender, EventArgs e)
        {
            int[] frequency = new int[13];
            DiceRollSimulation(frequency);
            DisplayVerticalHistogram(frequency);
        }

        private double FindVerticalScalingFactor(int[] f)
        {
            int biggest = f[1];
            double vercticalSf;

            for (int i = 1; i <= f.Count() - 1; i++)
            {
                if (f[i] > biggest)
                    biggest = f[i];
            }

            vercticalSf = Convert.ToDouble(biggest) / (panel1.Height - 125);

            return vercticalSf;
        }

        private void DisplayVerticalHistogram(int[] f)
        {
            g.Clear(panel1.BackColor);

            Random r = new Random();

            double verticalSf = FindVerticalScalingFactor(f);

            Font font = new Font("Verdana", 8);
            SolidBrush brush = new SolidBrush(Color.Blue);
            g.DrawString("Dice Roller Histogram", font, brush, panel1.Width / 2, 10);

            for (int i = 2; i <= f.Count() - 1; i++)
            {
                g.DrawString(i.ToString(), font, brush, 50 + i * 35, panel1.Height - 50);

                //We use the scaling factor to calculate how many pixels is the height for each rectangle.
                int rectangleHeight = (int)(f[i] / verticalSf);

                Color randomColor = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
                SolidBrush brushRandomColor = new SolidBrush(randomColor);
                g.FillRectangle(brushRandomColor, 50 + i * 35, panel1.Height - 50 - rectangleHeight, 15, rectangleHeight);

                g.DrawString(f[i].ToString(), font, brush, 45 + i * 35, panel1.Height - 75 - rectangleHeight);
            }
        }

        private int[] ClearArray(int[] ar)
        {
            for (int i = 0; i <= ar.Count() - 1; i++)
            {
                ar[i] = 0;
            }

            return ar;
        }

        private void FormDiceRollerHistogram_Load(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
