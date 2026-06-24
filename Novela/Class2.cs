using System;
using System.Collections.Generic;

namespace project.Choices
{
    
    public class Choice
    {
        public string Text { get; set; }
        public string IdTyping { get; set; }

        public Choice(string text, string idTyping)
        {
            Text = text;
            IdTyping = idTyping;
        }
    }

    public class ChoiceManager
    {
        private string _lastResult;
        private readonly Dictionary<int, List<Choice>> _sceneChoices = new();

        public ChoiceManager()
        {
           
            AddChoice(0, new Choice("Эмма поворачивает стрелки.", "a1"));
            AddChoice(0, new Choice("Эмма закрывает шкатулку и отказывается от этой идеи.", "b1"));

            
            AddChoice(1, new Choice("Эмма остаётся с пациентом. Диана, Роня и Динара идут в церковь.", "endF"));
            AddChoice(1, new Choice("Эмма идёт с девочками. Пациент остаётся один.", "puzzle"));

            AddChoice(2, new Choice("Что вы делаете здесь, в подвале?", "q1"));
            AddChoice(2, new Choice("Как нам вернуться в своё время?", "q2"));
            AddChoice(2, new Choice("Зачем вам всё это?", "q3"));
            AddChoice(2, new Choice("Почему вы ждали именно нас?", "q4"));

            AddChoice(3, new Choice("Согласиться помогать Фалексу.", "agree"));
            AddChoice(3, new Choice("Отказаться и действовать самостоятельно.", "endR"));

            AddChoice(4, new Choice("Немедленно отступать, оставив компонент.", "endD"));

            AddChoice(5, new Choice("Прорываться к ящику и забирать компонент.", "endG"));
        }

    
        public void AddChoice(int sceneIndex, Choice choice)
        {
            if (!_sceneChoices.TryGetValue(sceneIndex, out var list))
            {
                list = new List<Choice>();
                _sceneChoices[sceneIndex] = list;
            }
            list.Add(choice);
        }

       
        public List<Choice> GetChoices(int sceneIndex)
        {
            return _sceneChoices.TryGetValue(sceneIndex, out var list)
                ? list
                : new List<Choice>();
        }

        public void SetChoice(string idTyping)
        {
            _lastResult = idTyping;
        }

        public string GetResult()
        {
            return _lastResult;
        }
    }
}
