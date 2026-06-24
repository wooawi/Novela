using System;
using System.Windows.Forms;
using project.MiniGame1;

namespace project
{
    public partial class SavingGame : Form
    {
        EmmaDies ed = new EmmaDies("be", 1);
        public SavingGame()
        {
            InitializeComponent();
            MessageBox.Show(
                "                                  Стабилизируйте пациента\n               Характеристики будут постепенно подниматься\nВаша задача: Продержаться 60 секунд и не дать пациенту погибнуть\n\n                                              Инструкция:\nСтимуляция /ОЧЕРНЕНИЕ КРОВИ/ (Очернение крови -15 | Температура + 0,5)\nОхлаждениe /ТЕМПЕРАТУРА/ (Температура -1 | Дыхание -10)\nИскусственное дыхание /ДЫХАНИЕ/ (Дыхание +15 | Очернение крови +10)\nСмерть на: Температура > 41, Дыхание < 20, Очернение крови 80%",
                "Инструкция",
                MessageBoxButtons.OK);
            ed.isGameRunning = true;
            timer2.Start();
            ed.Game();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ed.Stimulate();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ed.Cool();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ed.Breathing();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (ed.isGameRunning)
            {
                label13.Text = ed.TimeLeft.ToString();
                progressBar2.Value = ed.BloodDark;
                progressBar1.Value = ed.Temperature;
                progressBar3.Value = ed.Breath;
            }
            else {
                timer2.Stop();
                if (ed.ss25sec) { MessageBox.Show("Резко подскочил уровень очернения крови!\nПациента вырвало чёрной субстанцией!\n Пациент умер... Игра окончена.");}
                else { MessageBox.Show("Пациент умер... Игра окончена."); }
                this.Close();
            }
        }
    }
}
