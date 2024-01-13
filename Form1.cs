﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Timers;

namespace KTANE_Bot
{
    public partial class Form1 : Form
    {
        private KTANE_Speech _ktaneSpeech;
        public static bool ResetBomb { get; private set; }   
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize speech recognition.
            _ktaneSpeech = new KTANE_Speech();
            _ktaneSpeech.RecognitionEngine.SpeechRecognized += DefaultSpeechRecognized;

            int currentVoiceIndex = 0;
            int targetVoice = 0;
            foreach (var voice in _ktaneSpeech.GetAllVoices())
            {
                comboBoxVoices.Items.Add(voice);
                   
                if(voice.Contains("David")) // Focus on David, he's the best... or at least not worst
                {
                    targetVoice = currentVoiceIndex;
                }

                currentVoiceIndex++;
            }

            comboBoxVoices.SelectedIndex = targetVoice;
            ResetBomb = true;
            UpdateInput();
        }
        
        private void DefaultSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            textBoxInput.Text = e.Result.Text;
            textBoxOutput.Text = _ktaneSpeech.AnalyzeSpeech(e.Result.Text);
            _ktaneSpeech.Speak(textBoxOutput.Text);

            UpdateInput();
            UpdateProperties();
        }

        private void UpdateInput()
        {
            var defuseGrammars = new Dictionary<string, string>
            {
                {
                    "Standard Defuse Grammar",
                    "Defuse <module>\nRandom Bomb | Skip Bomb | Bomb Check |\nThe bomb is Defused | The bomb exploded"
                },
                {
                    "Complicated Grammar",
                    "To escape say \"escape module\"\n<THE COLORS OF THE WIRE> + <nothing|star|light|star and light>"
                },
                {
                    "Sequence Grammar",
                    "To escape say \"done\"\n<COLOR OF THE WIRE> + <alpha|bravo|charlie>"
                },
                {
                    "Button Grammar",
                    "To escape say \"escape module\"\n<color> <label>|<color> stripe"
                },
                {
                    "Simon Says Grammar",
                    "To escape say \"done\"\n<color that flashes last>\nstrikes <0/1/2>" 
                },
                {
                    "Wires Grammar",
                    "To escape say \"escape module\"\n<color> (await for next wire)"
                },
                {
                    "Who's On First Grammar",
                    "To escape say \"escape module\"\nSay what you see as is except: red color, u(r) letter(s), ar ee ee dee, el ee ee dee, their OR your pronoun," +
                    " you're or they're apostrophe, charlie echo echo (cee), u h space u h\nSay \"Stop\" to stop speaking."
                },
                {
                    "Password Grammar",
                    "To escape say \"escape module\"\n<military alphabet|regular alphabet> (await for next)"
                },
                {
                    "Morse Grammar",
                    "To escape say \"escape module\"\n<0>|<1> (await for next letter) (0 = DOT, 1 = DASH)"
                },
                {
                    "Knob Grammar",
                    "To escape say \"escape module\"\n<zero or one, three times> space <zero or one, three times>\n(Say the upper right and lower right lights. Zero is unlit, one is lit)"
                },
                {
                    "Symbols Grammar",
                    "To escape say \"escape module\"\n<symbol> (await for next symbol)\nCheck Symbols.txt to see and edit all available symbols"
                },
                { 
                    "Maze Grammar",
                    "To escape say \"escape module\"\n<0-6>, <0-6>"
                },
                { 
                    "Memory Grammar",
                    "Numbers <all four numbers that you see>."
                },
                {
                    "Bomb Check Grammar",
                    "To escape say \"done\"\nBatteries <0-6> or none or more than two|Freak <yes/true/false/no>|Car <yes/true/false/no>|\nVowel <yes/true/false/no>|Port <yes/true/false/no>|Digit <odd/even>"
                }
            };

            labelGrammarInput.Text = defuseGrammars[_ktaneSpeech.RecognitionEngine.Grammars.Last().Name];
        }
        
        private void buttonStart_Click(object sender, EventArgs e)
        {
            //start listening.
            if (!_ktaneSpeech.Enabled)
            {
                _ktaneSpeech.Enable();
                buttonStart.Text = "STOP LISTENING";
                buttonStart.BackColor = Color.DarkRed;
                textBoxInput.Enabled = textBoxOutput.Enabled = _ktaneSpeech.Enabled;
                
            }
            
            //stop listening.
            else
            {
                _ktaneSpeech.Disable();
                buttonStart.Text = "START LISTENING";
                buttonStart.BackColor = Color.Green;
                textBoxInput.Enabled = textBoxOutput.Enabled = _ktaneSpeech.Enabled;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    @"Are you sure you want to reset the bomb? All its properties will be gone, and you will have to initialize them again.",
                    @"Reset Bomb", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            _ktaneSpeech.ResetBomb();
            UpdateProperties();
        }

        private void buttonRandomBomb_Click(object sender, EventArgs e)
        {
            _ktaneSpeech.InitializeRandomBomb();
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            labelBatteries.Text = $@"Batteries: {(_ktaneSpeech.BombProperties["Batteries"] == -1 ? "--" : _ktaneSpeech.BombProperties["Batteries"].ToString())}";
            labelFRK.Text = $@"FRK: {(_ktaneSpeech.BombProperties["Freak"] == -1 ? "--" : _ktaneSpeech.BombProperties["Freak"] == 1 ? "Yes" : "No")}";
            labelCAR.Text = $@"CAR: {(_ktaneSpeech.BombProperties["Car"] == -1 ? "--" : _ktaneSpeech.BombProperties["Car"] == 1 ? "Yes" : "No")}";
            labelPort.Text = $@"Port: {(_ktaneSpeech.BombProperties["Port"] == -1 ? "--" : _ktaneSpeech.BombProperties["Port"] == 1 ? "Yes" : "No")}";
            labelVowel.Text = $@"Vowel: {(_ktaneSpeech.BombProperties["Vowel"] == -1 ? "--" : _ktaneSpeech.BombProperties["Vowel"] == 1 ? "Yes" : "No")}";
            labelDigit.Text = $@"Digit: {(_ktaneSpeech.BombProperties["Digit"] == -1 ? "--" : _ktaneSpeech.BombProperties["Digit"] == 1 ? "Odd" : "Even")}";
        }

        private void comboBoxVoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ktaneSpeech.ChangeVoice(comboBoxVoices.SelectedItem.ToString());
        }

        private void checkBoxResetBomb_CheckedChanged(object sender, EventArgs e)
        {
            ResetBomb = checkBoxResetBomb.Checked;
        }
    }
}
