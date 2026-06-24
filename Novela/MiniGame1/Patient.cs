using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.MiniGame1.Patientt
{
    public class Patient
    {
        public double Temperature;
        public int Breath;
        public int BloodDark;

        public Patient() { Temperature = 38; Breath = 100; BloodDark = 0; }
        public bool IsAlive() { return Temperature < 41 && Breath > 20 && BloodDark < 80; }
        public void Stimulate() { 
            BloodDark -= 15;
            if (BloodDark < 0) { BloodDark = 0; }
            Temperature += 0.5; 
            if (Temperature > 41) { Temperature = 41; } 
        }
        public void Cool() { 
            Temperature -= 1; 
            if (Temperature < 0) { Temperature = 0; }
            Breath -= 10; 
            if (Breath < 20) { Breath = 20; } 
        }
        public void Breathing()
        {
            Breath += 15;
            if (Breath > 100) { Breath = 100; }
            BloodDark += 10;
            if (BloodDark > 80) { BloodDark = 80; }
        }
    }
}
