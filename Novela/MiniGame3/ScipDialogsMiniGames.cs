using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.MiniGame3.Skip
{
    public class SkipDialogs
    {
        private System.Windows.Forms.Timer timerTyping = new System.Windows.Forms.Timer();
        private string fullText;
        private string currentText;
        private int currentIndex;
        private bool isTyping;
        private bool skipTyping;
        private bool isWaitingForEnter;
        private TaskCompletionSource<bool> enterTcs;

        public event Action<string> OnTextUpdated;
        public event Action OnTypingComplete;

        public SkipDialogs()
        {
            timerTyping.Tick += TimerTyping_Tick;
            isTyping = false;
            skipTyping = false;
            isWaitingForEnter = false;
            currentText = "";
        }

        public void StartTyping(string text, int delayMs)
        {
            fullText = text;
            currentText = "";
            currentIndex = 0;
            isTyping = true;
            skipTyping = false;

            timerTyping.Interval = delayMs;
            timerTyping.Start();
            OnTextUpdated?.Invoke(currentText);
        }
        public void Skip()
        {
            if (isTyping)
            {
                skipTyping = true;
                currentText = fullText;
                timerTyping.Stop();
                isTyping = false;
                OnTextUpdated?.Invoke(currentText);
                OnTypingComplete?.Invoke();
            }
        }

        public async Task WaitForEnterAsync()
        {
            isWaitingForEnter = true;
            enterTcs = new TaskCompletionSource<bool>();
            await enterTcs.Task;
            isWaitingForEnter = false;
        }

        public void OnEnterPressed()
        {
            if (isWaitingForEnter)
            {
                enterTcs?.TrySetResult(true);
            }
        }

        private void TimerTyping_Tick(object sender, EventArgs e)
        {
            if (skipTyping)
            {
                currentText = fullText;
                timerTyping.Stop();
                isTyping = false;
                OnTextUpdated?.Invoke(currentText);
                OnTypingComplete?.Invoke();
                return;
            }

            if (currentIndex < fullText.Length)
            {
                currentText += fullText[currentIndex];
                currentIndex++;
                OnTextUpdated?.Invoke(currentText);
            }
            else
            {
                timerTyping.Stop();
                isTyping = false;
                OnTypingComplete?.Invoke();
            }
        }

        public string GetFullText()
        {
            return fullText;
        }

        public bool IsTyping() => isTyping;
    }
}
