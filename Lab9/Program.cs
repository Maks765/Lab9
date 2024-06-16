using System;
using System.Drawing;
using System.Windows.Forms;

namespace GraphPlotter
{
    public class GraphPlotterForm : Form
    {
        private TextBox coefficientATextBox;
        private TextBox coefficientBTextBox;
        private Button plotButton;
        private PictureBox graphPictureBox;

        private const int plotWidth = 800;
        private const int plotHeight = 600;
        private const int plotMargin = 20;

        public GraphPlotterForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            // Налаштування форми
            Text = "Графік функції";
            Size = new Size(plotWidth + 2 * plotMargin, plotHeight + 4 * plotMargin + 50);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Налаштування елементів управління
            coefficientATextBox = new TextBox();
            coefficientATextBox.Location = new Point(plotMargin, plotMargin);
            coefficientATextBox.Size = new Size(50, 20);

            coefficientBTextBox = new TextBox();
            coefficientBTextBox.Location = new Point(plotMargin, 2 * plotMargin);
            coefficientBTextBox.Size = new Size(50, 20);

            plotButton = new Button();
            plotButton.Text = "Побудувати графік";
            plotButton.Location = new Point(plotMargin, 3 * plotMargin);
            plotButton.Click += PlotButton_Click;

            graphPictureBox = new PictureBox();
            graphPictureBox.Location = new Point(plotMargin, 4 * plotMargin);
            graphPictureBox.Size = new Size(plotWidth, plotHeight);
            graphPictureBox.BackColor = Color.White;
            graphPictureBox.Paint += GraphPictureBox_Paint;

            // Додавання елементів управління до форми
            Controls.Add(coefficientATextBox);
            Controls.Add(coefficientBTextBox);
            Controls.Add(plotButton);
            Controls.Add(graphPictureBox);
        }

        private void PlotButton_Click(object sender, EventArgs e)
        {
            graphPictureBox.Refresh();
        }

        private void GraphPictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Отримання коефіцієнтів a, b з текстових полів
            if (!double.TryParse(coefficientATextBox.Text, out double a) ||
                !double.TryParse(coefficientBTextBox.Text, out double b))
            {
                MessageBox.Show("Некоректні коефіцієнти. Будь ласка, введіть числові значення.");
                return;
            }

            // Створення об'єкту Graphics для малювання на PictureBox
            Graphics g = e.Graphics;
            Pen graphPenX = new Pen(Color.Red, 2);
            Pen graphPenY = new Pen(Color.Blue, 2);

            // Розрахунок масштабу
            double scale = Math.Min(plotWidth / 20.0, plotHeight / 20.0);

            // Малювання графіку
            for (double t = -10; t <= 10; t += 0.1)
            {
                double x = a * Math.Cos(t);
                double y = b * Math.Cos(t);

                float xPlot = (float)(plotWidth / 2 + x * scale);
                float yPlot = (float)(plotHeight / 2 - y * scale);

                // Перевірка меж для xPlot
                if (xPlot < 0 || xPlot >= plotWidth)
                {
                    continue;
                }

                // Перевірка меж для yPlot
                if (yPlot < 0 || yPlot >= plotHeight)
                {
                    continue;
                }

                g.DrawEllipse(graphPenX, xPlot, yPlot, 2, 2);
                g.DrawEllipse(graphPenY, xPlot, yPlot, 2, 2);
            }

            graphPenX.Dispose();
            graphPenY.Dispose();
        }
    }

    public class Program
    {
        public static void Main()
        {
            Application.Run(new GraphPlotterForm());
        }
    }
}
