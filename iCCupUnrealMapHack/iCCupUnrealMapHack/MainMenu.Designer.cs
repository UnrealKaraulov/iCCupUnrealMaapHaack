namespace iCCupUnrealMapHack
{
    partial class MainMenu
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.StartMinimapHack = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.DiagnozBtn = new System.Windows.Forms.Button();
            this.ProblemScanner1 = new System.Windows.Forms.Timer(this.components);
            this.LeftProfileArrow = new System.Windows.Forms.PictureBox();
            this.RightProfileArrow = new System.Windows.Forms.PictureBox();
            this.TSelectedProfile = new System.Windows.Forms.PictureBox();
            this.JustHideWorker = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MinihackOnlyEnemy = new System.Windows.Forms.CheckBox();
            this.MinihackTransparent = new System.Windows.Forms.CheckBox();
            this.FastDrawMinimap = new System.Windows.Forms.CheckBox();
            this.NotClicabledMinihackTrick = new System.Windows.Forms.CheckBox();
            this.MaphackHotkey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.IsMinimapNoTower = new System.Windows.Forms.CheckBox();
            this.IsMinimapNoNeutrals = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ignore_player_1 = new System.Windows.Forms.CheckBox();
            this.ignore_player_2 = new System.Windows.Forms.CheckBox();
            this.ignore_player_3 = new System.Windows.Forms.CheckBox();
            this.ignore_player_4 = new System.Windows.Forms.CheckBox();
            this.ignore_player_5 = new System.Windows.Forms.CheckBox();
            this.ignore_player_6 = new System.Windows.Forms.CheckBox();
            this.ignore_player_7 = new System.Windows.Forms.CheckBox();
            this.ignore_player_8 = new System.Windows.Forms.CheckBox();
            this.ignore_player_9 = new System.Windows.Forms.CheckBox();
            this.ignore_player_10 = new System.Windows.Forms.CheckBox();
            this.ignore_player_11 = new System.Windows.Forms.CheckBox();
            this.ignore_player_12 = new System.Windows.Forms.CheckBox();
            this.ignore_player_13 = new System.Windows.Forms.CheckBox();
            this.ignore_player_14 = new System.Windows.Forms.CheckBox();
            this.ignore_player_15 = new System.Windows.Forms.CheckBox();
            this.AnimatedMinihackHeroes = new System.Windows.Forms.CheckBox();
            this.SkipMinimapSettingsSave = new System.Windows.Forms.CheckBox();
            this.ChangeMinimapImageLabel = new System.Windows.Forms.CheckBox();
            this.ChangeMinimapImageHotkey = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.LeftProfileArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightProfileArrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TSelectedProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // StartMinimapHack
            // 
            this.StartMinimapHack.BackgroundImage = global::iCCupUnrealMapHack.Properties.Resources.minihack;
            this.StartMinimapHack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StartMinimapHack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartMinimapHack.Location = new System.Drawing.Point(12, 12);
            this.StartMinimapHack.Name = "StartMinimapHack";
            this.StartMinimapHack.Size = new System.Drawing.Size(102, 31);
            this.StartMinimapHack.TabIndex = 0;
            this.StartMinimapHack.UseVisualStyleBackColor = true;
            this.StartMinimapHack.Click += new System.EventHandler(this.StartMinimapHack_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.BackgroundImage = global::iCCupUnrealMapHack.Properties.Resources.exit;
            this.ExitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitButton.Location = new System.Drawing.Point(348, 12);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(82, 31);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // DiagnozBtn
            // 
            this.DiagnozBtn.BackgroundImage = global::iCCupUnrealMapHack.Properties.Resources.diagnostics;
            this.DiagnozBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DiagnozBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DiagnozBtn.Location = new System.Drawing.Point(162, 12);
            this.DiagnozBtn.Name = "DiagnozBtn";
            this.DiagnozBtn.Size = new System.Drawing.Size(123, 31);
            this.DiagnozBtn.TabIndex = 0;
            this.DiagnozBtn.UseVisualStyleBackColor = true;
            this.DiagnozBtn.Click += new System.EventHandler(this.DiagnozBtn_Click);
            // 
            // ProblemScanner1
            // 
            this.ProblemScanner1.Enabled = true;
            this.ProblemScanner1.Interval = 7000;
            this.ProblemScanner1.Tick += new System.EventHandler(this.ProblemScanner1_Tick);
            // 
            // LeftProfileArrow
            // 
            this.LeftProfileArrow.BackColor = System.Drawing.Color.Transparent;
            this.LeftProfileArrow.BackgroundImage = global::iCCupUnrealMapHack.Properties.Resources.LEFT;
            this.LeftProfileArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LeftProfileArrow.Location = new System.Drawing.Point(12, 86);
            this.LeftProfileArrow.Name = "LeftProfileArrow";
            this.LeftProfileArrow.Size = new System.Drawing.Size(25, 25);
            this.LeftProfileArrow.TabIndex = 3;
            this.LeftProfileArrow.TabStop = false;
            this.LeftProfileArrow.Click += new System.EventHandler(this.LeftProfileArrow_Click);
            this.LeftProfileArrow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftProfileArrow_MouseDown);
            // 
            // RightProfileArrow
            // 
            this.RightProfileArrow.BackColor = System.Drawing.Color.Transparent;
            this.RightProfileArrow.BackgroundImage = global::iCCupUnrealMapHack.Properties.Resources.RIGHT;
            this.RightProfileArrow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RightProfileArrow.Location = new System.Drawing.Point(452, 86);
            this.RightProfileArrow.Name = "RightProfileArrow";
            this.RightProfileArrow.Size = new System.Drawing.Size(25, 25);
            this.RightProfileArrow.TabIndex = 3;
            this.RightProfileArrow.TabStop = false;
            this.RightProfileArrow.Click += new System.EventHandler(this.RightProfileArrow_Click);
            this.RightProfileArrow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightProfileArrow_MouseDown);
            // 
            // TSelectedProfile
            // 
            this.TSelectedProfile.BackColor = System.Drawing.Color.Transparent;
            this.TSelectedProfile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TSelectedProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TSelectedProfile.Location = new System.Drawing.Point(48, 76);
            this.TSelectedProfile.Name = "TSelectedProfile";
            this.TSelectedProfile.Size = new System.Drawing.Size(398, 45);
            this.TSelectedProfile.TabIndex = 4;
            this.TSelectedProfile.TabStop = false;
            // 
            // JustHideWorker
            // 
            this.JustHideWorker.Enabled = true;
            this.JustHideWorker.Interval = 1000;
            this.JustHideWorker.Tick += new System.EventHandler(this.JustHideWorker_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Black", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(136, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 28);
            this.label1.TabIndex = 5;
            this.label1.Text = "SЕLЕCT PROFILЕ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Black", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(136, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "SЕTTINGS";
            // 
            // MinihackOnlyEnemy
            // 
            this.MinihackOnlyEnemy.AutoSize = true;
            this.MinihackOnlyEnemy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MinihackOnlyEnemy.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinihackOnlyEnemy.ForeColor = System.Drawing.Color.Black;
            this.MinihackOnlyEnemy.Location = new System.Drawing.Point(12, 310);
            this.MinihackOnlyEnemy.Name = "MinihackOnlyEnemy";
            this.MinihackOnlyEnemy.Size = new System.Drawing.Size(124, 24);
            this.MinihackOnlyEnemy.TabIndex = 6;
            this.MinihackOnlyEnemy.Text = "Only еnеmy";
            this.MinihackOnlyEnemy.UseVisualStyleBackColor = true;
            this.MinihackOnlyEnemy.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // MinihackTransparent
            // 
            this.MinihackTransparent.AutoSize = true;
            this.MinihackTransparent.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MinihackTransparent.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinihackTransparent.ForeColor = System.Drawing.Color.Black;
            this.MinihackTransparent.Location = new System.Drawing.Point(227, 155);
            this.MinihackTransparent.Name = "MinihackTransparent";
            this.MinihackTransparent.Size = new System.Drawing.Size(124, 24);
            this.MinihackTransparent.TabIndex = 6;
            this.MinihackTransparent.Text = "Transparеnt";
            this.MinihackTransparent.UseVisualStyleBackColor = true;
            // 
            // FastDrawMinimap
            // 
            this.FastDrawMinimap.AutoSize = true;
            this.FastDrawMinimap.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FastDrawMinimap.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FastDrawMinimap.ForeColor = System.Drawing.Color.Black;
            this.FastDrawMinimap.Location = new System.Drawing.Point(12, 155);
            this.FastDrawMinimap.Name = "FastDrawMinimap";
            this.FastDrawMinimap.Size = new System.Drawing.Size(194, 24);
            this.FastDrawMinimap.TabIndex = 6;
            this.FastDrawMinimap.Text = "Fast rеdraw minimap";
            this.FastDrawMinimap.UseVisualStyleBackColor = true;
            // 
            // NotClicabledMinihackTrick
            // 
            this.NotClicabledMinihackTrick.AutoSize = true;
            this.NotClicabledMinihackTrick.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.NotClicabledMinihackTrick.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NotClicabledMinihackTrick.ForeColor = System.Drawing.Color.Black;
            this.NotClicabledMinihackTrick.Location = new System.Drawing.Point(12, 185);
            this.NotClicabledMinihackTrick.Name = "NotClicabledMinihackTrick";
            this.NotClicabledMinihackTrick.Size = new System.Drawing.Size(385, 24);
            this.NotClicabledMinihackTrick.TabIndex = 6;
            this.NotClicabledMinihackTrick.Text = "Clicking thrоugh minihаck if KEY nоt pressed:";
            this.NotClicabledMinihackTrick.UseVisualStyleBackColor = true;
            // 
            // MaphackHotkey
            // 
            this.MaphackHotkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaphackHotkey.Location = new System.Drawing.Point(368, 187);
            this.MaphackHotkey.MaxLength = 100;
            this.MaphackHotkey.Multiline = true;
            this.MaphackHotkey.Name = "MaphackHotkey";
            this.MaphackHotkey.ReadOnly = true;
            this.MaphackHotkey.Size = new System.Drawing.Size(100, 20);
            this.MaphackHotkey.TabIndex = 7;
            this.MaphackHotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MaphackHotkey.WordWrap = false;
            this.MaphackHotkey.TextChanged += new System.EventHandler(this.MaphackHotkey_TextChanged);
            this.MaphackHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaphackHotkey_KeyDown);
            this.MaphackHotkey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MaphackHotkey_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial Black", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(136, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "DRАW";
            // 
            // IsMinimapNoTower
            // 
            this.IsMinimapNoTower.AutoSize = true;
            this.IsMinimapNoTower.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.IsMinimapNoTower.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IsMinimapNoTower.ForeColor = System.Drawing.Color.Black;
            this.IsMinimapNoTower.Location = new System.Drawing.Point(125, 310);
            this.IsMinimapNoTower.Name = "IsMinimapNoTower";
            this.IsMinimapNoTower.Size = new System.Drawing.Size(116, 24);
            this.IsMinimapNoTower.TabIndex = 6;
            this.IsMinimapNoTower.Text = "Nо Tоwers";
            this.IsMinimapNoTower.UseVisualStyleBackColor = true;
            this.IsMinimapNoTower.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // IsMinimapNoNeutrals
            // 
            this.IsMinimapNoNeutrals.AutoSize = true;
            this.IsMinimapNoNeutrals.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.IsMinimapNoNeutrals.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IsMinimapNoNeutrals.ForeColor = System.Drawing.Color.Black;
            this.IsMinimapNoNeutrals.Location = new System.Drawing.Point(229, 310);
            this.IsMinimapNoNeutrals.Name = "IsMinimapNoNeutrals";
            this.IsMinimapNoNeutrals.Size = new System.Drawing.Size(125, 24);
            this.IsMinimapNoNeutrals.TabIndex = 6;
            this.IsMinimapNoNeutrals.Text = "Nо Nеutrals";
            this.IsMinimapNoNeutrals.UseVisualStyleBackColor = true;
            this.IsMinimapNoNeutrals.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial Black", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 337);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 28);
            this.label4.TabIndex = 5;
            this.label4.Text = "IGNОRЕ PLАYERS:";
            // 
            // ignore_player_1
            // 
            this.ignore_player_1.AutoSize = true;
            this.ignore_player_1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_1.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_1.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_1.Location = new System.Drawing.Point(12, 368);
            this.ignore_player_1.Name = "ignore_player_1";
            this.ignore_player_1.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_1.TabIndex = 6;
            this.ignore_player_1.Text = "1";
            this.ignore_player_1.UseVisualStyleBackColor = true;
            this.ignore_player_1.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_2
            // 
            this.ignore_player_2.AutoSize = true;
            this.ignore_player_2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_2.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_2.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_2.Location = new System.Drawing.Point(41, 368);
            this.ignore_player_2.Name = "ignore_player_2";
            this.ignore_player_2.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_2.TabIndex = 6;
            this.ignore_player_2.Text = "2";
            this.ignore_player_2.UseVisualStyleBackColor = true;
            this.ignore_player_2.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_3
            // 
            this.ignore_player_3.AutoSize = true;
            this.ignore_player_3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_3.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_3.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_3.Location = new System.Drawing.Point(72, 368);
            this.ignore_player_3.Name = "ignore_player_3";
            this.ignore_player_3.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_3.TabIndex = 6;
            this.ignore_player_3.Text = "3";
            this.ignore_player_3.UseVisualStyleBackColor = true;
            this.ignore_player_3.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_4
            // 
            this.ignore_player_4.AutoSize = true;
            this.ignore_player_4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_4.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_4.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_4.Location = new System.Drawing.Point(104, 368);
            this.ignore_player_4.Name = "ignore_player_4";
            this.ignore_player_4.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_4.TabIndex = 6;
            this.ignore_player_4.Text = "4";
            this.ignore_player_4.UseVisualStyleBackColor = true;
            this.ignore_player_4.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_5
            // 
            this.ignore_player_5.AutoSize = true;
            this.ignore_player_5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_5.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_5.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_5.Location = new System.Drawing.Point(132, 368);
            this.ignore_player_5.Name = "ignore_player_5";
            this.ignore_player_5.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_5.TabIndex = 6;
            this.ignore_player_5.Text = "5";
            this.ignore_player_5.UseVisualStyleBackColor = true;
            this.ignore_player_5.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_6
            // 
            this.ignore_player_6.AutoSize = true;
            this.ignore_player_6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_6.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_6.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_6.Location = new System.Drawing.Point(162, 368);
            this.ignore_player_6.Name = "ignore_player_6";
            this.ignore_player_6.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_6.TabIndex = 6;
            this.ignore_player_6.Text = "6";
            this.ignore_player_6.UseVisualStyleBackColor = true;
            this.ignore_player_6.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_7
            // 
            this.ignore_player_7.AutoSize = true;
            this.ignore_player_7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_7.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_7.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_7.Location = new System.Drawing.Point(187, 368);
            this.ignore_player_7.Name = "ignore_player_7";
            this.ignore_player_7.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_7.TabIndex = 6;
            this.ignore_player_7.Text = "7";
            this.ignore_player_7.UseVisualStyleBackColor = true;
            this.ignore_player_7.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_8
            // 
            this.ignore_player_8.AutoSize = true;
            this.ignore_player_8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_8.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_8.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_8.Location = new System.Drawing.Point(217, 368);
            this.ignore_player_8.Name = "ignore_player_8";
            this.ignore_player_8.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_8.TabIndex = 6;
            this.ignore_player_8.Text = "8";
            this.ignore_player_8.UseVisualStyleBackColor = true;
            this.ignore_player_8.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_9
            // 
            this.ignore_player_9.AutoSize = true;
            this.ignore_player_9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_9.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_9.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_9.Location = new System.Drawing.Point(242, 368);
            this.ignore_player_9.Name = "ignore_player_9";
            this.ignore_player_9.Size = new System.Drawing.Size(43, 24);
            this.ignore_player_9.TabIndex = 6;
            this.ignore_player_9.Text = "9";
            this.ignore_player_9.UseVisualStyleBackColor = true;
            this.ignore_player_9.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_10
            // 
            this.ignore_player_10.AutoSize = true;
            this.ignore_player_10.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_10.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_10.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_10.Location = new System.Drawing.Point(266, 368);
            this.ignore_player_10.Name = "ignore_player_10";
            this.ignore_player_10.Size = new System.Drawing.Size(52, 24);
            this.ignore_player_10.TabIndex = 6;
            this.ignore_player_10.Text = "10";
            this.ignore_player_10.UseVisualStyleBackColor = true;
            this.ignore_player_10.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_11
            // 
            this.ignore_player_11.AutoSize = true;
            this.ignore_player_11.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_11.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_11.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_11.Location = new System.Drawing.Point(302, 368);
            this.ignore_player_11.Name = "ignore_player_11";
            this.ignore_player_11.Size = new System.Drawing.Size(52, 24);
            this.ignore_player_11.TabIndex = 6;
            this.ignore_player_11.Text = "11";
            this.ignore_player_11.UseVisualStyleBackColor = true;
            this.ignore_player_11.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_12
            // 
            this.ignore_player_12.AutoSize = true;
            this.ignore_player_12.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_12.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_12.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_12.Location = new System.Drawing.Point(337, 368);
            this.ignore_player_12.Name = "ignore_player_12";
            this.ignore_player_12.Size = new System.Drawing.Size(52, 24);
            this.ignore_player_12.TabIndex = 6;
            this.ignore_player_12.Text = "12";
            this.ignore_player_12.UseVisualStyleBackColor = true;
            this.ignore_player_12.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_13
            // 
            this.ignore_player_13.AutoSize = true;
            this.ignore_player_13.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_13.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_13.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_13.Location = new System.Drawing.Point(370, 368);
            this.ignore_player_13.Name = "ignore_player_13";
            this.ignore_player_13.Size = new System.Drawing.Size(52, 24);
            this.ignore_player_13.TabIndex = 6;
            this.ignore_player_13.Text = "13";
            this.ignore_player_13.UseVisualStyleBackColor = true;
            this.ignore_player_13.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_14
            // 
            this.ignore_player_14.AutoSize = true;
            this.ignore_player_14.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_14.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_14.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_14.Location = new System.Drawing.Point(406, 368);
            this.ignore_player_14.Name = "ignore_player_14";
            this.ignore_player_14.Size = new System.Drawing.Size(52, 24);
            this.ignore_player_14.TabIndex = 6;
            this.ignore_player_14.Text = "14";
            this.ignore_player_14.UseVisualStyleBackColor = true;
            this.ignore_player_14.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // ignore_player_15
            // 
            this.ignore_player_15.AutoSize = true;
            this.ignore_player_15.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ignore_player_15.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ignore_player_15.ForeColor = System.Drawing.Color.Black;
            this.ignore_player_15.Location = new System.Drawing.Point(442, 368);
            this.ignore_player_15.Name = "ignore_player_15";
            this.ignore_player_15.Size = new System.Drawing.Size(52, 24);
            this.ignore_player_15.TabIndex = 6;
            this.ignore_player_15.Text = "15";
            this.ignore_player_15.UseVisualStyleBackColor = true;
            this.ignore_player_15.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // AnimatedMinihackHeroes
            // 
            this.AnimatedMinihackHeroes.AutoSize = true;
            this.AnimatedMinihackHeroes.Checked = true;
            this.AnimatedMinihackHeroes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AnimatedMinihackHeroes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AnimatedMinihackHeroes.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnimatedMinihackHeroes.ForeColor = System.Drawing.Color.Black;
            this.AnimatedMinihackHeroes.Location = new System.Drawing.Point(348, 310);
            this.AnimatedMinihackHeroes.Name = "AnimatedMinihackHeroes";
            this.AnimatedMinihackHeroes.Size = new System.Drawing.Size(135, 24);
            this.AnimatedMinihackHeroes.TabIndex = 6;
            this.AnimatedMinihackHeroes.Text = "Animate hero";
            this.AnimatedMinihackHeroes.UseVisualStyleBackColor = true;
            this.AnimatedMinihackHeroes.CheckedChanged += new System.EventHandler(this.MinihackOnlyEnemy_CheckedChanged);
            // 
            // SkipMinimapSettingsSave
            // 
            this.SkipMinimapSettingsSave.AutoSize = true;
            this.SkipMinimapSettingsSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SkipMinimapSettingsSave.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SkipMinimapSettingsSave.ForeColor = System.Drawing.Color.Black;
            this.SkipMinimapSettingsSave.Location = new System.Drawing.Point(12, 215);
            this.SkipMinimapSettingsSave.Name = "SkipMinimapSettingsSave";
            this.SkipMinimapSettingsSave.Size = new System.Drawing.Size(447, 24);
            this.SkipMinimapSettingsSave.TabIndex = 6;
            this.SkipMinimapSettingsSave.Text = "Do not overwrite saved minimap coordinates and size.";
            this.SkipMinimapSettingsSave.UseVisualStyleBackColor = true;
            // 
            // ChangeMinimapImageLabel
            // 
            this.ChangeMinimapImageLabel.AutoSize = true;
            this.ChangeMinimapImageLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ChangeMinimapImageLabel.Font = new System.Drawing.Font("Arial Black", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChangeMinimapImageLabel.ForeColor = System.Drawing.Color.Black;
            this.ChangeMinimapImageLabel.Location = new System.Drawing.Point(12, 245);
            this.ChangeMinimapImageLabel.Name = "ChangeMinimapImageLabel";
            this.ChangeMinimapImageLabel.Size = new System.Drawing.Size(221, 24);
            this.ChangeMinimapImageLabel.TabIndex = 6;
            this.ChangeMinimapImageLabel.Text = "Change minimap hotkey:";
            this.ChangeMinimapImageLabel.UseVisualStyleBackColor = true;
            // 
            // ChangeMinimapImageHotkey
            // 
            this.ChangeMinimapImageHotkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChangeMinimapImageHotkey.Location = new System.Drawing.Point(239, 248);
            this.ChangeMinimapImageHotkey.MaxLength = 100;
            this.ChangeMinimapImageHotkey.Multiline = true;
            this.ChangeMinimapImageHotkey.Name = "ChangeMinimapImageHotkey";
            this.ChangeMinimapImageHotkey.ReadOnly = true;
            this.ChangeMinimapImageHotkey.Size = new System.Drawing.Size(100, 20);
            this.ChangeMinimapImageHotkey.TabIndex = 7;
            this.ChangeMinimapImageHotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ChangeMinimapImageHotkey.WordWrap = false;
            this.ChangeMinimapImageHotkey.TextChanged += new System.EventHandler(this.ChangeMinimapImageHotkey_TextChanged_1);
            this.ChangeMinimapImageHotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChangeMinimapImageHotkey_KeyDown);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::iCCupUnrealMapHack.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(497, 436);
            this.Controls.Add(this.ChangeMinimapImageHotkey);
            this.Controls.Add(this.MaphackHotkey);
            this.Controls.Add(this.SkipMinimapSettingsSave);
            this.Controls.Add(this.ChangeMinimapImageLabel);
            this.Controls.Add(this.NotClicabledMinihackTrick);
            this.Controls.Add(this.FastDrawMinimap);
            this.Controls.Add(this.MinihackTransparent);
            this.Controls.Add(this.AnimatedMinihackHeroes);
            this.Controls.Add(this.IsMinimapNoNeutrals);
            this.Controls.Add(this.IsMinimapNoTower);
            this.Controls.Add(this.ignore_player_15);
            this.Controls.Add(this.ignore_player_14);
            this.Controls.Add(this.ignore_player_13);
            this.Controls.Add(this.ignore_player_12);
            this.Controls.Add(this.ignore_player_11);
            this.Controls.Add(this.ignore_player_10);
            this.Controls.Add(this.ignore_player_9);
            this.Controls.Add(this.ignore_player_8);
            this.Controls.Add(this.ignore_player_7);
            this.Controls.Add(this.ignore_player_6);
            this.Controls.Add(this.ignore_player_5);
            this.Controls.Add(this.ignore_player_4);
            this.Controls.Add(this.ignore_player_3);
            this.Controls.Add(this.ignore_player_2);
            this.Controls.Add(this.ignore_player_1);
            this.Controls.Add(this.MinihackOnlyEnemy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TSelectedProfile);
            this.Controls.Add(this.RightProfileArrow);
            this.Controls.Add(this.LeftProfileArrow);
            this.Controls.Add(this.DiagnozBtn);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.StartMinimapHack);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainMenu";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LeftProfileArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightProfileArrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TSelectedProfile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartMinimapHack;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button DiagnozBtn;
        private System.Windows.Forms.Timer ProblemScanner1;
        private System.Windows.Forms.PictureBox LeftProfileArrow;
        private System.Windows.Forms.PictureBox RightProfileArrow;
        private System.Windows.Forms.PictureBox TSelectedProfile;
        private System.Windows.Forms.Timer JustHideWorker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox MinihackOnlyEnemy;
        private System.Windows.Forms.CheckBox MinihackTransparent;
        private System.Windows.Forms.CheckBox FastDrawMinimap;
        private System.Windows.Forms.CheckBox NotClicabledMinihackTrick;
        private System.Windows.Forms.TextBox MaphackHotkey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox IsMinimapNoTower;
        private System.Windows.Forms.CheckBox IsMinimapNoNeutrals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ignore_player_1;
        private System.Windows.Forms.CheckBox ignore_player_2;
        private System.Windows.Forms.CheckBox ignore_player_3;
        private System.Windows.Forms.CheckBox ignore_player_4;
        private System.Windows.Forms.CheckBox ignore_player_5;
        private System.Windows.Forms.CheckBox ignore_player_6;
        private System.Windows.Forms.CheckBox ignore_player_7;
        private System.Windows.Forms.CheckBox ignore_player_8;
        private System.Windows.Forms.CheckBox ignore_player_9;
        private System.Windows.Forms.CheckBox ignore_player_10;
        private System.Windows.Forms.CheckBox ignore_player_11;
        private System.Windows.Forms.CheckBox ignore_player_12;
        private System.Windows.Forms.CheckBox ignore_player_13;
        private System.Windows.Forms.CheckBox ignore_player_14;
        private System.Windows.Forms.CheckBox ignore_player_15;
        private System.Windows.Forms.CheckBox AnimatedMinihackHeroes;
        private System.Windows.Forms.CheckBox SkipMinimapSettingsSave;
        private System.Windows.Forms.CheckBox ChangeMinimapImageLabel;
        private System.Windows.Forms.TextBox ChangeMinimapImageHotkey;
    }
}

