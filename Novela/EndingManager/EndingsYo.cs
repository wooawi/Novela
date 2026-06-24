using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.EndingManager
{
    public class EndingsYo
    {
        public string Name { get; private set; }
        public bool IsUnlocked { get; private set; }

        public EndingsYo(string name) { Name = name; IsUnlocked = false; }

        public void Unlock() { IsUnlocked = true; }
    }
}
