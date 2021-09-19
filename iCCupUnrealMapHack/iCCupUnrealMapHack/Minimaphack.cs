using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace iCCupUnrealMapHack
{
    public partial class Minimaphack : Form
    {
        // string licencekey3 = "";

        WebClient webClient = new WebClient();

        private int minimapid = 0;

        protected override void WndProc(ref Message m)
        {
            const int wmNcHitTest = 0x84;
            const int htLeft = 10;
            const int htRight = 11;
            const int htTop = 12;
            const int htTopLeft = 13;
            const int htTopRight = 14;
            const int htBottom = 15;
            const int htBottomLeft = 16;
            const int htBottomRight = 17;

            if (m.Msg == wmNcHitTest)
            {
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));
                Size clientSize = ClientSize;
                ///allow resize on the lower right corner
                if (pt.X >= clientSize.Width - 10 && pt.Y >= clientSize.Height - 10 && clientSize.Height >= 10)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
                ///allow resize on the lower left corner
                if (pt.X <= 10 && pt.Y >= clientSize.Height - 10 && clientSize.Height >= 10)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft);
                    return;
                }
                ///allow resize on the upper right corner
                if (pt.X <= 10 && pt.Y <= 10 && clientSize.Height >= 10)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft);
                    return;
                }
                ///allow resize on the upper left corner
                if (pt.X >= clientSize.Width - 10 && pt.Y <= 10 && clientSize.Height >= 10)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopLeft : htTopRight);
                    return;
                }
                ///allow resize on the top border
                if (pt.Y <= 10 && clientSize.Height >= 10)
                {
                    m.Result = (IntPtr)(htTop);
                    return;
                }
                ///allow resize on the bottom border
                if (pt.Y >= clientSize.Height - 10 && clientSize.Height >= 10)
                {
                    m.Result = (IntPtr)(htBottom);
                    return;
                }
                ///allow resize on the left border
                if (pt.X <= 10 && clientSize.Height >= 10)
                {
                    m.Result = (IntPtr)(htLeft);
                    return;
                }
                ///allow resize on the right border
                if (pt.X >= clientSize.Width - 10 && clientSize.Height >= 10)
                {
                    m.Result = (IntPtr)(htRight);
                    return;
                }
            }
            base.WndProc(ref m);
        }
        //***********************************************************
        //***********************************************************
        //This gives us the drop shadow behind the borderless form
        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        //***********************************************************

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_NCLBUTTONUP = 0xA2;
        public const int HT_CAPTION = 0x2;

        string libname = "Game.dll";


        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        Form thisform = null;
        bool GameAlreadStarted = false;
        bool FirstGameFound = false;
        int happyoffset = 256;
        int happyframeid = 0;
        List<Image> happyframes = new List<Image>();


        MainMenu.MaphackProfileStruct maphackprofile;

        int _InGame1Offset = 0xACF678;
        int _InGame2Offset = 0xAB62A4;

        int _GlobalClassOffset = 0xAB4F80;

        int _MinimapInfoOffset = 0xAB6214;

        int _GlobalPlayerOffset = 0xAB65F4;

        private void Set126aVersion()
        {
            _InGame1Offset = 0xACF678;
            _InGame2Offset = 0xAB62A4;
            _GlobalClassOffset = 0xAB4F80;
            _MinimapInfoOffset = 0xAB6214;
            _GlobalPlayerOffset = 0xAB65F4;
        }

        private void Set127aVersion()
        {
            _InGame1Offset = 0xBE6530;
            _InGame2Offset = 0xBE3D70;
            _GlobalClassOffset = 0xBE6350;
            _MinimapInfoOffset = 0xBE6DC4;
            _GlobalPlayerOffset = 0xBE4238;
        }

        private void Set127bVersion()
        {
            _InGame1Offset = 0xD6AA98;
            _InGame2Offset = 0xD682D8;
            _GlobalClassOffset = 0xD6A8B8;
            _MinimapInfoOffset = 0xD6B32C;
            _GlobalPlayerOffset = 0xD687A8;
        }

        private void Set128aVersion()
        {
            _InGame1Offset = 0xD753E0;
            _InGame2Offset = 0xD72C20;
            _GlobalClassOffset = 0xD75200;
            _MinimapInfoOffset = 0xD75C70;
            _GlobalPlayerOffset = 0xD730F0;
        }


        private void Set128bVersion()
        {
            _InGame1Offset = 0xD7A438;
            _InGame2Offset = 0xD77C78;
            _GlobalClassOffset = 0xD7A258;
            _MinimapInfoOffset = 0xD7ACC8;
            _GlobalPlayerOffset = 0xD78148;
        }


        private void Set128cVersion()
        {
            _InGame1Offset = 0xD7A438;
            _InGame2Offset = 0xD77C78;
            _GlobalClassOffset = 0xD7A258;
            _MinimapInfoOffset = 0xD7ACC8;
            _GlobalPlayerOffset = 0xD78148;
        }

        private void Set1284Version()
        {
            _InGame1Offset = 0xD314D0;
            _InGame2Offset = 0xD2ED10;
            _GlobalClassOffset = 0xD312F0;
            _MinimapInfoOffset = 0xD31D60;
            _GlobalPlayerOffset = 0xD2F1E0;
        }

        private void Set1285Version()
        {
            _InGame1Offset = 0xD328D0;
            _InGame2Offset = 0xD30110;
            _GlobalClassOffset = 0xD326F0;
            _MinimapInfoOffset = 0xD33160;
            _GlobalPlayerOffset = 0xD305E0;
        }

        private void Set1290Version()
        {
            _InGame1Offset = 0xD3A81C;
            _InGame2Offset = 0xD75A0C;
            _GlobalClassOffset = 0xD3A7F8;
            _MinimapInfoOffset = 0xD3B228;
            _GlobalPlayerOffset = 0xD386F4;
            __unitclassoffset = 0x3FC;
            __unitdatastartoffset = 0x6A0;
        }

        private void Set1300Version()
        {
            _InGame1Offset = 0xD3A81C;
            _InGame2Offset = 0xD75A0C;
            _GlobalClassOffset = 0xD3A7F8;
            _MinimapInfoOffset = 0xD3B228;
            _GlobalPlayerOffset = 0xD386F4;
            __unitclassoffset = 0x3FC;
            __unitdatastartoffset = 0x6A0;
        }


        int __unitclassoffset = 0x3BC;
        int __unitdatastartoffset = 0x604;


        public Minimaphack(MainMenu.MaphackProfileStruct maphackprofile)
        {
            this.maphackprofile = maphackprofile;

            //try
            {
                happyframes = MainMenu.GetImageFrames(Properties.Resources.Absol);
            }
            /*catch
            {

            }*/

            webClient.Encoding = Encoding.UTF8;
            //  licencekey3 = webClient.DownloadString("http://*************/.txt");
            //if (licencekey3.IndexOf(ProcessHelper.GetHash(ProcessHelper.ID())) > -1)
            {
                // try
                {
                    thisform = this;
                    InitializeComponent();
                    thisform = this;
                }
                /* catch
                 {

                 }*/
            }
        }
        private Point MouseDownLocation;


        private bool MinimapColored = false;



        //if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control)
        //{
        //    ReleaseCapture();
        //    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        //}


        private void MiniMapImg_DoubleClick(object sender, EventArgs e)
        {

            if (!maphackprofile.IsSkipMinimapSettingsSave)
            {
                try
                {
                    var parsersettings = new FileIniDataParser();
                    IniData datasettings = parsersettings.ReadFile(System.Reflection.Assembly.GetEntryAssembly().Location + "_.ini");
                    datasettings["General"]["WindowPos"] = this.Location.ToString().Replace("{", "").Replace("}", "").Replace(" ", "").Replace("X=", "").Replace("Y=", "").Replace("Width=", "").Replace("Height=", "");
                    datasettings["General"]["WindowSize"] = this.Size.ToString().Replace("{", "").Replace("}", "").Replace(" ", "").Replace("X=", "").Replace("Y=", "").Replace("Width=", "").Replace("Height=", "");
                    parsersettings.WriteFile(System.Reflection.Assembly.GetEntryAssembly().Location + "_.ini", datasettings, Encoding.UTF8);

                }
                catch
                {
                    File.Create(System.Reflection.Assembly.GetEntryAssembly().Location + "_.ini").Close();
                }
            }

            //Properties.Settings.Default.WindowPos = this.Location;
            //Properties.Settings.Default.WindowSize = this.Size;


            MouseEventArgs me = e as MouseEventArgs;

            MouseButtons buttonPushed = me.Button;
            if ((buttonPushed & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Close();
            }
        }

        private void Minimaphack_DoubleClick(object sender, EventArgs e)
        {

        }

        private void DragTimer_Tick(object sender, EventArgs e)
        {
        }

        private double GetDistance(PointF point1, PointF point2)
        {
            //pythagorean theorem c^2 = a^2 + b^2
            //thus c = square root(a^2 + b^2)
            double a = point2.X - point1.X;
            double b = point2.Y - point1.Y;

            return Math.Sqrt(a * a + b * b);
        }


        Bitmap BmpForDraw = null;

        Process war3process = null;
        ProcessMemory war3processmemory = null;
        bool timerwork = false;



        private int DrawType(int playerslot, int unitslot, string unitclass)
        {
            int retvalue = 2;

            //File.AppendAllText("DrawType.txt", "ID:" + unitclass);

            if (!maphackprofile.IsEnemyOnly)
            {
                foreach (MainMenu.War3ObjectDraw curobjitem in maphackprofile.units)
                {
                    if (curobjitem.name == unitclass)
                    {
                        return 5;
                    }
                }
            }

            //File.AppendAllText("DrawType.txt", " - Not found in drawobjects. DrawObjects:" + maphackprofile.units.Length.ToString( ) +"\n");

            if (unitslot == 12)
                retvalue = 4;

            else if (unitslot > 12)
                retvalue = 3;
            else
            {
                if (playerslot == unitslot)
                    retvalue = 0;

                if (playerslot < 6 && unitslot < 6)
                    retvalue = 1;

                if (playerslot >= 6 && unitslot >= 6)
                    retvalue = 1;
            }

            if (maphackprofile.teams.Length > 0)
            {
                int unitteam = -1;
                int playerteam = -1;

                for (int i = 0; i < maphackprofile.teams.Length; i++)
                {
                    foreach (int playerslotid in maphackprofile.teams[i].playerids)
                    {
                        if (unitslot == playerslotid)
                        {
                            unitteam = i;
                        }
                        if (playerslot == playerslotid)
                        {
                            playerteam = i;
                        }
                    }
                }



                if (unitteam != -1 && playerteam != -1)
                {
                    if (unitteam != playerteam)
                        retvalue = 2;
                    else
                        retvalue = 1;
                }


            }

            if (maphackprofile.IsEnemyOnly && retvalue == 2)
            {
                foreach (MainMenu.War3ObjectDraw curobjitem in maphackprofile.units)
                {
                    if (curobjitem.name == unitclass)
                    {
                        return 5;
                    }
                }
            }
            else if (maphackprofile.IsEnemyOnly && (retvalue != 2 && retvalue != 4))
                return -1;


            if (retvalue == 4 || retvalue == 3)
            {
                if (maphackprofile.IsNoNeutrals)
                    return -1;
            }

            return retvalue;
        }

        bool IsWhiteListedUnit(string unitclass)
        {
            foreach (MainMenu.War3ObjectDraw curobjitem in maphackprofile.units)
            {
                if (curobjitem.name == unitclass)
                {
                    return true;
                }
            }
            return false;
        }


        private bool IsScourge(int slotid)
        {
            return slotid < 6;
        }


        private Color GetColorByPlayerSlot(int slot)
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

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


        private Image GetHeroImageByPlayerSlot(int slot, bool needupdateid = false)
        {

            for (int i = 0; i < maphackprofile.playercolors.Length; i++)
            {
                if (maphackprofile.playercolors[i].playerid == slot)
                {
                    if (maphackprofile.playercolors[i].heroframes.Length > 0)
                    {
                        if (maphackprofile.playercolors[i].heroframes.Length == 1)
                        {
                            if (needupdateid)
                                return null;

                            return maphackprofile.playercolors[i].heroframes[0];
                        }

                        if (needupdateid)
                        {
                            if (maphackprofile.playercolors[i].herocurrentframe < maphackprofile.playercolors[i].heroframes.Length)
                            {
                                maphackprofile.playercolors[i].herocurrentframe++;
                            }
                            if (maphackprofile.playercolors[i].herocurrentframe < maphackprofile.playercolors[i].heroframes.Length)
                            {

                            }
                            else
                            {
                                maphackprofile.playercolors[i].herocurrentframe = 0;
                            }
                        }

                        if (needupdateid)
                            return null;

                        return maphackprofile.playercolors[i].heroframes[maphackprofile.playercolors[i].herocurrentframe];
                    }
                    break;
                }
            }
            if (needupdateid)
                return null;


            return Properties.Resources.DefaultHeroIcon;
        }


        int sleeptimes = 0;
        bool FirstWork = true;

        private void MinihackTimer_Tick(object sender, EventArgs e)
        {
            if (timerwork)
            {
                return;
            }
            else
            {
                if (sleeptimes > 0)
                {
                    sleeptimes--;
                    return;
                }
            }

            timerwork = true;
            try
            {
                if (war3processmemory == null || !war3processmemory.CheckProcess())
                {
                    int war3proclen = Process.GetProcessesByName("war3").Length;
                    int war3proclen2 = Process.GetProcessesByName("Warcraft III").Length;

                    if (war3proclen == 0 && war3proclen2 == 0)
                    {

                        war3processmemory = null;


                        foreach (Process prc in Process.GetProcesses())
                        {
                            if (prc.MainModule.FileVersionInfo.OriginalFilename.ToLower() == ("war3.exe".ToLower()))
                            {
                                //
                                war3process = prc;
                                ProcessMemory war3mem =
                                    new ProcessMemory(prc.Id, prc.ProcessName);
                                war3mem.StartProcessReadWrite();
                                war3processmemory = war3mem;

                            }
                            else if (prc.MainModule.FileVersionInfo.OriginalFilename.ToLower() == ("Warcraft III.exe".ToLower()))
                            {
                                //Warcraft III.exe
                                war3process = prc;
                                ProcessMemory war3mem =
                                    new ProcessMemory(prc.Id, prc.ProcessName);
                                war3mem.StartProcessReadWrite();
                                war3processmemory = war3mem;

                            }
                        }

                    }
                    else
                    {
                        if (war3proclen == 1 || war3proclen2 == 1)
                        {
                            try
                            {
                                war3process = Process.GetProcessesByName("war3")[0];
                                ProcessMemory war3mem =
                                    new ProcessMemory(war3process.Id, "war3");
                                war3mem.StartProcessReadWrite();
                                war3processmemory = war3mem;
                            }
                            catch
                            {
                                war3process = Process.GetProcessesByName("Warcraft III")[0];
                                ProcessMemory war3mem =
                                    new ProcessMemory(war3process.Id, "Warcraft III");
                                war3mem.StartProcessReadWrite();
                                war3processmemory = war3mem;
                            }

                        }
                        else
                        {
                            if (MessageBox.Show("Warning found multiple war3 process. Close all?", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            {
                                foreach (Process prc in Process.GetProcessesByName("war3"))
                                {
                                    prc.Kill();
                                }
                            }
                        }
                    }

                    if (war3processmemory == null)
                    {

                    }
                    else
                    {
                        if (FirstWork)
                        {

                            if (war3process.MainModule.FileVersionInfo.FileVersion == "1, 26, 0, 6401")
                            {
                                Set126aVersion();

                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1, 27, 0, 52240")
                            {
                                Set127aVersion();

                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1, 27, 1, 7085")
                            {
                                Set127bVersion();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.0.7205")
                            {
                                Set128aVersion();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.1.7365")
                            {
                                Set128bVersion();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.2.7395")
                            {
                                Set128cVersion();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.4.7608")
                            {
                                Set1284Version();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.5.7680")
                            {
                                Set1285Version();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.29.0.9055")
                            {
                                Set1290Version();
                            }

                            else
                            {
                                MessageBox.Show("Warning! Warcraft :" + war3process.MainModule.FileVersionInfo.FileVersion + "\n NOT SUPPORTED. Please contact developer!");
                                this.Close();
                            }
                            FirstWork = false;
                        }


                    }

                }

            }
            catch
            {

            }

            //  try
            {
                if (war3processmemory != null)
                {
                    if (!war3processmemory.CheckProcess())
                    {
                        war3processmemory = null;
                        timerwork = false;
                        return;
                    }

                    #region MAP_BOUNDS

                    int GameDll = war3processmemory.DllImageAddress(libname);

                    if (GameDll == 0)
                    {
                        foreach (ProcessModule mdl in war3process.Modules)
                        {
                            if (mdl.FileName.ToLower().IndexOf("game.dll".ToLower()) > -1)
                            {
                                GameDll = war3processmemory.DllImageAddress(mdl.ModuleName);
                                break;
                            }
                        }
                    }

                    if (GameDll == 0)
                    {
                        GameDll = war3processmemory.DllImageAddress("Warcraft III.exe");

                        if (GameDll == 0)
                        {
                            foreach (ProcessModule mdl in war3process.Modules)
                            {
                                if (mdl.FileName.ToLower().IndexOf("Warcraft III.exe".ToLower()) > -1)
                                {
                                    GameDll = war3processmemory.DllImageAddress(mdl.ModuleName);
                                    break;
                                }
                            }
                        }
                    }

                    if (GameDll <= 0)
                    {
                        GameDll = war3processmemory.MyProcess.MainModule.BaseAddress.ToInt32();
                    }

                    if (GameDll == 0 || GameDll < 0)
                    {
                        war3processmemory = null;

                        timerwork = false;
                        return;
                    }


                    int ingame1 = war3processmemory.ReadInt(_InGame1Offset + GameDll);
                    int ingame2 = war3processmemory.ReadInt(_InGame2Offset + GameDll);

                    if (ingame1 <= 0 && ingame2 <= 0)
                    {
                        GameAlreadStarted = false;
                        timerwork = false;
                        try
                        {
                            File.Delete(MainMenu.MinimapPath);
                        }
                        catch
                        {

                        }
                        return;
                    }

                    if (!GameAlreadStarted)
                    {
                        GameAlreadStarted = true;
                        FirstGameFound = true;
                        happyoffset = 256;
                    }

                    int GlobalClassOffset1 = war3processmemory.ReadInt(GameDll + _GlobalClassOffset);
                    int MapOffset = war3processmemory.ReadInt(GlobalClassOffset1 + 0x254);

                    float mapleft = war3processmemory.ReadFloat(MapOffset + 1232) + maphackprofile.Map_LEFT_offset; // DOTA: -640
                    float maptop = war3processmemory.ReadFloat(MapOffset + 1236) + maphackprofile.Map_TOP_offset;// DOTA: 350
                    float mapright = war3processmemory.ReadFloat(MapOffset + 1240) + maphackprofile.Map_RIGHT_offset; // DOTA: 800
                    float mapbot = war3processmemory.ReadFloat(MapOffset + 1228) + maphackprofile.Map_BOT_offset;// DOTA: -250

                    float fromlefttoright = Math.Abs(mapleft) + Math.Abs(mapright);
                    float fromtoptodown = Math.Abs(maptop) + Math.Abs(mapbot);

                    float minimaponex = fromlefttoright / this.Width;
                    float minimaponey = fromtoptodown / this.Height;


                    #endregion
                    int minimapinfo = war3processmemory.ReadInt(GameDll + _MinimapInfoOffset);
                    int minimapbackground = war3processmemory.ReadInt(minimapinfo + 0x17c) + 0x20;

                    MinimapColored = war3processmemory.ReadInt(minimapinfo + 0x638) == 1;

                    // try
                    {
                        if (maphackprofile.orgminimap.Count == 0)
                        {
                            byte[] datareadbmp = war3processmemory.ReadMem(minimapbackground, 262144);

                            Bitmap ReadMapMinimap = new Bitmap(256, 256);

                            for (int i = 0; i < 256; i++)
                            {
                                for (int n = 0; n < 256; n++)
                                {
                                    int idinmemory = (256 * n + i) * 4;
                                    List<byte> currentcolorread = new List<byte>();
                                    currentcolorread.Add(datareadbmp[idinmemory]);
                                    currentcolorread.Add(datareadbmp[idinmemory + 1]);
                                    currentcolorread.Add(datareadbmp[idinmemory + 2]);
                                    currentcolorread.Add(datareadbmp[idinmemory + 3]);

                                    uint coloruint = BitConverter.ToUInt32(currentcolorread.ToArray(), 0);
                                    Color colornorm = UIntToColor(coloruint);
                                    ReadMapMinimap.SetPixel(i, n, colornorm);
                                }
                            }


                            maphackprofile.orgminimap.Add(ReadMapMinimap);
                        }
                    }
                    /*catch
                    {

                    }*/


                    if (maphackprofile.orgminimap.Count != 0)
                    {
                        int GlobalPlayerAddr = war3processmemory.ReadInt(GameDll + _GlobalPlayerOffset);
                        int LocalPlayerSlot = war3processmemory.ReadShort(GlobalPlayerAddr + 0x28);

                        int GlobalUnitClass = war3processmemory.ReadInt(GameDll + _GlobalClassOffset);
                        int UnitsArrayClassAddr = war3processmemory.ReadInt(GlobalUnitClass + __unitclassoffset);

                        int UnitsCount = war3processmemory.ReadInt(UnitsArrayClassAddr + __unitdatastartoffset);
                        int UnitsArrayAddr = war3processmemory.ReadInt(UnitsArrayClassAddr + __unitdatastartoffset + 4);


                        int ItemsArrayClassAddr = UnitsArrayClassAddr + 0x10;

                        int ItemsCount = war3processmemory.ReadInt(ItemsArrayClassAddr + __unitdatastartoffset);
                        int ItemsArrayAddr = war3processmemory.ReadInt(ItemsArrayClassAddr + __unitdatastartoffset + 4);


                        float originalwidth = 256;
                        float originalheight = 256;

                        float imagescalex = this.Width / originalwidth;
                        float imagescaley = this.Height / originalheight;


                        Bitmap TempBmp = new Bitmap(maphackprofile.orgminimap[minimapid], this.Width, this.Height);


                        using (Graphics g = Graphics.FromImage(TempBmp))
                        {
                            if (maphackprofile.IsTransparentMinimap)
                            {
                                g.Clear(Color.FromArgb(255, 12, 123, 123));
                            }

                            switch (maphackprofile.DrawAccurate)
                            {
                                case MainMenu.DrawQuality.Accurate:
                                    g.CompositingQuality = CompositingQuality.HighQuality;
                                    g.SmoothingMode = SmoothingMode.HighQuality;
                                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    break;
                                case MainMenu.DrawQuality.Fast:
                                    g.CompositingQuality = CompositingQuality.HighSpeed;
                                    g.SmoothingMode = SmoothingMode.HighSpeed;
                                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                                    g.InterpolationMode = InterpolationMode.Low;
                                    break;
                                default:
                                    break;
                            }


                            for (int i = 0; i < ItemsCount; i++)
                            {
                                try
                                {
                                    int CurrentItemId = i;
                                    int CurrentItemAdress = ItemsArrayAddr + CurrentItemId * 4;
                                    CurrentItemAdress = war3processmemory.ReadInt(CurrentItemAdress);
                                    float CurrentItemHP = war3processmemory.ReadFloat(CurrentItemAdress + 0x58);
                                    int itemflags = war3processmemory.ReadInt(CurrentItemAdress + 0x20);
                                    if (CurrentItemHP > 0.0f && (itemflags & 1) == 0)
                                    {
                                        string CurrentItemStringId = Reverse(war3processmemory.ReadStringWarcraft(CurrentItemAdress + 0x30, 4));
                                        int CurrentItemInfoAddr = war3processmemory.ReadInt(CurrentItemAdress + 0x28);
                                        float CurrentItemX = war3processmemory.ReadFloat(CurrentItemInfoAddr + 0x88) + Math.Abs(mapleft);
                                        float CurrentItemY = war3processmemory.ReadFloat(CurrentItemInfoAddr + 0x8C) + Math.Abs(mapbot);


                                        float CurrentItemMiniMapX = CurrentItemX / minimaponex;
                                        float CurrentItemMiniMapY = this.Height - (CurrentItemY / minimaponey);

                                        for (int objid = 0; objid < maphackprofile.items.Length; objid++)
                                        {
                                            if (maphackprofile.items[objid].name == CurrentItemStringId)
                                            {
                                                if (maphackprofile.items[objid].framecount > 1)
                                                {
                                                    //maphackprofile.items[objid].frameid++;
                                                    if (maphackprofile.items[objid].frameid >= maphackprofile.items[objid].framecount)
                                                        maphackprofile.items[objid].frameid = 0;


                                                    float imageoffsetx = maphackprofile.items[objid].frames[maphackprofile.items[objid].frameid].Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                    float imageoffsety = maphackprofile.items[objid].frames[maphackprofile.items[objid].frameid].Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                    float drawoffsetx = CurrentItemMiniMapX;
                                                    drawoffsetx -= imageoffsetx;
                                                    float drawoffsety = CurrentItemMiniMapY;
                                                    drawoffsety -= imageoffsety;
                                                    g.DrawImage(maphackprofile.items[objid].frames[maphackprofile.items[objid].frameid]
                                                        , drawoffsetx
                                                        , drawoffsety
                                                        , maphackprofile.items[objid].frames[maphackprofile.items[objid].frameid].Width * imagescalex * maphackprofile.DefaultIconScale, maphackprofile.items[objid].frames[maphackprofile.items[objid].frameid].Height * imagescaley * maphackprofile.DefaultIconScale);
                                                }
                                                else
                                                {
                                                    float imageoffsetx = maphackprofile.items[objid].image.Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                    float imageoffsety = maphackprofile.items[objid].image.Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                    float drawoffsetx = CurrentItemMiniMapX;
                                                    drawoffsetx -= imageoffsetx;
                                                    float drawoffsety = CurrentItemMiniMapY;
                                                    drawoffsety -= imageoffsety;

                                                    g.DrawImage(maphackprofile.items[objid].image
                                                        , drawoffsetx
                                                        , drawoffsety
                                                        , maphackprofile.items[objid].image.Width * imagescalex * maphackprofile.DefaultIconScale, maphackprofile.items[objid].image.Height * imagescaley * maphackprofile.DefaultIconScale);
                                                }
                                            }
                                        }


                                        /*if ( CurrentItemStringId == "K00I" )
                                            g.FillRectangle( new SolidBrush( Color.Blue ) , CurrentItemMiniMapX - 4 , CurrentItemMiniMapY - 4 , 8 , 8 );
                                        else if ( CurrentItemStringId == "800I" )
                                            g.FillRectangle( new SolidBrush( Color.Green ) , CurrentItemMiniMapX - 4 , CurrentItemMiniMapY - 4 , 8 , 8 );
                                        else if ( CurrentItemStringId == "J00I" )
                                            g.FillRectangle( new SolidBrush( Color.BlueViolet ) , CurrentItemMiniMapX - 4 , CurrentItemMiniMapY - 4 , 8 , 8 );
                                        else if ( CurrentItemStringId == "600I" )
                                            g.FillRectangle( new SolidBrush( Color.Red ) , CurrentItemMiniMapX - 4 , CurrentItemMiniMapY - 4 , 8 , 8 );
                                        else if ( CurrentItemStringId == "700I" )
                                            g.FillRectangle( new SolidBrush( Color.Orange ) , CurrentItemMiniMapX - 4 , CurrentItemMiniMapY - 4 , 8 , 8 );
                                        else if ( CurrentItemStringId == "CR0I" )
                                            g.FillRectangle( new SolidBrush( Color.Gold ) , CurrentItemMiniMapX - 4 , CurrentItemMiniMapY - 4 , 8 , 8 );
                                        */

                                    }
                                }
                                catch
                                {

                                }
                            }



                            for (int DrawHeroes = 0; DrawHeroes < 2; DrawHeroes++)
                            {
                                for (int i = 0; i < UnitsCount; i++)
                                {
                                    // try
                                    {
                                        int CurrentUnitId = i;
                                        int CurrentUnitAdress = UnitsArrayAddr + CurrentUnitId * 4;
                                        CurrentUnitAdress = war3processmemory.ReadInt(CurrentUnitAdress);

                                        uint CurrentUnitFlag = war3processmemory.ReadUInt(CurrentUnitAdress + 0x5C);

                                        uint CurrentUnitFlag2 = war3processmemory.ReadUInt(CurrentUnitAdress + 0x20);

                                        //   uint CurrentUnitFlag3 = war3processmemory.ReadUInt(CurrentUnitAdress + 0x48);

                                        uint CurrentUnitClass = war3processmemory.ReadUInt(CurrentUnitAdress + 0x30);
                                        string CurrentUnitClassString = Reverse(war3processmemory.ReadStringWarcraft(CurrentUnitAdress + 0x30, 4));

                                        bool IsUnitIllusion = false;

                                        if ((CurrentUnitFlag & 0x40000000u) == 0)
                                        {

                                        }
                                        else
                                        {
                                            IsUnitIllusion = true;
                                        }


                                        if ((CurrentUnitFlag & 0x100u) == 0)
                                        {

                                        }
                                        else
                                        {
                                            continue;
                                        }


                                        if (!IsWhiteListedUnit(CurrentUnitClassString))
                                        {

                                            if ((CurrentUnitFlag2 & 0x1u) == 0)
                                            {


                                            }
                                            else
                                            {
                                                continue;
                                            }

                                            if ((CurrentUnitFlag2 & 0x2u) == 0)
                                            {
                                                //    MessageBox.Show("Error2");
                                                continue;
                                            }
                                            else
                                            {

                                            }

                                            if ((CurrentUnitFlag2 & 0x4u) == 0)
                                            {
                                                continue;
                                            }
                                            else
                                            {

                                            }


                                            /*    if ((CurrentUnitFlag3 & 0x1u) == 0)
                                                {
                                                   // MessageBox.Show("Error22");
                                                    continue;
                                                }
                                                else
                                                {

                                                }*/


                                            if ((CurrentUnitFlag2 & 0x8) == 0)
                                            {

                                            }
                                            else
                                            {
                                                ///MessageBox.Show("Error55");
                                                // continue;
                                            }




                                            if ((CurrentUnitFlag & 0x10u) == 0)
                                            {

                                            }
                                            else
                                            {
                                                //   MessageBox.Show("Error2221");
                                                continue;
                                            }


                                            if ((CurrentUnitFlag & 0x300u) == 0)
                                            {
                                                //  continue;
                                            }
                                            else
                                            {
                                                /*   MessageBox.Show("Error555");*/
                                            }

                                            if ((CurrentUnitFlag & 0x80000) == 0)
                                            {

                                            }
                                            else
                                            {
                                                continue;
                                            }

                                            if (IsUnitIllusion)
                                            {
                                                continue;
                                            }
                                        }

                                        bool IsCurrentUnitTower = false;

                                        if ((CurrentUnitFlag & 0x10000) == 0)
                                        {

                                        }
                                        else
                                        {
                                            if (maphackprofile.IsNoTowers)
                                                continue;
                                            IsCurrentUnitTower = true;
                                        }



                                        uint IsCurrentUnitHero = war3processmemory.ReadUInt(CurrentUnitAdress + 48);
                                        IsCurrentUnitHero = IsCurrentUnitHero >> 24;
                                        IsCurrentUnitHero = IsCurrentUnitHero - 64;
                                        if (IsCurrentUnitHero < 0x19)
                                            IsCurrentUnitHero = 1;
                                        else
                                            IsCurrentUnitHero = 0;

                                        if (IsCurrentUnitHero != DrawHeroes)
                                            continue;

                                        int CurrentUnitPlayerSlot = war3processmemory.ReadInt(CurrentUnitAdress + 88);


                                        try
                                        {
                                            if (maphackprofile.ignoreplayers[CurrentUnitPlayerSlot])
                                                continue;
                                        }
                                        catch
                                        {


                                        }
                                        float _UnitX = war3processmemory.ReadFloat(0x284 + CurrentUnitAdress);
                                        float _UnitY = war3processmemory.ReadFloat(0x288 + CurrentUnitAdress);

                                        if (_UnitX == _UnitY || -_UnitX == _UnitY || _UnitX == -_UnitY)
                                        {
                                            //  continue;
                                        }

                                        float CurrentUnitX = _UnitX + Math.Abs(mapleft);
                                        float CurrentUnitY = _UnitY + Math.Abs(mapbot);



                                        float CurrentUnitMiniMapX = CurrentUnitX / minimaponex;
                                        float CurrentUnitMiniMapY = this.Height - (CurrentUnitY / minimaponey);





                                        int CurrentDrawType = -1;
                                        try
                                        {
                                            CurrentDrawType = DrawType(LocalPlayerSlot, CurrentUnitPlayerSlot, CurrentUnitClassString);
                                        }
                                        catch
                                        {

                                        }




                                        switch (CurrentDrawType)
                                        {
                                            case 0:
                                                if (IsCurrentUnitHero == 0)
                                                {
                                                    float drawsizex = 4 * imagescalex;
                                                    float drawsizey = 4 * imagescaley;

                                                    float drawoffsetx = CurrentUnitMiniMapX;
                                                    drawoffsetx -= drawsizex / 2;

                                                    float drawoffsety = CurrentUnitMiniMapY;
                                                    drawoffsety -= 4 * imagescaley / 2;


                                                    g.FillRectangle(new SolidBrush(Color.White), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                }
                                                else
                                                {
                                                    if (maphackprofile.IsAnimatedMinihackHeroes)
                                                    {
                                                        Image PlayerHeroBitmap = maphackprofile.herowhite[maphackprofile.herowhiteframe];
                                                        float imageoffsetx = PlayerHeroBitmap.Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                        float imageoffsety = PlayerHeroBitmap.Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                        float drawoffsetx = CurrentUnitMiniMapX;
                                                        drawoffsetx -= imageoffsetx;
                                                        float drawoffsety = CurrentUnitMiniMapY;
                                                        drawoffsety -= imageoffsety;
                                                        g.DrawImage(PlayerHeroBitmap
                                                            , drawoffsetx
                                                            , drawoffsety
                                                            , PlayerHeroBitmap.Width * imagescalex * maphackprofile.DefaultIconScale, PlayerHeroBitmap.Height * imagescaley * maphackprofile.DefaultIconScale);
                                                    }
                                                    else
                                                    {
                                                        float drawsizex = 10 * imagescalex;
                                                        float drawsizey = 10 * imagescaley;

                                                        float drawoffsetx = CurrentUnitMiniMapX;
                                                        drawoffsetx -= drawsizex / 2;

                                                        float drawoffsety = CurrentUnitMiniMapY;
                                                        drawoffsety -= 4 * imagescaley / 2;


                                                        g.FillEllipse(new SolidBrush(Color.White), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                    }
                                                }
                                                break;
                                            case 1:
                                                if (!MinimapColored)
                                                {
                                                    if (IsCurrentUnitHero == 0)
                                                    {
                                                        if (IsCurrentUnitTower)
                                                        {
                                                            float drawsizex = 6 * imagescalex;
                                                            float drawsizey = 6 * imagescaley;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= drawsizex / 2;

                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= 4 * imagescaley / 2;


                                                            g.FillRectangle(new SolidBrush(Color.Aqua), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                        }
                                                        else
                                                        {
                                                            float drawsizex = 4 * imagescalex;
                                                            float drawsizey = 4 * imagescaley;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= drawsizex / 2;

                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= 4 * imagescaley / 2;


                                                            g.FillRectangle(new SolidBrush(Color.Aqua), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (maphackprofile.IsAnimatedMinihackHeroes)
                                                        {
                                                            Image PlayerHeroBitmap = maphackprofile.heroaqua[maphackprofile.heroaquaframe];
                                                            float imageoffsetx = PlayerHeroBitmap.Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                            float imageoffsety = PlayerHeroBitmap.Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= imageoffsetx;
                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= imageoffsety;
                                                            g.DrawImage(PlayerHeroBitmap
                                                                , drawoffsetx
                                                                , drawoffsety
                                                                , PlayerHeroBitmap.Width * imagescalex * maphackprofile.DefaultIconScale, PlayerHeroBitmap.Height * imagescaley * maphackprofile.DefaultIconScale);

                                                        }
                                                        else
                                                        {
                                                            float drawsizex = 10 * imagescalex;
                                                            float drawsizey = 10 * imagescaley;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= drawsizex / 2;

                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= 4 * imagescaley / 2;


                                                            g.FillEllipse(new SolidBrush(Color.Aqua), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Color herocolor = GetColorByPlayerSlot(CurrentUnitPlayerSlot);
                                                    bool IsCurrentUnitSentinel = IsScourge(CurrentUnitPlayerSlot);
                                                    if (IsCurrentUnitHero == 0)
                                                    {
                                                        if (IsCurrentUnitSentinel)
                                                        {
                                                            if (IsCurrentUnitTower)
                                                            {
                                                                float drawsizex = 6 * imagescalex;
                                                                float drawsizey = 6 * imagescaley;

                                                                float drawoffsetx = CurrentUnitMiniMapX;
                                                                drawoffsetx -= drawsizex / 2;

                                                                float drawoffsety = CurrentUnitMiniMapY;
                                                                drawoffsety -= 4 * imagescaley / 2;


                                                                g.FillRectangle(new SolidBrush(Color.Red), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                            }
                                                            else
                                                            {
                                                                float drawsizex = 4 * imagescalex;
                                                                float drawsizey = 4 * imagescaley;

                                                                float drawoffsetx = CurrentUnitMiniMapX;
                                                                drawoffsetx -= drawsizex / 2;

                                                                float drawoffsety = CurrentUnitMiniMapY;
                                                                drawoffsety -= 4 * imagescaley / 2;


                                                                g.FillRectangle(new SolidBrush(Color.Red), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (IsCurrentUnitTower)
                                                            {
                                                                float drawsizex = 6 * imagescalex;
                                                                float drawsizey = 6 * imagescaley;

                                                                float drawoffsetx = CurrentUnitMiniMapX;
                                                                drawoffsetx -= drawsizex / 2;

                                                                float drawoffsety = CurrentUnitMiniMapY;
                                                                drawoffsety -= 4 * imagescaley / 2;


                                                                g.FillRectangle(new SolidBrush(Color.Green), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                            }
                                                            else
                                                            {
                                                                float drawsizex = 4 * imagescalex;
                                                                float drawsizey = 4 * imagescaley;

                                                                float drawoffsetx = CurrentUnitMiniMapX;
                                                                drawoffsetx -= drawsizex / 2;

                                                                float drawoffsety = CurrentUnitMiniMapY;
                                                                drawoffsety -= 4 * imagescaley / 2;


                                                                g.FillRectangle(new SolidBrush(Color.Green), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (maphackprofile.IsAnimatedMinihackHeroes)
                                                        {
                                                            Image PlayerHeroBitmap = GetHeroImageByPlayerSlot(CurrentUnitPlayerSlot);
                                                            float imageoffsetx = PlayerHeroBitmap.Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                            float imageoffsety = PlayerHeroBitmap.Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= imageoffsetx;
                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= imageoffsety;
                                                            g.DrawImage(PlayerHeroBitmap
                                                                , drawoffsetx
                                                                , drawoffsety
                                                                , PlayerHeroBitmap.Width * imagescalex * maphackprofile.DefaultIconScale, PlayerHeroBitmap.Height * imagescaley * maphackprofile.DefaultIconScale);

                                                        }
                                                        else
                                                        {
                                                            float drawsizex = 10 * imagescalex;
                                                            float drawsizey = 10 * imagescaley;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= drawsizex / 2;

                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= 4 * imagescaley / 2;


                                                            g.FillEllipse(new SolidBrush(herocolor), drawoffsetx, drawoffsety, drawsizex, drawsizey);

                                                        }
                                                    }
                                                }
                                                break;
                                            case 2:
                                                if (!MinimapColored)
                                                {
                                                    if (IsCurrentUnitHero == 0)
                                                    {
                                                        if (IsCurrentUnitTower)
                                                        {
                                                            float drawsizex = 6 * imagescalex;
                                                            float drawsizey = 6 * imagescaley;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= drawsizex / 2;

                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= 4 * imagescaley / 2;


                                                            g.FillRectangle(new SolidBrush(Color.Red), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                        }
                                                        else
                                                        {
                                                            float drawsizex = 4 * imagescalex;
                                                            float drawsizey = 4 * imagescaley;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= drawsizex / 2;

                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= 4 * imagescaley / 2;


                                                            g.FillRectangle(new SolidBrush(Color.Red), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (maphackprofile.IsAnimatedMinihackHeroes)
                                                        {
                                                            Image PlayerHeroBitmap = maphackprofile.herored[maphackprofile.heroredframe];
                                                            float imageoffsetx = PlayerHeroBitmap.Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                            float imageoffsety = PlayerHeroBitmap.Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= imageoffsetx;
                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= imageoffsety;
                                                            g.DrawImage(PlayerHeroBitmap
                                                                , drawoffsetx
                                                                , drawoffsety
                                                                , PlayerHeroBitmap.Width * imagescalex * maphackprofile.DefaultIconScale, PlayerHeroBitmap.Height * imagescaley * maphackprofile.DefaultIconScale);

                                                        }
                                                        else
                                                        {

                                                            float drawsizex = 10 * imagescalex;
                                                            float drawsizey = 10 * imagescaley;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= drawsizex / 2;

                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= 4 * imagescaley / 2;


                                                            g.FillEllipse(new SolidBrush(Color.Red), drawoffsetx, drawoffsety, drawsizex, drawsizey);

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Color herocolor = GetColorByPlayerSlot(CurrentUnitPlayerSlot);
                                                    bool IsCurrentUnitSentinel = IsScourge(CurrentUnitPlayerSlot);
                                                    if (IsCurrentUnitHero == 0)
                                                    {
                                                        if (IsCurrentUnitSentinel)
                                                        {
                                                            if (IsCurrentUnitTower)
                                                            {
                                                                float drawsizex = 6 * imagescalex;
                                                                float drawsizey = 6 * imagescaley;

                                                                float drawoffsetx = CurrentUnitMiniMapX;
                                                                drawoffsetx -= drawsizex / 2;

                                                                float drawoffsety = CurrentUnitMiniMapY;
                                                                drawoffsety -= 4 * imagescaley / 2;


                                                                g.FillRectangle(new SolidBrush(Color.Red), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                            }
                                                            else
                                                            {
                                                                float drawsizex = 4 * imagescalex;
                                                                float drawsizey = 4 * imagescaley;

                                                                float drawoffsetx = CurrentUnitMiniMapX;
                                                                drawoffsetx -= drawsizex / 2;

                                                                float drawoffsety = CurrentUnitMiniMapY;
                                                                drawoffsety -= 4 * imagescaley / 2;


                                                                g.FillRectangle(new SolidBrush(Color.Red), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (IsCurrentUnitTower)
                                                            {
                                                                float drawsizex = 6 * imagescalex;
                                                                float drawsizey = 6 * imagescaley;

                                                                float drawoffsetx = CurrentUnitMiniMapX;
                                                                drawoffsetx -= drawsizex / 2;

                                                                float drawoffsety = CurrentUnitMiniMapY;
                                                                drawoffsety -= 4 * imagescaley / 2;


                                                                g.FillRectangle(new SolidBrush(Color.Green), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                            }
                                                            else
                                                            {
                                                                float drawsizex = 4 * imagescalex;
                                                                float drawsizey = 4 * imagescaley;

                                                                float drawoffsetx = CurrentUnitMiniMapX;
                                                                drawoffsetx -= drawsizex / 2;

                                                                float drawoffsety = CurrentUnitMiniMapY;
                                                                drawoffsety -= 4 * imagescaley / 2;


                                                                g.FillRectangle(new SolidBrush(Color.Green), drawoffsetx, drawoffsety, drawsizex, drawsizey);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (maphackprofile.IsAnimatedMinihackHeroes)
                                                        {
                                                            Image PlayerHeroBitmap = GetHeroImageByPlayerSlot(CurrentUnitPlayerSlot);
                                                            float imageoffsetx = PlayerHeroBitmap.Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                            float imageoffsety = PlayerHeroBitmap.Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= imageoffsetx;
                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= imageoffsety;
                                                            g.DrawImage(PlayerHeroBitmap
                                                                , drawoffsetx
                                                                , drawoffsety
                                                                , PlayerHeroBitmap.Width * imagescalex * maphackprofile.DefaultIconScale, PlayerHeroBitmap.Height * imagescaley * maphackprofile.DefaultIconScale);

                                                        }
                                                        else
                                                        {
                                                            float drawsizex = 10 * imagescalex;
                                                            float drawsizey = 10 * imagescaley;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= drawsizex / 2;

                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= 4 * imagescaley / 2;


                                                            g.FillEllipse(new SolidBrush(herocolor), drawoffsetx, drawoffsety, drawsizex, drawsizey);

                                                        }
                                                    }
                                                }
                                                break;
                                            case 3:
                                                // g.FillRectangle( new SolidBrush( Color.Green ) , CurrentUnitMiniMapX - 2 , CurrentUnitMiniMapY - 2 , 4 , 4 );
                                                break;
                                            case 4:
                                                {
                                                    float drawsizex = 4 * imagescalex;
                                                    float drawsizey = 4 * imagescaley;

                                                    float drawoffsetx = CurrentUnitMiniMapX;
                                                    drawoffsetx -= drawsizex / 2;

                                                    float drawoffsety = CurrentUnitMiniMapY;
                                                    drawoffsety -= 4 * imagescaley / 2;


                                                    g.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 50)), drawoffsetx, drawoffsety, drawsizex, drawsizey);

                                                }
                                                break;
                                            case 5:

                                                for (int objid = 0; objid < maphackprofile.units.Length; objid++)
                                                {
                                                    if (maphackprofile.units[objid].name == CurrentUnitClassString)
                                                    {
                                                        if (maphackprofile.units[objid].framecount > 1)
                                                        {
                                                            // maphackprofile.units[objid].frameid++;
                                                            if (maphackprofile.units[objid].frameid >= maphackprofile.units[objid].framecount)
                                                                maphackprofile.units[objid].frameid = 0;

                                                            float imageoffsetx = maphackprofile.units[objid].frames[maphackprofile.units[objid].frameid].Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                            float imageoffsety = maphackprofile.units[objid].frames[maphackprofile.units[objid].frameid].Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= imageoffsetx;
                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= imageoffsety;
                                                            g.DrawImage(maphackprofile.units[objid].frames[maphackprofile.units[objid].frameid]
                                                                , drawoffsetx
                                                                , drawoffsety
                                                                , maphackprofile.units[objid].frames[maphackprofile.units[objid].frameid].Width * imagescalex * maphackprofile.DefaultIconScale, maphackprofile.units[objid].frames[maphackprofile.units[objid].frameid].Height * imagescaley * maphackprofile.DefaultIconScale);
                                                        }
                                                        else
                                                        {
                                                            float imageoffsetx = maphackprofile.units[objid].image.Width * (maphackprofile.DefaultIconScale * imagescalex) / 2.0f;
                                                            float imageoffsety = maphackprofile.units[objid].image.Height * (maphackprofile.DefaultIconScale * imagescaley) / 2.0f;

                                                            float drawoffsetx = CurrentUnitMiniMapX;
                                                            drawoffsetx -= imageoffsetx;
                                                            float drawoffsety = CurrentUnitMiniMapY;
                                                            drawoffsety -= imageoffsety;
                                                            g.DrawImage(maphackprofile.units[objid].image
                                                                , drawoffsetx
                                                                , drawoffsety
                                                                , maphackprofile.units[objid].image.Width * imagescalex * maphackprofile.DefaultIconScale, maphackprofile.units[objid].image.Height * imagescaley * maphackprofile.DefaultIconScale);
                                                        }

                                                    }
                                                }


                                                break;
                                        }

                                    }
                                    //catch
                                    //{

                                    //}
                                }
                            }

                            if (maphackprofile.herowhite.Length > 1)
                            {
                                if (maphackprofile.herowhiteframe < maphackprofile.herowhite.Length)
                                {
                                    maphackprofile.herowhiteframe++;
                                }
                                if (maphackprofile.herowhiteframe < maphackprofile.herowhite.Length)
                                {

                                }
                                else
                                {
                                    maphackprofile.herowhiteframe = 0;
                                }
                            }

                            if (maphackprofile.herored.Length > 1)
                            {
                                if (maphackprofile.heroredframe < maphackprofile.herored.Length)
                                {
                                    maphackprofile.heroredframe++;
                                }
                                if (maphackprofile.heroredframe < maphackprofile.herored.Length)
                                {

                                }
                                else
                                {
                                    maphackprofile.heroredframe = 0;
                                }
                            }

                            if (maphackprofile.heroaqua.Length > 1)
                            {
                                if (maphackprofile.heroaquaframe < maphackprofile.heroaqua.Length)
                                {
                                    maphackprofile.heroaquaframe++;
                                }
                                if (maphackprofile.heroaquaframe < maphackprofile.heroaqua.Length)
                                {

                                }
                                else
                                {
                                    maphackprofile.heroaquaframe = 0;
                                }
                            }



                            for (int objid = 0; objid < maphackprofile.units.Length; objid++)
                            {
                                if (maphackprofile.units[objid].framecount > 0)
                                {
                                    if (maphackprofile.units[objid].frameid < maphackprofile.units[objid].framecount)
                                        maphackprofile.units[objid].frameid++;
                                    if (maphackprofile.units[objid].frameid < maphackprofile.units[objid].framecount)
                                    {

                                    }
                                    else
                                    {
                                        maphackprofile.units[objid].frameid = 0;
                                    }

                                }

                            }

                            for (int objid = 0; objid < maphackprofile.items.Length; objid++)
                            {
                                if (maphackprofile.items[objid].framecount > 0)
                                {
                                    if (maphackprofile.items[objid].frameid < maphackprofile.items[objid].framecount)
                                        maphackprofile.items[objid].frameid++;
                                    if (maphackprofile.items[objid].frameid < maphackprofile.items[objid].framecount)
                                    {

                                    }
                                    else
                                    {
                                        maphackprofile.items[objid].frameid = 0;
                                    }
                                }

                            }



                            for (int i = 0; i < 12; i++)
                            {
                                GetHeroImageByPlayerSlot(i, true);
                            }

                            #region DrawHappy

                            if (FirstGameFound && happyoffset > -50)
                            {
                                happyoffset -= 15;
                                try
                                {
                                    if (happyframeid < happyframes.Count - 1)
                                    {
                                        happyframeid++;
                                    }
                                    else
                                    {
                                        happyframeid = 0;
                                    }

                                    if (happyframeid < happyframes.Count - 1 && happyframeid != 0)
                                    {
                                        happyframeid++;
                                    }
                                    else
                                    {
                                        happyframeid = 0;
                                    }

                                    if (happyframeid < happyframes.Count - 1 && happyframeid != 0)
                                    {
                                        happyframeid++;
                                    }
                                    else
                                    {
                                        happyframeid = 0;
                                    }

                                    if (happyframeid < happyframes.Count - 1 && happyframeid != 0)
                                    {
                                        happyframeid++;
                                    }
                                    else
                                    {
                                        happyframeid = 0;
                                    }

                                    g.DrawImage(happyframes[happyframeid], 60 * imagescalex, 100 * imagescaley, 100 * imagescalex, 100 * imagescaley);
                                }
                                catch
                                {

                                }
                            }
                            else
                            {
                                FirstGameFound = false;
                            }

                            #endregion


                        }

                        BmpForDraw = TempBmp;
                        this.BackgroundImage = /*ResizeImage(*/BmpForDraw/*, this.Width, this.Height)*/;
                        this.Update();
                    }

                    /* if ( BmpForDraw != null )
                     {
                         byte [ ] writedata = MinimapToByte( ( Image ) ( new Bitmap( BmpForDraw ) ) );
                         war3processmemory.WriteMem( minimapbackground , writedata );
                     }*/

                    /* MessageBox.Show( "MapCoord: LEFT->" + mapleft.ToString( ) + ", TOP->" + maptop
                         + ", RIGHT->" + mapright.ToString( ) + ", BOTTOM->" + mapbot.ToString( ) );
                     */

                    //return;


                }
            }
            /*  catch
              {

              }*/

            timerwork = false;
        }


        private void MiniMapImg_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void MiniMapImg_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.9;
        }

        static Random rnd = new Random();
        private void Minimaphack_Load(object sender, EventArgs e)
        {

            // ControlMoverOrResizer.WorkType = ControlMoverOrResizer.MoveOrResize.Resize;
            //  ControlMoverOrResizer.Init(this);

            this.Width += rnd.Next(0, 50) - 25;
            this.Height += rnd.Next(0, 50) - 25;

            thisform = (Form)sender;

            if (maphackprofile.IsFastRedraw)
                MinihackTimer.Interval = 75;


            if (!maphackprofile.IsClickabledWithCTRL)
            {
                ClickabledTimer.Enabled = false;
            }


            try
            {
                var parsersettings = new FileIniDataParser();
                IniData datasettings = parsersettings.ReadFile(System.Reflection.Assembly.GetEntryAssembly().Location + "_.ini");

                try
                {
                    string[] coords = datasettings["General"]["WindowPos"].Split(',');
                    Point point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                    this.Location = point;
                }
                catch
                {

                }
                try
                {
                    string s = datasettings["General"]["WindowSize"];
                    int width, height;
                    string[] dims = s.Split(',');
                    if (int.TryParse(dims[0], out width) && int.TryParse(dims[1], out height))
                    {
                        System.Drawing.Size size = new System.Drawing.Size(width, height);
                        this.Size = size;
                    }
                }
                catch
                {

                }
            }
            catch
            {
                File.Create(System.Reflection.Assembly.GetEntryAssembly().Location + "_.ini").Close();
            }
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private Color UIntToColor(uint color)
        {
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(r, g, b);
        }
        private byte[] MinimapToByte(Image img)
        {
            if (img != null)
            {
                List<byte> byteArray = new List<byte>();
                using (MemoryStream stream = new MemoryStream())
                {
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    //MessageBox.Show( "1" );
                    img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    // MessageBox.Show( "2" );
                    byte[] outarray = stream.ToArray();
                    //MessageBox.Show( "3" );
                    byteArray.AddRange(outarray);
                }

                byteArray.RemoveRange(0, 54);
                return byteArray.ToArray();
            }
            return new List<byte>().ToArray();
        }

        private void MiniMapImg_Resize(object sender, EventArgs e)
        {
            // this.Location = MiniMapImg.Location;
        }

        private void Minimaphack_Resize(object sender, EventArgs e)
        {

        }

        private void MiniMapImg_Click(object sender, EventArgs e)
        {

        }
        public enum GWL
        {
            ExStyle = -20
        }

        public enum WS_EX
        {
            Transparent = 0x20,
            Layered = 0x80000
        }

        public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x2
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern uint GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);


        bool NowAlphaUsed = false;
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private void ClickabledTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void Minimaphack_LocationChanged(object sender, EventArgs e)
        {

        }


        private void MiniMapImg_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void Minimaphack_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }

        private void MyTimerFix_Tick(object sender, EventArgs e)
        {
            int keyforcheck = (int)(maphackprofile.ActiveHotKey & 0xFF);

            if (keyforcheck == 0)
            {
                if ((maphackprofile.ActiveHotKey & 0x40000) > 0)
                {
                    keyforcheck = (int)Keys.LShiftKey;
                }
                else if ((maphackprofile.ActiveHotKey & 0x10000) > 0)
                {
                    keyforcheck = (int)Keys.LMenu;
                }
                else if ((maphackprofile.ActiveHotKey & 0x20000) > 0)
                {
                    keyforcheck = (int)Keys.LControlKey;
                }
            }

            if (keyforcheck > 0)
            {
                if ((GetAsyncKeyState(keyforcheck) & 0x8000) > 0)
                {
                    if (NowAlphaUsed)
                    {
                        NowAlphaUsed = false;
                        uint oldwl = GetWindowLong(this.Handle, GWL.ExStyle);

                        //if ((oldwl & 0x80000) > 0)
                        //{
                        //    oldwl = oldwl - 0x80000;
                        //}

                        if ((oldwl & 0x20) > 0)
                        {
                            oldwl = oldwl - 0x20;
                        }

                        // SetLayeredWindowAttributes(this.Handle, 0, 255, LWA.Alpha);
                        SetWindowLong(this.Handle, GWL.ExStyle, oldwl);
                    }
                }
                else
                {
                    if (!NowAlphaUsed)
                    {
                        NowAlphaUsed = true;
                        uint oldwl = GetWindowLong(this.Handle, GWL.ExStyle);

                        //if ((oldwl & 0x80000) == 0)
                        //{
                        //    oldwl = oldwl | 0x80000;
                        //}

                        if ((oldwl & 0x20) == 0)
                        {
                            oldwl = oldwl | 0x20;
                        }

                        SetWindowLong(this.Handle, GWL.ExStyle, oldwl);
                        //SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);
                    }
                }

            }

            keyforcheck = (int)(maphackprofile.ChangeMinimapHotkey & 0xFF);

            if (keyforcheck == 0)
            {
                if ((maphackprofile.ChangeMinimapHotkey & 0x40000) > 0)
                {
                    keyforcheck = (int)Keys.LShiftKey;
                }
                else if ((maphackprofile.ChangeMinimapHotkey & 0x10000) > 0)
                {
                    keyforcheck = (int)Keys.LMenu;
                }
                else if ((maphackprofile.ChangeMinimapHotkey & 0x20000) > 0)
                {
                    keyforcheck = (int)Keys.LControlKey;
                }
            }

            if (keyforcheck > 0)
            {
                if ((GetAsyncKeyState(keyforcheck) & 0x8000) > 0)
                {
                    minimapid = 1;

                    if (minimapid < maphackprofile.orgminimap.Count)
                    {

                    }
                    else
                    {
                        minimapid = 0;
                    }
                }
                else
                {
                    minimapid = 0;
                }
            }
        }
    }

}
