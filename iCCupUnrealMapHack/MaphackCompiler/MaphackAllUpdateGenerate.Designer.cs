namespace MaphackCompiler
{
    partial class MaphackAllUpdateGenerate
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.HWID_DIR = new System.Windows.Forms.TextBox();
            this.PROJECTDIR = new System.Windows.Forms.TextBox();
            this.DEVENVPATH = new System.Windows.Forms.TextBox();
            this.MH_SOURCES_DIR = new System.Windows.Forms.TextBox();
            this.PROTECTPATH = new System.Windows.Forms.TextBox();
            this.START = new System.Windows.Forms.Button();
            this.TextToHWIDPath = new System.Windows.Forms.TextBox();
            this.OutTextPath = new System.Windows.Forms.TextBox();
            this.ProtectProject = new System.Windows.Forms.TextBox();
            this.ProtectedMHPath = new System.Windows.Forms.TextBox();
            this.RARPATH = new System.Windows.Forms.TextBox();
            this.ProtectedPathForPack = new System.Windows.Forms.TextBox();
            this.DirOutPack = new System.Windows.Forms.TextBox();
            this.ResultDirForPack = new System.Windows.Forms.TextBox();
            this.BuildedMaphackPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // HWID_DIR
            // 
            this.HWID_DIR.Location = new System.Drawing.Point(64, 23);
            this.HWID_DIR.Name = "HWID_DIR";
            this.HWID_DIR.Size = new System.Drawing.Size(560, 20);
            this.HWID_DIR.TabIndex = 0;
            this.HWID_DIR.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\testmyhwid\\";
            // 
            // PROJECTDIR
            // 
            this.PROJECTDIR.Location = new System.Drawing.Point(64, 205);
            this.PROJECTDIR.Name = "PROJECTDIR";
            this.PROJECTDIR.Size = new System.Drawing.Size(560, 20);
            this.PROJECTDIR.TabIndex = 1;
            this.PROJECTDIR.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\iCCupUnrealMapHack\\iCCupUnrealMapHack" +
    ".csproj";
            // 
            // DEVENVPATH
            // 
            this.DEVENVPATH.Location = new System.Drawing.Point(64, 49);
            this.DEVENVPATH.Name = "DEVENVPATH";
            this.DEVENVPATH.Size = new System.Drawing.Size(560, 20);
            this.DEVENVPATH.TabIndex = 1;
            this.DEVENVPATH.Text = "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\MS" +
    "Build.exe";
            this.DEVENVPATH.TextChanged += new System.EventHandler(this.DEVENVPATH_TextChanged);
            // 
            // MH_SOURCES_DIR
            // 
            this.MH_SOURCES_DIR.Location = new System.Drawing.Point(64, 153);
            this.MH_SOURCES_DIR.Name = "MH_SOURCES_DIR";
            this.MH_SOURCES_DIR.Size = new System.Drawing.Size(560, 20);
            this.MH_SOURCES_DIR.TabIndex = 1;
            this.MH_SOURCES_DIR.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\iCCupUnrealMapHack\\";
            // 
            // PROTECTPATH
            // 
            this.PROTECTPATH.Location = new System.Drawing.Point(64, 127);
            this.PROTECTPATH.Name = "PROTECTPATH";
            this.PROTECTPATH.Size = new System.Drawing.Size(560, 20);
            this.PROTECTPATH.TabIndex = 1;
            this.PROTECTPATH.Text = "c:\\Projects\\protectors\\ConfuserEx-master\\Confuser.CLI.exe";
            // 
            // START
            // 
            this.START.Location = new System.Drawing.Point(260, 387);
            this.START.Name = "START";
            this.START.Size = new System.Drawing.Size(75, 23);
            this.START.TabIndex = 2;
            this.START.Text = "button1";
            this.START.UseVisualStyleBackColor = true;
            this.START.Click += new System.EventHandler(this.START_Click);
            // 
            // TextToHWIDPath
            // 
            this.TextToHWIDPath.Location = new System.Drawing.Point(64, 75);
            this.TextToHWIDPath.Name = "TextToHWIDPath";
            this.TextToHWIDPath.Size = new System.Drawing.Size(560, 20);
            this.TextToHWIDPath.TabIndex = 1;
            this.TextToHWIDPath.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\Release\\ConvertHWIDtoTEXT.exe";
            // 
            // OutTextPath
            // 
            this.OutTextPath.Location = new System.Drawing.Point(64, 101);
            this.OutTextPath.Name = "OutTextPath";
            this.OutTextPath.Size = new System.Drawing.Size(560, 20);
            this.OutTextPath.TabIndex = 1;
            this.OutTextPath.Text = ".\\outtext.txt";
            // 
            // ProtectProject
            // 
            this.ProtectProject.Location = new System.Drawing.Point(61, 231);
            this.ProtectProject.Name = "ProtectProject";
            this.ProtectProject.Size = new System.Drawing.Size(560, 20);
            this.ProtectProject.TabIndex = 1;
            this.ProtectProject.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\Release\\IMH.crproj";
            // 
            // ProtectedMHPath
            // 
            this.ProtectedMHPath.Location = new System.Drawing.Point(61, 257);
            this.ProtectedMHPath.Name = "ProtectedMHPath";
            this.ProtectedMHPath.Size = new System.Drawing.Size(560, 20);
            this.ProtectedMHPath.TabIndex = 1;
            this.ProtectedMHPath.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\Release\\Confused\\Launcher.exe";
            // 
            // RARPATH
            // 
            this.RARPATH.Location = new System.Drawing.Point(61, 309);
            this.RARPATH.Name = "RARPATH";
            this.RARPATH.Size = new System.Drawing.Size(560, 20);
            this.RARPATH.TabIndex = 1;
            this.RARPATH.Text = "c:\\Program Files\\WinRAR\\Rar.exe";
            // 
            // ProtectedPathForPack
            // 
            this.ProtectedPathForPack.Location = new System.Drawing.Point(31, 283);
            this.ProtectedPathForPack.Name = "ProtectedPathForPack";
            this.ProtectedPathForPack.Size = new System.Drawing.Size(590, 20);
            this.ProtectedPathForPack.TabIndex = 1;
            this.ProtectedPathForPack.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\Release\\Confused\\MaphackRelease\\Unrea" +
    "lMH\\UnrealMapHack.exe";
            // 
            // DirOutPack
            // 
            this.DirOutPack.Location = new System.Drawing.Point(61, 335);
            this.DirOutPack.Name = "DirOutPack";
            this.DirOutPack.Size = new System.Drawing.Size(560, 20);
            this.DirOutPack.TabIndex = 1;
            this.DirOutPack.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\Release\\Confused\\";
            // 
            // ResultDirForPack
            // 
            this.ResultDirForPack.Location = new System.Drawing.Point(61, 361);
            this.ResultDirForPack.Name = "ResultDirForPack";
            this.ResultDirForPack.Size = new System.Drawing.Size(560, 20);
            this.ResultDirForPack.TabIndex = 1;
            this.ResultDirForPack.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\Release\\Confused\\MaphackRelease\\";
            // 
            // BuildedMaphackPath
            // 
            this.BuildedMaphackPath.Location = new System.Drawing.Point(61, 179);
            this.BuildedMaphackPath.Name = "BuildedMaphackPath";
            this.BuildedMaphackPath.Size = new System.Drawing.Size(560, 20);
            this.BuildedMaphackPath.TabIndex = 1;
            this.BuildedMaphackPath.Text = "c:\\Projects\\HackProjects\\iCCupUnrealMapHack\\Release\\Launcher.exe";
            // 
            // MaphackAllUpdateGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 422);
            this.Controls.Add(this.START);
            this.Controls.Add(this.DEVENVPATH);
            this.Controls.Add(this.TextToHWIDPath);
            this.Controls.Add(this.OutTextPath);
            this.Controls.Add(this.PROTECTPATH);
            this.Controls.Add(this.BuildedMaphackPath);
            this.Controls.Add(this.MH_SOURCES_DIR);
            this.Controls.Add(this.ResultDirForPack);
            this.Controls.Add(this.DirOutPack);
            this.Controls.Add(this.RARPATH);
            this.Controls.Add(this.ProtectedPathForPack);
            this.Controls.Add(this.ProtectedMHPath);
            this.Controls.Add(this.ProtectProject);
            this.Controls.Add(this.PROJECTDIR);
            this.Controls.Add(this.HWID_DIR);
            this.Name = "MaphackAllUpdateGenerate";
            this.Text = "Автоматическая сборка";
            this.Load += new System.EventHandler(this.MaphackAllUpdateGenerate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox HWID_DIR;
        private System.Windows.Forms.TextBox PROJECTDIR;
        private System.Windows.Forms.TextBox DEVENVPATH;
        private System.Windows.Forms.TextBox MH_SOURCES_DIR;
        private System.Windows.Forms.TextBox PROTECTPATH;
        private System.Windows.Forms.Button START;
        private System.Windows.Forms.TextBox TextToHWIDPath;
        private System.Windows.Forms.TextBox OutTextPath;
        private System.Windows.Forms.TextBox ProtectProject;
        private System.Windows.Forms.TextBox ProtectedMHPath;
        private System.Windows.Forms.TextBox RARPATH;
        private System.Windows.Forms.TextBox ProtectedPathForPack;
        private System.Windows.Forms.TextBox DirOutPack;
        private System.Windows.Forms.TextBox ResultDirForPack;
        private System.Windows.Forms.TextBox BuildedMaphackPath;
    }
}

