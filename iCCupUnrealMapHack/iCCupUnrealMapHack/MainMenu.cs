using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Win32;
using IniParser;
using IniParser.Parser;
using IniParser.Model;
using System.Threading;
using IniParser.Exceptions;

namespace iCCupUnrealMapHack
{
    public partial class MainMenu : Form
    {
        //string licencekey2 = "";
        string UserName2 = "minimap2";
        string disablekey = "TestTest2TestTest3";


        WebClient webClient = new WebClient();

        public static string MinimapPath = ".\\map.bmp";


        public static string CameraDistancedll = "CameraDistance.dll";
        public static string CameraDistanceFunc = "SetCameraDistance";
        public static string ManaBardll = "MB.dll";
        public static string ManaberLogin = "Login";


        private string TryCrackText = "Обнаружена попытка взлома.\n Detected hacking attempt.\nАбсолютно все ваши данные отправлены разработчику\nдля предотвращения будущих попыток взлома.\nУдачи Вам.\nЕсли это ошибка то не пытайтесь запускать мх\nи обратитесь к разработчику.";
        private string TryCrackTitle = "Warning. Detected hacking attempt?";

        Process curprocess = null;

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        private static string GetRandomString()
        {
            string path = Path.GetRandomFileName();

            path = path.Replace("1", "a1");
            path = path.Replace("2", "a2");
            path = path.Replace("3", "a3");
            path = path.Replace("4", "a4");
            path = path.Replace("5", "a5");

            path = path.Replace(".", ""); // Remove period.
            path = path.Replace("\\", ""); // Remove period.
            path = path.Replace("/", ""); // Remove period.
            path = path.Replace("crack", "piDar"); // Remove period.
            path = path.Replace("crk", "suK"); // Remove period.
            path = path.Replace("clean", "su4ka"); // Remove period.
            path = path.Replace("recon", "piZdc"); // Remove period.
            path = path.Replace("test", "xHui"); // Remove period.
            path = path.Replace("unpack", "dermoT");

            if (File.Exists(".\\" + path + ".exe") || File.Exists(".\\" + path + ".exe"))
            {
                return GetRandomString();
            }


            return path;
        }


        void ChangeCurrentLocationMaphack()
        {
            string newpath = Path.GetDirectoryName(fullPath) + "//" + GetRandomString() + ".exe";
            File.Move(fullPath, newpath);
            Thread.Sleep(300);
            if (File.Exists(fullPath) && File.Exists(newpath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch
                {

                }
                try
                {
                    File.Delete(newpath);
                }
                catch
                {

                }
            }
        }



        public static bool KillBadProcesses()
        {
            try
            {
                ServiceInstaller.StopService("NPF");
            }
            catch
            {

            }

            try
            {
                ServiceInstaller.Uninstall("NPF");
            }
            catch
            {

            }



            foreach (Process proc in Process.GetProcesses())
            {
                try
                {
                    string FullPathToProc = proc.MainModule.FileName;
                    bool IsBadProcess = false;
                    foreach (string file2 in Directory.GetFiles(Path.GetDirectoryName(FullPathToProc)))
                    {
                        string file = Path.GetFileName(file2);

                        if (file.ToLower() == "d3dhook.dll".ToLower() || file.ToLower() == "d3dhook64.dll".ToLower() ||
                            file.ToLower() == "speedhack-i386.dll".ToLower() || file.ToLower() == "speedhack-x86_64.dll".ToLower() ||
                             file.ToLower() == "Cheat Engine.exe".ToLower())
                        {
                            IsBadProcess = true;
                        }
                        else
                        {
                            if (file.ToLower() == "kprocesshacker.sys".ToLower())
                            {
                                IsBadProcess = true;
                            }
                            else
                            {
                                if (file.ToLower() == "HTTPAnalyzer.chms".ToLower())
                                {
                                    IsBadProcess = true;
                                }
                                else
                                {

                                }

                            }


                        }
                    }
                    if (IsBadProcess)
                    {
                        proc.Kill();
                        MessageBox.Show("Извините но процесс " + proc.ProcessName + " был закрыт");
                        return true;
                    }
                }
                catch
                {

                }
            }
            return false;
        }
        public static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZqwerasdfzxcvtyuighjkbnmopl";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        string profiledirectorypath;

        public static string fullPath = null;
        public MainMenu()
        {

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };

            CameraDistancedll = GetRandomString() + RandomString(MainMenu.random.Next(3, 8)) + ".dll";
            ManaBardll = GetRandomString() + RandomString(MainMenu.random.Next(3, 8)) + ".dll";

            try
            {
                if (Registry.CurrentUser.OpenSubKey(@"Software\Blizzard Entertainment\WarCraft III\String").GetValue("userbnet") == null && Registry.CurrentUser.OpenSubKey(@"Software\Blizzard Entertainment\WarCraft III\String").GetValue("userlocal") == null)
                {
                    MessageBox.Show("Игровые данные не доступны.\nДля запуска мапхака выполните следующие действия:\n1. Войдите на сервер под своим ником\n2. выйдите с батлнета\n3. выйдите из игры кнопкой \"Выйти из игры\"\n\nТеперь попытайтесь запустить мапхак заново.", "Критическая ошибка!");
                    Application.Exit();
                    Environment.Exit(0);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Нет доступа к реестру. Дальшейшая работа не возможна.", "Критическая ошибка!");
                Application.Exit();
                Environment.Exit(0);
                return;
            }


            var process = Process.GetCurrentProcess();
            this.MouseDown += new MouseEventHandler(move_window); // binding the method to the event

            fullPath = process.MainModule.FileName;

            if (fullPath.Length < 6)
            {
                fullPath = Assembly.GetExecutingAssembly().CodeBase;
                fullPath = Path.GetFullPath(fullPath);
            }

            if (KillBadProcesses())
            {
                this.Enabled = false;

                SendSimpleMessage("Error code:111. MainH.", "User with HWID:" + GetFullHWID() + ".\r\nError code:111____.");
                MessageBox.Show(TryCrackText + "_1", TryCrackTitle);
                this.Close();
                Environment.Exit(0);
                Application.Exit();
            }

           
            foreach (string dir in Directory.GetDirectories(Directory.GetCurrentDirectory()))
            {
                string dirpath = Path.GetFileName(dir);
                if (dirpath[0] == '1' ||
                    dirpath[0] == '2' ||
                    dirpath[0] == '3' ||
                    dirpath[0] == '4' ||
                    dirpath[0] == '5'
                     )
                {
                    Directory.Delete(dir);
                }
            }



            try
            {
                Directory.Move(Directory.GetDirectories(Directory.GetCurrentDirectory())[0],
                    Directory.GetParent(Directory.GetDirectories(Directory.GetCurrentDirectory())[0]) + "\\" + RandomString(random.Next(13, 20))
                    );
            }
            catch
            {

            }
            
            profiledirectorypath = Directory.GetCurrentDirectory() + "\\" + Path.GetFileName(Directory.GetDirectories(Directory.GetCurrentDirectory())[0]);


            for (int i = 1; i <= 5; i++)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\" + i + GetRandomString());
            }

            if (Directory.GetDirectories(Directory.GetCurrentDirectory()).Length != 6)
            {
                //MessageBox.Show("Maphack folder corrupt. Reinstall maphack!\nПапка с мх повреждена переустановите мх.", "Warning! Внимание!");
                //return;
            }

            webClient.Encoding = Encoding.UTF8;
           // licencekey2 = webClient.DownloadString("http://*************/.txt");

            //if (licencekey2.IndexOf(disablekey) > -1)
           // {
           //     return;
           // }




            curprocess = Process.GetCurrentProcess();
          //  if (licencekey2.IndexOf(ProcessHelper.GetHash(ProcessHelper.ID())) > -1)
            //{
               // ChangeCurrentLocationMaphack();

                    InitializeComponent();
              
           // }
          //  else
            //{
            //    ChangeCurrentLocationMaphack();
            //    SendSimpleMessage("Error code:777. MainH.", "User old HWID:[" + ProcessHelper.GetHash(ProcessHelper.ID()) + ", " + UserName2 + "] transfer hack to HWID:" + GetFullHWID() + ".\r\nError code:777.");
            //    MessageBox.Show(TryCrackText + "8", TryCrackTitle);
          //  }

        }




        Random myrnd = MainMenu.random;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int LPAR);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);


        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;

        private void move_window(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }



        private void ExitButton_Click(object sender, EventArgs e)
        {
            ExitInfo exinf = new ExitInfo();
            exinf.ShowDialog();
            exinf.Dispose();
            try
            {
                foreach (string file in Directory.GetFiles(Path.GetDirectoryName(fullPath), "*.dll"))
                {
                    try
                    {
                       // File.Delete(file);
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {


            }
            try
            {
                Application.Exit();
            }
            catch
            {
                Environment.Exit(0);
            }
        }

        private void StartMinimapHack_Click(object sender, EventArgs e)
        {

           // string idstr = ProcessHelper.ID();

            //if (licencekey2.IndexOf(ProcessHelper.GetHash(ProcessHelper.ID())) > -1)
            {
                MessageBox.Show("For close minimap : double click\r\nFor move window, press and drag Right Mouse!\nFor resize use Left Mouse at corners");
                MaphackProfileStruct selectedmhprofileformaphack = new MaphackProfileStruct();
                if (selectedprofile < MaphackProfileStructListLength)
                    selectedmhprofileformaphack = MaphackProfileStructList[selectedprofile];
                Minimaphack minihack = null;

                selectedmhprofileformaphack.IsTransparentMinimap = this.MinihackTransparent.Checked;
                selectedmhprofileformaphack.IsEnemyOnly = this.MinihackOnlyEnemy.Checked;
                selectedmhprofileformaphack.IsFastRedraw = this.FastDrawMinimap.Checked;
                selectedmhprofileformaphack.IsClickabledWithCTRL = this.NotClicabledMinihackTrick.Checked;


                selectedmhprofileformaphack.IsNoNeutrals = this.IsMinimapNoNeutrals.Checked;
                selectedmhprofileformaphack.IsNoTowers = this.IsMinimapNoTower.Checked;
                selectedmhprofileformaphack.IsAnimatedMinihackHeroes = this.AnimatedMinihackHeroes.Checked;

                selectedmhprofileformaphack.IsSkipMinimapSettingsSave = this.SkipMinimapSettingsSave.Checked;


                selectedmhprofileformaphack.ignoreplayers = new bool[17];
                selectedmhprofileformaphack.ignoreplayers[0] = this.ignore_player_1.Checked;
                selectedmhprofileformaphack.ignoreplayers[1] = this.ignore_player_2.Checked;
                selectedmhprofileformaphack.ignoreplayers[2] = this.ignore_player_3.Checked;
                selectedmhprofileformaphack.ignoreplayers[3] = this.ignore_player_4.Checked;
                selectedmhprofileformaphack.ignoreplayers[4] = this.ignore_player_5.Checked;
                selectedmhprofileformaphack.ignoreplayers[5] = this.ignore_player_6.Checked;
                selectedmhprofileformaphack.ignoreplayers[6] = this.ignore_player_7.Checked;
                selectedmhprofileformaphack.ignoreplayers[7] = this.ignore_player_8.Checked;
                selectedmhprofileformaphack.ignoreplayers[8] = this.ignore_player_9.Checked;
                selectedmhprofileformaphack.ignoreplayers[9] = this.ignore_player_10.Checked;
                selectedmhprofileformaphack.ignoreplayers[10] = this.ignore_player_11.Checked;
                selectedmhprofileformaphack.ignoreplayers[11] = this.ignore_player_12.Checked;
                selectedmhprofileformaphack.ignoreplayers[12] = this.ignore_player_13.Checked;
                selectedmhprofileformaphack.ignoreplayers[13] = this.ignore_player_14.Checked;
                selectedmhprofileformaphack.ignoreplayers[14] = this.ignore_player_15.Checked;


                minihack = new Minimaphack(selectedmhprofileformaphack);
           
     
                this.Visible = false;
                this.Enabled = false;
                try
                {
                    File.Delete("temp.bmp");
                }
                catch
                {

                }

                minihack.ShowDialog();

                this.Visible = true;
                this.Enabled = true;

                minihack.Dispose();
            }
        }

        public static List<Image> GetImageFrames(Image originalImg)
        {
            if (originalImg == null)
            {
                Debug.WriteLine("Image not found");
                return null;
            }
            Debug.WriteLine("Image found");
            List<Image> images = new List<Image>();
            Bitmap bitmap = originalImg as Bitmap;
            Debug.WriteLine("Start get frames");
            try
            {
                int count = bitmap.GetFrameCount(FrameDimension.Time);

                Debug.WriteLine("Frames {0}", count);
                for (int idx = 0; idx < count; idx++)
                {
                    Debug.WriteLine("Read frame {0}", idx);
                    // save each frame to a bytestream
                    bitmap.SelectActiveFrame(FrameDimension.Time, idx);
                    Debug.WriteLine("Read....", idx);
                    MemoryStream byteStream = new MemoryStream();
                    Debug.WriteLine("Read2....", idx);
                    bitmap.Save(byteStream, ImageFormat.Png);
                    Debug.WriteLine("Read3....", idx);
                    // and then create a new Image from it
                    images.Add(Image.FromStream(byteStream));
                    Debug.WriteLine("Read4....", idx);
                }
            }
            catch
            {
                return null;
            }
            return images;
        }

        //public static Image[] GetImageFrames(Image originalImg)
        //{

        //    int numberOfFrames = originalImg.GetFrameCount(FrameDimension.Time);

        //    Image[] frames = new Image[numberOfFrames];



        //    for (int i = 0; i < numberOfFrames; i++)
        //    {

        //        originalImg.SelectActiveFrame(FrameDimension.Time, i);

        //        frames[i] = ((Image)originalImg.Clone());

        //    }



        //    return frames;

        //}


        private void DrawRandomPixAtBitmap(ref Bitmap bmp)
        {
            int x = bmp.Width - 1;
            int y = bmp.Height - 1;
            y = myrnd.Next(0, y);
            x = myrnd.Next(0, x);
            bmp.SetPixel(x, y, Color.FromArgb(myrnd.Next(255), myrnd.Next(255), myrnd.Next(255)));
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.Size = new Size(this.Width + ProcessHelper.rnd.Next(2, 35), this.Height + ProcessHelper.rnd.Next(1, 25));
           
                    /*try
                    {
                        WebRequest.DefaultWebProxy = null;
                    }
                    catch
                    {

                    }

                    int allbad = 0;



                    if (allbad != 0)
                    {
                        File.WriteAllText(".\\badx.log", "Нeт доступа к интернету[" + allbad + "]. Дальнейшая работа не возможна.");
                        this.Enabled = false;
                        this.Visible = false;
                        this.Close();
                        return;
                    }
                    */

                    try
                    {
                Syringe.Injector war3inject = new Syringe.Injector(Process.GetProcessesByName("war3")[0]);
                war3inject.EjectLibraryXXX(MainMenu.CameraDistancedll);
            }
            catch
            {
                Debug.WriteLine("No war3 found or no CameraHack found.");
            }

            try
            {
                Syringe.Injector war3inject = new Syringe.Injector(Process.GetProcessesByName("war3")[0]);
                war3inject.EjectLibraryXXX(MainMenu.ManaBardll);
            }
            catch
            {
                Debug.WriteLine("No war3 found or no ManaBardll found.");
            }
            
            string procname = Process.GetCurrentProcess().ProcessName;
            int currentprocid = Process.GetCurrentProcess().Id;
            int processes = Process.GetProcessesByName(procname).Length;
            if (processes > 1)
            {
                MessageBox.Show("Вероятно обнаружен запущенный мапхак", "Внимание!", MessageBoxButtons.OK);
                Process.GetCurrentProcess().Kill();
            }

            Bitmap minihackbg = (Bitmap)StartMinimapHack.BackgroundImage;
            Bitmap diagbg = (Bitmap)DiagnozBtn.BackgroundImage;
            Bitmap exitbg = (Bitmap)ExitButton.BackgroundImage;
            Bitmap justbg = (Bitmap)this.BackgroundImage;
            
            Bitmap arrleft = (Bitmap)LeftProfileArrow.BackgroundImage;
            Bitmap arrright = (Bitmap)RightProfileArrow.BackgroundImage;
            Bitmap slprofilebg = new Bitmap(TSelectedProfile.Width, TSelectedProfile.Height);

            DrawRandomPixAtBitmap(ref minihackbg);
            DrawRandomPixAtBitmap(ref diagbg);
            DrawRandomPixAtBitmap(ref exitbg);
            DrawRandomPixAtBitmap(ref justbg);
            DrawRandomPixAtBitmap(ref arrleft);
            DrawRandomPixAtBitmap(ref arrright);
            DrawRandomPixAtBitmap(ref slprofilebg);
            
            LeftProfileArrow.BackgroundImage = arrleft;
            RightProfileArrow.BackgroundImage = arrright;
            TSelectedProfile.BackgroundImage = slprofilebg;
            StartMinimapHack.BackgroundImage = minihackbg;
            DiagnozBtn.BackgroundImage = diagbg;
            ExitButton.BackgroundImage = exitbg;
            this.BackgroundImage = justbg;
            
            LeftProfileArrow.Left = LeftProfileArrow.Left + myrnd.Next(-3, 3);
            LeftProfileArrow.Top = LeftProfileArrow.Top + myrnd.Next(-3, 3);
            LeftProfileArrow.Width = LeftProfileArrow.Width + myrnd.Next(-3, 3);
            LeftProfileArrow.Height = LeftProfileArrow.Height + myrnd.Next(-3, 3);

            RightProfileArrow.Left = RightProfileArrow.Left + myrnd.Next(-3, 3);
            RightProfileArrow.Top = RightProfileArrow.Top + myrnd.Next(-3, 3);
            RightProfileArrow.Width = RightProfileArrow.Width + myrnd.Next(-3, 3);
            RightProfileArrow.Height = RightProfileArrow.Height + myrnd.Next(-3, 3);
            
            TSelectedProfile.Left = TSelectedProfile.Left + myrnd.Next(-3, 3);
            TSelectedProfile.Top = TSelectedProfile.Top + myrnd.Next(-3, 3);
            TSelectedProfile.Width = TSelectedProfile.Width + myrnd.Next(-3, 3);
            TSelectedProfile.Height = TSelectedProfile.Height + myrnd.Next(-3, 3);


            StartMinimapHack.Left = StartMinimapHack.Left + myrnd.Next(-3, 3);
            StartMinimapHack.Top = StartMinimapHack.Top + myrnd.Next(-3, 3);
            StartMinimapHack.Width = StartMinimapHack.Width + myrnd.Next(-3, 3);
            StartMinimapHack.Height = StartMinimapHack.Height + myrnd.Next(-3, 3);
            

            DiagnozBtn.Left = DiagnozBtn.Left + myrnd.Next(-3, 3);
            DiagnozBtn.Top = DiagnozBtn.Top + myrnd.Next(-3, 3);
            DiagnozBtn.Width = DiagnozBtn.Width + myrnd.Next(-3, 3);
            DiagnozBtn.Height = DiagnozBtn.Height + myrnd.Next(-3, 3);


            ExitButton.Left = ExitButton.Left + myrnd.Next(-3, 3);
            ExitButton.Top = ExitButton.Top + myrnd.Next(-3, 3);
            ExitButton.Width = ExitButton.Width + myrnd.Next(-3, 3);
            ExitButton.Height = ExitButton.Height + myrnd.Next(-3, 3);




            MaphackProfileStructList = new MaphackProfileStruct[256];




            if (Directory.Exists(profiledirectorypath))
            {
                foreach (string profdir in Directory.GetDirectories(profiledirectorypath))
                {

                    Image[] TempImageFramesForDef = new Image[] { Properties.Resources.DefaultHeroIcon };


                    MaphackProfileStruct tmpprofilestructure = new MaphackProfileStruct();
                    tmpprofilestructure.profdir = profdir;
                    tmpprofilestructure.IsTransparentMinimap = false;
                    tmpprofilestructure.IsEnemyOnly = false;
                    tmpprofilestructure.IsFastRedraw = false;
                    tmpprofilestructure.IsClickabledWithCTRL = false;
                    tmpprofilestructure.IsNoNeutrals = false;
                    tmpprofilestructure.ignoreplayers = new bool[0];
                    tmpprofilestructure.IsNoTowers = false;
                    tmpprofilestructure.IsAnimatedMinihackHeroes = false;
                    tmpprofilestructure.IsSkipMinimapSettingsSave = false;

                    tmpprofilestructure.Map_BOT_offset = 0;
                    tmpprofilestructure.Map_LEFT_offset = 0;
                    tmpprofilestructure.Map_RIGHT_offset = 0;
                    tmpprofilestructure.Map_TOP_offset = 0;
                    tmpprofilestructure.HERO_X_OFFSET = 0;
                    tmpprofilestructure.HERO_Y_OFFSET = 0;
                    tmpprofilestructure.DefaultIconScale = 1.0f;
                    tmpprofilestructure.orgminimap = new List<Bitmap>( );
                    tmpprofilestructure.DrawZoomOut = 0;
                    tmpprofilestructure.DrawAccurate = DrawQuality.Normal;
                    string profilename = Path.GetFileName(profdir);
                    List<War3ObjectDraw> unitsobjects = new List<War3ObjectDraw>();
                    List<War3ObjectDraw> itemsobjects = new List<War3ObjectDraw>();

                    List<TeamStruct> teamlist = new List<TeamStruct>();

                    List<PlayerInfo> PlayerColorList = new List<PlayerInfo>();

                    tmpprofilestructure.heroaqua = TempImageFramesForDef;
                    tmpprofilestructure.herowhite = TempImageFramesForDef;
                    tmpprofilestructure.herored = TempImageFramesForDef;


                    tmpprofilestructure.heroaquaframe = 0;
                    tmpprofilestructure.heroredframe = 0;
                    tmpprofilestructure.herowhiteframe = 0;
                    tmpprofilestructure.mphpwhitelist = new List<int>();

                    Debug.WriteLine("Start read config");

                    //ItemsRead
                    {
                        try
                        {
                            var parser = new FileIniDataParser();
                            IniData data = parser.ReadFile(profdir + "\\items.ini");

                            foreach (SectionData sect in data.Sections)
                            {
                                foreach (KeyData keys in sect.Keys)
                                {

                                    //File.AppendAllText("DumpItems.txt", sect.SectionName + "->" + keys.KeyName + " = " + keys.Value + "\n");

                                    try
                                    {
                                        War3ObjectDraw tmpdrawobj = new War3ObjectDraw();
                                        tmpdrawobj.name = keys.KeyName;
                                        tmpdrawobj.frameid = 0;
                                        tmpdrawobj.framecount = 0;
                                        tmpdrawobj.frames = null;
                                        tmpdrawobj.image = (Bitmap)Image.FromFile(profdir + "\\" + keys.Value);

                                        try
                                        {
                                            var framesofobject = GetImageFrames((Image)tmpdrawobj.image);
                                            if (framesofobject != null && framesofobject.Count > 1)
                                            {
                                                tmpdrawobj.framecount = framesofobject.Count;
                                                tmpdrawobj.frames = framesofobject;
                                            }
                                        }
                                        catch
                                        {
                                            Debug.WriteLine("Error read frames from image");
                                        }

                                        itemsobjects.Add(tmpdrawobj);
                                    }
                                    catch
                                    {
                                        Debug.WriteLine("Error in adding War3Object");
                                    }
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("ItemFileLoadError");
                            Application.Exit();
                            Environment.Exit(0);
                        }
                    }


                    //UnitsRead
                    {
                        try
                        {
                            var parser = new FileIniDataParser();
                            IniData data = parser.ReadFile(profdir + "\\units.ini");

                            foreach (SectionData sect in data.Sections)
                            {
                                foreach (KeyData keys in sect.Keys)
                                {
                                    
                                    try
                                    {
                                        War3ObjectDraw tmpdrawobj = new War3ObjectDraw();
                                        tmpdrawobj.name = keys.KeyName;
                                        tmpdrawobj.frameid = 0;
                                        tmpdrawobj.framecount = 0;
                                        tmpdrawobj.frames = null;
                                        tmpdrawobj.image = (Bitmap)Image.FromFile(profdir + "\\" + keys.Value);
                                        //File.AppendAllText("DumpUnits.txt", sect.SectionName + "->" + keys.KeyName + " = " + profdir + "\\" + keys.Value + "\n");

                                        try
                                        {
                                            var framesofobject = GetImageFrames((Image)tmpdrawobj.image);
                                            if (framesofobject != null && framesofobject.Count > 1)
                                            {
                                                tmpdrawobj.framecount = framesofobject.Count;
                                                tmpdrawobj.frames = framesofobject;
                                            }
                                        }
                                        catch
                                        {
                                            Debug.WriteLine("Error read frames from image 2");
                                        }


                                        unitsobjects.Add(tmpdrawobj);
                                    }
                                    catch
                                    {
                                        Debug.WriteLine("Error in adding War3Object 2");
                                    }
                                }
                            }
                        }
                        catch
                        {
                            MessageBox.Show("UnitFileLoadError");
                            Application.Exit();
                            Environment.Exit(0);
                        }
                    }


                    //TeamsRead
                    {
                        try
                        {
                            var parser = new FileIniDataParser();
                            IniData data = parser.ReadFile(profdir + "\\teams.ini");

                            foreach (SectionData sect in data.Sections)
                            {
                                try
                                {
                                    TeamStruct tempteam = new TeamStruct();
                                    List<int> playerids = new List<int>();
                                    foreach (KeyData keys in sect.Keys)
                                    {
                                        string playeridstr = keys.KeyName.Remove(0, 6);
                                        int curplayerid = int.Parse(playeridstr);
                                        playerids.Add(curplayerid);
                                    }
                                    if (playerids.Count > 0)
                                    {
                                        tempteam.playerids = playerids.ToArray();
                                        teamlist.Add(tempteam);
                                    }
                                }
                                catch
                                {
                                    Debug.WriteLine("Error read teams 1");
                                }
                            }
                        }
                        catch
                        {
                            Debug.WriteLine("Error read teams 2");
                        }
                    }

                    //PlayerColorRead
                    {
                        try
                        {
                            var parser = new FileIniDataParser();
                            IniData data = parser.ReadFile(profdir + "\\playercolors.ini");

                            foreach (SectionData sect in data.Sections)
                            {
                                try
                                {
                                    string playeridstr = sect.SectionName.Remove(0, 6);
                                    int curplayerid = int.Parse(playeridstr);
                                    PlayerInfo CurrentPlayerColor = new PlayerInfo();
                                    CurrentPlayerColor.herocurrentframe = 0;
                                    CurrentPlayerColor.playerid = curplayerid;
                                    foreach (KeyData keys in sect.Keys)
                                    {
                                        if (keys.KeyName.ToLower() == "red".ToLower())
                                        {
                                            CurrentPlayerColor.red = int.Parse(keys.Value);
                                        }
                                        else if (keys.KeyName.ToLower() == "green".ToLower())
                                        {
                                            CurrentPlayerColor.green = int.Parse(keys.Value);
                                        }
                                        else if (keys.KeyName.ToLower() == "blue".ToLower())
                                        {
                                            CurrentPlayerColor.blue = int.Parse(keys.Value);
                                        }

                                    }
                                    CurrentPlayerColor.heroframes = new Image[] { GetHeroImageForPlayerForDefaultImage(Properties.Resources.DefaultHeroIcon, CurrentPlayerColor) };
                                    PlayerColorList.Add(CurrentPlayerColor);

                                }
                                catch
                                {
                                    Debug.WriteLine("Error read players 1");
                                }
                            }
                        }
                        catch
                        {
                            Debug.WriteLine("Error read players 2");
                        }
                    }

                    if (PlayerColorList.Count < 12)
                    {
                        if (PlayerColorList.Count != 0)
                        {
                            MessageBox.Show("playercolors error. Bad player count. Need more that 11");
                            PlayerColorList.Clear();
                        }

                        for (int i = 0; i < 12; i++)
                        {
                            int playerid = i;
                            tmpprofilestructure.playercolors = PlayerColorList.ToArray();
                            Color playercolor = GetColorByPlayerSlot(playerid, tmpprofilestructure);
                            PlayerInfo tmpplinfo = new PlayerInfo();
                            tmpplinfo.playerid = playerid;
                            tmpplinfo.red = playercolor.R;
                            tmpplinfo.green = playercolor.G;
                            tmpplinfo.blue = playercolor.B;
                            tmpplinfo.herocurrentframe = 0;


                            tmpplinfo.heroframes = new Image[] { GetHeroImageForPlayerForDefaultImage(Properties.Resources.DefaultHeroIcon, tmpplinfo) };
                            PlayerColorList.Add(tmpplinfo);
                        }
                    }
                    //tmpprofilestructure.mphpwhitelist

                    //mphpwhitelist
                    try
                    {
                        if (File.Exists(profdir + "\\mphpwhitelist.ini"))
                        {
                            string[] lines = File.ReadAllLines(profdir + "\\mphpwhitelist.ini");
                            foreach (string s in lines)
                            {
                                tmpprofilestructure.mphpwhitelist.Add(BitConverter.ToInt32(Encoding.UTF8.GetBytes(s), 0));
                            }
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("Error read whitelist mp hp");
                    }

                    // Mapconfig
                    {
                        try
                        {
                            var parser = new FileIniDataParser();
                            IniData data = parser.ReadFile(profdir + "\\mapconfig.ini");

                            foreach (SectionData sect in data.Sections)
                            {
                                try
                                {

                                    foreach (KeyData keys in sect.Keys)
                                    {
                                        try
                                        {
                                            if (keys.KeyName.ToLower() == "LEFT_OFFSET".ToLower())
                                            {
                                                tmpprofilestructure.Map_LEFT_offset = int.Parse(keys.Value);
                                            }
                                            else if (keys.KeyName.ToLower() == "TOP_OFFSET".ToLower())
                                            {
                                                tmpprofilestructure.Map_TOP_offset = int.Parse(keys.Value);
                                            }
                                            else if (keys.KeyName.ToLower() == "RIGHT_OFFSET".ToLower())
                                            {
                                                tmpprofilestructure.Map_RIGHT_offset = int.Parse(keys.Value);
                                            }
                                            else if (keys.KeyName.ToLower() == "BOT_OFFSET".ToLower())
                                            {
                                                tmpprofilestructure.Map_BOT_offset = int.Parse(keys.Value);
                                            }
                                        }
                                        catch
                                        {
                                            MessageBox.Show(profilename + ": Error loading map offsets.");
                                        }

                                        try
                                        {
                                            if (keys.KeyName.ToLower() == "HERO_X_OFFSET".ToLower())
                                            {
                                                tmpprofilestructure.HERO_X_OFFSET = int.Parse(keys.Value);
                                            }
                                            else if (keys.KeyName.ToLower() == "HERO_Y_OFFSET".ToLower())
                                            {
                                                tmpprofilestructure.HERO_Y_OFFSET = int.Parse(keys.Value);
                                            }
                                        }
                                        catch
                                        {
                                            MessageBox.Show(profilename + ": Error loading hero offsets.");
                                        }


                                        if (keys.KeyName.ToLower().IndexOf("Map_Original_minimap".ToLower()) > -1)
                                        {
                                            try
                                            {
                                                string minimaprofilepath = profdir + "\\" + keys.Value;
                                                Image myprofminimap = Image.FromFile(minimaprofilepath);

                                                try
                                                {
                                                    if (myprofminimap.RawFormat != ImageFormat.Bmp)
                                                    {
                                                        File.Delete(minimaprofilepath);
                                                        myprofminimap.Save(MinimapPath, ImageFormat.Bmp);
                                                        myprofminimap = Image.FromFile(minimaprofilepath);
                                                        File.Delete(minimaprofilepath);
                                                    }
                                                }
                                                catch
                                                {
                                                    Debug.WriteLine("Error read mapconfig 1");
                                                    try
                                                    {
                                                        myprofminimap = Image.FromFile(minimaprofilepath);
                                                    }
                                                    catch
                                                    {
                                                        Debug.WriteLine("Error read mapconfig 2");
                                                    }
                                                }

                                                tmpprofilestructure.orgminimap.Add(myprofminimap as Bitmap);
                                            }
                                            catch
                                            {
                                                MessageBox.Show(profilename + ": Error loading original minimap.");
                                            }
                                        }
                                        else if (keys.KeyName.ToLower() == "InitialImagesZoomOut".ToLower())
                                        {
                                            try
                                            {
                                                int tmpDrawZoomOut = int.Parse(keys.Value);
                                                if (tmpDrawZoomOut < 1)
                                                {
                                                    throw new System.Exception();
                                                }
                                                tmpprofilestructure.DrawZoomOut = tmpDrawZoomOut;
                                            }
                                            catch
                                            {
                                                MessageBox.Show("InitialImagesZoomOut error: 'RUKI IZ JOPI' DETECTED!");
                                            }
                                        }
                                        else if (keys.KeyName.ToLower() == "DrawAccurate".ToLower())
                                        {
                                            if (keys.Value.ToLower() == "fast")
                                            {
                                                tmpprofilestructure.DrawAccurate = DrawQuality.Fast;
                                            }
                                            else if (keys.Value.ToLower() == "normal")
                                            {
                                                tmpprofilestructure.DrawAccurate = DrawQuality.Normal;
                                            }
                                            else if (keys.Value.ToLower() == "accurate")
                                            {
                                                tmpprofilestructure.DrawAccurate = DrawQuality.Accurate;
                                            }
                                        }
                                        else if (keys.KeyName.ToLower() == "DefaultScale".ToLower())
                                        {
                                            try
                                            {
                                                int tmpDrawZoomOut = int.Parse(keys.Value);
                                                if (tmpDrawZoomOut < 1)
                                                {
                                                    throw new System.Exception();
                                                }
                                                tmpprofilestructure.DefaultIconScale = (float)tmpDrawZoomOut / 100.0f;
                                            }
                                            catch
                                            {
                                                MessageBox.Show("DefaultScale error: 'RUKI IZ JOPI' DETECTED!");
                                            }
                                        }
                                        else if (keys.KeyName.ToLower() == "Hotkey".ToLower())
                                        {
                                            try
                                            {
                                                uint tmpDrawZoomOut = uint.Parse(keys.Value);
                                                tmpprofilestructure.ActiveHotKey = tmpDrawZoomOut;
                                            }
                                            catch
                                            {
                                                MessageBox.Show("Hotkey error: 'RUKI IZ JOPI' DETECTED!");
                                            }
                                        }
                                        else if (keys.KeyName.ToLower() == "ChangeMinimapHotkey".ToLower())
                                        {
                                            try
                                            {
                                                uint tmpDrawZoomOut = uint.Parse(keys.Value);
                                                tmpprofilestructure.ChangeMinimapHotkey = tmpDrawZoomOut;
                                            }
                                            catch
                                            {
                                                MessageBox.Show("Hotkey error: 'RUKI IZ JOPI' DETECTED!");
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    Debug.WriteLine("Error read mapconfig 3");
                                }
                            }
                        }
                        catch
                        {
                            Debug.WriteLine("Error read mapconfig 4");
                        }
                    }

                    // map config : icons

                    List<Image> HeroFrames = new List<Image>();

                    {
                        try
                        {
                            var parser = new FileIniDataParser();
                            IniData data = parser.ReadFile(profdir + "\\mapconfig.ini");

                            foreach (SectionData sect in data.Sections)
                            {
                                try
                                {

                                    foreach (KeyData keys in sect.Keys)
                                    {

                                        if (keys.KeyName.ToLower() == "HeroCustomIcon".ToLower())
                                        {
                                            try
                                            {


                                                HeroFrames.Add((Bitmap)Image.FromFile(profdir + "\\" + keys.Value));

                                                try
                                                {
                                                    var framesofobject = GetImageFrames((Image)HeroFrames[0]);
                                                    if (framesofobject != null && framesofobject.Count > 1)
                                                    {
                                                        HeroFrames = framesofobject;
                                                    }

                                                   // MessageBox.Show("Unitsframecount:" + HeroFrames.Count.ToString());
                                                }
                                                catch
                                                {
                                                    Debug.WriteLine("Error read mapicons 1");
                                                }





                                            }
                                            catch
                                            {
                                                Debug.WriteLine("Error read mapicons 2");
                                            }
                                        }

                                    }
                                }
                                catch
                                {
                                    Debug.WriteLine("Error read mapicons 3");
                                }
                            }
                        }
                        catch
                        {
                            Debug.WriteLine("Error read mapicons 4");
                        }
                    }

                    if (HeroFrames.Count > 0)
                    {

                        tmpprofilestructure.heroaqua = HeroFrames.ToArray();
                        tmpprofilestructure.herowhite = HeroFrames.ToArray();
                        tmpprofilestructure.herored = HeroFrames.ToArray();

                        for (int i = 0; i < PlayerColorList.Count; i++)
                        {
                            PlayerInfo NewPlayerInfoStruct = PlayerColorList[i];
                            List<Image> NewHeroFrames = new List<Image>();
                            foreach (Image img in HeroFrames)
                            {
                                NewHeroFrames.Add(GetHeroImageForPlayer(new Bitmap(img), NewPlayerInfoStruct));
                            }


                            NewPlayerInfoStruct.heroframes = NewHeroFrames.ToArray();
                            PlayerColorList[i] = NewPlayerInfoStruct;
                        }

                    }



                    for (int i = 0; i < tmpprofilestructure.heroaqua.Length; i++)
                    {
                        PlayerInfo tmpPlayerInfo = new PlayerInfo();
                        tmpPlayerInfo.playerid = -1;
                        tmpPlayerInfo.red = Color.Aqua.R;
                        tmpPlayerInfo.green = Color.Aqua.G;
                        tmpPlayerInfo.blue = Color.Aqua.B;
                        tmpprofilestructure.heroaqua[i] = GetHeroImageForPlayer(new Bitmap(tmpprofilestructure.heroaqua[i]), tmpPlayerInfo);
                    }

                    for (int i = 0; i < tmpprofilestructure.herored.Length; i++)
                    {
                        PlayerInfo tmpPlayerInfo = new PlayerInfo();
                        tmpPlayerInfo.playerid = -1;
                        tmpPlayerInfo.red = 255;
                        tmpPlayerInfo.green = 0;
                        tmpPlayerInfo.blue = 0;
                        tmpprofilestructure.herored[i] = GetHeroImageForPlayer(new Bitmap(tmpprofilestructure.herored[i]), tmpPlayerInfo);
                    }

                    for (int i = 0; i < tmpprofilestructure.herowhite.Length; i++)
                    {
                        PlayerInfo tmpPlayerInfo = new PlayerInfo();
                        tmpPlayerInfo.playerid = -1;
                        tmpPlayerInfo.red = 255;
                        tmpPlayerInfo.green = 255;
                        tmpPlayerInfo.blue = 255;
                        tmpprofilestructure.herowhite[i] = GetHeroImageForPlayer(new Bitmap(tmpprofilestructure.herowhite[i]), tmpPlayerInfo);
                    }


                    tmpprofilestructure.NAME = profilename;
                    tmpprofilestructure.teams = teamlist.ToArray();
                    tmpprofilestructure.playercolors = PlayerColorList.ToArray();

                    tmpprofilestructure.items = itemsobjects.ToArray();
                    tmpprofilestructure.units = unitsobjects.ToArray();
                    MaphackProfileStructList[MaphackProfileStructListLength]= tmpprofilestructure;
                    MaphackProfileStructListLength++;

                }
                ChangeProfile();
            }

            //if (!File.Exists(MainMenu.MinimapPath))
            //{
            //    File.WriteAllBytes(MainMenu.MinimapPath, Properties.Resources.war3minimap);
            //}

            bool localhost = false;
            try
            {
                string getbnetname = (string)Registry.CurrentUser.OpenSubKey(@"Software\Blizzard Entertainment\WarCraft III\String").GetValue("userbnet");

                if (getbnetname == null)
                {
                    localhost = true;
                    getbnetname = (string)Registry.CurrentUser.OpenSubKey(@"Software\Blizzard Entertainment\WarCraft III\String").GetValue("userlocal");
                }

                if (getbnetname.ToLower().IndexOf("iccup.".ToLower()) > -1)
                {
                    if (localhost)
                        getbnetname = "[LOCALHOST]:[" + getbnetname + "]";
                    if (!File.Exists(".\\check1for.hide"))
                    {
                        File.Create(".\\check1for.hide").Close();
                        SendSimpleMessage("Error code:26. MainH.", "User [name:" + getbnetname + "] with HWID:" + GetFullHWID() + ".\r\nError code:26.");
                    }
                }
                else if (getbnetname.ToLower().IndexOf("fukk".ToLower()) == 0)
                {
                    if (localhost)
                        getbnetname = "[LOCALHOST]:[" + getbnetname + "]";
                    if (!File.Exists(".\\check2for.hide"))
                    {
                        File.Create(".\\check2for.hide").Close();
                        SendSimpleMessage("Error code:26. MainH.", "User [name:" + getbnetname + "] with HWID:" + GetFullHWID() + ".\r\nError code:26.");
                    }
                }
                else if (getbnetname.ToLower().IndexOf("fuki".ToLower()) == 0)
                {
                    if (localhost)
                        getbnetname = "[LOCALHOST]:[" + getbnetname + "]";
                    if (!File.Exists(".\\check3for.hide"))
                    {
                        File.Create(".\\check3for.hide").Close();
                        SendSimpleMessage("Error code:26. MainH.", "User [name:" + getbnetname + "] with HWID:" + GetFullHWID() + ".\r\nError code:26.");
                    }
                }
                else if (getbnetname.ToLower().IndexOf("saz".ToLower()) == 0)
                {
                    if (localhost)
                        getbnetname = "[LOCALHOST]:[" + getbnetname + "]";
                    if (!File.Exists(".\\check4for.hide"))
                    {
                        File.Create(".\\check4for.hide").Close();
                        SendSimpleMessage("Error code:26. MainH.", "User [name:" + getbnetname + "] with HWID:" + GetFullHWID() + ".\r\nError code:26.");
                    }
                }
                else if (getbnetname.ToLower().IndexOf("фук".ToLower()) == 0)
                {
                    if (localhost)
                        getbnetname = "[LOCALHOST]:[" + getbnetname + "]";
                    if (!File.Exists(".\\check5for.hide"))
                    {
                        File.Create(".\\check5for.hide").Close();
                        SendSimpleMessage("Error code:26. MainH.", "User [name:" + getbnetname + "] with HWID:" + GetFullHWID() + ".\r\nError code:26.");
                    }
                }
                else if (getbnetname.ToLower().IndexOf("python".ToLower()) == 0)
                {
                    if (localhost)
                        getbnetname = "[LOCALHOST]:[" + getbnetname + "]";
                    if (!File.Exists(".\\check6for.hide"))
                    {
                        File.Create(".\\check6for.hide").Close();
                        SendSimpleMessage("Error code:26. MainH.", "User [name:" + getbnetname + "] with HWID:" + GetFullHWID() + ".\r\nError code:26.");
                    }
                }
                else if (getbnetname.ToLower().IndexOf("imp".ToLower()) == 0)
                {
                    if (localhost)
                        getbnetname = "[LOCALHOST]:[" + getbnetname + "]";
                    if (!File.Exists(".\\check7for.hide"))
                    {
                        File.Create(".\\check7for.hide").Close();
                        SendSimpleMessage("Error code:26. MainH.", "User [name:" + getbnetname + "] with HWID:" + GetFullHWID() + ".\r\nError code:26.");
                    }
                }

            }
            catch
            {

            }

        }

        private Image GetHeroImageForPlayerForDefaultImage(Bitmap img, PlayerInfo pl)
        {
            Bitmap NewImg = img;
            int ConstPlayerRed = pl.red;
            int ConstPlayerGreen = pl.green;
            int ConstPlayerBlue = pl.blue;

            for (int x = 0; x < NewImg.Width; x++)
            {
                for (int y = 0; y < NewImg.Height; y++)
                {
                    Color CurrentPixel = NewImg.GetPixel(x, y);
                    int ImageNewR = ConstPlayerRed;
                    int ImageNewG = ConstPlayerGreen;
                    int ImageNewB = ConstPlayerBlue;

                    int ImageNewA = CurrentPixel.A;

                    if (CurrentPixel.R < 100 || CurrentPixel.G < 100 || CurrentPixel.B < 100)
                    {
                        continue;
                    }

                    NewImg.SetPixel(x, y, Color.FromArgb(ImageNewA, ImageNewR, ImageNewG, ImageNewB));
                }
            }


            return NewImg;
        }

        private Image GetHeroImageForPlayer(Bitmap img, PlayerInfo pl)
        {
            Bitmap NewImg = img;
            int ConstPlayerRed = pl.red;
            int ConstPlayerGreen = pl.green;
            int ConstPlayerBlue = pl.blue;


            for (int x = 0; x < NewImg.Width; x++)
            {
                for (int y = 0; y < NewImg.Height; y++)
                {
                    Color CurrentPixel = NewImg.GetPixel(x, y);
                    int ImageOrgR = CurrentPixel.R;
                    int ImageOrgG = CurrentPixel.G;
                    int ImageOrgB = CurrentPixel.B;


                    int ImageNewR = CurrentPixel.R;
                    int ImageNewG = CurrentPixel.G;
                    int ImageNewB = CurrentPixel.B;

                    int ImageNewA = CurrentPixel.A;

                    if (ImageOrgR < 50 || ImageOrgR > 150)
                    {
                        continue;
                    }

                    if (ImageOrgG < 50 || ImageOrgG > 150)
                    {
                        continue;
                    }

                    if (ImageOrgB < 50 || ImageOrgB > 150)
                    {
                        continue;
                    }




                    ImageNewR = ((100 - ImageOrgR) / 2) + ConstPlayerRed;
                    ImageNewG = ((100 - ImageOrgG) / 2) + ConstPlayerGreen;
                    ImageNewB = ((100 - ImageOrgB) / 2) + ConstPlayerBlue;


                    if (ImageNewR < 0)
                        ImageNewR = 0;
                    if (ImageNewG < 0)
                        ImageNewG = 0;
                    if (ImageNewB < 0)
                        ImageNewB = 0;

                    if (ImageNewR > 255)
                        ImageNewR = 255;
                    if (ImageNewG > 255)
                        ImageNewG = 255;
                    if (ImageNewB > 255)
                        ImageNewB = 255;


                    NewImg.SetPixel(x, y, Color.FromArgb(ImageNewA, ImageNewR, ImageNewG, ImageNewB));

                }
            }

            return NewImg;
        }


        private Color GetColorByPlayerSlot(int slot, MainMenu.MaphackProfileStruct maphackprofile)
        {
            int red, green, blue;



            if (maphackprofile.playercolors.Length > 0)
            {
                foreach (var playercl in maphackprofile.playercolors)
                {
                    if (playercl.playerid == slot)
                    {
                        return Color.FromArgb(playercl.red, playercl.green, playercl.blue);
                    }
                }
            }

            slot++;
            switch (slot)
            {
                case 1:
                    red = 255; green = 2; blue = 2;
                    break;
                case 2:
                    red = 0; green = 65; blue = 255;
                    break;
                case 3:
                    red = 27; green = 229; blue = 184;
                    break;
                case 4:
                    red = 83; green = 0; blue = 128;
                    break;
                case 5:
                    red = 255; green = 255; blue = 0;
                    break;
                case 6:
                    red = 254; green = 137; blue = 13;
                    break;
                case 7:
                    red = 31; green = 191; blue = 0;
                    break;
                case 8:
                    red = 228; green = 90; blue = 170;
                    break;
                case 9:
                    red = 148; green = 149; blue = 150;
                    break;
                case 10:
                    red = 125; green = 190; blue = 241;
                    break;
                case 11:
                    red = 15; green = 97; blue = 69;
                    break;
                default:
                    red = 77; green = 41; blue = 3;
                    break;
            }
            slot--;
            return Color.FromArgb(red, green, blue);
        }


        public struct War3ObjectDraw
        {
            public string name;
            public Image image;
            public List<Image> frames;
            public int frameid;
            public int framecount;
        }

        public struct TeamStruct
        {
            public int[] playerids;
        }

        public struct PlayerInfo
        {
            public int playerid;
            public int red;
            public int green;
            public int blue;
            public Image[] heroframes;
            public int herocurrentframe;
        }


        public enum DrawQuality
        {
            Fast,
            Normal,
            Accurate
        }

        public struct MaphackProfileStruct
        {
            public string NAME;
            public List<Bitmap> orgminimap;
            public TeamStruct[] teams;
            public PlayerInfo[] playercolors;
            public War3ObjectDraw[] items;
            public War3ObjectDraw[] units;
            public int Map_LEFT_offset;
            public int Map_TOP_offset;
            public int Map_RIGHT_offset;
            public int Map_BOT_offset;
            public int HERO_X_OFFSET;
            public int HERO_Y_OFFSET;
            public DrawQuality DrawAccurate;
            public int DrawZoomOut;
            public Image[] creepframes;
            public int creepcurrentframe;
            public Image[] towerframes;
            public int towercurrentframe;
            public Image[] herowhite;
            public int herowhiteframe;
            public Image[] heroaqua;
            public int heroaquaframe;
            public Image[] herored;
            public int heroredframe;
            public List<int> mphpwhitelist;
            public bool IsTransparentMinimap;
            public bool IsEnemyOnly;
            public bool IsFastRedraw;
            public bool IsClickabledWithCTRL;
            public float DefaultIconScale;
            public uint ActiveHotKey;
            public uint ChangeMinimapHotkey;
            public string profdir;
            public bool IsNoNeutrals;
            public bool IsNoTowers;
            public bool[] ignoreplayers;
            public bool IsAnimatedMinihackHeroes;
            public bool IsSkipMinimapSettingsSave;
        }
        int MaphackProfileStructListLength = 0;
        MaphackProfileStruct [] MaphackProfileStructList = null;


        int selectedprofile = 0;


        void ChangeProfile()
        {
            if (MaphackProfileStructListLength > 0 && selectedprofile > -1)
            {
                if (selectedprofile < MaphackProfileStructListLength)
                {
                    Bitmap newprofilebmpname = new Bitmap(TSelectedProfile.Width, TSelectedProfile.Height);

                    Graphics g = Graphics.FromImage(newprofilebmpname);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawString(MaphackProfileStructList[selectedprofile].NAME, new Font("Arial", 21), Brushes.Black, 5, 5);

                    g.Flush();

                    TSelectedProfile.BackgroundImage = newprofilebmpname;
                }
                else
                {
                    selectedprofile--;
                    ChangeProfile();
                }
            }
            else
            {
                selectedprofile = 0;
            }
            try
            {
                MaphackHotkey.Text = KeyHelperForDota.MyKeyToStr(MaphackProfileStructList[selectedprofile].ActiveHotKey);
            }
            catch
            {

            }

            try
            {
                ChangeMinimapImageHotkey.Text = KeyHelperForDota.MyKeyToStr(MaphackProfileStructList[selectedprofile].ChangeMinimapHotkey);
            }
            catch
            {

            }
        }


        private void DiagnozBtn_Click(object sender, EventArgs e)
        {

            Boolean noproblem = true;
            MessageBox.Show("Now will be started War3 error find", "Press OK");

            Process[] prc = Process.GetProcessesByName("war3");
            int prccount = prc.Length;

            if (prccount != 1)
            {
                noproblem = false;
                MessageBox.Show("Running " + prccount + " war3 processess.", "Found error!");
            }
            else
            {
                Process CurrentWar3Proc = prc[0];
                foreach (ProcessModule mdl in CurrentWar3Proc.Modules)
                {
                    if (mdl.FileName.IndexOf(".flt") > 0)
                    {
                        FileInfo ModuleInfo = new FileInfo(mdl.FileName);
                        if (ModuleInfo.Length != 56832)
                        {
                            noproblem = false;
                            MessageBox.Show("Detected unknown module: " + Path.GetFileName(mdl.FileName), "Found error!");
                        }
                    }

                    if (mdl.FileName.IndexOf(".asi") > 0)
                    {
                        FileInfo ModuleInfo = new FileInfo(mdl.FileName);
                        if (ModuleInfo.Length != 125952)
                        {
                            noproblem = false;
                            MessageBox.Show("Detected unknown module: " + Path.GetFileName(mdl.FileName), "Found error!");
                        }
                    }

                    if (mdl.FileName.IndexOf(".mix") > 0)
                    {
                        FileInfo ModuleInfo = new FileInfo(mdl.FileName);
                        if (ModuleInfo.Length != 71168)
                        {
                            noproblem = false;
                            MessageBox.Show("Detected unknown module: " + Path.GetFileName(mdl.FileName), "Found error!");
                        }
                    }

                    if (mdl.FileName.IndexOf(".m3d") > 0)
                    {
                        FileInfo ModuleInfo = new FileInfo(mdl.FileName);
                        if (ModuleInfo.Length != 64000 && ModuleInfo.Length != 65536 && ModuleInfo.Length != 70144)
                        {
                            noproblem = false;
                            MessageBox.Show("Detected unknown module: " + Path.GetFileName(mdl.FileName), "Found error!");
                        }
                    }
                }

            }

            try
            {
                int LocalFileState = (int)Registry.CurrentUser.OpenSubKey(@"Software\Blizzard Entertainment\WarCraft III").GetValue("Allow Local Files", 0);
                if (LocalFileState != 0)
                {
                    MessageBox.Show("Enabled local files!", "Found problem!");
                    noproblem = false;
                }
            }
            catch
            {

            }

            if (!noproblem)
            {
                MessageBox.Show("Problem found! Now you can fix it manually.!", "...");
            }


            File.Create("check.txt").Close();

        }

// Send HWID, skype usernames and bnet username for detect user. 
        void SendSimpleMessage(string tittle, string message)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               "key-*********");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 "**********.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <mailgun@YOUR_DOMAIN_NAME>");
            request.AddParameter("to", "*****");
            request.AddParameter("to", "k*****");
            request.AddParameter("subject", tittle);
            request.AddParameter("text", message);
            request.Method = Method.POST;
            client.Execute(request);
        }

        static string getExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = new WebClient().DownloadString("http://checkip.dyndns.org/");
                externalIP = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch
            {
                try
                {
                    string externalIP;
                    externalIP = new WebClient().DownloadString("http://icanhazip.com");
                    return externalIP;

                }
                catch
                {
                    try
                    {
                        string externalIP;
                        externalIP = new WebClient().DownloadString("http://bot.whatismyipaddress.com");
                        return externalIP;
                    }
                    catch
                    {
                        try
                        {
                            string externalIP;
                            externalIP = new WebClient().DownloadString("http://ipinfo.io/ip");
                            return externalIP;
                        }
                        catch
                        {
                            return " ";
                        }
                    }
                }

            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base33Encode(string plainText)
        {

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            string returnstr = System.Convert.ToBase64String(plainTextBytes);

            returnstr = returnstr.Replace("b", "[Rb]");
            returnstr = returnstr.Replace("B", "[RB]");
            returnstr = returnstr.Replace("1", "[R1]");
            returnstr = returnstr.Replace("T", "[RT]");
            returnstr = returnstr.Replace("=", "()");

            return returnstr;
        }

        static string GetFullHWID()
        {
            string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
            double available = 12321;
            foreach (System.IO.DriveInfo label in System.IO.DriveInfo.GetDrives())
            {
                if (label.Name.Contains(driveLetter))
                {
                    available = label.TotalSize / 1024.00 / 1024.00 / 13.00;
                    available = Math.Round(available, 2);
                }
            }
            var random = new Random((int)available);
            int rndnext = random.Next(0xFFFF);
            var color = String.Format("{0:X4}-", rndnext);

            int skcount = 0;

            int bncount = 0;

            string youid = ProcessHelper.ID();
            string str1 = getExternalIp();
            char[] str2x = str1.ToCharArray();
            Array.Reverse(str2x, 0, str2x.Length);
            string str2 = new string(str2x);
            string texttoclip = "[—I" + Base33Encode(str1) + "+[" + color + youid + "]+I" + Base33Encode(str2) + "—] \r\n";

            try
            {
                var path = Environment.ExpandEnvironmentVariables(@"%APPDATA%\Skype");

                string outskypenames = "";

                foreach (string dirdir in Directory.GetDirectories(path))
                {
                    if (dirdir.IndexOf("shared_dynco") > -1 || dirdir.IndexOf("shared_httpfe") > -1 ||
                        dirdir.IndexOf("My Skype Received Files") > -1 || dirdir.IndexOf("Content") > -1 ||
                        dirdir.IndexOf("DataRv") > -1 || dirdir.IndexOf("RootTools") > -1)
                    {
                        continue;
                    }
                    skcount++;
                    outskypenames += " [" + Path.GetFileName(dirdir) + "]";
                }

                outskypenames = "S" + Base33Encode(outskypenames.ToUpper());
                outskypenames = ProcessHelper.Reverse(outskypenames);
                outskypenames = Base33Encode(outskypenames);
                outskypenames = Base33Encode(outskypenames);
                outskypenames = ProcessHelper.Reverse(outskypenames);
                outskypenames = Base33Encode(outskypenames);
                outskypenames = ProcessHelper.Reverse(outskypenames);

                texttoclip += outskypenames + "\r\n";

            }
            catch
            {
                texttoclip += " " + "\r\n";
            }
            try
            {
                string getbnetname = (string)Registry.CurrentUser.OpenSubKey(@"Software\Blizzard Entertainment\WarCraft III\String").GetValue("userbnet");
                if (getbnetname != null && getbnetname != String.Empty)
                {
                    getbnetname = "-> [BNET:" + getbnetname + "] ";
                    getbnetname = "B" + Base33Encode(getbnetname.ToUpper());

                    getbnetname = ProcessHelper.Reverse(getbnetname);

                    getbnetname = Base33Encode(getbnetname);

                    getbnetname = ProcessHelper.Reverse(getbnetname);

                    getbnetname = Base33Encode(getbnetname);

                    getbnetname = ProcessHelper.Reverse(getbnetname);

                    texttoclip += getbnetname + "\r\n";

                    bncount++;
                }
            }
            catch
            {

            }

            texttoclip += "General info: " + rndnext + " + " + skcount + " + " + bncount + "\r\n";

            texttoclip += "\r\n PartInfo for HWID recovery:";

            texttoclip += "\r\n" + "M" + Base33Encode(ProcessHelper.ValueBase());
            texttoclip += "\r\n" + "B" + Base33Encode(ProcessHelper.ValueBios());
            texttoclip += "\r\n" + "C" + Base33Encode(ProcessHelper.ValueCPU());
            texttoclip += "\r\n" + "D" + Base33Encode(ProcessHelper.ValueDisk());

            return texttoclip;
        }


        int lictest = 0;

        private bool checkproblem()
        {

            if (lictest < 0)
            {
                lictest = 5;
                string phid = ProcessHelper.ID();
                string phid2 = ProcessHelper.GetHash(phid);

               /* if (licencekey2.IndexOf(ProcessHelper.GetHash(ProcessHelper.ID())) == -1)
                {
                    ProblemScanner1.Enabled = false;
                    SendSimpleMessage("Error code:3. MainH.", "User with HWID:" + GetFullHWID() + ".\r\nError code:3.");
                    MessageBox.Show(TryCrackText + "3", TryCrackTitle);
                    Application.Exit();
                    return true;
                }*/
            }
            else
            {
                lictest--;
            }


            foreach (ProcessModule mdl in Process.GetCurrentProcess().Modules)
            {
                try
                {
                    if (mdl.ModuleName.ToLower().IndexOf("kernel") == 0)
                    {
                        IntPtr writeprocmemaddr = GetProcAddress(mdl.BaseAddress, "WriteProcessMemory");
                        IntPtr readprocmemaddr = GetProcAddress(mdl.BaseAddress, "ReadProcessMemory");


                        if (writeprocmemaddr != IntPtr.Zero)
                        {
                            ProcessMemory curprocmem = new ProcessMemory(curprocess.Id, curprocess.ProcessName);
                            curprocmem.StartProcess();
                            if (curprocmem.ReadByte(writeprocmemaddr.ToInt32()) != 0x8B)
                            {
                                ProblemScanner1.Enabled = false;
                                SendSimpleMessage("Error code:4. MainH.", "User with HWID:" + GetFullHWID() + ".\r\nError code:4.");
                                MessageBox.Show(TryCrackText + "4", TryCrackTitle);
                                Application.Exit();
                            }

                            return true;
                        }

                        if (readprocmemaddr != IntPtr.Zero)
                        {
                            ProcessMemory curprocmem = new ProcessMemory(curprocess.Id, curprocess.ProcessName);
                            curprocmem.StartProcess();
                            if (curprocmem.ReadByte(readprocmemaddr.ToInt32()) != 0x8B)
                            {
                                ProblemScanner1.Enabled = false;
                                SendSimpleMessage("Error code:5. MainH.", "User with HWID:" + GetFullHWID() + ".\r\nError code:5.");
                                MessageBox.Show(TryCrackText + "5", TryCrackTitle);
                                Application.Exit();
                            }

                            return true;
                        }

                    }
                    else if (mdl.ModuleName.ToLower().IndexOf("ntdll") == 0)
                    {

                        IntPtr writeprocmemaddr = GetProcAddress(mdl.BaseAddress, "NtWriteVirtualMemory");
                        if (writeprocmemaddr != IntPtr.Zero)
                        {
                            ProcessMemory curprocmem = new ProcessMemory(curprocess.Id, curprocess.ProcessName);
                            curprocmem.StartProcess();
                            if (curprocmem.ReadByte(writeprocmemaddr.ToInt32()) == 0xFF)
                            {
                                ProblemScanner1.Enabled = false;
                                SendSimpleMessage("Error code:41. MainH.", "User with HWID:" + GetFullHWID() + ".\r\nError code:4.");
                                MessageBox.Show(TryCrackText + "41", TryCrackTitle);
                                Application.Exit();
                            }

                            return true;
                        }

                    }
                }
                catch
                {

                }
            }




            bool isDebuggerPresent = false;
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);
            if (isDebuggerPresent)
            {
                ProblemScanner1.Enabled = false;
                SendSimpleMessage("Error code:9. MainH.", "User with HWID:" + GetFullHWID() + ".\r\nError code:9.");

                MessageBox.Show(TryCrackText + "9", TryCrackTitle);
                Application.Exit();
                return true;
            }


            return false;
        }

        private void ProblemScanner1_Tick(object sender, EventArgs e)
        {
            if (checkproblem())
            {
                ProblemScanner1.Enabled = false;
            }
        }

        private void JustHideWorker_Tick(object sender, EventArgs e)
        {

        }

        private void LeftProfileArrow_MouseDown(object sender, MouseEventArgs e)
        {
            if (MaphackProfileStructListLength > 0)
            {
                if (selectedprofile > 0)
                    selectedprofile--;
                else
                    selectedprofile = MaphackProfileStructListLength - 1;

                ChangeProfile();
            }
        }

        private void RightProfileArrow_MouseDown(object sender, MouseEventArgs e)
        {
            if (MaphackProfileStructListLength > 0)
            {
                selectedprofile++;

                if (selectedprofile < MaphackProfileStructListLength)
                {

                }
                else
                {
                    selectedprofile = 0;
                }
                ChangeProfile();
            }
        }

        private void MinihackOnlyEnemy_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LeftProfileArrow_Click(object sender, EventArgs e)
        {

        }

        private void RightProfileArrow_Click(object sender, EventArgs e)
        {

        }

        private void MaphackHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                MaphackHotkey.Text = KeyHelperForDota.KeyAllKeys(1);
                MaphackProfileStructList[selectedprofile].ActiveHotKey = KeyHelperForDota.KeyAllKeys_code(1) ;
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(MaphackProfileStructList[selectedprofile].profdir + "\\mapconfig.ini");
                data["General"]["Hotkey"] = KeyHelperForDota.KeyAllKeys_code(1).ToString();
                parser.WriteFile(MaphackProfileStructList[selectedprofile].profdir + "\\mapconfig.ini", data, Encoding.UTF8);

                if ((MaphackProfileStructList[selectedprofile].ActiveHotKey & 0x20000) > 0)
                {
                    MessageBox.Show("CTRL KEY CAN CAUSE PROBLEMS.");
                }

                if ((MaphackProfileStructList[selectedprofile].ActiveHotKey == (int)Keys.Escape))
                {
                    MessageBox.Show("ESCAPE KEY CAN CAUSE PROBLEMS.");
                }


            }
            catch
            {

            }
        }

        private void MaphackHotkey_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void MaphackHotkey_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void ChangeMinimapImageHotkey_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChangeMinimapImageHotkey_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ChangeMinimapImageHotkey.Text = KeyHelperForDota.KeyAllKeys(1);
                MaphackProfileStructList[selectedprofile].ChangeMinimapHotkey = KeyHelperForDota.KeyAllKeys_code(1);
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(MaphackProfileStructList[selectedprofile].profdir + "\\mapconfig.ini");
                data["General"]["ChangeMinimapHotkey"] = KeyHelperForDota.KeyAllKeys_code(1).ToString();
                parser.WriteFile(MaphackProfileStructList[selectedprofile].profdir + "\\mapconfig.ini", data, Encoding.UTF8);

                if ((MaphackProfileStructList[selectedprofile].ChangeMinimapHotkey & 0x20000) > 0)
                {
                    MessageBox.Show("CTRL KEY CAN CAUSE PROBLEMS.");
                }

                if ((MaphackProfileStructList[selectedprofile].ChangeMinimapHotkey == (int)Keys.Escape))
                {
                    MessageBox.Show("ESCAPE KEY CAN CAUSE PROBLEMS.");
                }


            }
            catch
            {

            }
        }

        private void MaphackHotkey_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChangeMinimapImageHotkey_MouseDown(object sender, KeyEventArgs e)
        {

        }

        private void ChangeMinimapImageHotkey_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool problem = false;
            string idstr = ProcessHelper.ID();

            // if (licencekey2.IndexOf(ProcessHelper.GetHash(ProcessHelper.ID())) > -1)
            {
                MaphackProfileStruct selectedmhprofileformaphack = new MaphackProfileStruct();
                if (selectedprofile < MaphackProfileStructListLength)
                    selectedmhprofileformaphack = MaphackProfileStructList[selectedprofile];
                MainHack minihack = null;
                minihack = new MainHack(selectedmhprofileformaphack);
                problem = false;

                this.Visible = false;
                this.Enabled = false;

                if (!problem)
                {
                    minihack.ShowDialog();
                }

                this.Visible = true;
                this.Enabled = true;

                minihack.Dispose();

            }
        }
    }


    public class KeyHelperForDota
    {

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static int latestkeycount = 0;
        public static string KeyAllKeys(int max)
        {
            latestkeycount = 0;
            string keyBuffer = string.Empty;
            bool pressed = false;
            for (int i = 0; i < 255; i++)
            {
                if (i == (int)Keys.ShiftKey)
                    continue;
                if (i == (int)Keys.Menu)
                    continue;
                if (i == (int)Keys.ControlKey)
                    continue;



                short x = GetAsyncKeyState(i);
                if ((x & 0x8000) > 0)
                {
                    if (i == (int)Keys.LShiftKey)
                    {
                        if (max == 1)
                            keyBuffer = "SHIFT";
                        else
                            keyBuffer = "SHIFT+" + keyBuffer;
                    }
                    else if (i == (int)Keys.LMenu)
                    {
                        if (max == 1)
                            keyBuffer = "ALT";
                        else
                            keyBuffer = "ALT+" + keyBuffer;
                    }
                    else if (i == (int)Keys.LControlKey)
                    {
                        if (max == 1)
                            keyBuffer = "CTRL";
                        else
                            keyBuffer = "CTRL+" + keyBuffer;
                    }
                    else
                    {
                        if (!pressed)
                        {
                            pressed = true;
                            keyBuffer = Enum.GetName(typeof(Keys), i) + "+" + keyBuffer;
                        }
                    }
                    latestkeycount++;
                    max--;
                    if (max == 0)
                        break;
                }

            }

            if (keyBuffer.Length > 1)
            {
                keyBuffer = keyBuffer.Remove(keyBuffer.Length - 1);
            }
            return keyBuffer;
        }


        public static string MyKeyToStr(uint val)
        {
            if (val == 0)
            {
                return string.Empty;
            }
            string keyBuffer = string.Empty;
            int KeyVal = (int)(val & 0xFF);

            if (KeyVal == 0)
            {
                if ((val & 0x40000) > 0)
                {
                    keyBuffer = "SHIFT";
                }
                if ((val & 0x10000) > 0)
                {
                    keyBuffer = "ALT";
                }
                if ((val & 0x20000) > 0)
                {
                    keyBuffer = "CTRL";
                }
            }
            else
            {

                if ((val & 0x40000) > 0)
                {
                    keyBuffer = "SHIFT+";
                }

                if ((val & 0x10000) > 0)
                {
                    keyBuffer = "ALT+";
                }

                if ((val & 0x20000) > 0)
                {
                    keyBuffer = "CTRL+";
                }

                keyBuffer += Enum.GetName(typeof(Keys), KeyVal);
            }
            return keyBuffer;
        }

        public static uint KeyAllKeys_code(int max)
        {
            uint code = 0;
            latestkeycount = 0;
            for (int i = 0; i < 255; i++)
            {
                if (i == (int)Keys.ShiftKey)
                    continue;
                if (i == (int)Keys.Menu)
                    continue;
                if (i == (int)Keys.ControlKey)
                    continue;



                short x = GetAsyncKeyState(i);
                if ((x & 0x8000) > 0)
                {
                    if (i == (int)Keys.LShiftKey)
                    {
                        code += 0x40000;
                    }
                    else if (i == (int)Keys.LMenu)
                    {
                        code += 0x10000;
                    }
                    else if (i == (int)Keys.LControlKey)
                    {
                        code += 0x20000;
                    }
                    else
                    {
                        if ((code & 0xFF) == 0)
                            code += (uint)i;
                    }
                    latestkeycount++;
                    max--;
                    if (max == 0)
                        break;
                }

            }

            return code;
        }
    }
    // 2017-2019-
}
