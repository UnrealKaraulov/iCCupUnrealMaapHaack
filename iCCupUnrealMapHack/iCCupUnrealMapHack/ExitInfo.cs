using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace iCCupUnrealMapHack
{
    public partial class ExitInfo : Form
    {
        public ExitInfo ( )
        {
            InitializeComponent( );
        }

        Random myrnd = new Random( );

        private void ExitInfo_Load ( object sender , EventArgs e )
        {
            this.Location = new Point( this.Location.X , this.Location.Y - 100 );
            Bitmap justbg = ( Bitmap ) this.BackgroundImage;
            DrawRandomPixAtBitmap( ref justbg );
            this.BackgroundImage = justbg;
            this.Size = new Size( this.Width + ProcessHelper.rnd.Next( 1 , 25 ) , this.Height + ProcessHelper.rnd.Next( 1 , 25 ) );
        }

        int exitint = 100;


        private void DrawRandomPixAtBitmap ( ref Bitmap bmp )
        {
            int x = bmp.Width - 1;
            int y = bmp.Height - 1;
            y = myrnd.Next( 0 , y );
            x = myrnd.Next( 0 , x );
            bmp.SetPixel( x , y , Color.FromArgb( myrnd.Next( 255 ) , myrnd.Next( 255 ) , myrnd.Next( 255 ) ) );
        }


        private void ExitTimer_Tick ( object sender , EventArgs e )
        {
            exitint--;
            if ( exitint < 70 )
            {
                this.Close( );
            }
            else if ( exitint < 72 )
            {
                this.Opacity = 0.30;
                this.Refresh( );
            }

            else if ( exitint < 75 )
            {
                this.Opacity = 0.40;
                this.Refresh( );
            }
            else if ( exitint < 80 )
            {
                this.Opacity = 0.50;
                this.Refresh( );
            }
            else if ( exitint < 84 )
            {
                this.Opacity = 0.60;
                this.Refresh( );
            }
            else if ( exitint < 88 )
            {
                this.Opacity = 0.70;
                this.Refresh( );
            }
            else if ( exitint < 92 )
            {
                this.Opacity = 0.80;
                this.Refresh( );
            }
            else if ( exitint < 95 )
            {
                this.Opacity = 0.90;
                this.Refresh( );
            }
            else if ( exitint < 99 )
            {
                this.Opacity = 1;
                this.Refresh( );
            }

        }
    }
}
