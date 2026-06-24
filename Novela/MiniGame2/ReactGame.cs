using project.MiniGame2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project
{
    public partial class ReactGame : Form
    {
        private React rt;

        public ReactGame()
        {
            InitializeComponent();

            rt = new React("r", 2);

            MessageBox.Show(
                "                                  Попадайте по стрелкам\n                В центре экрана будет появляться стрелка\nВаша задача: Успевать нажимать на ту же стрелку на клавиатуре 25 раз подряд\nЕсли не нажали ту стрелку или не успели, то счёт начинается заново\nУ вас есть 60 секунд",
                "Инструкция",
                MessageBoxButtons.OK);

            timer1.Tick += timer1_Tick;
            rt.Game();
            timer1.Start();
        }

        private void ReactGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (rt == null) return;
            if (!rt.isGameRunning) return;

            rt.ForPressedKey(e.KeyCode);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (rt.isGameRunning)
            {
                label3.Text = rt.TimeLeft.ToString();
                label4.Text = rt.Streak.ToString() + " | 25";
                label1.Text = rt.NowKeys;
                label2.Text = rt.PressedKey;
            }
            else
            {
                timer1.Stop();
                if (rt.result)
                {
                    MessageBox.Show("        |Замок открыт|     ");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("          |Замок заблокирован.|\n|Повторите попытку через 5 минут.|");
                    this.DialogResult = DialogResult.Cancel;
                }
                this.Close();
            }
        }
    }
}