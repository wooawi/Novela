using project.Choices;
using project.DialogMnager.бурмаджа_.dialogue;
using project.EndingManager.EndingUsee;
using project.MiniGame3;
using project.MiniGame3.HelpClass;
using project.MiniGame3.Skip;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace project
{
    public partial class MainFormGame : Form
    {
        System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
        private SkipDialogs skipd = new SkipDialogs();
        private BaseText bt = new BaseText();
        private List<DialogueLine> currentDialogList;
        private int currentDialogIndex = 0;
        private int currentDialogListIndex = 0;
        private bool isDialogPlaying = false;
        private bool isWaitingForEnter = false;
        private string currentFullText = "";
        private int currentSprites = -1;
        private int currentBackground = -1;
        private Image currentBackgroundImage;

        private bool skipStepRequested = false;
        private bool isAutoSkipping = false;
        private System.Windows.Forms.Timer skipTimer;

        private List<Image> SpritesImages;
        private List<Image> BackgroundImages;

        private ChoiceManager choiceManager = new ChoiceManager();
        private int pendingChoiceSceneIndex = -1;

        EndingUse eu = new EndingUse();

        private int activeSaveSlot = 1;
        private bool isChoiceActive = false;


        private readonly Dictionary<string, int> choiceTargetList = new()
        {
            { "a1", 1 },     // повернула стрелки -> список 1 (b...)
            { "b1", 10 },    // отказалась -> список 10 (lineB...)

            { "puzzle", 3 }, // все идут в церковь -> список 3 (z...)

            { "agree", 8 },  // согласиться помогать Фалексу -> список 8 (c...)
            { "endR", 9 },   // отказаться -> список 9 (dEndR...)

            { "lineB", 11 }, // -> список 11 (endD...)
            { "endG", 12 },  // -> список 12 (endG...)
        };

        private readonly Dictionary<string, int> questionAnswerList = new()
        {
            { "q1", 4 },
            { "q2", 5 },
            { "q3", 6 },
            { "q4", 7 },
        };

        private int returnListIndex = -1;
        private int returnLineIndex = -1;

        private void ToggleAutoSkip()
        {
            isAutoSkipping = !isAutoSkipping;

            if (isAutoSkipping)
            {
                label4.Text = "||";
                skipTimer.Start();
            }
            else
            {
                StopSkip();
            }
        }

        private void SkipTimer_Tick(object sender, EventArgs e)
        {
            if (!isAutoSkipping)
            {
                skipTimer.Stop();
                return;
            }

            if (panel2.Visible)
            {
                StopSkip();
                return;
            }

            if (skipd.IsTyping())
            {
                skipd.Skip();
                return;
            }

            skipStepRequested = true;
            ProcessSkipStep();
        }

        private void StopSkip()
        {
            isAutoSkipping = false;
            skipTimer.Stop();
            label4.Text = ">";
        }

        private void ProcessSkipStep()
        {
            if (!skipStepRequested) return;
            skipStepRequested = false;

            if (panel2.Visible)
            {
                StopSkip();
                return;
            }

            if (!isDialogPlaying)
            {
                StopSkip();
                return;
            }

            ShowNextDialog();
        }

        public MainFormGame()
        {
            InitializeComponent();
            LoadImagesToList();
            panel2.BringToFront();
            panel1.BringToFront();
            panel1.Parent = pictureBox1;
            label1.Parent = pictureBox1;
            pictureBox2.Parent = pictureBox1;

            panel1.Visible = true;
            label1.Visible = true;
            pictureBox2.Visible = true;
            panel2.Visible = false;

            label4.Click += (s, e) => ToggleAutoSkip();

            skipd.OnTextUpdated += (text) =>
            {
                if (label2.InvokeRequired) label2.Invoke(new Action(() => label2.Text = text));
                else label2.Text = text;
            };

            skipd.OnTypingComplete += () => { isWaitingForEnter = true; };

            skipTimer = new System.Windows.Forms.Timer();
            skipTimer.Interval = 1;
            skipTimer.Tick += SkipTimer_Tick;

            button3.Click += (s, e) => HandleChoiceButton(2);
            button4.Click += (s, e) => HandleChoiceButton(1);
            button5.Click += (s, e) => HandleChoiceButton(0);
            button9.Click += (s, e) => HandleChoiceButton(3);

            button6.Text = "💾 Слот 1";
            button7.Text = "💾 Слот 2";
            button8.Text = "💾 Слот 3";

            UpdateSaveButtonsDisplay();

            timer1.Start();
            this.Select();
            
        }

        private void button6_Click(object sender, EventArgs e) => ToggleSaveLoad(1);
        private void button7_Click(object sender, EventArgs e) => ToggleSaveLoad(2);
        private void button8_Click(object sender, EventArgs e) => ToggleSaveLoad(3);

        private void ToggleSaveLoad(int slot)
        {
            if (SaveSystem.Exists(slot)) QuickLoad(slot);
            else QuickSave(slot);
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) QuickLoad(1);
        }
        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) QuickLoad(2);
        }
        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) QuickLoad(3);
        }

        private void MainFormGame_Shown(object sender, EventArgs e)
        {
            if (SaveSystem.Exists(1))
            {
                var ask = MessageBox.Show(
                    "Найдено сохранение. Продолжить?",
                    "Загрузить игру",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (ask == DialogResult.Yes) { LoadGame(1); return; }
            }
            StartNewGame();
        }

        private void StartNewGame()
        {
            richTextBox1.Text = "";
            currentDialogListIndex = 0;
            currentDialogList = bt.GetDialog(0);
            currentDialogIndex = 0;
            isDialogPlaying = true;
            currentSprites = -1;
            currentBackground = -1;
            currentFullText = "";
            ShowNextDialog();
        }

        public bool SaveGame(int slot = 1)
        {
            var data = new Save
            {
                DialogListIndex = currentDialogListIndex,
                DialogueIndex = currentDialogIndex,
                CurrentSprites = currentSprites,
                CurrentBackground = currentBackground,
                HistoryText = richTextBox1.Text,
                UnlockedEndings = eu.GetUnlockedList(),
                SaveTime = DateTime.Now
            };

            bool ok = SaveSystem.SaveToSlot(data, slot);
            if (ok) { activeSaveSlot = slot; ShowSaveToast(); }
            return ok;
        }

        public bool LoadGame(int slot = 1)
        {
            var data = SaveSystem.Load(slot);
            if (data == null) return false;

            activeSaveSlot = slot;
            currentDialogListIndex = data.DialogListIndex;
            currentDialogIndex = data.DialogueIndex;
            currentSprites = data.CurrentSprites;
            currentBackground = data.CurrentBackground;
            richTextBox1.Text = data.HistoryText;

            if (data.UnlockedEndings != null)
                foreach (var en in data.UnlockedEndings) eu.Unlocking(en);

            if (currentBackground >= 0 && currentBackground < BackgroundImages.Count)
            {
                pictureBox1.Image = BackgroundImages[currentBackground];
                currentBackgroundImage = BackgroundImages[currentBackground];
            }
            if (currentSprites >= 0 && currentSprites < SpritesImages.Count)
                pictureBox2.Image = SpritesImages[currentSprites];

            currentDialogList = bt.GetDialog(currentDialogListIndex);
            isDialogPlaying = true;
            isWaitingForEnter = false;

            panel1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            panel2.Visible = false;

            ShowNextDialog();
            return true;
        }

        private void ShowSaveToast()
        {
            var l = new Label
            {
                Text = "✔ Сохранено",
                ForeColor = Color.LimeGreen,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            tabPage1.Controls.Add(l);
            l.BringToFront();

            var t = new System.Windows.Forms.Timer { Interval = 2000 };
            t.Tick += (s, e) => { t.Stop(); tabPage1.Controls.Remove(l); l.Dispose(); };
            t.Start();
        }

        private void QuickSave(int slot)
        {
            if (SaveGame(slot))
            {
                activeSaveSlot = slot;
                UpdateSaveButtonsDisplay();
                ShowQuickSaveToast(slot);
            }
        }

        private void QuickLoad(int slot)
        {
            if (SaveSystem.Exists(slot))
            {
                if (LoadGame(slot))
                {
                    UpdateSaveButtonsDisplay();
                    ShowQuickLoadToast(slot);
                }
            }
            else
            {
                MessageBox.Show($"Сохранение в слоте {slot} не существует!", "Ошибка загрузки");
            }
        }

        private void UpdateSaveButtonsDisplay()
        {
            button6.BackColor = activeSaveSlot == 1 ? Color.LimeGreen : SystemColors.Control;
            button7.BackColor = activeSaveSlot == 2 ? Color.LimeGreen : SystemColors.Control;
            button8.BackColor = activeSaveSlot == 3 ? Color.LimeGreen : SystemColors.Control;
        }

        private void ShowQuickSaveToast(int slot)
        {
            var l = new Label
            {
                Text = $"✔ Сохранено в слот {slot}",
                ForeColor = Color.LimeGreen,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 40)
            };
            tabPage1.Controls.Add(l);
            l.BringToFront();

            var t = new System.Windows.Forms.Timer { Interval = 1500 };
            t.Tick += (s, e) => { t.Stop(); tabPage1.Controls.Remove(l); l.Dispose(); };
            t.Start();
        }

        private void ShowQuickLoadToast(int slot)
        {
            var l = new Label
            {
                Text = $"⟳ Загружено из слота {slot}",
                ForeColor = Color.DeepSkyBlue,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 40)
            };
            tabPage1.Controls.Add(l);
            l.BringToFront();

            var t = new System.Windows.Forms.Timer { Interval = 1500 };
            t.Tick += (s, e) => { t.Stop(); tabPage1.Controls.Remove(l); l.Dispose(); };
            t.Start();
        }

        public void GoToDialogList(int index)
        {
            currentDialogListIndex = index;
            currentDialogList = bt.GetDialog(index);
            currentDialogIndex = 0;
            isDialogPlaying = true;
            isWaitingForEnter = false;

            panel1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            pictureBox2.Visible = true;
            panel2.Visible = false;

            ShowNextDialog();
        }
        private void GoToDialogList(int index, int startLineIndex)
        {
            currentDialogListIndex = index;
            currentDialogList = bt.GetDialog(index);
            currentDialogIndex = Math.Max(startLineIndex, 0);
            isDialogPlaying = true;
            isWaitingForEnter = false;

            panel1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            pictureBox2.Visible = true;
            panel2.Visible = false;

            ShowNextDialog();
        }

        public void ShowChoices(int sceneIndex)
        {
            isAutoSkipping = false;
            label4.Text = ">";
            panel2.Visible = true;
            panel2.BringToFront();
            var choices = choiceManager.GetChoices(sceneIndex);
            if (choices.Count == 0) return;

            pendingChoiceSceneIndex = sceneIndex;
            isChoiceActive = true;

            button5.Text = choices.Count > 0 ? choices[0].Text : "";
            button4.Text = choices.Count > 1 ? choices[1].Text : "";
            button3.Text = choices.Count > 2 ? choices[2].Text : "";
            button9.Text = choices.Count > 3 ? choices[3].Text : "";

            button5.Visible = choices.Count > 0;
            button4.Visible = choices.Count > 1;
            button3.Visible = choices.Count > 2;
            button9.Visible = choices.Count > 3;

            panel1.Visible = true;
            label1.Visible = true;
            pictureBox2.Visible = true;
            panel2.Visible = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button9.Enabled = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button9.Visible = true;
        }

        private void HandleChoiceButton(int buttonIndex)
        {
            if (!isChoiceActive) return;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button9.Enabled = false;
            if (pendingChoiceSceneIndex < 0) return;

            var choices = choiceManager.GetChoices(pendingChoiceSceneIndex);
            if (buttonIndex >= choices.Count) return;

            var chosen = choices[buttonIndex];
            choiceManager.SetChoice(chosen.IdTyping);

            isChoiceActive = false;
            pendingChoiceSceneIndex = -1;

            panel2.Visible = false;

            
            var saveData = SaveSystem.Load(activeSaveSlot) ?? new Save();
            saveData.ChoicesMade.Add(chosen.IdTyping);
            SaveSystem.SaveToSlot(saveData, activeSaveSlot);

            
            if (chosen.IdTyping == "puzzle")
            {
                StartMiniGame(1);
                return;
            }

            if (chosen.IdTyping == "agree")
            {
                StartMiniGame(2);
                return;
            }

            if (chosen.IdTyping == "lineB")
            {
                StartMiniGame(3);
                return;
            }

            
            if (chosen.IdTyping == "endF")
            {
                new SavingGame().ShowDialog();
                GoToDialogList(2);
                return;
            }

            
            if (questionAnswerList.TryGetValue(chosen.IdTyping, out int answerList))
            {
                GoToDialogList(answerList);
                return;
            }

            if (choiceTargetList.TryGetValue(chosen.IdTyping, out int targetList))
            {
                GoToDialogList(targetList);
                return;
            }

            // fallback
            ShowNextDialog();
        }

        private void LoadImagesToList()
        {
            SpritesImages = new List<Image>
            {
                Properties.Resources.EmmaSp1,
                Properties.Resources.EmmaSp2,
                Properties.Resources.EmmaSp3,
                Properties.Resources.EmmaSp4,
                Properties.Resources.EmmaSp5,
                Properties.Resources.EmmaSp6,
                Properties.Resources.DianaSp1,
                Properties.Resources.DianaSp2,
                Properties.Resources.DianaSp3,
                Properties.Resources.DianaSp4,
                Properties.Resources.DianaSp5,
                Properties.Resources.DianaSp6,
                Properties.Resources.DianaSp7,
                Properties.Resources.DianaSp8,
                Properties.Resources.RonyaSp1,
                Properties.Resources.RonyaSp2,
                Properties.Resources.RonyaSp3,
                Properties.Resources.RonyaSp4,
                Properties.Resources.RonyaSp5,
                Properties.Resources.RonyaSp6,
                Properties.Resources.RonyaSp7,
                Properties.Resources.DinaraSp1,
                Properties.Resources.DinaraSp2,
                Properties.Resources.DinaraSp3,
                Properties.Resources.DinaraSp4,
                Properties.Resources.DinaraSp5,
                Properties.Resources.DinaraSp6,
                Properties.Resources.DinaraSp7,
                Properties.Resources.FalexSp1,
                Properties.Resources.FalexSp2,
                Properties.Resources.FalexSp3,
                Properties.Resources.FalexSp4,
                Properties.Resources.FalexSp5,
                Properties.Resources.FalexSp6,
                Properties.Resources.FalexSp7
            };

            BackgroundImages = new List<Image>
            {
                Properties.Resources.Basement,
                Properties.Resources.Church,
                Properties.Resources.ChurchWarehouse,
                Properties.Resources.ChurchBasement,
                Properties.Resources.ChernobylAES,
                Properties.Resources.Road,
                Properties.Resources.FutureBasement,
                Properties.Resources.FutureChurch
            };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter) { HandleEnterPress(); return true; }
            if (keyData == (Keys.Control | Keys.S)) { SaveGame(activeSaveSlot); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainFormGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandleEnterPress();
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void HandleEnterPress()
        {
            if (skipd.IsTyping())
            {
                skipd.Skip();
                currentFullText = skipd.GetFullText();
                isWaitingForEnter = true;
                return;
            }

            if (!isWaitingForEnter) return;
            isWaitingForEnter = false;

            if (currentDialogIndex > 0 && currentDialogIndex <= currentDialogList.Count)
            {
                var justShown = currentDialogList[currentDialogIndex - 1];

                if (justShown.ChoiceScene >= 0)
                {
                    ShowChoices(justShown.ChoiceScene);
                    return;
                }
                if (justShown.MiniGameId >= 0)
                {
                    StartMiniGame(justShown.MiniGameId);
                    return;
                }
            }

            ShowNextDialog();
        }

        private void StartMiniGame(int id)
        {
            Form miniGame = null;

            switch (id)
            {
                case 1:
                    miniGame = new ReactGame();
                    break;

                case 2:
                    miniGame = new SavingGame();
                    break;

                case 3:
                    miniGame = new ChoicesMiniGameForm();
                    break;
            }

            if (miniGame != null)
            {
                var result = miniGame.ShowDialog();

                
                if (result == DialogResult.OK)
                {
                    ShowNextDialog();
                }
            }
        }
        private void ShowNextDialog()
        {
            if (currentDialogIndex < currentDialogList.Count)
            {
                var dialog = currentDialogList[currentDialogIndex];

                // ПРОВЕРКА: если у текущей реплики есть ChoiceScene, показываем выборы ДО отображения текста
                if (dialog.ChoiceScene >= 0)
                {
                    // Сохраняем текущую реплику в историю без текста (или с текстом)
                    richTextBox1.Text += dialog.Name == "Автор"
                        ? dialog.Text + "\n\n"
                        : dialog.Name + ":  " + dialog.Text + "\n\n";

                    // Обновляем спрайты и фон
                    UpdateSpritesAndBackground(dialog);

                    // Показываем выборы
                    ShowChoices(dialog.ChoiceScene);
                    currentDialogIndex++; // Переходим к следующей реплике
                    return;
                }

                // Обычный диалог
                richTextBox1.Text += dialog.Name == "Автор"
                    ? dialog.Text + "\n\n"
                    : dialog.Name + ":  " + dialog.Text + "\n\n";

                if (dialog.Name == "Автор")
                {
                    pictureBox2.Visible = false;
                    label1.Visible = false;
                }
                else
                {
                    pictureBox2.Visible = true;
                    label1.Visible = true;
                    label1.Text = dialog.Name;
                }

                if (dialog.indIm != currentSprites)
                {
                    currentSprites = dialog.indIm;
                    if (dialog.indIm >= 0 && dialog.indIm < SpritesImages.Count)
                        pictureBox2.Image = SpritesImages[dialog.indIm];
                }

                if (dialog.intImB != currentBackground)
                {
                    currentBackground = dialog.intImB;
                    if (dialog.intImB >= 0 && dialog.intImB < BackgroundImages.Count)
                    {
                        pictureBox1.Image = BackgroundImages[dialog.intImB];
                        currentBackgroundImage = BackgroundImages[dialog.intImB];
                    }
                }

                label2.Text = "";
                currentFullText = "";
                skipd.StartTyping(dialog.Text, 30);
                currentDialogIndex++;
                isWaitingForEnter = false;

                if (currentDialogIndex % 10 == 0)
                    SaveGame(activeSaveSlot);
            }
            else
            {
                // Конец диалога
                panel1.Visible = false;
                label1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox1.Image = currentBackgroundImage;

                isDialogPlaying = false;
                isWaitingForEnter = false;
                currentFullText = "";

                SaveGame(activeSaveSlot);
                this.Select();
            }
        }

        // Вспомогательный метод для обновления спрайтов и фона
        private void UpdateSpritesAndBackground(DialogueLine dialog)
        {
            if (dialog.indIm != currentSprites)
            {
                currentSprites = dialog.indIm;
                if (dialog.indIm >= 0 && dialog.indIm < SpritesImages.Count)
                    pictureBox2.Image = SpritesImages[dialog.indIm];
            }

            if (dialog.intImB != currentBackground)
            {
                currentBackground = dialog.intImB;
                if (dialog.intImB >= 0 && dialog.intImB < BackgroundImages.Count)
                {
                    pictureBox1.Image = BackgroundImages[dialog.intImB];
                    currentBackgroundImage = BackgroundImages[dialog.intImB];
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) => EndingsLabel2.Text = eu.ShowAll();
        private void button2_Click(object sender, EventArgs e) => EndingsLabel2.Text = eu.ShowAllUnlocked();

        public void Unlocking(string a)
        {
            eu.Unlocking(a);
            SaveGame(activeSaveSlot);
        }

        


    }
}