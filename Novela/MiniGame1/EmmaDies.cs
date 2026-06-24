using project.MiniGamess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project.MiniGame1.Patientt;

namespace project.MiniGame1
{
    public class EmmaDies : MiniGames
    {
        private Random random = new Random();
        private Patient patient = new Patient();
        private int timeLeft;
        private System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timer3 = new System.Windows.Forms.Timer();
        private bool s25seconds = false;

        public int TimeLeft => timeLeft;
        public int Temperature => Convert.ToInt32(patient.Temperature * 10);
        public int Breath => patient.Breath;
        public int BloodDark => patient.BloodDark;
        public bool ss25sec => s25seconds;

        public void Stimulate()
        {
            if (isGameRunning) patient.Stimulate();
        }

        public void Cool()
        {
            if (isGameRunning) patient.Cool();
        }

        public void Breathing()
        {
            if (isGameRunning) patient.Breathing();
        }

        public EmmaDies(string name, int id) : base(name, id) { }

        public override void Game()
        {
            isGameRunning = true;
            timeLeft = 60;
            s25seconds = false;
            timer3.Interval = 1000;
            timer1.Interval = 5000;
            timer3.Start();
            timer1.Start();
            timer1.Tick += UpdatePatientStats;
            timer3.Tick += UpdatePatientStatsTime;
            if (!isGameRunning) { timer1.Stop(); timer3.Stop(); timer1.Enabled = false; timer3.Enabled = false; }
        }

        private void UpdatePatientStatsTime(object sender, EventArgs e)
        {
            if (!isGameRunning) return;
            if (patient.IsAlive())
            {
                timeLeft--;
                if (timeLeft == 25 && !s25seconds)
                {
                    s25seconds = true;
                    patient.Temperature = 41; patient.Breath = 20; patient.BloodDark = 80;
                }
            }
        }
        private void UpdatePatientStats(object sender, EventArgs e)
        {
            if (!isGameRunning) return;

            double randomval = random.Next(1, 10) / 10.0;
            patient.Temperature += randomval;
            patient.BloodDark += random.Next(1, 20);
            patient.Breath -= random.Next(1, 20);

            
            if (!patient.IsAlive())
            {
                EndGame();
            }
        }
        private void EndGame()
        {
            if (!isGameRunning) return;
            isGameRunning = false;

            timer1.Stop();
            timer3.Stop();
        }
    }
}
