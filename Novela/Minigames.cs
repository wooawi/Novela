using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.MiniGamess
{
    public class MiniGames
    {
        public string name;
        public int id;
        public bool result;
        public bool isGameRunning;

        public MiniGames(string pname, int pid) { name = pname; id = pid; isGameRunning = false; }
        public bool GetResult() { return result; }
        public virtual void Game() => Console.WriteLine();
    }
}