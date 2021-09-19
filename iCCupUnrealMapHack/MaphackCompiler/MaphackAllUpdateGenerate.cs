using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;


namespace MaphackCompiler
{
    public partial class MaphackAllUpdateGenerate : Form
    {
        public MaphackAllUpdateGenerate()
        {
            InitializeComponent();
        }
        //devenvC:\Projects\HackProjects\iCCupUnrealMapHack\iCCupUnrealMapHack.sln
        private void START_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(HWID_DIR.Text))
            {
                MessageBox.Show("Не правильная папка hwid");
            }


            string regex = @"\[.*?\[(.*?)\]";
            string[] files = Directory.GetFiles(HWID_DIR.Text);

            for (int n = 0; n < files.Length; n++)
            {
                string file = files[n];
                string[] HWIDData = File.ReadAllLines(file);
                string HWIDName = Path.GetFileNameWithoutExtension(file);

                Match GetHWIDrg = Regex.Match(HWIDData[1], regex);
                if (GetHWIDrg.Success)
                {
                    string hwidorg = GetHWIDrg.Groups[1].Value;
                    if (File.Exists(OutTextPath.Text))
                        File.Delete(OutTextPath.Text);


                    ProcessStartInfo psinfo = new ProcessStartInfo(TextToHWIDPath.Text, hwidorg);
                    psinfo.WindowStyle = ProcessWindowStyle.Hidden;
                    psinfo.UseShellExecute = false;
                    Process.Start(psinfo).WaitForExit();

                    if (File.Exists(OutTextPath.Text))
                    {


                        string hwidnew = File.ReadAllText(OutTextPath.Text);

                        if (!File.Exists(OutTextPath.Text + "_new.txt"))
                        {
                            File.Create(OutTextPath.Text + "_new.txt").Close();
                        }

                        File.AppendAllText(OutTextPath.Text + "_new.txt",  "-|" + hwidnew);


                        continue;
                    }
                }
            }
            //{

            //    {
            //        {


            //            if (File.Exists(MH_SOURCES_DIR.Text + "\\MainMenu.cs") &&
            //                File.Exists(MH_SOURCES_DIR.Text + "\\Minimaphack.cs") &&
            //                File.Exists(MH_SOURCES_DIR.Text + "\\MainHack.cs"))
            //            {
            //                try
            //                {
            //                    if (File.Exists(ProtectedMHPath.Text))
            //                    {
            //                        File.Delete(ProtectedMHPath.Text);
            //                    }
            //                }
            //                catch
            //                {
            //                    MessageBox.Show("Ошибка удаления mh");
            //                }
            //                try
            //                {
            //                    if (File.Exists(ProtectedPathForPack.Text))
            //                    {
            //                        File.Delete(ProtectedPathForPack.Text);
            //                    }
            //                }
            //                catch
            //                {
            //                    MessageBox.Show("Ошибка удаления mh2");
            //                }




            //                string getlicence = @"(.*?string.*?licencekey.*?\=.*?\"")(.*?)(\"".*)";
            //                string getusername = @"(.*?string.*?UserName.*?\=.*?\"")(.*?)(\"".*)";

            //                string[] MainMenuFile = File.ReadAllLines(MH_SOURCES_DIR.Text + "\\MainMenu.cs");
            //                string[] MainHackFile = File.ReadAllLines(MH_SOURCES_DIR.Text + "\\MainHack.cs");
            //                string[] MinimapFile = File.ReadAllLines(MH_SOURCES_DIR.Text + "\\Minimaphack.cs");



            //                for (int i = 0; i < 100; i++)
            //                {
            //                    Match GetLicenceRegx = Regex.Match(MainMenuFile[i], getlicence);
            //                    if (GetLicenceRegx.Success)
            //                    {
            //                        MainMenuFile[i] = Regex.Replace(MainMenuFile[i], getlicence, "${1}" + hwidnew + "${3}");

            //                        break;
            //                    }
            //                }

            //                for (int i = 0; i < 100; i++)
            //                {
            //                    Match GetLicenceRegx = Regex.Match(MainMenuFile[i], getusername);
            //                    if (GetLicenceRegx.Success)
            //                    {
            //                        MainMenuFile[i] = Regex.Replace(MainMenuFile[i], getusername, "${1}" + HWIDName + "${3}");

            //                        break;
            //                    }
            //                }



            //                for (int i = 0; i < 100; i++)
            //                {
            //                    Match GetLicenceRegx = Regex.Match(MainHackFile[i], getlicence);
            //                    if (GetLicenceRegx.Success)
            //                    {
            //                        MainHackFile[i] = Regex.Replace(MainHackFile[i], getlicence, "${1}" + hwidnew + "${3}");

            //                        break;
            //                    }
            //                }

            //                for (int i = 0; i < 100; i++)
            //                {
            //                    Match GetLicenceRegx = Regex.Match(MinimapFile[i], getlicence);
            //                    if (GetLicenceRegx.Success)
            //                    {
            //                        MinimapFile[i] = Regex.Replace(MinimapFile[i], getlicence, "${1}" + hwidnew + "${3}");

            //                        break;
            //                    }
            //                }

            //                File.WriteAllLines(MH_SOURCES_DIR.Text + "\\MainMenu.cs", MainMenuFile);
            //                File.WriteAllLines(MH_SOURCES_DIR.Text + "\\MainHack.cs", MainHackFile);
            //                File.WriteAllLines(MH_SOURCES_DIR.Text + "\\Minimaphack.cs", MinimapFile);



            //                try
            //                {
            //                    if (File.Exists(BuildedMaphackPath.Text))
            //                    {
            //                        File.Delete(BuildedMaphackPath.Text);
            //                    }
            //                }
            //                catch
            //                {
            //                    MessageBox.Show("Не могу удалить старую копию мх!");
            //                }



            //                psinfo = new ProcessStartInfo(DEVENVPATH.Text, PROJECTDIR.Text + @" /nologo /clp:NoSummary /p:Configuration=""Release"" /p:Platform=""x86"" /v:m /t:Rebuild");
            //                psinfo.WindowStyle = ProcessWindowStyle.Hidden;
            //                psinfo.UseShellExecute = false;


            //                Process.Start(psinfo).WaitForExit();

            //                psinfo = new ProcessStartInfo(PROTECTPATH.Text, ProtectProject.Text);
            //                psinfo.WindowStyle = ProcessWindowStyle.Hidden;
            //                psinfo.UseShellExecute = false;
            //                Process protectorproc = Process.Start(psinfo);




            //                bool found1 = false;

            //                while (true)
            //                {
            //                    Thread.Sleep(200);
            //                    if (File.Exists(ProtectedMHPath.Text))
            //                    {
            //                        Thread.Sleep(1000);
            //                        File.Move(ProtectedMHPath.Text, ProtectedPathForPack.Text);
            //                        break;
            //                    }
            //                }
            //                protectorproc.Kill();
            //                psinfo = new ProcessStartInfo(RARPATH.Text, "a -r -ep1 -m5 -p" + HWIDName + " " + DirOutPack.Text + "[" + (new Random().Next(0, 999)) + "]" + HWIDName + ".rar " + ResultDirForPack.Text + "*.*");
            //                psinfo.WindowStyle = ProcessWindowStyle.Hidden;
            //                psinfo.UseShellExecute = false;
            //                Process.Start(psinfo).WaitForExit();




            //            }
            //            else
            //            {
            //                MessageBox.Show("Error mh file not found!");
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Error Getting HWID TEXT!");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("error!");
            //    }
            //}

        }

        private void MaphackAllUpdateGenerate_Load(object sender, EventArgs e)
        {

        }

        private void DEVENVPATH_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
