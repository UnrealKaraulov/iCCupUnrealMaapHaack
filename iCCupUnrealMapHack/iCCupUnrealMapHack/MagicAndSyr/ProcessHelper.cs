using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;


    public class ProcessHelper
    {

        public static Random rnd = new Random();

        public static string ID ( )
        {
            rnd = new Random( );
            return Value( );
        }

        public static string ValueCPU ( )
        {
            return GetHash( CpuId( ) );
        }


        public static string ValueBios ( )
        {
            return GetHash( BiosId( ) );
        }

        public static string ValueBase ( )
        {
            return GetHash( BaseId( ) );
        }


        public static string ValueDisk ( )
        {
            return GetHash( DiskId( ) );
        }



        private static string _fingerPrint = string.Empty;
        private static string Value ( )
        {
            //You don't need to generate the HWID again if it has already been generated. This is better for performance
            //Also, your HWID generally doesn't change when your computer is turned on but it can happen.
            //It's up to you if you want to keep generating a HWID or not if the function is called.
            if ( string.IsNullOrEmpty( _fingerPrint ) )
            {
                _fingerPrint = GetHash( "PU >> " + CpuId( ) + "\nBIOS >> " + BiosId( ) + "\nBASE >> " + BaseId( ) + "\nDISK >> " + DiskId( ) + "\nVIDEO >> " + DiskId( ) + "\nMAC >> " + GetHash( CpuId( ) ) );
            }
            return _fingerPrint;
        }
        public static string Reverse ( string s )
        {
            char [ ] charArray = s.ToCharArray( );
            Array.Reverse( charArray );
            return new string( charArray );
        }
        public static string GetHash ( string s )
        {
            //Initialize a new MD5 Crypto Service Provider in order to generate a hash
            MD5 sec = new MD5CryptoServiceProvider( );
            //Grab the bytes of the variable 's'
            byte [ ] bt = Encoding.ASCII.GetBytes( s );
            //Grab the Hexadecimal value of the MD5 hash
            return GetHexString( sec.ComputeHash( bt ) );
        }

        private static string GetHexString ( IList<byte> bt )
        {
            string s = string.Empty;
            for ( int i = 0 ; i < bt.Count ; i++ )
            {
                byte b = bt [ i ];
                int n = b;
                int n1 = n & 15;
                int n2 = ( n >> 4 ) & 15;
                if ( n2 > 9 )
                    s += ( ( char ) ( n2 - 10 + 'A' ) ).ToString( CultureInfo.InvariantCulture );
                else
                    s += n2.ToString( CultureInfo.InvariantCulture );
                if ( n1 > 9 )
                    s += ( ( char ) ( n1 - 10 + 'A' ) ).ToString( CultureInfo.InvariantCulture );
                else
                    s += n1.ToString( CultureInfo.InvariantCulture );
                if ( ( i + 1 ) != bt.Count && ( i + 1 ) % 2 == 0 ) s += "-";
            }
            return s;
        }

        //Return a hardware identifier
        private static string Identifier ( string wmiClass , string wmiProperty , string wmiMustBeTrue )
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass( wmiClass );
            System.Management.ManagementObjectCollection moc = mc.GetInstances( );
            foreach ( System.Management.ManagementBaseObject mo in moc )
            {
                if ( mo [ wmiMustBeTrue ].ToString( ) != "True" ) continue;
                //Only get the first one
                if ( result != "" ) continue;
                try
                {
                    result = mo [ wmiProperty ].ToString( );
                    break;
                }
                catch
                {
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string Identifier ( string wmiClass , string wmiProperty )
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass( wmiClass );
            System.Management.ManagementObjectCollection moc = mc.GetInstances( );
            foreach ( System.Management.ManagementBaseObject mo in moc )
            {
                //Only get the first one
                if ( result != "" ) continue;
                try
                {
                    result = mo [ wmiProperty ].ToString( );
                    break;
                }
                catch
                {
                }
            }
            return result;
        }
        private static string CpuId ( )
        {
            string retVal = Identifier( "Win32_Processor" , "UniqueId" );
            if ( retVal.Length > 0 )
                return retVal;
            retVal = Identifier( "Win32_Processor" , "ProcessorId" );
            if ( retVal.Length > 0 )
                return retVal;
            retVal = Identifier( "Win32_Processor" , "Name" );
            if ( retVal.Length > 0 )
                return retVal;
            retVal = Identifier( "Win32_Processor" , "Manufacturer" );
            if ( retVal.Length > 0 )
                return retVal;
            return "BAD";
        }
        //BIOS Identifier
        private static string BiosId ( )
        {
            return Identifier( "Win32_BIOS" , "Manufacturer" ) + Identifier( "Win32_BIOS" , "SerialNumber" );
        }
        //Main physical hard drive ID
        private static string DiskId ( )
        {
            return " ";
        }
        //Motherboard ID
        private static string BaseId ( )
        {
            return Identifier( "Win32_BaseBoard" , "Model" ) + Identifier( "Win32_BaseBoard" , "Manufacturer" ) + Identifier( "Win32_BaseBoard" , "Name" ) + Identifier( "Win32_BaseBoard" , "SerialNumber" );
        }
        //Primary video controller ID
        private static string VideoId ( )
        {
            return " ";
        }
        //First enabled network card ID
        private static string MacId ( )
        {
            return Identifier( "Win32_NetworkAdapterConfiguration" , "MACAddress" , "IPEnabled" );
        }
    }

