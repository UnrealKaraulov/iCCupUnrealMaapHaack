namespace iCCupUnrealMapHack
{
    partial class MainHack
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.components = new System.ComponentModel.Container();
            this.DrawUnitsOnMainMap = new System.Windows.Forms.CheckBox();
            this.DrawMinimapIngame = new System.Windows.Forms.CheckBox();
            this.TransferGoldEnabled = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BypassAntihack = new System.Windows.Forms.CheckBox();
            this.All_allies = new System.Windows.Forms.CheckBox();
            this.All_items = new System.Windows.Forms.CheckBox();
            this.CameraDistText = new System.Windows.Forms.TextBox();
            this.GeneralFeatures = new System.Windows.Forms.Label();
            this.DrawOther = new System.Windows.Forms.CheckBox();
            this.CameraDistance = new System.Windows.Forms.CheckBox();
            this.ColoredHeroes = new System.Windows.Forms.CheckBox();
            this.ReadGameColorSettings = new System.Windows.Forms.CheckBox();
            this.DrawNeutrals = new System.Windows.Forms.CheckBox();
            this.DrawHeroes = new System.Windows.Forms.CheckBox();
            this.DrawAllUnits = new System.Windows.Forms.CheckBox();
            this.MiniMapEnabled = new System.Windows.Forms.CheckBox();
            this.NeedHideMMap = new System.Windows.Forms.CheckBox();
            this.DrawOnlyHeroesAtMinimap = new System.Windows.Forms.CheckBox();
            this.DrawForceBadUnits = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MH_AutoClose = new System.Windows.Forms.CheckBox();
            this.DrawHPMPMainMap = new System.Windows.Forms.CheckBox();
            this.DrawSpeed = new System.Windows.Forms.TrackBar();
            this.SpeedDrawLabel = new System.Windows.Forms.Label();
            this.AdditionalFeatures = new System.Windows.Forms.Label();
            this.autodetectplayercolor = new System.Windows.Forms.CheckBox();
            this.AutoDetectPlayerTeams = new System.Windows.Forms.CheckBox();
            this.DrawIllusions = new System.Windows.Forms.CheckBox();
            this.ExitButton1 = new System.Windows.Forms.Button();
            this.GoldTransferWatch = new System.Windows.Forms.Timer(this.components);
            this.MinimapUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SelectedGameProfileName = new System.Windows.Forms.Label();
            this.WarnWarnWarn = new System.Windows.Forms.Label();
            this.CloseTimer = new System.Windows.Forms.Timer(this.components);
            this.WarcraftVersion = new System.Windows.Forms.Label();
            this.HIDEMENUBUTTON = new System.Windows.Forms.Button();
            this.GameTime = new System.Windows.Forms.Label();
            this.GameStatistic = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MapFixOffsetBot = new System.Windows.Forms.TextBox();
            this.MapFixOffsetRight = new System.Windows.Forms.TextBox();
            this.MapFixOffsetTop = new System.Windows.Forms.TextBox();
            this.MapFixOffsetLeft = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowCoordinates = new System.Windows.Forms.Button();
            this.GameDllAddrShow = new System.Windows.Forms.Label();
            this.DebugLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawSpeed)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DrawUnitsOnMainMap
            // 
            this.DrawUnitsOnMainMap.AutoSize = true;
            this.DrawUnitsOnMainMap.Checked = true;
            this.DrawUnitsOnMainMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawUnitsOnMainMap.Location = new System.Drawing.Point(27, 157);
            this.DrawUnitsOnMainMap.Name = "DrawUnitsOnMainMap";
            this.DrawUnitsOnMainMap.Size = new System.Drawing.Size(149, 17);
            this.DrawUnitsOnMainMap.TabIndex = 0;
            this.DrawUnitsOnMainMap.Text = "Mаinmap(Глaвная карта)";
            this.DrawUnitsOnMainMap.UseVisualStyleBackColor = true;
            this.DrawUnitsOnMainMap.CheckedChanged += new System.EventHandler(this.DrawUnitsOnMainMap_CheckedChanged);
            // 
            // DrawMinimapIngame
            // 
            this.DrawMinimapIngame.AutoSize = true;
            this.DrawMinimapIngame.Checked = true;
            this.DrawMinimapIngame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawMinimapIngame.Location = new System.Drawing.Point(28, 19);
            this.DrawMinimapIngame.Name = "DrawMinimapIngame";
            this.DrawMinimapIngame.Size = new System.Drawing.Size(185, 17);
            this.DrawMinimapIngame.TabIndex = 0;
            this.DrawMinimapIngame.Text = "Oтoбpaзить вcex нa миникaртe";
            this.DrawMinimapIngame.UseVisualStyleBackColor = true;
            this.DrawMinimapIngame.CheckedChanged += new System.EventHandler(this.DrawMinimapIngame_CheckedChanged);
            // 
            // TransferGoldEnabled
            // 
            this.TransferGoldEnabled.AutoSize = true;
            this.TransferGoldEnabled.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TransferGoldEnabled.Location = new System.Drawing.Point(27, 18);
            this.TransferGoldEnabled.Name = "TransferGoldEnabled";
            this.TransferGoldEnabled.Size = new System.Drawing.Size(176, 17);
            this.TransferGoldEnabled.TabIndex = 0;
            this.TransferGoldEnabled.Text = "Paзрeшить пepeдaчу зoлoтa *";
            this.TransferGoldEnabled.UseVisualStyleBackColor = true;
            this.TransferGoldEnabled.CheckedChanged += new System.EventHandler(this.TransferGoldEnabled_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.BypassAntihack);
            this.panel1.Controls.Add(this.All_allies);
            this.panel1.Controls.Add(this.All_items);
            this.panel1.Controls.Add(this.CameraDistText);
            this.panel1.Controls.Add(this.GeneralFeatures);
            this.panel1.Controls.Add(this.DrawOther);
            this.panel1.Controls.Add(this.CameraDistance);
            this.panel1.Controls.Add(this.ColoredHeroes);
            this.panel1.Controls.Add(this.ReadGameColorSettings);
            this.panel1.Controls.Add(this.DrawNeutrals);
            this.panel1.Controls.Add(this.DrawHeroes);
            this.panel1.Controls.Add(this.DrawAllUnits);
            this.panel1.Controls.Add(this.MiniMapEnabled);
            this.panel1.Controls.Add(this.DrawUnitsOnMainMap);
            this.panel1.Controls.Add(this.NeedHideMMap);
            this.panel1.Controls.Add(this.DrawOnlyHeroesAtMinimap);
            this.panel1.Controls.Add(this.DrawMinimapIngame);
            this.panel1.Location = new System.Drawing.Point(24, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 225);
            this.panel1.TabIndex = 1;
            // 
            // BypassAntihack
            // 
            this.BypassAntihack.AutoSize = true;
            this.BypassAntihack.Location = new System.Drawing.Point(177, 171);
            this.BypassAntihack.Name = "BypassAntihack";
            this.BypassAntihack.Size = new System.Drawing.Size(78, 17);
            this.BypassAntihack.TabIndex = 4;
            this.BypassAntihack.Text = "Byраss -ah";
            this.BypassAntihack.UseVisualStyleBackColor = true;
            this.BypassAntihack.CheckedChanged += new System.EventHandler(this.BypassAntihack_CheckedChanged);
            // 
            // All_allies
            // 
            this.All_allies.AutoSize = true;
            this.All_allies.Location = new System.Drawing.Point(189, 148);
            this.All_allies.Name = "All_allies";
            this.All_allies.Size = new System.Drawing.Size(50, 17);
            this.All_allies.TabIndex = 6;
            this.All_allies.Text = "Allies";
            this.All_allies.UseVisualStyleBackColor = true;
            // 
            // All_items
            // 
            this.All_items.AutoSize = true;
            this.All_items.Location = new System.Drawing.Point(28, 134);
            this.All_items.Name = "All_items";
            this.All_items.Size = new System.Drawing.Size(64, 17);
            this.All_items.TabIndex = 6;
            this.All_items.Text = "All items";
            this.All_items.UseVisualStyleBackColor = true;
            // 
            // CameraDistText
            // 
            this.CameraDistText.Location = new System.Drawing.Point(145, 108);
            this.CameraDistText.MaxLength = 5;
            this.CameraDistText.Name = "CameraDistText";
            this.CameraDistText.Size = new System.Drawing.Size(81, 20);
            this.CameraDistText.TabIndex = 2;
            this.CameraDistText.Text = "1650";
            this.CameraDistText.Visible = false;
            this.CameraDistText.TextChanged += new System.EventHandler(this.CameraDistText_TextChanged);
            // 
            // GeneralFeatures
            // 
            this.GeneralFeatures.AutoSize = true;
            this.GeneralFeatures.Location = new System.Drawing.Point(76, 3);
            this.GeneralFeatures.Name = "GeneralFeatures";
            this.GeneralFeatures.Size = new System.Drawing.Size(100, 13);
            this.GeneralFeatures.TabIndex = 1;
            this.GeneralFeatures.Text = "Глaвныe фyнкции:";
            // 
            // DrawOther
            // 
            this.DrawOther.AutoSize = true;
            this.DrawOther.Checked = true;
            this.DrawOther.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawOther.Location = new System.Drawing.Point(177, 194);
            this.DrawOther.Name = "DrawOther";
            this.DrawOther.Size = new System.Drawing.Size(83, 17);
            this.DrawOther.TabIndex = 0;
            this.DrawOther.Text = "Ocтaльные";
            this.DrawOther.UseVisualStyleBackColor = true;
            // 
            // CameraDistance
            // 
            this.CameraDistance.AutoSize = true;
            this.CameraDistance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CameraDistance.Location = new System.Drawing.Point(28, 111);
            this.CameraDistance.Name = "CameraDistance";
            this.CameraDistance.Size = new System.Drawing.Size(113, 17);
            this.CameraDistance.TabIndex = 0;
            this.CameraDistance.Text = "Camеra Distance: ";
            this.CameraDistance.UseVisualStyleBackColor = true;
            this.CameraDistance.CheckedChanged += new System.EventHandler(this.CameraDistance_CheckedChanged);
            // 
            // ColoredHeroes
            // 
            this.ColoredHeroes.AutoSize = true;
            this.ColoredHeroes.Checked = true;
            this.ColoredHeroes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ColoredHeroes.Location = new System.Drawing.Point(28, 65);
            this.ColoredHeroes.Name = "ColoredHeroes";
            this.ColoredHeroes.Size = new System.Drawing.Size(123, 17);
            this.ColoredHeroes.TabIndex = 0;
            this.ColoredHeroes.Text = "Оnly Cоlоred Herоes";
            this.ColoredHeroes.UseVisualStyleBackColor = true;
            this.ColoredHeroes.Visible = false;
            // 
            // ReadGameColorSettings
            // 
            this.ReadGameColorSettings.AutoSize = true;
            this.ReadGameColorSettings.Checked = true;
            this.ReadGameColorSettings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReadGameColorSettings.Location = new System.Drawing.Point(28, 42);
            this.ReadGameColorSettings.Name = "ReadGameColorSettings";
            this.ReadGameColorSettings.Size = new System.Drawing.Size(175, 17);
            this.ReadGameColorSettings.TabIndex = 0;
            this.ReadGameColorSettings.Text = "Rеad InGamе Minimap Sеttings";
            this.ReadGameColorSettings.UseVisualStyleBackColor = true;
            this.ReadGameColorSettings.CheckedChanged += new System.EventHandler(this.ReadGameColorSettings_CheckedChanged);
            // 
            // DrawNeutrals
            // 
            this.DrawNeutrals.AutoSize = true;
            this.DrawNeutrals.Checked = true;
            this.DrawNeutrals.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawNeutrals.Location = new System.Drawing.Point(94, 194);
            this.DrawNeutrals.Name = "DrawNeutrals";
            this.DrawNeutrals.Size = new System.Drawing.Size(77, 17);
            this.DrawNeutrals.TabIndex = 0;
            this.DrawNeutrals.Text = "Heйтрaлы";
            this.DrawNeutrals.UseVisualStyleBackColor = true;
            // 
            // DrawHeroes
            // 
            this.DrawHeroes.AutoSize = true;
            this.DrawHeroes.Checked = true;
            this.DrawHeroes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawHeroes.Location = new System.Drawing.Point(28, 194);
            this.DrawHeroes.Name = "DrawHeroes";
            this.DrawHeroes.Size = new System.Drawing.Size(56, 17);
            this.DrawHeroes.TabIndex = 0;
            this.DrawHeroes.Text = "Гepoи";
            this.DrawHeroes.UseVisualStyleBackColor = true;
            // 
            // DrawAllUnits
            // 
            this.DrawAllUnits.AutoSize = true;
            this.DrawAllUnits.Location = new System.Drawing.Point(98, 134);
            this.DrawAllUnits.Name = "DrawAllUnits";
            this.DrawAllUnits.Size = new System.Drawing.Size(122, 17);
            this.DrawAllUnits.TabIndex = 0;
            this.DrawAllUnits.Text = "Draw Аll as Enеmies";
            this.DrawAllUnits.UseVisualStyleBackColor = true;
            this.DrawAllUnits.CheckedChanged += new System.EventHandler(this.DrawALLUnits_CheckedChanged);
            // 
            // MiniMapEnabled
            // 
            this.MiniMapEnabled.AutoSize = true;
            this.MiniMapEnabled.Checked = true;
            this.MiniMapEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MiniMapEnabled.Location = new System.Drawing.Point(27, 174);
            this.MiniMapEnabled.Name = "MiniMapEnabled";
            this.MiniMapEnabled.Size = new System.Drawing.Size(127, 17);
            this.MiniMapEnabled.TabIndex = 0;
            this.MiniMapEnabled.Text = "Minimap(Миникарта)";
            this.MiniMapEnabled.UseVisualStyleBackColor = true;
            this.MiniMapEnabled.CheckedChanged += new System.EventHandler(this.DrawUnitsOnMainMap_CheckedChanged);
            // 
            // NeedHideMMap
            // 
            this.NeedHideMMap.AutoSize = true;
            this.NeedHideMMap.Location = new System.Drawing.Point(28, 88);
            this.NeedHideMMap.Name = "NeedHideMMap";
            this.NeedHideMMap.Size = new System.Drawing.Size(190, 17);
            this.NeedHideMMap.TabIndex = 0;
            this.NeedHideMMap.Text = "Hide minimap(Скрыть миникарту)";
            this.NeedHideMMap.UseVisualStyleBackColor = true;
            this.NeedHideMMap.CheckedChanged += new System.EventHandler(this.NeedHideMMap_CheckedChanged);
            // 
            // DrawOnlyHeroesAtMinimap
            // 
            this.DrawOnlyHeroesAtMinimap.AutoSize = true;
            this.DrawOnlyHeroesAtMinimap.Location = new System.Drawing.Point(28, 19);
            this.DrawOnlyHeroesAtMinimap.Name = "DrawOnlyHeroesAtMinimap";
            this.DrawOnlyHeroesAtMinimap.Size = new System.Drawing.Size(168, 17);
            this.DrawOnlyHeroesAtMinimap.TabIndex = 0;
            this.DrawOnlyHeroesAtMinimap.Text = "Только герои на миникарте";
            this.DrawOnlyHeroesAtMinimap.UseVisualStyleBackColor = true;
            this.DrawOnlyHeroesAtMinimap.Visible = false;
            this.DrawOnlyHeroesAtMinimap.CheckedChanged += new System.EventHandler(this.DrawOnlyHeroesAtMinimap_CheckedChanged);
            // 
            // DrawForceBadUnits
            // 
            this.DrawForceBadUnits.AutoSize = true;
            this.DrawForceBadUnits.Location = new System.Drawing.Point(27, 151);
            this.DrawForceBadUnits.Name = "DrawForceBadUnits";
            this.DrawForceBadUnits.Size = new System.Drawing.Size(99, 17);
            this.DrawForceBadUnits.TabIndex = 6;
            this.DrawForceBadUnits.Text = "Fоrce bаd units";
            this.DrawForceBadUnits.UseVisualStyleBackColor = true;
            this.DrawForceBadUnits.CheckedChanged += new System.EventHandler(this.DrawForceBadUnits_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.MH_AutoClose);
            this.panel2.Controls.Add(this.DrawForceBadUnits);
            this.panel2.Controls.Add(this.DrawHPMPMainMap);
            this.panel2.Controls.Add(this.DrawSpeed);
            this.panel2.Controls.Add(this.SpeedDrawLabel);
            this.panel2.Controls.Add(this.AdditionalFeatures);
            this.panel2.Controls.Add(this.TransferGoldEnabled);
            this.panel2.Controls.Add(this.autodetectplayercolor);
            this.panel2.Controls.Add(this.AutoDetectPlayerTeams);
            this.panel2.Controls.Add(this.DrawIllusions);
            this.panel2.Location = new System.Drawing.Point(313, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(265, 225);
            this.panel2.TabIndex = 2;
            // 
            // MH_AutoClose
            // 
            this.MH_AutoClose.AutoSize = true;
            this.MH_AutoClose.Location = new System.Drawing.Point(27, 89);
            this.MH_AutoClose.Name = "MH_AutoClose";
            this.MH_AutoClose.Size = new System.Drawing.Size(175, 17);
            this.MH_AutoClose.TabIndex = 5;
            this.MH_AutoClose.Text = "AutoClose. (Автозакрытие мх)";
            this.MH_AutoClose.UseVisualStyleBackColor = true;
            // 
            // DrawHPMPMainMap
            // 
            this.DrawHPMPMainMap.AutoSize = true;
            this.DrawHPMPMainMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DrawHPMPMainMap.Location = new System.Drawing.Point(27, 42);
            this.DrawHPMPMainMap.Name = "DrawHPMPMainMap";
            this.DrawHPMPMainMap.Size = new System.Drawing.Size(192, 17);
            this.DrawHPMPMainMap.TabIndex = 4;
            this.DrawHPMPMainMap.Text = "MР/HР Bar (Рress and hold \'АLT) *";
            this.DrawHPMPMainMap.UseVisualStyleBackColor = true;
            this.DrawHPMPMainMap.CheckedChanged += new System.EventHandler(this.DrawHPMPMainMap_CheckedChanged);
            // 
            // DrawSpeed
            // 
            this.DrawSpeed.Location = new System.Drawing.Point(6, 187);
            this.DrawSpeed.Maximum = 500;
            this.DrawSpeed.Minimum = 100;
            this.DrawSpeed.Name = "DrawSpeed";
            this.DrawSpeed.Size = new System.Drawing.Size(247, 45);
            this.DrawSpeed.TabIndex = 3;
            this.DrawSpeed.TickFrequency = 20;
            this.DrawSpeed.Value = 430;
            this.DrawSpeed.Scroll += new System.EventHandler(this.DrawSpeed_Scroll);
            // 
            // SpeedDrawLabel
            // 
            this.SpeedDrawLabel.AutoSize = true;
            this.SpeedDrawLabel.Location = new System.Drawing.Point(87, 171);
            this.SpeedDrawLabel.Name = "SpeedDrawLabel";
            this.SpeedDrawLabel.Size = new System.Drawing.Size(69, 13);
            this.SpeedDrawLabel.TabIndex = 2;
            this.SpeedDrawLabel.Text = "Drаw Spееd:";
            // 
            // AdditionalFeatures
            // 
            this.AdditionalFeatures.AutoSize = true;
            this.AdditionalFeatures.Location = new System.Drawing.Point(65, 2);
            this.AdditionalFeatures.Name = "AdditionalFeatures";
            this.AdditionalFeatures.Size = new System.Drawing.Size(144, 13);
            this.AdditionalFeatures.TabIndex = 1;
            this.AdditionalFeatures.Text = "Дoпoлнитeльные фyнкции:";
            // 
            // autodetectplayercolor
            // 
            this.autodetectplayercolor.AutoSize = true;
            this.autodetectplayercolor.Checked = true;
            this.autodetectplayercolor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autodetectplayercolor.Location = new System.Drawing.Point(27, 134);
            this.autodetectplayercolor.Name = "autodetectplayercolor";
            this.autodetectplayercolor.Size = new System.Drawing.Size(153, 17);
            this.autodetectplayercolor.TabIndex = 0;
            this.autodetectplayercolor.Text = "Plаyer Colоr AutоDetection";
            this.autodetectplayercolor.UseVisualStyleBackColor = true;
            this.autodetectplayercolor.CheckedChanged += new System.EventHandler(this.DrawUnitHpMp_CheckedChanged);
            // 
            // AutoDetectPlayerTeams
            // 
            this.AutoDetectPlayerTeams.AutoSize = true;
            this.AutoDetectPlayerTeams.Checked = true;
            this.AutoDetectPlayerTeams.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoDetectPlayerTeams.Location = new System.Drawing.Point(27, 112);
            this.AutoDetectPlayerTeams.Name = "AutoDetectPlayerTeams";
            this.AutoDetectPlayerTeams.Size = new System.Drawing.Size(124, 17);
            this.AutoDetectPlayerTeams.TabIndex = 0;
            this.AutoDetectPlayerTeams.Text = "Tеаm AutоDеtеction";
            this.AutoDetectPlayerTeams.UseVisualStyleBackColor = true;
            this.AutoDetectPlayerTeams.CheckedChanged += new System.EventHandler(this.DrawUnitHpMp_CheckedChanged);
            // 
            // DrawIllusions
            // 
            this.DrawIllusions.AutoSize = true;
            this.DrawIllusions.Location = new System.Drawing.Point(28, 65);
            this.DrawIllusions.Name = "DrawIllusions";
            this.DrawIllusions.Size = new System.Drawing.Size(125, 17);
            this.DrawIllusions.TabIndex = 0;
            this.DrawIllusions.Text = "Illusiоns (\'\"Иллюзии)";
            this.DrawIllusions.UseVisualStyleBackColor = true;
            this.DrawIllusions.CheckedChanged += new System.EventHandler(this.DrawUnitHpMp_CheckedChanged);
            // 
            // ExitButton1
            // 
            this.ExitButton1.Location = new System.Drawing.Point(327, 282);
            this.ExitButton1.Name = "ExitButton1";
            this.ExitButton1.Size = new System.Drawing.Size(117, 23);
            this.ExitButton1.TabIndex = 3;
            this.ExitButton1.Text = "Oтключить(Close)";
            this.ExitButton1.UseVisualStyleBackColor = true;
            this.ExitButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GoldTransferWatch
            // 
            this.GoldTransferWatch.Enabled = true;
            this.GoldTransferWatch.Interval = 1000;
            this.GoldTransferWatch.Tick += new System.EventHandler(this.GoldTransferWatch_Tick);
            // 
            // MinimapUpdateTimer
            // 
            this.MinimapUpdateTimer.Enabled = true;
            this.MinimapUpdateTimer.Interval = 150;
            this.MinimapUpdateTimer.Tick += new System.EventHandler(this.MinimapUpdateTimer_Tick);
            // 
            // SelectedGameProfileName
            // 
            this.SelectedGameProfileName.AutoSize = true;
            this.SelectedGameProfileName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectedGameProfileName.Location = new System.Drawing.Point(210, 316);
            this.SelectedGameProfileName.Name = "SelectedGameProfileName";
            this.SelectedGameProfileName.Size = new System.Drawing.Size(221, 19);
            this.SelectedGameProfileName.TabIndex = 4;
            this.SelectedGameProfileName.Text = "Sеlected Profilе: UNNAMED";
            // 
            // WarnWarnWarn
            // 
            this.WarnWarnWarn.AutoSize = true;
            this.WarnWarnWarn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.WarnWarnWarn.Location = new System.Drawing.Point(167, 240);
            this.WarnWarnWarn.Name = "WarnWarnWarn";
            this.WarnWarnWarn.Size = new System.Drawing.Size(277, 39);
            this.WarnWarnWarn.TabIndex = 5;
            this.WarnWarnWarn.Text = "* Помеченные красным цветом - повышенный риск.\r\n (т.е возможны проблемы на некото" +
    "рых серверах)\r\n(Red Color - Warning for some servers. Can be detected!)";
            // 
            // CloseTimer
            // 
            this.CloseTimer.Interval = 2000;
            this.CloseTimer.Tick += new System.EventHandler(this.CloseTimer_Tick);
            // 
            // WarcraftVersion
            // 
            this.WarcraftVersion.AutoSize = true;
            this.WarcraftVersion.Font = new System.Drawing.Font("Arial", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WarcraftVersion.Location = new System.Drawing.Point(16, 320);
            this.WarcraftVersion.Name = "WarcraftVersion";
            this.WarcraftVersion.Size = new System.Drawing.Size(188, 16);
            this.WarcraftVersion.TabIndex = 6;
            this.WarcraftVersion.Text = "UNKNOWN GAME VERSION";
            // 
            // HIDEMENUBUTTON
            // 
            this.HIDEMENUBUTTON.Location = new System.Drawing.Point(151, 282);
            this.HIDEMENUBUTTON.Name = "HIDEMENUBUTTON";
            this.HIDEMENUBUTTON.Size = new System.Drawing.Size(100, 23);
            this.HIDEMENUBUTTON.TabIndex = 7;
            this.HIDEMENUBUTTON.Text = "HIDE MENU";
            this.HIDEMENUBUTTON.UseVisualStyleBackColor = true;
            this.HIDEMENUBUTTON.Click += new System.EventHandler(this.HIDEMENUBUTTON_Click);
            // 
            // GameTime
            // 
            this.GameTime.AutoSize = true;
            this.GameTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GameTime.ForeColor = System.Drawing.Color.Firebrick;
            this.GameTime.Location = new System.Drawing.Point(30, 251);
            this.GameTime.Name = "GameTime";
            this.GameTime.Size = new System.Drawing.Size(69, 15);
            this.GameTime.TabIndex = 8;
            this.GameTime.Text = "XX:XX:XX";
            // 
            // GameStatistic
            // 
            this.GameStatistic.AutoSize = true;
            this.GameStatistic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GameStatistic.Location = new System.Drawing.Point(28, 274);
            this.GameStatistic.Name = "GameStatistic";
            this.GameStatistic.Size = new System.Drawing.Size(48, 15);
            this.GameStatistic.TabIndex = 9;
            this.GameStatistic.Text = "XX-XX";
            this.GameStatistic.Click += new System.EventHandler(this.GameStatistic_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.MapFixOffsetBot);
            this.panel3.Controls.Add(this.MapFixOffsetRight);
            this.panel3.Controls.Add(this.MapFixOffsetTop);
            this.panel3.Controls.Add(this.MapFixOffsetLeft);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(604, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(265, 225);
            this.panel3.TabIndex = 2;
            // 
            // MapFixOffsetBot
            // 
            this.MapFixOffsetBot.Location = new System.Drawing.Point(105, 112);
            this.MapFixOffsetBot.Name = "MapFixOffsetBot";
            this.MapFixOffsetBot.Size = new System.Drawing.Size(66, 20);
            this.MapFixOffsetBot.TabIndex = 2;
            this.MapFixOffsetBot.Text = "0";
            // 
            // MapFixOffsetRight
            // 
            this.MapFixOffsetRight.Location = new System.Drawing.Point(173, 76);
            this.MapFixOffsetRight.Name = "MapFixOffsetRight";
            this.MapFixOffsetRight.Size = new System.Drawing.Size(66, 20);
            this.MapFixOffsetRight.TabIndex = 2;
            this.MapFixOffsetRight.Text = "0";
            // 
            // MapFixOffsetTop
            // 
            this.MapFixOffsetTop.Location = new System.Drawing.Point(105, 39);
            this.MapFixOffsetTop.Name = "MapFixOffsetTop";
            this.MapFixOffsetTop.Size = new System.Drawing.Size(66, 20);
            this.MapFixOffsetTop.TabIndex = 2;
            this.MapFixOffsetTop.Text = "0";
            // 
            // MapFixOffsetLeft
            // 
            this.MapFixOffsetLeft.Location = new System.Drawing.Point(33, 76);
            this.MapFixOffsetLeft.Name = "MapFixOffsetLeft";
            this.MapFixOffsetLeft.Size = new System.Drawing.Size(66, 20);
            this.MapFixOffsetLeft.TabIndex = 2;
            this.MapFixOffsetLeft.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Тecтовые фyнкции:";
            // 
            // ShowCoordinates
            // 
            this.ShowCoordinates.Location = new System.Drawing.Point(478, 282);
            this.ShowCoordinates.Name = "ShowCoordinates";
            this.ShowCoordinates.Size = new System.Drawing.Size(89, 23);
            this.ShowCoordinates.TabIndex = 10;
            this.ShowCoordinates.Text = "Cоrdinаtes ->->";
            this.ShowCoordinates.UseVisualStyleBackColor = true;
            this.ShowCoordinates.Click += new System.EventHandler(this.ShowCoordinates_Click);
            // 
            // GameDllAddrShow
            // 
            this.GameDllAddrShow.AutoSize = true;
            this.GameDllAddrShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GameDllAddrShow.Location = new System.Drawing.Point(27, 296);
            this.GameDllAddrShow.Name = "GameDllAddrShow";
            this.GameDllAddrShow.Size = new System.Drawing.Size(76, 13);
            this.GameDllAddrShow.TabIndex = 9;
            this.GameDllAddrShow.Text = "0x00000000";
            // 
            // DebugLabel
            // 
            this.DebugLabel.AutoSize = true;
            this.DebugLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DebugLabel.Location = new System.Drawing.Point(51, 309);
            this.DebugLabel.Name = "DebugLabel";
            this.DebugLabel.Size = new System.Drawing.Size(66, 13);
            this.DebugLabel.TabIndex = 9;
            this.DebugLabel.Text = "0x00000000";
            // 
            // MainHack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 344);
            this.ControlBox = false;
            this.Controls.Add(this.ShowCoordinates);
            this.Controls.Add(this.DebugLabel);
            this.Controls.Add(this.GameDllAddrShow);
            this.Controls.Add(this.GameStatistic);
            this.Controls.Add(this.GameTime);
            this.Controls.Add(this.HIDEMENUBUTTON);
            this.Controls.Add(this.WarcraftVersion);
            this.Controls.Add(this.WarnWarnWarn);
            this.Controls.Add(this.SelectedGameProfileName);
            this.Controls.Add(this.ExitButton1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainHack";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainHack_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrawSpeed)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox DrawUnitsOnMainMap;
        private System.Windows.Forms.CheckBox DrawMinimapIngame;
        private System.Windows.Forms.CheckBox TransferGoldEnabled;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label GeneralFeatures;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label AdditionalFeatures;
        private System.Windows.Forms.Button ExitButton1;
        private System.Windows.Forms.Timer GoldTransferWatch;
        private System.Windows.Forms.Timer MinimapUpdateTimer;
        private System.Windows.Forms.CheckBox CameraDistance;
        private System.Windows.Forms.TextBox CameraDistText;
        private System.Windows.Forms.CheckBox DrawHeroes;
        private System.Windows.Forms.CheckBox DrawOther;
        private System.Windows.Forms.CheckBox DrawNeutrals;
        private System.Windows.Forms.CheckBox ColoredHeroes;
        private System.Windows.Forms.CheckBox ReadGameColorSettings;
        private System.Windows.Forms.CheckBox DrawAllUnits;
        private System.Windows.Forms.CheckBox DrawIllusions;
        private System.Windows.Forms.CheckBox NeedHideMMap;
        private System.Windows.Forms.Label SelectedGameProfileName;
        private System.Windows.Forms.TrackBar DrawSpeed;
        private System.Windows.Forms.Label SpeedDrawLabel;
        private System.Windows.Forms.CheckBox DrawHPMPMainMap;
        private System.Windows.Forms.Label WarnWarnWarn;
        private System.Windows.Forms.CheckBox DrawOnlyHeroesAtMinimap;
        private System.Windows.Forms.Timer CloseTimer;
        private System.Windows.Forms.CheckBox BypassAntihack;
        private System.Windows.Forms.Label WarcraftVersion;
        private System.Windows.Forms.Button HIDEMENUBUTTON;
        private System.Windows.Forms.CheckBox MH_AutoClose;
        private System.Windows.Forms.Label GameTime;
        private System.Windows.Forms.Label GameStatistic;
        private System.Windows.Forms.CheckBox All_items;
        private System.Windows.Forms.CheckBox All_allies;
        private System.Windows.Forms.CheckBox AutoDetectPlayerTeams;
        private System.Windows.Forms.CheckBox autodetectplayercolor;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox MapFixOffsetBot;
        private System.Windows.Forms.TextBox MapFixOffsetRight;
        private System.Windows.Forms.TextBox MapFixOffsetTop;
        private System.Windows.Forms.TextBox MapFixOffsetLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ShowCoordinates;
        private System.Windows.Forms.CheckBox DrawForceBadUnits;
        private System.Windows.Forms.CheckBox MiniMapEnabled;
        private System.Windows.Forms.Label GameDllAddrShow;
        private System.Windows.Forms.Label DebugLabel;
    }
}