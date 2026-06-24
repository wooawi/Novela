using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace project.EndingManager.EndingUsee
{
    public class EndingUse
    {
        private string EndingsName;
        private List<EndingsYo> endings = new List<EndingsYo>()
        {
            new EndingsYo("Концовка А"),
            new EndingsYo("Концовка В"),
            new EndingsYo("Концовка Г"),
            new EndingsYo("Концовка С")
        };
        private string textBe = "";

        public void Adding(EndingsYo ending)
        {
            if (ending == null) { EndingsYo e = new EndingsYo("None"); ending = e; }
            endings.Add(ending);
        }
        public void Unlocking(string a)
        {
            if (a != null)
            {
                foreach (EndingsYo ending in endings)
                {
                    if (a == ending.Name) { ending.Unlock(); }
                }
            }
        }

        public List<string> GetUnlockedList()
        {
            var result = new List<string>();
            foreach (EndingsYo ending in endings)
            {
                if (ending.IsUnlocked) result.Add(ending.Name);
            }
            return result;
        }

        private string GetCloseName(string name)
        {
            string b = "";
            foreach (char a in name)
            {
                b += "#";
            }
            return b;
        }
        public string ShowAll()
        {
            textBe = "";
            int a = 0;
            int b = 0;

            foreach (EndingsYo ending in endings)
            {
                string NameBe = "";
                if (ending.IsUnlocked) { NameBe = ending.Name; a++; } else { NameBe = GetCloseName(ending.Name); }
                b++;
                textBe += b + ". " + NameBe + "\n";
            }
            textBe += "\nОткрыто: " + a + " концовок из " + endings.Count;
            return textBe;

        }

        public string ShowAllUnlocked()
        {
            textBe = "";
            int c = 0;
            foreach (EndingsYo ending in endings)
            {
                if (ending.IsUnlocked) { c++; textBe += c + ". " + ending.Name + "\n"; }
            }
            if (c == 0) { textBe = "Вы пока не получили ни одну из концовок"; }
            return textBe;
        }
    }
}
