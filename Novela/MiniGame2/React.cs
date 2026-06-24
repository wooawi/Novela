using project.MiniGamess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace project.MiniGame2
{
    public class React : MiniGames
    {
        private static List<Keys> Symbols = new List<Keys>()
        {
            Keys.Up, Keys.Down, Keys.Left, Keys.Right
        };

        private Random random = new Random();
        private int streak;
        private Keys nowSymbol;
        private Keys pressedKey;
        private DateTime lastChange;
        private int timeLeft;
        private System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timer3 = new System.Windows.Forms.Timer();

        public int TimeLeft => timeLeft;
        public int Streak => streak;
        public string NowKeys => GetSymbol(nowSymbol);
        public string PressedKey => GetSymbol(pressedKey);

        public React(string name, int id) : base(name, id)
        {
            timer2.Tick += UpdateSymbol;
            timer3.Tick += UpdateTime;
        }

        public override void Game()
        {
            lastChange = DateTime.Now;
            streak = 0;
            timeLeft = 60;
            isGameRunning = true;
            pressedKey = Keys.None;
            result = false;
            nowSymbol = Symbols[random.Next(0, 4)];

            timer2.Interval = 1000;
            timer3.Interval = 1000;

            timer2.Tick -= UpdateSymbol;
            timer2.Tick += UpdateSymbol;
            timer3.Tick -= UpdateTime;
            timer3.Tick += UpdateTime;

            timer2.Start();
            timer3.Start();
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            if (!isGameRunning) return;

            timeLeft--;


            if (streak > 0 && (DateTime.Now - lastChange).TotalMilliseconds >= 1000)
            {
                streak = 0;
            }

            if (streak >= 25)
            {
                result = true;
                EndGame();
                return;
            }

            if (timeLeft <= 0)
            {
                result = false;
                EndGame();
            }
        }

        private void UpdateSymbol(object sender, EventArgs e)
        {
            if (!isGameRunning) return;

            int randomsym = random.Next(0, 4);
            while (nowSymbol == Symbols[randomsym])
            {
                randomsym = random.Next(0, 4);
            }
            nowSymbol = Symbols[randomsym];
        }

        private string GetSymbol(Keys key)
        {
            switch (key)
            {
                case Keys.Up: return "↑";
                case Keys.Down: return "↓";
                case Keys.Left: return "←";
                case Keys.Right: return "→";
                default: return " ";
            }
        }

        public void ForPressedKey(Keys key)
        {
            if (!isGameRunning) return;

            if (key != Keys.Up && key != Keys.Down && key != Keys.Left && key != Keys.Right)
                return;

            pressedKey = key;
            lastChange = DateTime.Now;

            if (key == nowSymbol) { streak++; }
            else { streak = 0; }
        }

        private void EndGame()
        {
            if (!isGameRunning) return;
            isGameRunning = false;

            timer2.Stop();
            timer3.Stop();
        }
    }
}
