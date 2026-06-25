using project.DialogMnager.бурмаджа_.dialogue;
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
            // Сцена 0: после a88
            AddChoice(0, new Choice("Эмма поворачивает стрелки.", "a1"));
            AddChoice(0, new Choice("Эмма закрывает шкатулку.", "b1"));

            // Сцена 1: после b26 / lineB32
            AddChoice(1, new Choice("Эмма остаётся с пациентом.", "endF"));
            AddChoice(1, new Choice("Эмма идёт с девочками.", "puzzle"));

            // Сцена 2: вопросы Фалексу, после z45
            AddChoice(2, new Choice("Что вы делаете здесь, в подвале?", "q1"));
            AddChoice(2, new Choice("Как нам вернуться в своё время?", "q2"));
            AddChoice(2, new Choice("Зачем вам всё это?", "q3"));
            AddChoice(2, new Choice("Почему вы ждали именно нас?", "q4"));

            // Сцена 3: после z49
            AddChoice(3, new Choice("Согласиться помогать Фалексу.", "agree"));
            AddChoice(3, new Choice("Отказаться и действовать самостоятельно.", "endR"));

            // Сцена 4: после c19
            AddChoice(4, new Choice("Немедленно отступать, оставив компонент.", "endD"));
            AddChoice(4, new Choice("Прорываться к ящику и забирать компонент.", "endG"));
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

        public void SetChoice(string idTyping) { _lastResult = idTyping; }
        public string GetResult() { return _lastResult; }
    }
}