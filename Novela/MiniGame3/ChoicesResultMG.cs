using project.MiniGamess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.MiniGame3.HelpClass
{
    public class ChoicesResultMG : MiniGames
    {
        List <List<DialogesChoicesMG>> aaa = new List<List<DialogesChoicesMG>> {
            new List<DialogesChoicesMG>
            { new DialogesChoicesMG("Эмма подошла к крыльцу, пнула половичок.\nПод ковриком было пусто, только заполоный паук резко помчался прочь","Автор",1 ,0),
            new DialogesChoicesMG("Ничего","Эмма",1 ,0),
            new DialogesChoicesMG("Тогда проверяй дальше","Диана",2 ,0) },

            new List<DialogesChoicesMG>
            { new DialogesChoicesMG("Эмма потянулась к фонарю над дверью. Сняла с крюка,заглянула внутрь.\nСреди паутины и сухих мух лежал старый чёрный ключ","Автор",1 ,0),
            new DialogesChoicesMG("Есть","Эмма",0 ,0),
            new DialogesChoicesMG("Давай сюда","Диана",3 ,0),
            new DialogesChoicesMG("Диана взяла ключ, вставила в замок.\nЗасов лязгнул","Автор",1 ,0)},

            new List<DialogesChoicesMG>
            { new DialogesChoicesMG("Эмма отодвинула булыжник. Под ним - влажная земля, черви, и больше ничего.\nНа нижней стороне камня она заметила какие-то странные царапины - символы,\nпохожие на детские каракули.Под ковриком было пусто","Автор",1 ,0),
            new DialogesChoicesMG("Ничего, только непонятные знаки","Эмма",1 ,0),
            new DialogesChoicesMG("Не отвлекайся","Роня",4 ,0) }
            };
        bool result;

        public ChoicesResultMG(string a, int b) : base(a, b) { result = false; } 

        public bool GetResult() { return result; }
        public List<DialogesChoicesMG> ChoiceA()
        {
            return aaa[0];
        }
        public List<DialogesChoicesMG> ChoiceB()
        {
            result = true;
            return aaa[1];
        }
        public List<DialogesChoicesMG> ChoiceC()
        {
            return aaa[2];
        }

        public override void Game()
        {
            isGameRunning = true;
            if (isGameRunning && result)
            {
                isGameRunning = false;
            }

        }
    }

    public class DialogesChoicesMG
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public int indIm { get; set; }
        public int intImB { get; set; }

        public DialogesChoicesMG(string text, string name, int imageIndex, int backgroundIndex)
        {
            Text = text;
            Name = name;
            indIm = imageIndex;
            intImB = backgroundIndex;
        }

    }
}
