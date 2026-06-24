using project.DialogMnager.бурмаджа_.dialogue;
using project.MiniGame3.HelpClass;
using project.MiniGame3.Skip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.MiniGame3
{
    public partial class ChoicesMiniGameForm : Form
    {
        private SkipDialogs skipd = new SkipDialogs();
        private ChoicesResultMG resMG = new ChoicesResultMG("skip", 2);
        private List<DialogesChoicesMG> currentDialogList;
        private int currentDialogIndex = 0;
        private bool isDialogPlaying = false;
        private bool isWaitingForEnter = false;
        private string currentFullText = "";
        private int currentSprites = -1;
        private int currentBackground = -1;

        private List<Image> SpritesImages;
        private List <Image> BackgroundImages;

        public ChoicesMiniGameForm()
        {
            InitializeComponent();
            LoadImagesToList();
            panel1.Parent = pictureBox1;
            label1.Parent = pictureBox1;
            panel2.Parent = pictureBox1;
            pictureBox2.Parent = pictureBox1;

            panel1.Visible = false;
            label1.Visible = false;
            pictureBox2.Visible = false;
            panel2.Visible = true;

            skipd.OnTextUpdated += (text) =>
            {
                if (label2.InvokeRequired)
                    label2.Invoke(new Action(() => label2.Text = text));
                else
                    label2.Text = text;
            };

            skipd.OnTypingComplete += () =>
            {
                isWaitingForEnter = true;

            };

            timer1.Start();
            this.KeyDown += ChoicesMiniGameForm_KeyDown;
            this.Select();
        }

        private void LoadImagesToList()
        {
            SpritesImages = new List<Image>
            {
                Properties.Resources.EmmaSp5,
                Properties.Resources.EmmaSp2,
                Properties.Resources.DianaSp1,
                Properties.Resources.DianaSp7,
                Properties.Resources.RonyaSp3,
            };

            BackgroundImages = new List<Image>
            {
                Properties.Resources.BackgroundMiniGame3
            };
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                HandleEnterPress();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ChoicesMiniGameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandleEnterPress();
                e.Handled = true;
                e.SuppressKeyPress = true;
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

            if (isWaitingForEnter)
            {
                isWaitingForEnter = false;
                if (label2.InvokeRequired)
                    label2.Invoke(new Action(() =>
                    {
                        string currentText = label2.Text;
                        label2.Text = currentText;
                    }));
                else
                {
                    string currentText = label2.Text;
                    label2.Text = currentText;
                }
                ShowNextDialog();
            }
        }

        private void ShowNextDialog()
        {
            if (currentDialogIndex < currentDialogList.Count)
            {
                var dialog = currentDialogList[currentDialogIndex];
                if (dialog.Name == "Автор")
                {
                    pictureBox2.Visible = false;
                    if (label1.InvokeRequired)
                        label1.Invoke(new Action(() => label1.Visible = false));
                    else
                        label1.Visible = false;
                }
                else
                {
                    pictureBox2.Visible = true;
                    if (label1.InvokeRequired)
                        label1.Invoke(new Action(() =>
                        {
                            label1.Visible = true;
                            label1.Text = dialog.Name;
                        }));
                    else
                    {
                        label1.Visible = true;
                        label1.Text = dialog.Name;
                    }
                }
                if (dialog.indIm != currentSprites)
                {
                    currentSprites = dialog.indIm;
                    if (pictureBox2.InvokeRequired)
                    {
                        pictureBox2.Invoke(new Action(() =>
                        {
                            if (dialog.indIm >= 0 && dialog.indIm < SpritesImages.Count)
                                pictureBox2.Image = SpritesImages[dialog.indIm];
                        }));
                    }
                    else
                    {
                        if (dialog.indIm >= 0 && dialog.indIm < SpritesImages.Count)
                            pictureBox2.Image = SpritesImages[dialog.indIm];
                    }
                }

                if (dialog.intImB != currentBackground) 
                {
                    currentBackground = dialog.intImB;
                    if (pictureBox1.InvokeRequired)
                    {
                        pictureBox1.Invoke(new Action(() =>
                        {
                            if (dialog.intImB >= 0 && dialog.intImB < BackgroundImages.Count)
                                pictureBox1.Image = BackgroundImages[dialog.intImB];
                        }));
                    }
                    else
                    {
                        if (dialog.intImB >= 0 && dialog.intImB < BackgroundImages.Count)
                            pictureBox1.Image = BackgroundImages[dialog.intImB];
                    }
                }

                if (label2.InvokeRequired)
                    label2.Invoke(new Action(() => label2.Text = ""));
                else
                    label2.Text = "";

                currentFullText = "";

                skipd.StartTyping(dialog.Text, 30);
                currentDialogIndex++;
                isWaitingForEnter = false;
            }
            else
            {
                if (resMG.GetResult())
                {
                    this.Close();
                    return;
                }
                if (panel1.InvokeRequired)
                    panel1.Invoke(new Action(() =>
                    {
                        panel1.Visible = false;
                        label1.Visible = false;
                        pictureBox2.Visible = false;
                        pictureBox1.Image = Properties.Resources.BackgroundMiniGame3;
                        panel2.Visible = true;
                        
                    }));
                else
                {
                    panel1.Visible = false;
                    label1.Visible = false;
                    pictureBox2.Visible = false;
                    pictureBox1.Image = Properties.Resources.BackgroundMiniGame3;
                    panel2.Visible = true;
                }

                isDialogPlaying = false;
                isWaitingForEnter = false;
                currentFullText = "";

                this.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isDialogPlaying) return;

            currentDialogList = resMG.ChoiceA();
            currentDialogIndex = 0;
            isDialogPlaying = true;
            currentSprites = -1;
            currentBackground = -1;

            panel2.Visible = false;
            panel1.Visible = true;
            label1.Visible = true;
            pictureBox2.Visible = true;
            label2.Text = "";
            currentFullText = "";

            ShowNextDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isDialogPlaying) return;

            currentDialogList = resMG.ChoiceB();
            currentDialogIndex = 0;
            isDialogPlaying = true;
            currentSprites = -1;
            currentBackground = -1;

            panel2.Visible = false;
            panel1.Visible = true;
            label1.Visible = true;
            pictureBox2.Visible = true;
            label2.Text = "";
            currentFullText = "";

            ShowNextDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isDialogPlaying) return;

            currentDialogList = resMG.ChoiceC();
            currentDialogIndex = 0;
            isDialogPlaying = true;
            currentSprites = -1;
            currentBackground = -1;

            panel2.Visible = false;
            panel1.Visible = true;
            label1.Visible = true;
            pictureBox2.Visible = true;
            label2.Text = "";
            currentFullText = "";

            ShowNextDialog();
        }
    }
}
