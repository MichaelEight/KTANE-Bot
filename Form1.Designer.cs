
namespace KTANE_Bot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonReset = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelOutput = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.buttonRandomBomb = new System.Windows.Forms.Button();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.labelDigit = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelVowel = new System.Windows.Forms.Label();
            this.labelCAR = new System.Windows.Forms.Label();
            this.labelFRK = new System.Windows.Forms.Label();
            this.labelBatteries = new System.Windows.Forms.Label();
            this.labelOutputVoice = new System.Windows.Forms.Label();
            this.comboBoxVoices = new System.Windows.Forms.ComboBox();
            this.labelGrammarInput = new System.Windows.Forms.Label();
            this.checkBoxResetBomb = new System.Windows.Forms.CheckBox();
            this.panelProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.BackColor = System.Drawing.Color.Red;
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonReset.Location = new System.Drawing.Point(18, 223);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(178, 45);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "RESET BOMB";
            this.buttonReset.UseVisualStyleBackColor = false;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxInput.Enabled = false;
            this.textBoxInput.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInput.Location = new System.Drawing.Point(18, 174);
            this.textBoxInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(566, 35);
            this.textBoxInput.TabIndex = 2;
            // 
            // labelInput
            // 
            this.labelInput.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInput.Location = new System.Drawing.Point(18, 138);
            this.labelInput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(82, 31);
            this.labelInput.TabIndex = 3;
            this.labelInput.Text = "INPUT";
            this.labelInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelOutput
            // 
            this.labelOutput.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOutput.Location = new System.Drawing.Point(18, 338);
            this.labelOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(104, 31);
            this.labelOutput.TabIndex = 4;
            this.labelOutput.Text = "OUTPUT";
            this.labelOutput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.BackColor = System.Drawing.Color.Green;
            this.buttonStart.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.White;
            this.buttonStart.Location = new System.Drawing.Point(18, 595);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(1164, 78);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "START LISTENING";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxOutput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxOutput.Enabled = false;
            this.textBoxOutput.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutput.Location = new System.Drawing.Point(18, 374);
            this.textBoxOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(566, 35);
            this.textBoxOutput.TabIndex = 6;
            // 
            // buttonRandomBomb
            // 
            this.buttonRandomBomb.BackColor = System.Drawing.Color.White;
            this.buttonRandomBomb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonRandomBomb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRandomBomb.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonRandomBomb.Location = new System.Drawing.Point(206, 223);
            this.buttonRandomBomb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRandomBomb.Name = "buttonRandomBomb";
            this.buttonRandomBomb.Size = new System.Drawing.Size(178, 45);
            this.buttonRandomBomb.TabIndex = 7;
            this.buttonRandomBomb.Text = "Random Bomb";
            this.buttonRandomBomb.UseVisualStyleBackColor = false;
            this.buttonRandomBomb.Click += new System.EventHandler(this.buttonRandomBomb_Click);
            // 
            // panelProperties
            // 
            this.panelProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProperties.Controls.Add(this.labelDigit);
            this.panelProperties.Controls.Add(this.labelPort);
            this.panelProperties.Controls.Add(this.labelVowel);
            this.panelProperties.Controls.Add(this.labelCAR);
            this.panelProperties.Controls.Add(this.labelFRK);
            this.panelProperties.Controls.Add(this.labelBatteries);
            this.panelProperties.Location = new System.Drawing.Point(928, 17);
            this.panelProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(258, 306);
            this.panelProperties.TabIndex = 8;
            // 
            // labelDigit
            // 
            this.labelDigit.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDigit.Location = new System.Drawing.Point(4, 168);
            this.labelDigit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDigit.Name = "labelDigit";
            this.labelDigit.Size = new System.Drawing.Size(138, 31);
            this.labelDigit.TabIndex = 9;
            this.labelDigit.Text = "Digit: --";
            this.labelDigit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPort
            // 
            this.labelPort.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort.Location = new System.Drawing.Point(4, 137);
            this.labelPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(138, 31);
            this.labelPort.TabIndex = 8;
            this.labelPort.Text = "Port: --";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVowel
            // 
            this.labelVowel.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVowel.Location = new System.Drawing.Point(4, 106);
            this.labelVowel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelVowel.Name = "labelVowel";
            this.labelVowel.Size = new System.Drawing.Size(138, 31);
            this.labelVowel.TabIndex = 7;
            this.labelVowel.Text = "Vowel: --";
            this.labelVowel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCAR
            // 
            this.labelCAR.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCAR.Location = new System.Drawing.Point(4, 75);
            this.labelCAR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCAR.Name = "labelCAR";
            this.labelCAR.Size = new System.Drawing.Size(138, 31);
            this.labelCAR.TabIndex = 6;
            this.labelCAR.Text = "CAR: --";
            this.labelCAR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelFRK
            // 
            this.labelFRK.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFRK.Location = new System.Drawing.Point(4, 45);
            this.labelFRK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFRK.Name = "labelFRK";
            this.labelFRK.Size = new System.Drawing.Size(138, 31);
            this.labelFRK.TabIndex = 5;
            this.labelFRK.Text = "FRK: --";
            this.labelFRK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelBatteries
            // 
            this.labelBatteries.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBatteries.Location = new System.Drawing.Point(4, 14);
            this.labelBatteries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBatteries.Name = "labelBatteries";
            this.labelBatteries.Size = new System.Drawing.Size(238, 31);
            this.labelBatteries.TabIndex = 4;
            this.labelBatteries.Text = "Batteries: --";
            this.labelBatteries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelOutputVoice
            // 
            this.labelOutputVoice.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOutputVoice.Location = new System.Drawing.Point(18, 432);
            this.labelOutputVoice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOutputVoice.Name = "labelOutputVoice";
            this.labelOutputVoice.Size = new System.Drawing.Size(148, 32);
            this.labelOutputVoice.TabIndex = 9;
            this.labelOutputVoice.Text = "Output voice:";
            this.labelOutputVoice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxVoices
            // 
            this.comboBoxVoices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVoices.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxVoices.FormattingEnabled = true;
            this.comboBoxVoices.Location = new System.Drawing.Point(176, 432);
            this.comboBoxVoices.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxVoices.Name = "comboBoxVoices";
            this.comboBoxVoices.Size = new System.Drawing.Size(349, 31);
            this.comboBoxVoices.TabIndex = 10;
            this.comboBoxVoices.SelectedIndexChanged += new System.EventHandler(this.comboBoxVoices_SelectedIndexChanged);
            // 
            // labelGrammarInput
            // 
            this.labelGrammarInput.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGrammarInput.Location = new System.Drawing.Point(15, 17);
            this.labelGrammarInput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGrammarInput.Name = "labelGrammarInput";
            this.labelGrammarInput.Size = new System.Drawing.Size(904, 121);
            this.labelGrammarInput.TabIndex = 11;
            this.labelGrammarInput.Text = "INPUT";
            // 
            // checkBoxResetBomb
            // 
            this.checkBoxResetBomb.Checked = true;
            this.checkBoxResetBomb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxResetBomb.Location = new System.Drawing.Point(18, 277);
            this.checkBoxResetBomb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxResetBomb.Name = "checkBoxResetBomb";
            this.checkBoxResetBomb.Size = new System.Drawing.Size(309, 37);
            this.checkBoxResetBomb.TabIndex = 12;
            this.checkBoxResetBomb.Text = "Reset Bomb upon Explosion/Defusal";
            this.checkBoxResetBomb.UseVisualStyleBackColor = true;
            this.checkBoxResetBomb.CheckedChanged += new System.EventHandler(this.checkBoxResetBomb_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.checkBoxResetBomb);
            this.Controls.Add(this.labelGrammarInput);
            this.Controls.Add(this.comboBoxVoices);
            this.Controls.Add(this.labelOutputVoice);
            this.Controls.Add(this.panelProperties);
            this.Controls.Add(this.buttonRandomBomb);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.buttonReset);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keep Talking and Nobody Explodes Defuser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelProperties.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.CheckBox checkBoxResetBomb;

        private System.Windows.Forms.Label labelGrammarInput;

        private System.Windows.Forms.ComboBox comboBoxVoices;

        private System.Windows.Forms.Label labelOutputVoice;

        private System.Windows.Forms.Label labelFRK;
        private System.Windows.Forms.Label labelCAR;
        private System.Windows.Forms.Label labelVowel;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelDigit;

        private System.Windows.Forms.Panel panelProperties;
        private System.Windows.Forms.Label labelBatteries;

        private System.Windows.Forms.Button buttonRandomBomb;

        private System.Windows.Forms.TextBox textBoxOutput;

        private System.Windows.Forms.Button buttonStart;

        private System.Windows.Forms.Label labelInput;

        private System.Windows.Forms.Label labelOutput;

        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.TextBox textBoxInput;

        #endregion
    }
}

