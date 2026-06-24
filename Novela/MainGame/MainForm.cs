using project.DialogMnager.бурмаджа_.dialogue;
using project.EndingManager.EndingUsee;
using project.MiniGame3.HelpClass;
using project.MiniGame3.Skip;
using project.Choices;           
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace project
{
    public partial class MainFormGame : Form
    {
        
        private SkipDialogs skipd = new SkipDialogs();
        private BaseText bt = new BaseText();
        private List<DialogueLine> currentDialogList;
        private int currentDialogIndex    = 0;
        private int currentDialogListIndex = 0;   
        private bool isDialogPlaying     = false;
        private bool isWaitingForEnter   = false;
        private string currentFullText   = "";
        private int currentSprites       = -1;
        private int currentBackground    = -1;
        private Image currentBackgroundImage;

        
        private List<Image> SpritesImages;
        private List<Image> BackgroundImages;

       
        private ChoiceManager choiceManager = new ChoiceManager();
        private int pendingChoiceSceneIndex = -1;  
        
        EndingUse eu = new EndingUse();

       
        private int activeSaveSlot = 1;

        
        public MainFormGame()
        {
            InitializeComponent();
            LoadImagesToList();

            panel1.Parent = pictureBox1;
            label1.Parent = pictureBox1;
            pictureBox2.Parent = pictureBox1;

            panel1.Visible  = true;
            label1.Visible  = true;
            pictureBox2.Visible = true;
            panel2.Visible  = false;   

            skipd.OnTextUpdated += (text) =>
            {
                if (label2.InvokeRequired) label2.Invoke(new Action(() => label2.Text = text));
                else label2.Text = text;
            };

            skipd.OnTypingComplete += () => { isWaitingForEnter = true; };

            
            button3.Click += (s, e) => HandleChoiceButton(0);
            button4.Click += (s, e) => HandleChoiceButton(1);
            button5.Click += (s, e) => HandleChoiceButton(2);

            timer1.Start();
            this.KeyDown += MainFormGame_KeyDown;
            this.Shown   += MainFormGame_Shown;
            this.Select();
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
            richTextBox1.Text       = "";
            currentDialogListIndex  = 0;
            currentDialogList       = bt.GetDialog(0);
            currentDialogIndex      = 0;
            isDialogPlaying         = true;
            currentSprites          = -1;
            currentBackground       = -1;
            currentFullText         = "";
            ShowNextDialog();
        }

        
        public bool SaveGame(int slot = 1)
        {
            var data = new Save
            {
                DialogListIndex   = currentDialogListIndex,
                DialogueIndex     = currentDialogIndex,
                CurrentSprites    = currentSprites,
                CurrentBackground = currentBackground,
                HistoryText       = richTextBox1.Text,
                UnlockedEndings   = eu.GetUnlockedList(),
                SaveTime          = DateTime.Now
            };

            bool ok = SaveSystem.SaveToSlot(data, slot);
            if (ok) { activeSaveSlot = slot; ShowSaveToast(); }
            return ok;
        }

        public bool LoadGame(int slot = 1)
        {
            var data = SaveSystem.Load(slot);
            if (data == null) return false;

            activeSaveSlot         = slot;
            currentDialogListIndex = data.DialogListIndex;
            currentDialogIndex     = data.DialogueIndex;
            currentSprites         = data.CurrentSprites;
            currentBackground      = data.CurrentBackground;
            richTextBox1.Text      = data.HistoryText;

            if (data.UnlockedEndings != null)
                foreach (var en in data.UnlockedEndings) eu.Unlocking(en);

            
            if (currentBackground >= 0 && currentBackground < BackgroundImages.Count)
            {
                pictureBox1.Image      = BackgroundImages[currentBackground];
                currentBackgroundImage = BackgroundImages[currentBackground];
            }
            if (currentSprites >= 0 && currentSprites < SpritesImages.Count)
                pictureBox2.Image = SpritesImages[currentSprites];

            currentDialogList = bt.GetDialog(currentDialogListIndex);
            isDialogPlaying   = true;
            isWaitingForEnter = false;

            panel1.Visible  = true;
            label1.Visible  = true;
            label2.Visible  = true;

            ShowNextDialog();
            return true;
        }

        private void ShowSaveToast()
        {
            var lbl = new Label
            {
                Text      = "✔ Сохранено",
                ForeColor = Color.LimeGreen,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Font      = new Font("Segoe UI", 12f, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(10, 10)
            };
            tabPage1.Controls.Add(lbl);
            lbl.BringToFront();

            var t = new System.Windows.Forms.Timer { Interval = 2000 };
            t.Tick += (s, e) => { t.Stop(); tabPage1.Controls.Remove(lbl); lbl.Dispose(); };
            t.Start();
        }

        
        public void GoToDialogList(int index)
        {
            currentDialogListIndex = index;
            currentDialogList      = bt.GetDialog(index);
            currentDialogIndex     = 0;
            isDialogPlaying        = true;
            isWaitingForEnter      = false;

            panel1.Visible  = true;
            label1.Visible  = true;
            label2.Visible  = true;
            panel2.Visible  = false;

            ShowNextDialog();
        }

        
        public void ShowChoices(int sceneIndex)
        {
            var choices = choiceManager.GetChoices(sceneIndex);
            if (choices.Count == 0) return;

            pendingChoiceSceneIndex = sceneIndex;

            button5.Text    = choices.Count > 0 ? choices[0].Text : "";
            button5.Visible = choices.Count > 0;
            button4.Text    = choices.Count > 1 ? choices[1].Text : "";
            button4.Visible = choices.Count > 1;
            button3.Text    = choices.Count > 2 ? choices[2].Text : "";
            button3.Visible = choices.Count > 2;

            
            panel1.Visible  = false;
            label1.Visible  = false;
            pictureBox2.Visible = false;
            panel2.Visible  = true;
        }

        private void HandleChoiceButton(int buttonIndex)
        {
            if (pendingChoiceSceneIndex < 0) return;

            var choices = choiceManager.GetChoices(pendingChoiceSceneIndex);
            if (buttonIndex >= choices.Count) return;

            var chosen = choices[buttonIndex];
            choiceManager.SetChoice(chosen.IdTyping);

            
            var saveData = SaveSystem.Load(activeSaveSlot) ?? new Save();
            saveData.ChoicesMade.Add(chosen.IdTyping);
            SaveSystem.SaveToSlot(saveData, activeSaveSlot);

            panel2.Visible = false;
            pendingChoiceSceneIndex = -1;


            string result = choiceManager.GetResult();

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
            if (keyData == Keys.Enter)          { HandleEnterPress(); return true; }
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
                currentFullText   = skipd.GetFullText();
                isWaitingForEnter = true;
                return;
            }
            if (isWaitingForEnter)
            {
                isWaitingForEnter = false;
                ShowNextDialog();
            }
        }

        private void ShowNextDialog()
        {
            if (currentDialogIndex < currentDialogList.Count)
            {
                var dialog = currentDialogList[currentDialogIndex];
                richTextBox1.Text += dialog.Name == "Автор"
                    ? dialog.Text + "\n\n"
                    : dialog.Name + ":  " + dialog.Text + "\n\n";

                if (dialog.Name == "Автор")
                {
                    pictureBox2.Visible = false;
                    label1.Visible      = false;
                }
                else
                {
                    pictureBox2.Visible = true;
                    label1.Visible      = true;
                    label1.Text         = dialog.Name;
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
                        pictureBox1.Image      = BackgroundImages[dialog.intImB];
                        currentBackgroundImage = BackgroundImages[dialog.intImB];
                    }
                }

                label2.Text     = "";
                currentFullText = "";
                skipd.StartTyping(dialog.Text, 30);
                currentDialogIndex++;
                isWaitingForEnter = false;

                
                if (currentDialogIndex % 10 == 0)
                    SaveGame(activeSaveSlot);
            }
            else
            {
                
                panel1.Visible      = false;
                label1.Visible      = false;
                pictureBox2.Visible = false;
                pictureBox1.Image   = currentBackgroundImage;

                isDialogPlaying   = false;
                isWaitingForEnter = false;
                currentFullText   = "";

                SaveGame(activeSaveSlot);   

                

                this.Select();
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
