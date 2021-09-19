using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using ControlManager;


namespace iCCupUnrealMapHack
{
    public partial class MainHack : Form
    {
        //private string licencekey1 = "";
        WebClient webClient = new WebClient();

        private int minimapid = 0;
        private static PrivateFontCollection myFonts;
        private static IntPtr fontBuffer;
        private FontFamily maphackfont = null;

        public Process war3process = null;

        int War3Version = 0x26a;

        static Random rnd = MainMenu.random;


        MainMenu.MaphackProfileStruct maphackprofile;





        /*
         * 
         *  UnitGLobalAddr = 1915C + Scanner.dll
         *  UnitGlobalCount = 19144 + Scanner.dll
         * 
         * 
         * */


        bool ClearMinimap = false;
        bool GameAlreadStarted = false;
        bool FirstGameFound = false;
        int happyoffset = 256;
        int happyframeid = 0;
        List<Image> happyframes = new List<Image>();

        private int GameDll = 0;
        private int trynewgamedll = -1;
        private int mmbgbackaddr = 0;


        bool NeedInjectManabar = false;


        int _InGame1Offset = 0xACF678;
        int _InGame2Offset = 0xAB62A4;

        int _GoldTransferOffset = 0xAB59C4;

        int _GlobalClassOffset = 0xAB4F80;

        int _MinimapInfoOffset = 0xAB6214;

        int _GlobalPlayerOffset = 0xAB65F4;

        int _pGameHashTable = 0xAB7788;



        byte[] _ManabarDll = Properties.Resources.MB;


        int __unitclassoffset = 0x3BC;
        int __unitdatastartoffset = 0x604;

        private void Set126aVersion()
        {

            WarcraftVersion.Text = "1.26a";

            _InGame1Offset = 0xAB62A4;
            _InGame2Offset = 0xAB7E98;
            _GoldTransferOffset = 0xAB59C4;
            _GlobalClassOffset = 0xAB4F80;
            _MinimapInfoOffset = 0xAB6214;
            _GlobalPlayerOffset = 0xAB65F4;
            _pGameHashTable = 0xAB7788;

            _ManabarDll = Properties.Resources.MB;

            War3Version = 0x26a;
        }


        private void Set127aVersion()
        {
            WarcraftVersion.Text = "1.27a";

            _InGame1Offset = 0xBE6530;
            _InGame2Offset = 0xBE3D70;
            _GoldTransferOffset = 0xBE6574;
            _GlobalClassOffset = 0xBE6350;
            _MinimapInfoOffset = 0xBE6DC4;
            _GlobalPlayerOffset = 0xBE4238;
            _pGameHashTable = 0xBE40A8;

            _ManabarDll = Properties.Resources.MB;

            War3Version = 0x27a;
        }


        private void Set127bVersion()
        {
            WarcraftVersion.Text = "1.27b";

            _InGame1Offset = 0xD6AA98;
            _InGame2Offset = 0xD682D8;
            _GoldTransferOffset = 0xD6AADC;
            _GlobalClassOffset = 0xD6A8B8;
            _MinimapInfoOffset = 0xD6B32C;
            _GlobalPlayerOffset = 0xD687A8;
            _pGameHashTable = 0xD68610;

            _ManabarDll = Properties.Resources.MB;

            War3Version = 0x27b;
        }


        private void Set128aVersion()
        {
            WarcraftVersion.Text = "1.28a";

            _InGame1Offset = 0xD753E0;
            _InGame2Offset = 0xD72C20;
            _GoldTransferOffset = 0xD75C20;
            _GlobalClassOffset = 0xD75200;
            _MinimapInfoOffset = 0xD75C70;
            _GlobalPlayerOffset = 0xD730F0;
            _pGameHashTable = 0xD72F58;

            _ManabarDll = Properties.Resources.MB;

            War3Version = 0x28a;
        }


        private void Set128bVersion()
        {
            WarcraftVersion.Text = "1.28.1";

            _InGame1Offset = 0xD7A438;
            _InGame2Offset = 0xD77C78;
            _GoldTransferOffset = 0xD7AC78;
            _GlobalClassOffset = 0xD7A258;
            _MinimapInfoOffset = 0xD7ACC8;
            _GlobalPlayerOffset = 0xD78148;
            _pGameHashTable = 0xD77FB0;

            _ManabarDll = Properties.Resources.MB;

            War3Version = 0x28b;
        }


        private void Set128cVersion()
        {
            WarcraftVersion.Text = "1.28.2";

            _InGame1Offset = 0xD7A438;
            _InGame2Offset = 0xD77C78;
            _GoldTransferOffset = 0xD7AC78;
            _GlobalClassOffset = 0xD7A258;
            _MinimapInfoOffset = 0xD7ACC8;
            _GlobalPlayerOffset = 0xD78148;
            _pGameHashTable = 0xD77FB0;

            _ManabarDll = Properties.Resources.MB;

            War3Version = 0x28c;
        }

        private void Set1284Version()
        {
            WarcraftVersion.Text = "1.28.4";

            _InGame1Offset = 0xD314D0;
            _InGame2Offset = 0xD2ED10;
            _GlobalClassOffset = 0xD312F0;
            _GlobalPlayerOffset = 0xD2F1E0;
            _GoldTransferOffset = 0xD31D10;
            _MinimapInfoOffset = 0xD31D60;
            _pGameHashTable = 0xD2F048;

            _ManabarDll = Properties.Resources.MB;

            War3Version = 0x284;
        }

        private void Set1285Version()
        {
            WarcraftVersion.Text = "1.28.5";

            _InGame1Offset = 0xD328D0;
            _InGame2Offset = 0xD30110;
            _GlobalClassOffset = 0xD326F0;
            _GlobalPlayerOffset = 0xD305E0;
            _MinimapInfoOffset = 0xD33160;

            _GoldTransferOffset = 0xD33110;

            _pGameHashTable = 0xD30448;

            _ManabarDll = Properties.Resources.MB;

            War3Version = 0x285;
        }

        private void Set1290Version()
        {
            WarcraftVersion.Text = "1.29.0.9";

            _InGame1Offset = 0xD3A81C;
            _InGame2Offset = 0xD75A0C;
            _GoldTransferOffset = 0xD3B1E8;
            _GlobalClassOffset = 0xD3A7F8;
            _MinimapInfoOffset = 0xD3B228;
            _GlobalPlayerOffset = 0xD386F4;
            _pGameHashTable = 0xD75EC0;

            _ManabarDll = null;// Properties.Resources.MB;

            __unitclassoffset = 0x3FC;
            __unitdatastartoffset = 0x6A0;

            War3Version = 0x29a;
            MapDataOffset = 0x294;
        }

        private void Set1291Version()
        {
            WarcraftVersion.Text = "1.29.0.9";

            _InGame1Offset = 0xD3A81C;
            _InGame2Offset = 0xD75A0C;
            _GoldTransferOffset = 0xD3B1E8;
            _GlobalClassOffset = 0xD3A7F8;
            _MinimapInfoOffset = 0xD3B228;
            _GlobalPlayerOffset = 0xD386F4;
            _pGameHashTable = 0xD75EC0;

            _ManabarDll = null;// Properties.Resources.MB;

            __unitclassoffset = 0x3FC;
            __unitdatastartoffset = 0x6A0;

            War3Version = 0x29a;
            MapDataOffset = 0x294;
        }

        private void Set1292Version()
        {
            WarcraftVersion.Text = "1.29.2";

            _InGame1Offset = 0xD3A81C + 0x3000;
            _InGame2Offset = 0xD75A0C + 0x3050;
            _GoldTransferOffset = 0xD3B1E8 + 0x3000;
            _GlobalClassOffset = 0xD3A7F8 + 0x3000;
            _MinimapInfoOffset = 0xD3B228 + 0x3000;
            _GlobalPlayerOffset = 0xD386F4 + 0x3000;
            _pGameHashTable = 0xD75EC0 + 0x3050;

            _ManabarDll = null;// Properties.Resources.MB;

            __unitclassoffset = 0x3FC;
            __unitdatastartoffset = 0x6A0;

            War3Version = 0x29a;
            MapDataOffset = 0x294;
        }

        int MapDataOffset = 0x254;

        int HashTableVal_12()
        {
            return war3processmemory.ReadInt(war3processmemory.ReadInt(GameDll + _pGameHashTable) + 12);
        }

        uint HashTableVal_28()
        {
            return war3processmemory.ReadUInt(war3processmemory.ReadInt(GameDll + _pGameHashTable) + 28);
        }

        int HashTableVal_44()
        {
            return war3processmemory.ReadInt(war3processmemory.ReadInt(GameDll + _pGameHashTable) + 44);
        }

        int HashTableVal_60()
        {
            return war3processmemory.ReadInt(war3processmemory.ReadInt(GameDll + _pGameHashTable) + 60);
        }




        int GetPointerFromHashTable(uint X, uint Y)
        {
            bool v2; // zf@3
            int result; // eax@7
            int v4; // ecx@9
            int v5; // ecx@10

            if (!((X >> 31) > 0))
            {
                if (X < HashTableVal_28())
                {
                    v2 = war3processmemory.ReadInt(HashTableVal_12() + (int)(8 * X)) == -2;
                    goto LABEL_6;
                }
                return 0;
            }
            if ((X & 0x7FFFFFFF) >= HashTableVal_60())
            {
                return 0;
            }
            v2 = war3processmemory.ReadInt(HashTableVal_44() + (int)(8 * X)) == -2;
            LABEL_6:
            if (!v2)
            {
                return 0;
            }
            if ((X >> 31) > 0)
            {
                v4 = war3processmemory.ReadInt(HashTableVal_44() + (int)(8 * X) + 4);
                result = war3processmemory.ReadUInt(v4 + 24) != Y ? 0 : v4;
            }
            else
            {
                v5 = war3processmemory.ReadInt(HashTableVal_12() + (int)(8 * X) + 4);
                result = war3processmemory.ReadUInt(v5 + 24) != Y ? 0 : v5;
            }
            return result;

        }

        float GetCameraHeight()
        {
            string outstr = "";
            int offset1 = war3processmemory.ReadInt(GameDll + _GlobalClassOffset);
            if (offset1 > 0)
            {
                outstr += offset1.ToString("x2") + " - ";
                DebugLabel.Text = outstr;
                offset1 = war3processmemory.ReadInt(offset1 + MapDataOffset);
                if (offset1 > 0)
                {
                    outstr += offset1.ToString("x2") + " - ";
                    DebugLabel.Text = outstr;
                    offset1 = (offset1 + 0xfc);
                    if (offset1 > 0)
                    {
                        outstr += offset1.ToString("x2") + " - ";
                        DebugLabel.Text = outstr;
                        uint CameraHashtableX = war3processmemory.ReadUInt(offset1 + 0x8);
                        uint CameraHashtableY = war3processmemory.ReadUInt(offset1 + 0xC);

                        outstr += CameraHashtableX.ToString("x2") + " - ";
                        outstr += CameraHashtableY.ToString("x2") + " - ";

                        DebugLabel.Text = outstr;
                        offset1 = GetPointerFromHashTable(CameraHashtableX, CameraHashtableY);

                        outstr += offset1.ToString("x2") + " - ";
                        DebugLabel.Text = outstr;
                        if (offset1 > 0)
                        {
                            return war3processmemory.ReadFloat(offset1 + 0x78);
                        }
                    }
                }
            }
            return 0.0f;
        }

        float SetCameraHeight(float newcamera)
        {
            string outstr = "";
            int offset1 = war3processmemory.ReadInt(GameDll + _GlobalClassOffset);
            if (offset1 > 0)
            {
                outstr += offset1.ToString("x2") + " - ";
                DebugLabel.Text = outstr;
                offset1 = war3processmemory.ReadInt(offset1 + MapDataOffset); //660
                if (offset1 > 0)
                {
                    outstr += offset1.ToString("x2") + " - ";
                    DebugLabel.Text = outstr;
                    offset1 = (offset1 + 0xfc);
                    if (offset1 > 0)
                    {
                        outstr += offset1.ToString("x2") + " - ";
                        DebugLabel.Text = outstr;
                        uint CameraHashtableX = war3processmemory.ReadUInt(offset1 + 0x8);
                        uint CameraHashtableY = war3processmemory.ReadUInt(offset1 + 0xC);

                        outstr += CameraHashtableX.ToString("x2") + " - ";
                        outstr += CameraHashtableY.ToString("x2") + " - ";

                        DebugLabel.Text = outstr;
                        offset1 = GetPointerFromHashTable(CameraHashtableX, CameraHashtableY);

                        outstr += offset1.ToString("x2") + " - ";
                        DebugLabel.Text = outstr;
                        if (offset1 > 0)
                        {
                            war3processmemory.WriteFloat(offset1 + 0x78, newcamera);
                            return newcamera;
                        }
                    }
                }
            }
            return 0.0f;
        }


        public MainHack(MainMenu.MaphackProfileStruct maphackprofile)
        {

            this.maphackprofile = maphackprofile;

            this.MouseDown += new MouseEventHandler(move_window); // binding the method to the event


            webClient.Encoding = Encoding.UTF8;
           // licencekey1 = webClient.DownloadString("http://*************/.txt");





            //if (licencekey1.IndexOf(ProcessHelper.GetHash(ProcessHelper.ID())) > -1)
                InitializeComponent();

            panel1.MouseDown += new MouseEventHandler(move_window); // binding the method to the event
            panel2.MouseDown += new MouseEventHandler(move_window); // binding the method to the event

            if (myFonts == null)
            {
                myFonts = new PrivateFontCollection();
                byte[] font = Properties.Resources.The_Goldsmith_Vintage;
                fontBuffer = Marshal.AllocCoTaskMem(font.Length);
                Marshal.Copy(font, 0, fontBuffer, font.Length);
                myFonts.AddMemoryFont(fontBuffer, font.Length);
                maphackfont = myFonts.Families[0];
            }


            try
            {
                happyframes = MainMenu.GetImageFrames(Properties.Resources.Absol);
            }
            catch
            {

            }




            SelectedGameProfileName.Text = "Sеlected Profilе: " + maphackprofile.NAME;
        }




        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int LPAR);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

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


        int goldtime = 10;
        private void GoldTransferWatch_Tick(object sender, EventArgs e)
        {
            goldtime--;
            if (goldtime < 0)
            {
                GoldTransferWatch.Enabled = false;
                goldtime = 10;
                TransferGoldEnabled.Checked = false;
                TransferGoldEnabled.Update();
            }
        }


        private void TransferGoldEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (TransferGoldEnabled.Checked)
            {
                GoldTransferWatch.Enabled = true;
                goldtime = 10;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                Syringe.Injector war3inject = new Syringe.Injector(war3process);

                try
                {
                    war3inject.EjectLibraryXXX(MainMenu.ManaBardll);
                }
                catch
                {

                }

            }
            catch
            {

            }

            Thread.Sleep(1400);
            timerwork = false;
            ClearMinimap = true;
        }

        private bool MinimapColored = false;
        Bitmap BmpForDraw = null;
        Bitmap OriginBmp = null;

        ProcessMemory war3processmemory = null;
        bool timerwork = false;

        private double GetDistance(PointF point1, PointF point2)
        {
            //pythagorean theorem c^2 = a^2 + b^2
            //thus c = square root(a^2 + b^2)
            double a = (double)(point2.X - point1.X);
            double b = (double)(point2.Y - point1.Y);

            return Math.Sqrt(a * a + b * b);
        }

        private int GetPlayerBySlot(int playerslot)
        {
            int pGlobalPlayerData = war3processmemory.ReadInt(GameDll + _GlobalPlayerOffset);
            pGlobalPlayerData += playerslot * 4;
            pGlobalPlayerData += 0x58;
            return war3processmemory.ReadInt(pGlobalPlayerData);
        }

        private int GetPlayerColorBySlot(int playerslot)
        {
            return war3processmemory.ReadInt(GetPlayerBySlot(playerslot) + 0x264);
        }

        private int GetTeamBySlot(int playerslot)
        {
            return war3processmemory.ReadInt(GetPlayerBySlot(playerslot) + 0x278);
        }

        private int DrawType(int playerslot, int unitslot, string unitclass)
        {
            if (!NeedHideMMap.Checked)
                if (playerslot == unitslot)
                    return 0;

            int retvalue = 2;



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
                            if (AutoDetectPlayerTeams.Checked)
                            {
                                unitteam = GetTeamBySlot(playerslotid);
                            }
                        }
                        if (playerslot == playerslotid)
                        {
                            playerteam = i;
                            if (AutoDetectPlayerTeams.Checked)
                            {
                                playerteam = GetTeamBySlot(playerslotid);
                            }
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
            else
            {
                if (AutoDetectPlayerTeams.Checked)
                {
                    if (GetTeamBySlot(playerslot) != GetTeamBySlot(unitslot))
                    {
                        retvalue = 2;
                    }

                }
            }

            if (retvalue == 2 || (DrawAllUnits.Checked && All_items.Checked))
            {
                foreach (MainMenu.War3ObjectDraw curobjitem in maphackprofile.units)
                {
                    if (curobjitem.name == unitclass)
                    {
                        return 5;
                    }
                }
            }

            if (DrawAllUnits.Checked && All_allies.Checked)
            {
                if (retvalue == 0)
                    return 1;
            }


            if (NeedHideMMap.Checked && retvalue == 0)
                retvalue = 1;

            return retvalue;

        }

        private bool IsScourge(int slotid)
        {
            return slotid < 6;
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

        private Color GetColorByPlayerSlot(int slot)
        {
            int red, green, blue;

            if (autodetectplayercolor.Checked)
            {
                slot = GetPlayerColorBySlot(slot);
            }
            else
            {
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


        int TimeOfTimer = 0;
        int zoomheroes = 1;
        int UpdateUnitsInfos = 0;


        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }




        bool lostprocessmemory = false;

        bool FirstWork = true;



        void RenameReloc(ref byte[] barray)
        {
            List<byte> testreloc = new List<byte>();
            /*   for (int i = 0; i < barray.Length - 10; i++)
               {
                   testreloc.Clear();
                   testreloc.Add(barray[i]);
                   testreloc.Add(barray[i + 1]);
                   testreloc.Add(barray[i + 2]);
                   testreloc.Add(barray[i + 3]);
                   testreloc.Add(barray[i + 4]);
                   if (Encoding.UTF8.GetString(testreloc.ToArray()) == "guard")
                   {
                       string randomloc = MainMenu.RandomString(MainMenu.random.Next(5));
                       byte[] outdata = Encoding.UTF8.GetBytes(randomloc);
                       for (int x = 0; x < outdata.Length; x++)
                       {
                           barray[i + x] = outdata[x];
                       }
                       break;
                   }

               }
               */

            for (int i = 0; i < barray.Length - 10; i++)
            {
                testreloc.Clear();
                testreloc.Add(barray[i]);
                testreloc.Add(barray[i + 1]);
                testreloc.Add(barray[i + 2]);
                testreloc.Add(barray[i + 3]);
                testreloc.Add(barray[i + 4]);
                if (Encoding.UTF8.GetString(testreloc.ToArray()) == "Login")
                {
                    string randomloc = MainMenu.RandomString(5);
                    byte[] outdata = Encoding.UTF8.GetBytes(randomloc);
                    for (int x = 0; x < outdata.Length; x++)
                    {
                        barray[i + x] = outdata[x];
                    }
                    MainMenu.ManaberLogin = randomloc;
                    break;
                }

            }



            for (int i = 0; i < barray.Length - 20; i++)
            {
                testreloc.Clear();
                testreloc.Add(barray[i]);//S
                testreloc.Add(barray[i + 1]);//e
                testreloc.Add(barray[i + 2]);//t
                testreloc.Add(barray[i + 3]);//C
                testreloc.Add(barray[i + 4]);//a
                testreloc.Add(barray[i + 5]);//m
                testreloc.Add(barray[i + 6]);//e
                testreloc.Add(barray[i + 7]);//r
                testreloc.Add(barray[i + 8]);//a
                testreloc.Add(barray[i + 9]);//D
                testreloc.Add(barray[i + 10]);//i
                testreloc.Add(barray[i + 11]);//s
                testreloc.Add(barray[i + 12]);//t
                testreloc.Add(barray[i + 13]);//a
                testreloc.Add(barray[i + 14]);//n
                testreloc.Add(barray[i + 15]);//c
                testreloc.Add(barray[i + 16]);//e
                if (Encoding.UTF8.GetString(testreloc.ToArray()) == "SetCameraDistance")
                {
                    string randomloc = MainMenu.RandomString(17);
                    byte[] outdata = Encoding.UTF8.GetBytes(randomloc);
                    for (int x = 0; x < outdata.Length; x++)
                    {
                        barray[i + x] = outdata[x];
                    }
                    MainMenu.CameraDistanceFunc = randomloc;
                    break;
                }

            }



        }


        private void SaveLibrariesToDir()
        {
            // File.WriteAllBytes(".\\mpress.exe", Properties.Resources.mpress);

            try
            {
                RenameReloc(ref _ManabarDll);
                File.WriteAllBytes(".\\" + MainMenu.ManaBardll, _ManabarDll);
                /*  ProcessStartInfo psinfo = new ProcessStartInfo(".\\mpress.exe", MainMenu.ManaBardll);
                  psinfo.WindowStyle = ProcessWindowStyle.Hidden;
                  psinfo.UseShellExecute = false;
                  psinfo.CreateNoWindow = true;
                  psinfo.RedirectStandardOutput = true;
                  psinfo.RedirectStandardError = true;
                  Process.Start(psinfo).WaitForExit();*/
            }
            catch
            {

            }



            // File.Delete(".\\mpress.exe");
        }

        bool NeedHideMMapChanged = false;

        bool lostmemory = false;

        int x3Timer = 0;

        private void MinimapUpdateTimer_Tick(object sender, EventArgs e)
        {
            //if ( !DrawMinimapIngame.Checked )
            //{
            //    timerwork = true;
            //}

            if (timerwork)
            {
                return;
            }
            timerwork = true;
            x3Timer++;
            if (x3Timer > 3)
                x3Timer = 0;

            if (ClearMinimap)
            {
                CloseTimer.Enabled = true;
            }


            if (TimeOfTimer < 1000 * 60)
                TimeOfTimer += MinimapUpdateTimer.Interval;
            else
            {
                TimeOfTimer = 0;
            }


            zoomheroes++;

            if (zoomheroes > 30)
                zoomheroes = 1;


            if (UpdateUnitsInfos > 3)
            {
                UpdateUnitsInfos = 1;
            }
            else
            {
                UpdateUnitsInfos++;
            }



            try
            {
                if (war3processmemory == null || !war3processmemory.CheckProcess())
                {
                    int war3proclen = Process.GetProcessesByName("war3").Length;
                    int war3proclen2 = Process.GetProcessesByName("Warcraft III").Length;
                    if (war3proclen == 0 && war3proclen2 == 0)
                    {
                        if (!lostprocessmemory)
                        {
                            lostprocessmemory = true;

                            war3processmemory = null;
                        }

                        bool vfound = false;

                        foreach (Process prc in Process.GetProcesses())
                        {
                            if (prc.MainModule.FileVersionInfo.OriginalFilename.ToLower() == ("war3.exe".ToLower()))
                            {
                                war3process = prc;
                                ProcessMemory war3mem =
                                    new ProcessMemory(prc.Id, prc.ProcessName);
                                war3mem.StartProcess();
                                war3processmemory = war3mem;
                                lostprocessmemory = false;
                                vfound = true;
                                break;
                            }
                            else if (prc.MainModule.FileVersionInfo.OriginalFilename.ToLower() == ("Warcraft III.exe".ToLower()))
                            {
                                war3process = prc;
                                ProcessMemory war3mem =
                                    new ProcessMemory(prc.Id, prc.ProcessName);
                                war3mem.StartProcess();
                                war3processmemory = war3mem;
                                lostprocessmemory = false;
                                vfound = true;
                                break;
                            }
                        }

                        if (!vfound)
                            foreach (Process prc in Process.GetProcesses())
                            {
                                if (prc.MainWindowTitle == ("Warcraft III".ToLower()))
                                {
                                    war3process = prc;
                                    ProcessMemory war3mem =
                                        new ProcessMemory(prc.Id, prc.ProcessName);
                                    war3mem.StartProcess();
                                    war3processmemory = war3mem;
                                    lostprocessmemory = false;
                                }
                            }

                    }
                    else if (war3proclen == 1 || war3proclen2 == 1)
                    {
                        try
                        {
                            war3process = Process.GetProcessesByName("war3")[0];
                            ProcessMemory war3mem =
                                new ProcessMemory(war3process.Id, "war3");
                            war3mem.StartProcess();
                            war3processmemory = war3mem;
                            lostprocessmemory = false;
                        }
                        catch
                        {
                            war3process = Process.GetProcessesByName("Warcraft III")[0];
                            ProcessMemory war3mem =
                                new ProcessMemory(war3process.Id, "Warcraft III");
                            war3mem.StartProcess();
                            war3processmemory = war3mem;
                            lostprocessmemory = false;
                        }
                    }
                    else if (war3proclen > 1 || war3proclen2 > 1)
                    {
                        if (MessageBox.Show("Warning found multiple war3 process. Close all?", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            foreach (Process prc in Process.GetProcessesByName("war3"))
                            {
                                prc.Kill();
                            }
                        }

                        button1_Click(1, new EventArgs());
                    }
                    else
                    {
                        MessageBox.Show("No war3 found!");
                    }

                    if (war3processmemory != null)
                    {
                        if (FirstWork)
                        {
                            try
                            {
                                Syringe.Injector war3inject = new Syringe.Injector(war3process);


                                try
                                {
                                    war3inject.EjectLibraryXXX(MainMenu.ManaBardll);
                                }
                                catch
                                {

                                }
                            }
                            catch
                            {

                            }
                            if (war3process.MainModule.FileVersionInfo.FileVersion == "1, 26, 0, 6401")
                            {
                                Set126aVersion();
                                SaveLibrariesToDir();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1, 27, 0, 52240")
                            {
                                Set127aVersion();
                                SaveLibrariesToDir();
                            }

                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1, 27, 1, 7085")
                            {
                                Set127bVersion();
                                SaveLibrariesToDir();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.0.7205")
                            {
                                Set128aVersion();
                                SaveLibrariesToDir();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.1.7365")
                            {
                                Set128bVersion();
                                SaveLibrariesToDir();
                            }

                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.2.7395")
                            {
                                Set128cVersion();
                                SaveLibrariesToDir();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.4.7608")
                            {
                                Set1284Version();
                                SaveLibrariesToDir();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.28.5.7680")
                            {
                                Set1285Version();
                                SaveLibrariesToDir();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.29.0.9055")
                            {
                                Set1290Version();
                                SaveLibrariesToDir();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.29.1.9160")
                            {
                                Set1291Version();
                                SaveLibrariesToDir();
                            }
                            else if (war3process.MainModule.FileVersionInfo.FileVersion == "1.29.2.9231")
                            {
                                Set1292Version();
                                SaveLibrariesToDir();
                            }
                            else
                            {
                                MessageBox.Show("Warning! Warcraft :" + war3process.MainModule.FileVersionInfo.FileVersion + "\n NOT SUPPORTED. Please contact developer!");
                                File.WriteAllText(".\\warver.txt", war3process.MainModule.FileVersionInfo.FileVersion);
                                button1_Click(1, new EventArgs());
                            }
                            FirstWork = false;

                        }




                    }

                }

            }
            catch
            {
                if (!lostprocessmemory)
                {
                    lostprocessmemory = true;

                    war3processmemory = null;
                }
            }


            try
            {
                if (war3processmemory != null)
                {

                    if (!war3processmemory.CheckProcess())
                    {
                        if (!lostprocessmemory)
                        {
                            lostprocessmemory = true;

                            war3processmemory = null;
                        }
                        GameTime.Text = "No proc found.";
                        timerwork = false;
                        return;
                    }

                    if (UpdateUnitsInfos > 2)
                    {
                        UpdateUnitsInfos = 0;


                    }

                    if (GameDll < 0)
                        GameDll = 0;

                    GameDllAddrShow.Text = GameDll.ToString("x2");


                    if (GameDll == 0 || trynewgamedll <= 0)
                    {
                        trynewgamedll = 100;
                        GameDll = war3processmemory.DllImageAddress("Game.dll");

                        if (GameDll < 0)
                            GameDll = 0;

                        if (GameDll == 0)
                        {
                            foreach (ProcessModule mdl in war3process.Modules)
                            {
                                if (mdl.FileName.ToLower().IndexOf("game.dll".ToLower()) > -1)
                                {
                                    GameDll = war3processmemory.DllImageAddress(mdl.ModuleName);
                                    break;
                                }
                                if (GameDll <= 0 && mdl.FileName.ToLower().IndexOf("Warcraft III.exe".ToLower()) > -1)
                                {
                                    GameDll = war3processmemory.DllImageAddress(mdl.ModuleName);
                                    break;
                                }
                            }

                            if (GameDll < 0)
                                GameDll = 0;

                            if (GameDll == 0)
                            {
                                GameDll = war3processmemory.MyProcess.MainModule.BaseAddress.ToInt32();
                            }

                            if (GameDll < 0)
                                GameDll = 0;

                            if (GameDll == 0)
                            {
                                if (!lostprocessmemory)
                                {
                                    lostprocessmemory = true;

                                    war3processmemory = null;
                                }
                                timerwork = false;
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (trynewgamedll > 0)
                        {
                            trynewgamedll--;
                        }
                    }



                    int ingame1 = war3processmemory.ReadInt(_InGame1Offset + GameDll);
                    uint ingame2 = war3processmemory.ReadUInt(_InGame2Offset + GameDll);

                    if (ingame1 <= 0 && ingame2 <= 0)
                    {
                        GameTime.Text = "No game found.";
                        if (GameAlreadStarted)
                        {

                            if (MH_AutoClose.Checked)
                                button1_Click(1, new EventArgs());
                            GameAlreadStarted = false;
                        }

                        mmbgbackaddr = 0;

                        File.Delete(MainMenu.MinimapPath);
                        timerwork = false;
                        return;
                    }
                    else
                    {
                        try
                        {
                            string Xseconds = ((int)(ingame2 / 1000) % 60).ToString("00");
                            string Xminutes = ((int)((ingame2 / (1000 * 60)) % 60)).ToString("00");
                            string Xhours = ((int)((ingame2 / (1000 * 60 * 60)) % 24)).ToString("00");

                            if (x3Timer == 0 || x3Timer == 2)
                                GameTime.Text = Xhours + ":" + Xminutes + ":" + Xseconds;
                        }
                        catch
                        {

                        }
                        if (CameraDistance.Checked)
                        {
                            SetCameraHeight(float.Parse(CameraDistText.Text));
                        }


                        if (!GameAlreadStarted)
                        {
                            GameAlreadStarted = true;

                            try
                            {
                                File.Delete(MainMenu.MinimapPath);



                                FirstGameFound = true;
                                happyoffset = 256;

                                Thread.Sleep(2000);


                                if (NeedInjectManabar)
                                {
                                    NeedInjectManabar = false;
                                    try
                                    {
                                        Syringe.Injector war3inject = new Syringe.Injector(Process.GetProcessesByName("war3")[0]);

                                        string path = Directory.GetCurrentDirectory() + "\\" + MainMenu.ManaBardll;
                                        war3inject.InjectLibraryW(path);
                                        Thread.Sleep(2000);
                                        try
                                        {
                                            war3inject.CallExport<int>(MainMenu.ManaBardll, MainMenu.ManaberLogin, War3Version);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    catch
                                    {

                                    }
                                }

                            }
                            catch
                            {

                            }
                            timerwork = false;
                            return;
                        }
                        else
                        {

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


                    for (int i = 0; i < 12; i++)
                    {
                        GetHeroImageByPlayerSlot(i, true);
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

                    if (TransferGoldEnabled.Checked)
                    {
                        int gdlad = _GoldTransferOffset + GameDll;
                        uint oldprot = war3processmemory.ProtectOffset(gdlad, 4, ProcessMemory.PAGE_EXECUTE_READWRITE);
                        war3processmemory.WriteInt(gdlad, gdlad);
                        war3processmemory.ProtectOffset(gdlad, 4, oldprot);
                    }



                    #region MAP_BOUNDS
                    // 1.27a BE6350
                    int GlobalClassOffset1 = war3processmemory.ReadInt(GameDll + _GlobalClassOffset);
                    int MapOffset = war3processmemory.ReadInt(GlobalClassOffset1 + MapDataOffset);

                    float mapleft = war3processmemory.ReadFloat(MapOffset + 1232) + maphackprofile.Map_LEFT_offset + // DOTA: -640
                    int.Parse(MapFixOffsetLeft.Text);
                    float maptop = war3processmemory.ReadFloat(MapOffset + 1236) + maphackprofile.Map_TOP_offset + // DOTA: 350
                    int.Parse(MapFixOffsetTop.Text);
                    float mapright = war3processmemory.ReadFloat(MapOffset + 1240) + maphackprofile.Map_RIGHT_offset + // DOTA: 800
                    int.Parse(MapFixOffsetRight.Text);
                    float mapbot = war3processmemory.ReadFloat(MapOffset + 1228) + maphackprofile.Map_BOT_offset + // DOTA: -250
                         int.Parse(MapFixOffsetBot.Text);

                    float fromlefttoright = Math.Abs(mapleft) + Math.Abs(mapright);
                    float fromtoptodown = Math.Abs(maptop) + Math.Abs(mapbot);

                    float minimaponex = fromlefttoright / 256.0f;
                    float minimaponey = fromtoptodown / 256.0f;

                    #endregion


                    // 1.27a BE6DC4
                    int minimapinfo = war3processmemory.ReadInt(GameDll + _MinimapInfoOffset);

                    if (NeedHideMMapChanged)
                    {
                        if (NeedHideMMap.Checked)
                        {
                            int minimapdrawed = war3processmemory.ReadInt(minimapinfo + 0x634);
                            if (minimapdrawed != 0)
                            {
                                war3processmemory.WriteInt(minimapinfo + 0x634, 0);
                            }
                        }
                        else
                        {
                            int minimapdrawed = war3processmemory.ReadInt(minimapinfo + 0x634);
                            if (minimapdrawed != 1)
                            {
                                war3processmemory.WriteInt(minimapinfo + 0x634, 1);
                            }
                        }
                    }

                    int minimapbackground = war3processmemory.ReadInt(minimapinfo + 0x17c) + 0x20;
                    int minimapterrainlevel = war3processmemory.ReadInt(minimapinfo + 0x63c);
                    int minimapterrainleveladdr = minimapinfo + 0x63c;

                    if (ReadGameColorSettings.Checked)
                        MinimapColored = war3processmemory.ReadInt(minimapinfo + 0x638) == 1;
                    else
                        MinimapColored = ColoredHeroes.Checked;

                    if (maphackprofile.orgminimap.Count != 0)
                    {
                        if (!File.Exists(MainMenu.MinimapPath))
                        {
                            maphackprofile.orgminimap[minimapid].Save(MainMenu.MinimapPath, ImageFormat.Bmp);
                        }
                    }


                    if (OriginBmp == null && !File.Exists(MainMenu.MinimapPath))
                    {

                        try
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

                            ReadMapMinimap.Save(MainMenu.MinimapPath, ImageFormat.Bmp);

                            BmpForDraw = null;
                            OriginBmp = BmpForDraw;
                        }
                        catch
                        {

                            MessageBox.Show("Error read/write bmp.");
                        }

                        OriginBmp = new Bitmap(256, 256);
                    }
                    else if (BmpForDraw == null)
                    {
                        try
                        {
                            using (MemoryStream mms = new MemoryStream(File.ReadAllBytes(MainMenu.MinimapPath)))
                            {
                                BmpForDraw = (Bitmap)new Bitmap(Image.FromStream(mms));
                                OriginBmp = BmpForDraw;
                            }
                        }
                        catch
                        {

                        }
                    }



                    if (BmpForDraw != null && OriginBmp != null)
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


                        Bitmap NewMinimapImage = new Bitmap(OriginBmp);





                        using (Graphics g = Graphics.FromImage(NewMinimapImage))
                        {
                            Bitmap ImageLayer1 = new Bitmap(NewMinimapImage.Width, NewMinimapImage.Height, PixelFormat.Format32bppArgb);
                            Bitmap ImageLayer2 = new Bitmap(NewMinimapImage.Width, NewMinimapImage.Height, PixelFormat.Format32bppArgb);
                            Bitmap ImageLayer3 = new Bitmap(NewMinimapImage.Width, NewMinimapImage.Height, PixelFormat.Format32bppArgb);

                            Graphics gImageLayer1 = Graphics.FromImage(ImageLayer1);
                            Graphics gImageLayer2 = Graphics.FromImage(ImageLayer2);
                            Graphics gImageLayer3 = Graphics.FromImage(ImageLayer3);



                            switch (maphackprofile.DrawAccurate)
                            {
                                case MainMenu.DrawQuality.Accurate:
                                    g.CompositingQuality = CompositingQuality.HighQuality;
                                    g.SmoothingMode = SmoothingMode.HighQuality;
                                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    gImageLayer1.CompositingQuality = CompositingQuality.HighQuality;
                                    gImageLayer1.SmoothingMode = SmoothingMode.HighQuality;
                                    gImageLayer1.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                                    gImageLayer1.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    gImageLayer2.CompositingQuality = CompositingQuality.HighQuality;
                                    gImageLayer2.SmoothingMode = SmoothingMode.HighQuality;
                                    gImageLayer2.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                                    gImageLayer2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    gImageLayer3.CompositingQuality = CompositingQuality.HighQuality;
                                    gImageLayer3.SmoothingMode = SmoothingMode.HighQuality;
                                    gImageLayer3.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                                    gImageLayer3.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    break;
                                case MainMenu.DrawQuality.Fast:
                                    g.CompositingQuality = CompositingQuality.HighSpeed;
                                    g.SmoothingMode = SmoothingMode.HighSpeed;
                                    g.TextRenderingHint = TextRenderingHint.SystemDefault;
                                    g.InterpolationMode = InterpolationMode.Low;
                                    gImageLayer1.CompositingQuality = CompositingQuality.HighSpeed;
                                    gImageLayer1.SmoothingMode = SmoothingMode.HighSpeed;
                                    gImageLayer1.TextRenderingHint = TextRenderingHint.SystemDefault;
                                    gImageLayer1.InterpolationMode = InterpolationMode.Low;
                                    gImageLayer2.CompositingQuality = CompositingQuality.HighSpeed;
                                    gImageLayer2.SmoothingMode = SmoothingMode.HighSpeed;
                                    gImageLayer2.TextRenderingHint = TextRenderingHint.SystemDefault;
                                    gImageLayer2.InterpolationMode = InterpolationMode.Low;
                                    gImageLayer3.CompositingQuality = CompositingQuality.HighSpeed;
                                    gImageLayer3.SmoothingMode = SmoothingMode.HighSpeed;
                                    gImageLayer3.TextRenderingHint = TextRenderingHint.SystemDefault;
                                    gImageLayer3.InterpolationMode = InterpolationMode.Low;
                                    break;
                                default:
                                    break;
                            }

                            if (NeedHideMMap.Checked)
                            {
                                g.FillRectangle(new SolidBrush(Color.Black), 0, 0, (float)NewMinimapImage.Width, (float)NewMinimapImage.Height);
                            }
                            // if (x3Timer == 3)
                            GameStatistic.Text = (UnitsCount) + "-" + (ItemsCount);

                            int badunits = 0;
                            int normalunits = 0;
                            #region ScanUnits
                            for (int i = 0; i < UnitsCount; i++)
                            {
                                int CurrentUnitId = i;
                                int CurrentUnitAdress = UnitsArrayAddr + CurrentUnitId * 4;
                                CurrentUnitAdress = war3processmemory.ReadInt(CurrentUnitAdress);


                                if (CurrentUnitAdress <= 100)
                                {
                                    badunits++;
                                    continue;
                                }

                                uint CurrentUnitFlag = war3processmemory.ReadUInt(CurrentUnitAdress + 0x5C);

                                uint CurrentUnitFlag2 = war3processmemory.ReadUInt(CurrentUnitAdress + 0x20);

                                //   uint CurrentUnitFlag3 = war3processmemory.ReadUInt(CurrentUnitAdress + 0x48);


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
                                    if (!DrawForceBadUnits.Checked)
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

                                if ((CurrentUnitFlag2 & 0x4u) == 0)
                                {
                                    if (!DrawForceBadUnits.Checked)
                                        continue;
                                }
                                else
                                {

                                }

                                if ((CurrentUnitFlag2 & 0x8) == 0)
                                {

                                }
                                else
                                {
                                    ///MessageBox.Show("Error55");
                                   // continue;
                                }



                                if ((CurrentUnitFlag & 0x100u) == 0)
                                {

                                }
                                else
                                {
                                    continue;
                                }





                                if ((CurrentUnitFlag & 0x10u) == 0)
                                {

                                }
                                else
                                {

                                    //   MessageBox.Show("Error2221");
                                    if (!DrawForceBadUnits.Checked)
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
                                    if (!DrawForceBadUnits.Checked)
                                        continue;
                                }


                                bool IsCurrentUnitTower = false;

                                if ((CurrentUnitFlag & 0x10000) == 0)
                                {

                                }
                                else
                                {
                                    IsCurrentUnitTower = true;
                                }

                                bool IsUnitInvisible = false;


                                if ((CurrentUnitFlag & 0x1000000) == 0)
                                {

                                }
                                else
                                {
                                    IsUnitInvisible = true;
                                }


                                uint IsCurrentUnitHero = war3processmemory.ReadUInt(CurrentUnitAdress + 48);
                                IsCurrentUnitHero = IsCurrentUnitHero >> 24;
                                IsCurrentUnitHero = IsCurrentUnitHero - 64;
                                if (IsCurrentUnitHero < 0x19)
                                    IsCurrentUnitHero = 1;
                                else
                                    IsCurrentUnitHero = 0;

                                bool IsUnitIllusion = false;

                                if ((CurrentUnitFlag & 0x40000000u) == 0)
                                {

                                }
                                else
                                {
                                    IsUnitIllusion = true;
                                }

                                if (IsUnitIllusion)
                                {
                                    if (DrawIllusions.Checked)
                                    {
                                        int UnitColorInfoAddress = war3processmemory.ReadInt(CurrentUnitAdress + 0x28);
                                        if (UnitColorInfoAddress > 0)
                                        {
                                            UnitColorInfoAddress = UnitColorInfoAddress + 300;

                                            byte red = (byte)(0);
                                            byte green = (byte)(0);
                                            byte blue = (byte)(200 + rnd.Next(0, 54));
                                            uint color = ColorToUInt(Color.FromArgb(red, green, blue));
                                            byte changecolorbyte = war3processmemory.ReadByte(UnitColorInfoAddress + 13);
                                            bool IsColorChanging = Get(changecolorbyte, 3);

                                            if (war3processmemory.ReadUInt(UnitColorInfoAddress + 28) != color && !IsColorChanging)
                                            {
                                                war3processmemory.WriteFloat(UnitColorInfoAddress + 24, 0.0f);
                                                war3processmemory.WriteInt(UnitColorInfoAddress + 16, 0);
                                                war3processmemory.WriteInt(UnitColorInfoAddress + 20, 0);
                                                war3processmemory.WriteUInt(UnitColorInfoAddress + 28, color);

                                                Set(ref changecolorbyte, 3, true);

                                                war3processmemory.WriteByte(UnitColorInfoAddress + 13, changecolorbyte);
                                            }
                                        }
                                    }
                                    else continue;
                                }

                                normalunits++;

                                int CurrentUnitPlayerSlot = war3processmemory.ReadInt(CurrentUnitAdress + 88);


                                float _UnitX = war3processmemory.ReadFloat(0x284 + CurrentUnitAdress);
                                float _UnitY = war3processmemory.ReadFloat(0x288 + CurrentUnitAdress);

                                if (_UnitX == _UnitY || -_UnitX == _UnitY || _UnitX == -_UnitY)
                                {
                                    if (!DrawForceBadUnits.Checked)
                                        continue;
                                }

                                float CurrentUnitX = _UnitX + Math.Abs(mapleft);
                                float CurrentUnitY = _UnitY + Math.Abs(mapbot);



                                float CurrentUnitMiniMapX = CurrentUnitX / minimaponex;
                                float CurrentUnitMiniMapY = 256 - (CurrentUnitY / minimaponey);

                                uint CurrentUnitClass = war3processmemory.ReadUInt(CurrentUnitAdress + 0x30);
                                string CurrentUnitClassString = Reverse(war3processmemory.ReadStringWarcraft(CurrentUnitAdress + 0x30, 4));







                                /*  if (IsCurrentUnitHero == 1)
                                  {
                                      war3processmemory.WriteInt(CurrentUnitAdress + 808, 0);
                                  }*/

                                /*if (IsCurrentUnitHero == 1)
                                    continue;*/

                                int CurrentDrawType = -1;

                                CurrentDrawType = DrawType(LocalPlayerSlot, CurrentUnitPlayerSlot, CurrentUnitClassString);


                                byte maindataon = war3processmemory.ReadByte(CurrentUnitAdress + 32);

                                bool mainhackforcurrentunit = Get(maindataon, 4);

                                if (IsUnitInvisible)
                                    mainhackforcurrentunit = true;

                                byte maindataoff = maindataon;



                                Set(ref maindataon, 4, true);
                                Set(ref maindataoff, 4, false);

                                if (DrawAllUnits.Checked)
                                {
                                    war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataon);
                                }

                                switch (CurrentDrawType)
                                {
                                    case 1:
                                        if (DrawAllUnits.Checked || NeedHideMMap.Checked)
                                        {


                                            /*
                                            if (DrawUnitsOnMainMap.Checked)
                                            {
                                                if (!mainhackforcurrentunit)
                                                {
                                                    war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataon);
                                                }
                                            }
                                            else
                                            {
                                                if (mainhackforcurrentunit)
                                                {
                                                    war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                                }
                                            }
                                            */



                                            if (DrawMinimapIngame.Checked)
                                            {

                                                if (!MinimapColored)
                                                {
                                                    if (IsCurrentUnitHero == 0)
                                                    {
                                                        if (IsCurrentUnitTower)
                                                            gImageLayer1.FillRectangle(new SolidBrush(Color.Aqua), CurrentUnitMiniMapX - 3, CurrentUnitMiniMapY - 3, 6, 6);
                                                        else
                                                            gImageLayer1.FillRectangle(new SolidBrush(Color.Aqua), CurrentUnitMiniMapX - 2, CurrentUnitMiniMapY - 2, 4, 4);
                                                    }
                                                    else
                                                    {
                                                        Image PlayerHeroBitmap = maphackprofile.heroaqua[maphackprofile.heroaquaframe];
                                                        gImageLayer2.DrawImage(PlayerHeroBitmap, maphackprofile.HERO_X_OFFSET + CurrentUnitMiniMapX - (PlayerHeroBitmap.Width / 2), maphackprofile.HERO_Y_OFFSET + CurrentUnitMiniMapY - (PlayerHeroBitmap.Height / 2), PlayerHeroBitmap.Width, PlayerHeroBitmap.Height);
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
                                                                gImageLayer1.FillRectangle(new SolidBrush(Color.Red), CurrentUnitMiniMapX - 3, CurrentUnitMiniMapY - 3, 6, 6);
                                                            else
                                                                gImageLayer1.FillRectangle(new SolidBrush(Color.Red), CurrentUnitMiniMapX - 2, CurrentUnitMiniMapY - 2, 4, 4);
                                                        }
                                                        else
                                                        {
                                                            if (IsCurrentUnitTower)
                                                                gImageLayer1.FillRectangle(new SolidBrush(Color.FromArgb(0xFF, 0, 0x90, 0)), CurrentUnitMiniMapX - 3, CurrentUnitMiniMapY - 3, 6, 6);
                                                            else
                                                                gImageLayer1.FillRectangle(new SolidBrush(Color.FromArgb(0xFF, 0, 0x90, 0)), CurrentUnitMiniMapX - 2, CurrentUnitMiniMapY - 2, 4, 4);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Image PlayerHeroBitmap = GetHeroImageByPlayerSlot(CurrentUnitPlayerSlot);
                                                        gImageLayer2.DrawImage(PlayerHeroBitmap, maphackprofile.HERO_X_OFFSET + CurrentUnitMiniMapX - (PlayerHeroBitmap.Width / 2), maphackprofile.HERO_Y_OFFSET + CurrentUnitMiniMapY - (PlayerHeroBitmap.Height / 2), PlayerHeroBitmap.Width, PlayerHeroBitmap.Height);
                                                    }

                                                }
                                            }
                                        }
                                        break;
                                    case 2:
                                        if (IsCurrentUnitHero != 0)
                                        {

                                            if (BypassAntihack.Checked)
                                            {
                                                if (IsCurrentUnitHero == 1)
                                                {
                                                    int UnitColorInfoAddress = war3processmemory.ReadInt(CurrentUnitAdress + 0x28);
                                                    if (UnitColorInfoAddress > 0)
                                                    {
                                                        UnitColorInfoAddress = UnitColorInfoAddress + 300;

                                                        Color UnitColor = UIntToColorA(war3processmemory.ReadUInt(UnitColorInfoAddress + 28));
                                                        byte changecolorbyte = war3processmemory.ReadByte(UnitColorInfoAddress + 13);

                                                        UnitColor = Color.FromArgb(255, UnitColor.R, UnitColor.G, UnitColor.B);
                                                        uint color = ColorToUInt(UnitColor);
                                                        war3processmemory.WriteFloat(UnitColorInfoAddress + 24, -1.0f);
                                                        war3processmemory.WriteInt(UnitColorInfoAddress + 16, 0);
                                                        war3processmemory.WriteInt(UnitColorInfoAddress + 20, 0);

                                                        war3processmemory.WriteUInt(UnitColorInfoAddress + 28, color);

                                                        war3processmemory.WriteInt(CurrentUnitAdress + 724, -1);


                                                        if (rnd.Next(0, 3) > 1)
                                                            changecolorbyte = 0x88;
                                                        else
                                                            changecolorbyte = 0x8;

                                                        war3processmemory.WriteByte(UnitColorInfoAddress + 13, changecolorbyte);

                                                    }
                                                }
                                            }



                                            if (!ClearMinimap)
                                            {
                                                if (DrawUnitsOnMainMap.Checked && DrawHeroes.Checked)
                                                {
                                                    if (!mainhackforcurrentunit)
                                                    {
                                                        war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataon);
                                                    }
                                                }
                                                else
                                                {
                                                    if (mainhackforcurrentunit)
                                                    {
                                                        war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                if (mainhackforcurrentunit)
                                                {
                                                    war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                                }
                                            }



                                        }
                                        else
                                        {


                                            if (!ClearMinimap)
                                            {
                                                if (DrawUnitsOnMainMap.Checked && DrawOther.Checked)
                                                {
                                                    if (!mainhackforcurrentunit)
                                                    {
                                                        war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataon);
                                                    }
                                                }
                                                else
                                                {
                                                    if (mainhackforcurrentunit)
                                                    {
                                                        war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (mainhackforcurrentunit)
                                                {
                                                    war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                                }
                                            }

                                        }


                                        if (DrawMinimapIngame.Checked)
                                        {

                                            if (!MinimapColored)
                                            {
                                                if (IsCurrentUnitHero == 0)
                                                {
                                                    if (IsCurrentUnitTower)
                                                        gImageLayer1.FillRectangle(new SolidBrush(Color.Red), CurrentUnitMiniMapX - 3, CurrentUnitMiniMapY - 3, 6, 6);
                                                    else
                                                        gImageLayer1.FillRectangle(new SolidBrush(Color.Red), CurrentUnitMiniMapX - 2, CurrentUnitMiniMapY - 2, 4, 4);
                                                }
                                                else
                                                {



                                                    Image PlayerHeroBitmap = maphackprofile.herored[maphackprofile.heroredframe];


                                                    gImageLayer2.DrawImage(PlayerHeroBitmap, maphackprofile.HERO_X_OFFSET + CurrentUnitMiniMapX - (PlayerHeroBitmap.Width / 2), maphackprofile.HERO_Y_OFFSET + CurrentUnitMiniMapY - (PlayerHeroBitmap.Height / 2), PlayerHeroBitmap.Width, PlayerHeroBitmap.Height);


                                                }
                                            }
                                            else
                                            {
                                                Color herocolor = GetColorByPlayerSlot(CurrentUnitPlayerSlot);
                                                bool IsCurrentUnitSentinel = IsScourge(CurrentUnitPlayerSlot);
                                                if (IsCurrentUnitHero == 0)
                                                {
                                                    if (!(ColoredHeroes.Checked && ColoredHeroes.Visible))
                                                    {
                                                        if (IsCurrentUnitSentinel)
                                                        {
                                                            if (IsCurrentUnitTower)
                                                                gImageLayer1.FillRectangle(new SolidBrush(Color.Red), CurrentUnitMiniMapX - 3, CurrentUnitMiniMapY - 3, 6, 6);
                                                            else
                                                                gImageLayer1.FillRectangle(new SolidBrush(Color.Red), CurrentUnitMiniMapX - 2, CurrentUnitMiniMapY - 2, 4, 4);
                                                        }
                                                        else
                                                        {
                                                            if (IsCurrentUnitTower)
                                                                gImageLayer1.FillRectangle(new SolidBrush(Color.FromArgb(0xFF, 0, 0x90, 0)), CurrentUnitMiniMapX - 3, CurrentUnitMiniMapY - 3, 6, 6);
                                                            else
                                                                gImageLayer1.FillRectangle(new SolidBrush(Color.FromArgb(0xFF, 0, 0x90, 0)), CurrentUnitMiniMapX - 2, CurrentUnitMiniMapY - 2, 4, 4);
                                                        }
                                                    }
                                                }
                                                else
                                                {

                                                    Image PlayerHeroBitmap = GetHeroImageByPlayerSlot(CurrentUnitPlayerSlot);

                                                    gImageLayer2.DrawImage(PlayerHeroBitmap, maphackprofile.HERO_X_OFFSET + CurrentUnitMiniMapX - (PlayerHeroBitmap.Width / 2), maphackprofile.HERO_Y_OFFSET + CurrentUnitMiniMapY - (PlayerHeroBitmap.Height / 2), PlayerHeroBitmap.Width, PlayerHeroBitmap.Height);


                                                }

                                            }
                                        }
                                        break;
                                    case 3:
                                        if (DrawAllUnits.Checked)
                                        {
                                            if (!ClearMinimap)
                                            {
                                                if (DrawUnitsOnMainMap.Checked)
                                                {
                                                    if (!mainhackforcurrentunit)
                                                    {
                                                        war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataon);
                                                    }
                                                }
                                                else
                                                {
                                                    if (mainhackforcurrentunit)
                                                    {
                                                        war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (mainhackforcurrentunit)
                                                {
                                                    war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                                }
                                            }

                                            if (DrawMinimapIngame.Checked)
                                            {
                                                gImageLayer1.FillRectangle(new SolidBrush(Color.FromArgb(0xFF, 0, 0x90, 0)), CurrentUnitMiniMapX - 2, CurrentUnitMiniMapY - 2, 4, 4);
                                            }
                                        }
                                        break;
                                    case 4:
                                        if (!ClearMinimap)
                                        {
                                            if (DrawUnitsOnMainMap.Checked && DrawNeutrals.Checked)
                                            {

                                                if (!mainhackforcurrentunit)
                                                {
                                                    war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataon);
                                                }
                                            }
                                            else
                                            {
                                                if (mainhackforcurrentunit)
                                                {
                                                    war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (mainhackforcurrentunit)
                                            {
                                                war3processmemory.WriteByte(CurrentUnitAdress + 32, maindataoff);
                                            }
                                        }

                                        if (DrawMinimapIngame.Checked)
                                        {
                                            gImageLayer1.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 50)), CurrentUnitMiniMapX - 2, CurrentUnitMiniMapY - 2, 4, 4);

                                        }
                                        break;
                                    case 5:
                                        if (DrawMinimapIngame.Checked)
                                        {
                                            for (int objid = 0; objid < maphackprofile.units.Length; objid++)
                                            {
                                                if (maphackprofile.units[objid].name == CurrentUnitClassString)
                                                {
                                                    if (maphackprofile.units[objid].framecount > 0)
                                                    {
                                                        gImageLayer2.DrawImageUnscaled(maphackprofile.units[objid].frames[maphackprofile.units[objid].frameid]
                                                            , Convert.ToInt32(CurrentUnitMiniMapX - (maphackprofile.units[objid].frames[maphackprofile.units[objid].frameid].Width / 2.0))
                                                            , Convert.ToInt32(CurrentUnitMiniMapY - (maphackprofile.units[objid].frames[maphackprofile.units[objid].frameid].Height / 2.0)));
                                                    }
                                                    else
                                                    {
                                                        gImageLayer2.DrawImageUnscaled(maphackprofile.units[objid].image, Convert.ToInt32(CurrentUnitMiniMapX - (maphackprofile.units[objid].image.Width / 2.0)), Convert.ToInt32(CurrentUnitMiniMapY - (maphackprofile.units[objid].image.Height / 2.0)));
                                                    }
                                                }
                                            }

                                        }
                                        break;
                                }


                            }
                            #endregion

                            GameStatistic.Text += "-" + (badunits) + "-" + (normalunits);

                            #region DrawItems
                            for (int i = 0; i < ItemsCount; i++)
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
                                    float CurrentItemX = war3processmemory.ReadFloat(CurrentItemInfoAddr + 0x88) + Math.Abs(mapleft); ;
                                    float CurrentItemY = war3processmemory.ReadFloat(CurrentItemInfoAddr + 0x8C) + Math.Abs(mapbot);


                                    float CurrentItemMiniMapX = /*256 - (*/ CurrentItemX / minimaponex /*)*/;
                                    float CurrentItemMiniMapY = 256 - (CurrentItemY / minimaponey);
                                    if (DrawMinimapIngame.Checked)
                                    {
                                        for (int objid = 0; objid < maphackprofile.items.Length; objid++)
                                        {
                                            if (maphackprofile.items[objid].name == CurrentItemStringId)
                                            {
                                                if (maphackprofile.items[objid].framecount > 0)
                                                {

                                                    gImageLayer2.DrawImageUnscaled(maphackprofile.items[objid].frames[maphackprofile.items[objid].frameid]
                                                        , Convert.ToInt32(CurrentItemMiniMapX - (maphackprofile.items[objid].frames[maphackprofile.items[objid].frameid].Width / 2.0))
                                                        , Convert.ToInt32(CurrentItemMiniMapY - (maphackprofile.items[objid].frames[maphackprofile.items[objid].frameid].Height / 2.0)));
                                                }
                                                else
                                                {
                                                    gImageLayer2.DrawImageUnscaled(maphackprofile.items[objid].image, Convert.ToInt32(CurrentItemMiniMapX - (maphackprofile.items[objid].image.Width / 2.0)), Convert.ToInt32(CurrentItemMiniMapY - (maphackprofile.items[objid].image.Height / 2.0)));
                                                }
                                            }
                                        }
                                    }


                                }
                            }
                            #endregion

                            #region DrawHappy

                            if (FirstGameFound && happyoffset > -100)
                            {
                                happyoffset -= 5;
                                try
                                {
                                    if (happyframeid < happyframes.Count - 1)
                                    {
                                        happyframeid++;
                                    }
                                    if (happyframeid < happyframes.Count - 1)
                                    {

                                    }
                                    else
                                    {
                                        happyframeid = 0;
                                    }

                                    gImageLayer3.DrawImage(happyframes[happyframeid], 60, 100, 100, 100);
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

                            if (!ClearMinimap)
                            {
                                g.DrawImageUnscaled(ImageLayer1, new Point());
                                g.DrawImageUnscaled(ImageLayer2, new Point());
                                g.DrawImageUnscaled(ImageLayer3, new Point());
                            }

                        }
                        if (ClearMinimap)
                        {
                            int minimapdrawed = war3processmemory.ReadInt(minimapinfo + 0x634);
                            if (minimapdrawed == 0)
                            {
                                war3processmemory.WriteInt(minimapinfo + 0x634, 1);
                            }
                        }
                        BmpForDraw = NewMinimapImage;

                    }
                    else
                    {
                        bool verybad = false;


                        if (BmpForDraw == null && OriginBmp == null)
                            verybad = true;

                        if (BmpForDraw == null)
                        {
                            BmpForDraw = new Bitmap(256, 256);
                        }
                        if (OriginBmp == null)
                        {
                            OriginBmp = new Bitmap(256, 256);
                        }

                        if (verybad)
                        {
                            MessageBox.Show("Ошибка чтения/записи миникарты. Проверьте наличие файла.");
                        }
                    }

                    if (BmpForDraw != null && MiniMapEnabled.Checked)
                    {
                        byte[] writedata = MinimapToByte((Image)(new Bitmap(BmpForDraw)));
                        war3processmemory.WriteMem(minimapbackground, writedata);
                        mmbgbackaddr = minimapbackground;

                    }


                }
                else
                {

                    if (FirstWork)
                    {
                        timerwork = true;
                        FirstWork = false;

                        MessageBox.Show("Warcraft III Not Found.", "Warning!");

                    }
                    else
                    {
                        timerwork = true;

                        if (!lostmemory)
                        {
                            lostmemory = true;
                            MessageBox.Show("Lost memory. No access? ");
                        }

                        button1_Click(1, new EventArgs());
                        return;
                    }
                }
            }
            catch
            {
                if (happyoffset > -100)
                {
                    lostprocessmemory = true;
                    war3processmemory = null;
                    happyoffset = -100;
                }
                if (!lostprocessmemory)
                {
                    lostprocessmemory = true;

                    war3processmemory = null;
                }
            }

            timerwork = false;
        }
        private static uint ColorToUInt(Color color)
        {
            return (uint)((color.A << 24) | (color.R << 16) |
                          (color.G << 8) | (color.B << 0));
        }




        private static Color UIntToColor(uint color)
        {
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(r, g, b);
        }

        private Color UIntToColorA(uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(a, r, g, b);
        }
        private byte[] MinimapToByte(Image img)
        {
            if (img == null)
                return new List<byte>().ToArray();

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
        private void Set(ref byte aByte, int pos, bool value)
        {
            if (value)
            {
                //left-shift 1, then bitwise OR
                aByte = (byte)(aByte | (1 << pos));
            }
            else
            {
                //left-shift 1, then take complement, then bitwise AND
                aByte = (byte)(aByte & ~(1 << pos));
            }
        }

        private bool Get(byte aByte, int pos)
        {
            //left-shift 1, then bitwise AND, then check for non-zero
            return ((aByte & (1 << pos)) != 0);
        }

        private void DrawUnitsOnMainMap_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CameraDistance_CheckedChanged(object sender, EventArgs e)
        {
            CameraDistText.Visible = CameraDistance.Checked;
        }

        private void CameraDistText_TextChanged(object sender, EventArgs e)
        {


        }

        private void ReadGameColorSettings_CheckedChanged(object sender, EventArgs e)
        {
            ColoredHeroes.Visible = !ReadGameColorSettings.Checked;
        }

        private bool skipxx = false;

        private void DrawALLUnits_CheckedChanged(object sender, EventArgs e)
        {
            if (skipxx)
                return;
            if (DrawAllUnits.Checked)
            {
                skipxx = true;
                DrawAllUnits.Checked = false;

                if (MessageBox.Show("Вы уверены что хотите включить это? :)", "Включить?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    DrawAllUnits.Checked = true;
                }

                skipxx = false;
            }
        }

        private void DrawUnitHpMp_CheckedChanged(object sender, EventArgs e)
        {

        }





        private void MainHack_Load(object sender, EventArgs e)
        {

            int allbad = 0;


            if (allbad != 0)
            {
                return;
            }


            this.Size = new Size(this.Width + ProcessHelper.rnd.Next(1, 25), this.Height + ProcessHelper.rnd.Next(1, 25));
            GeneralFeatures.Left = GeneralFeatures.Left + rnd.Next(-2, 2);
            GeneralFeatures.Top = GeneralFeatures.Top + rnd.Next(-2, 2);
            GeneralFeatures.Width = GeneralFeatures.Width + rnd.Next(-2, 2);
            GeneralFeatures.Height = GeneralFeatures.Height + rnd.Next(-2, 2);

            GeneralFeatures.Left = GeneralFeatures.Left + rnd.Next(-2, 2);
            GeneralFeatures.Top = GeneralFeatures.Top + rnd.Next(-2, 2);
            GeneralFeatures.Width = GeneralFeatures.Width + rnd.Next(-2, 2);
            GeneralFeatures.Height = GeneralFeatures.Height + rnd.Next(-2, 2);

            SpeedDrawLabel.Left = SpeedDrawLabel.Left + rnd.Next(-2, 2);
            SpeedDrawLabel.Top = SpeedDrawLabel.Top + rnd.Next(-2, 2);
            SpeedDrawLabel.Width = SpeedDrawLabel.Width + rnd.Next(-2, 2);
            SpeedDrawLabel.Height = SpeedDrawLabel.Height + rnd.Next(-2, 2);


            WarnWarnWarn.Left = WarnWarnWarn.Left + rnd.Next(-2, 2);
            WarnWarnWarn.Top = WarnWarnWarn.Top + rnd.Next(-2, 2);
            WarnWarnWarn.Width = WarnWarnWarn.Width + rnd.Next(-2, 2);
            WarnWarnWarn.Height = WarnWarnWarn.Height + rnd.Next(-2, 2);

            panel1.Left = panel1.Left + rnd.Next(-2, 2);
            panel1.Top = panel1.Top + rnd.Next(-2, 2);
            panel1.Width = panel1.Width + rnd.Next(-2, 2);
            panel1.Height = panel1.Height + rnd.Next(-2, 2);

            panel2.Left = panel2.Left + rnd.Next(-2, 2);
            panel2.Top = panel2.Top + rnd.Next(-2, 2);
            panel2.Width = panel2.Width + rnd.Next(-2, 2);
            panel2.Height = panel2.Height + rnd.Next(-2, 2);


            DrawMinimapIngame.Left = DrawMinimapIngame.Left + rnd.Next(-2, 2);
            DrawMinimapIngame.Top = DrawMinimapIngame.Top + rnd.Next(-2, 2);
            DrawMinimapIngame.Width = DrawMinimapIngame.Width + rnd.Next(-2, 2);
            DrawMinimapIngame.Height = DrawMinimapIngame.Height + rnd.Next(-2, 2);

            ReadGameColorSettings.Left = ReadGameColorSettings.Left + rnd.Next(-2, 2);
            ReadGameColorSettings.Top = ReadGameColorSettings.Top + rnd.Next(-2, 2);
            ReadGameColorSettings.Width = ReadGameColorSettings.Width + rnd.Next(-2, 2);
            ReadGameColorSettings.Height = ReadGameColorSettings.Height + rnd.Next(-2, 2);

            ColoredHeroes.Left = ColoredHeroes.Left + rnd.Next(-2, 2);
            ColoredHeroes.Top = ColoredHeroes.Top + rnd.Next(-2, 2);
            ColoredHeroes.Width = ColoredHeroes.Width + rnd.Next(-2, 2);
            ColoredHeroes.Height = ColoredHeroes.Height + rnd.Next(-2, 2);

            CameraDistance.Left = CameraDistance.Left + rnd.Next(-2, 2);
            CameraDistance.Top = CameraDistance.Top + rnd.Next(-2, 2);
            CameraDistance.Width = CameraDistance.Width + rnd.Next(-2, 2);
            CameraDistance.Height = CameraDistance.Height + rnd.Next(-2, 2);

            CameraDistText.Left = CameraDistText.Left + rnd.Next(-2, 2);
            CameraDistText.Top = CameraDistText.Top + rnd.Next(-2, 2);
            CameraDistText.Width = CameraDistText.Width + rnd.Next(-2, 2);
            CameraDistText.Height = CameraDistText.Height + rnd.Next(-2, 2);

            DrawAllUnits.Left = DrawAllUnits.Left + rnd.Next(-2, 2);
            DrawAllUnits.Top = DrawAllUnits.Top + rnd.Next(-2, 2);
            DrawAllUnits.Width = DrawAllUnits.Width + rnd.Next(-2, 2);
            DrawAllUnits.Height = DrawAllUnits.Height + rnd.Next(-2, 2);

            DrawUnitsOnMainMap.Left = DrawUnitsOnMainMap.Left + rnd.Next(-2, 2);
            DrawUnitsOnMainMap.Top = DrawUnitsOnMainMap.Top + rnd.Next(-2, 2);
            DrawUnitsOnMainMap.Width = DrawUnitsOnMainMap.Width + rnd.Next(-2, 2);
            DrawUnitsOnMainMap.Height = DrawUnitsOnMainMap.Height + rnd.Next(-2, 2);

            DrawHeroes.Left = DrawHeroes.Left + rnd.Next(-2, 2);
            DrawHeroes.Top = DrawHeroes.Top + rnd.Next(-2, 2);
            DrawHeroes.Width = DrawHeroes.Width + rnd.Next(-2, 2);
            DrawHeroes.Height = DrawHeroes.Height + rnd.Next(-2, 2);

            DrawNeutrals.Left = DrawNeutrals.Left + rnd.Next(-2, 2);
            DrawNeutrals.Top = DrawNeutrals.Top + rnd.Next(-2, 2);
            DrawNeutrals.Width = DrawNeutrals.Width + rnd.Next(-2, 2);
            DrawNeutrals.Height = DrawNeutrals.Height + rnd.Next(-2, 2);

            DrawOther.Left = DrawOther.Left + rnd.Next(-2, 2);
            DrawOther.Top = DrawOther.Top + rnd.Next(-2, 2);
            DrawOther.Width = DrawOther.Width + rnd.Next(-2, 2);
            DrawOther.Height = DrawOther.Height + rnd.Next(-2, 2);

            TransferGoldEnabled.Left = TransferGoldEnabled.Left + rnd.Next(-2, 2);
            TransferGoldEnabled.Top = TransferGoldEnabled.Top + rnd.Next(-2, 2);
            TransferGoldEnabled.Width = TransferGoldEnabled.Width + rnd.Next(-2, 2);
            TransferGoldEnabled.Height = TransferGoldEnabled.Height + rnd.Next(-2, 2);

            ExitButton1.Left = ExitButton1.Left + rnd.Next(-2, 2);
            ExitButton1.Top = ExitButton1.Top + rnd.Next(-2, 2);
            ExitButton1.Width = ExitButton1.Width + rnd.Next(-2, 2);
            ExitButton1.Height = ExitButton1.Height + rnd.Next(-2, 2);

            NeedHideMMap.Left = NeedHideMMap.Left + rnd.Next(-2, 2);
            NeedHideMMap.Top = NeedHideMMap.Top + rnd.Next(-2, 2);
            NeedHideMMap.Width = NeedHideMMap.Width + rnd.Next(-2, 2);
            NeedHideMMap.Height = NeedHideMMap.Height + rnd.Next(-2, 2);




            this.BackColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            panel1.BackColor = Color.FromArgb(100 + rnd.Next(-25, 25), rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
            panel2.BackColor = Color.FromArgb(100 + rnd.Next(-25, 25), rnd.Next(0, 255), rnd.Next(0, 255));


            if (File.Exists(MainMenu.MinimapPath))
            {
                File.Delete(MainMenu.MinimapPath);
            }


            if (File.Exists(MainMenu.MinimapPath + ".rem"))
            {
                File.Delete(MainMenu.MinimapPath + ".rem");
            }



            if (maphackprofile.orgminimap.Count != 0)
            {

                if (!File.Exists(MainMenu.MinimapPath))
                {
                    maphackprofile.orgminimap[minimapid].Save(MainMenu.MinimapPath, ImageFormat.Bmp);
                }


            }
        }

        private void DrawSpeed_Scroll(object sender, EventArgs e)
        {
            try
            {
                MinimapUpdateTimer.Interval = 600 - DrawSpeed.Value;
            }
            catch
            {

            }
        }

        private void DrawHPMPMainMap_CheckedChanged(object sender, EventArgs e)
        {
            int allbad = 0;
            try
            {
                var address = Dns.GetHostAddresses("iccup.com")[0];
                if (address.ToString().IndexOf("127.0") == 0)
                {
                    allbad = 1;
                    Environment.Exit(0);
                }

                if (address.ToString().IndexOf("192.168") == 0)
                {
                    allbad = 1;
                    Environment.Exit(0);
                }


            }
            catch
            {

            }

            try
            {
                var address2 = Dns.GetHostAddresses("google.com")[0];
                if (address2.ToString().IndexOf("127.0") == 0)
                {
                    allbad = 2;
                    Environment.Exit(0);
                }

                if (address2.ToString().IndexOf("192.168") == 0)
                {
                    allbad = 2;
                    Environment.Exit(0);
                }
            }

            catch
            {

            }

            if (allbad != 0)
            {
                Environment.Exit(0);
            }




            try
            {
                if (DrawHPMPMainMap.Checked)
                {
                    NeedInjectManabar = true;
                    Syringe.Injector war3inject = new Syringe.Injector(war3process);
                    try
                    {
                        war3inject.EjectLibraryXXX(MainMenu.ManaBardll);
                    }
                    catch
                    {

                    }
                    string path = Directory.GetCurrentDirectory() + "\\" + MainMenu.ManaBardll;
                    war3inject.InjectLibraryW(path);
                    Thread.Sleep(1000);
                    try
                    {
                        war3inject.CallExport<int>(MainMenu.ManaBardll, MainMenu.ManaberLogin, War3Version);
                    }
                    catch
                    {

                    }

                    try
                    {
                        foreach (int i in maphackprofile.mphpwhitelist)
                        {
                            war3inject.CallExport<int>(MainMenu.ManaBardll, "Test", i);
                        }
                    }
                    catch
                    {

                    }
                    NeedInjectManabar = false;
                }
                else
                {
                    Syringe.Injector war3inject = new Syringe.Injector(war3process);
                    war3inject.EjectLibraryXXX(MainMenu.ManaBardll);
                    NeedInjectManabar = false;
                }
            }
            catch
            {

            }
        }

        bool skipminimapchange = false;
        bool skipherochange = false;

        private void DrawMinimapIngame_CheckedChanged(object sender, EventArgs e)
        {
            if (skipminimapchange)
                return;
            skipherochange = true;
            if (DrawMinimapIngame.Checked)
            {
                DrawOnlyHeroesAtMinimap.Checked = false;
                DrawOnlyHeroesAtMinimap.Visible = false;
            }
            else
            {
                DrawOnlyHeroesAtMinimap.Checked = true;
                DrawOnlyHeroesAtMinimap.Visible = true;
                DrawMinimapIngame.Visible = false;
            }
            skipherochange = false;
        }

        private void DrawOnlyHeroesAtMinimap_CheckedChanged(object sender, EventArgs e)
        {
            if (skipherochange)
                return;
            skipminimapchange = true;
            if (DrawOnlyHeroesAtMinimap.Checked)
            {
                DrawMinimapIngame.Checked = false;
                DrawMinimapIngame.Visible = false;
            }
            else
            {
                DrawMinimapIngame.Checked = true;
                DrawMinimapIngame.Visible = true;
                DrawOnlyHeroesAtMinimap.Visible = false;
            }
            skipminimapchange = false;
        }


        bool first = true;
        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            if (first)
            {
                first = false;
                try
                {
                    this.Refresh();
                }
                catch
                {

                }
                MinimapUpdateTimer.Enabled = false;
            }
            this.Close();
        }



        private void HideLabel(ref Label lab)
        {
            lab.Hide();
            lab.Text = "HIDE";
        }


        private void HideCheckBox(ref CheckBox cb)
        {
            cb.Hide();
            cb.Text = "HIDE";
        }


        private void HideButton(ref Button btn)
        {
            btn.Hide();
            btn.Text = "HIDE";
        }


        private void HIDEMENUBUTTON_Click(object sender, EventArgs e)
        {
            HideButton(ref HIDEMENUBUTTON);
            HideLabel(ref SelectedGameProfileName);
            HideLabel(ref WarnWarnWarn);
            ExitButton1.Text = "Exit";



            HideLabel(ref AdditionalFeatures);
            HideLabel(ref SpeedDrawLabel);
            HideLabel(ref GeneralFeatures);
            HideLabel(ref WarcraftVersion);

            HideCheckBox(ref DrawOnlyHeroesAtMinimap);
            HideCheckBox(ref DrawMinimapIngame);
            HideCheckBox(ref ReadGameColorSettings);
            HideCheckBox(ref ColoredHeroes);
            HideCheckBox(ref NeedHideMMap);
            HideCheckBox(ref CameraDistance);
            HideCheckBox(ref DrawAllUnits);
            HideCheckBox(ref DrawUnitsOnMainMap);
            HideCheckBox(ref DrawHeroes);
            HideCheckBox(ref DrawOther);
            HideCheckBox(ref NeedHideMMap);
            HideCheckBox(ref BypassAntihack);
            HideCheckBox(ref TransferGoldEnabled);

            HideCheckBox(ref DrawHPMPMainMap);
            HideCheckBox(ref DrawIllusions);


            panel1.Hide();
            panel2.Hide();
            CameraDistText.Hide();
            DrawSpeed.Hide();

            ExitButton1.Left = 10 + rnd.Next(0, 25);
            ExitButton1.Top = 10 + rnd.Next(0, 25);
            ExitButton1.Width = 70;

            this.Width = 125 + rnd.Next(0, 25);
            this.Height = 50 + rnd.Next(0, 25);



        }

        private void BypassAntihack_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ShowCoordinates_Click(object sender, EventArgs e)
        {
            this.Width += 350;
            ShowCoordinates.Visible = false;
        }

        private void NeedHideMMap_CheckedChanged(object sender, EventArgs e)
        {
            NeedHideMMapChanged = true;
        }

        private void DrawForceBadUnits_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GameStatistic_Click(object sender, EventArgs e)
        {

        }
    }
}
