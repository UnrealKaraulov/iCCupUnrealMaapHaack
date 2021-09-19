#pragma region Headers
#define _WIN32_WINNT 0x0501 
#define WINVER 0x0501 
#define NTDDI_VERSION 0x05010000
//#define BOTDEBUG
#define WIN32_LEAN_AND_MEAN
#define IsKeyPressed(CODE) (GetAsyncKeyState(CODE) & 0x8000) > 0

#include <Windows.h>
#include <stdlib.h>
#include <time.h>
#include <string>
#include <vector>
using namespace std;

union DWFP
{
	unsigned int dw;
	float fl;
};

BOOL IsNotMIX = FALSE;


DWORD __stdcall Login( int password )
{
	if ( *( int* ) password == 412345 )
	{
		IsNotMIX = TRUE;
	}
	return 0;
}



char hexmap[ ] = { '0', '1', '2', '3', '4', '5', '6', '7',
'8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };



std::string hexStr( unsigned char *data, int len )
{
	std::string s( len * 2, '0' );
	for ( int i = 0; i < len; ++i )
	{
		s[ 2 * i ] = hexmap[ ( data[ i ] & 0xF0 ) >> 4 ];
		s[ 2 * i + 1 ] = hexmap[ data[ i ] & 0x0F ];
	}
	return s;
}

UINT_PTR ttt = NULL;
int GameDll = 0;

struct UnitInfo
{
	int unitaddr;
	float unithp;
	float unitmaxhp;
	float unitmp;
	float unitmaxmp;
	float unitfacing;
	int unitlevel;
};


vector<UnitInfo> unitglobaldata;

int unitglobaldataaddr = 0;
int unitglobaldatavectorsize = 0;





BOOL IsGame( ) // my offset + public
{
	return ( *( int* ) ( ( UINT32 ) GameDll + 0xBE6530 ) > 0 || *( int* ) ( ( UINT32 ) GameDll + 0xBE6530 ) > 0 )/* && !IsLagScreen( )*/;
}

BOOL IsWindowActive( BOOL SkipNoCheck = FALSE )
{
	return *( BOOL* ) ( GameDll + 0xB673EC );
}


// Pure code: Get unit count and units array
UINT GetUnitCountAndUnitArray( int ** unitarray )
{
	int GlobalClassOffset = *( int* ) ( GameDll + 0xBE6350 );
	int UnitsOffset1 = *( int* ) ( GlobalClassOffset + 0x3BC );
	int UnitsCount = *( int* ) ( UnitsOffset1 + 0x604 );
	if ( UnitsCount > 0 && UnitsOffset1 > 0 )
	{
		*unitarray = ( int * ) *( int* ) ( UnitsOffset1 + 0x608 );
		return UnitsCount;
	}
	return 0;
}

BOOL IsUnitDead( int unitaddr )
{
	unsigned int isdolbany = *( unsigned int* ) ( unitaddr + 0x5C );
	BOOL UnitNotDead = ( ( isdolbany & 0x100u ) == 0 );
	return UnitNotDead == FALSE;
}



BOOL IsHero( int unitaddr )
{
	unsigned int ishero = *( unsigned int* ) ( unitaddr + 48 );
	ishero = ishero >> 24;
	ishero = ishero - 64;
	return ishero < 0x19;
}



BOOL IsNotBadUnit( int unitaddr )
{
	if ( unitaddr > 0 )
	{
		int xaddr = GameDll + 0xA4A704;
		int xaddraddr = ( int ) &xaddr;

		if ( *( BYTE* ) xaddraddr != *( BYTE* ) unitaddr )
			return FALSE;
		else if ( *( BYTE* ) ( xaddraddr + 1 ) != *( BYTE* ) ( unitaddr + 1 ) )
			return FALSE;
		else if ( *( BYTE* ) ( xaddraddr + 2 ) != *( BYTE* ) ( unitaddr + 2 ) )
			return FALSE;
		else if ( *( BYTE* ) ( xaddraddr + 3 ) != *( BYTE* ) ( unitaddr + 3 ) )
			return FALSE;

		unsigned int isdolbany = *( unsigned int* ) ( unitaddr + 0x5C );

		BOOL returnvalue = ( isdolbany != 0x1001u && ( isdolbany & 0x40000000u ) == 0 && !IsUnitDead( unitaddr ) );
		return returnvalue;
	}

	return FALSE;
}




int _GetFloatStat = 0;
float GetUnitFloatStat( int unitaddr, unsigned int statNum )
{
	float result = 0;
	__asm
	{
		PUSH statNum;
		LEA EAX, result
			PUSH EAX
			MOV ECX, unitaddr
			CALL _GetFloatStat
	}
	return result;
}

// CreateTextTag
typedef unsigned int( __cdecl *GAME_CreateTextTag )( void );
typedef void( __cdecl *GAME_SetTextTagPos )( unsigned int t, float *x, float *y, float *heightOffset );
// SetTextTagColor
typedef void( __cdecl *GAME_SetTextTagColor )( unsigned int t, int red, int green, int blue, int alpha );
// SetTextTagVelocity
typedef void( __cdecl *GAME_SetTextTagVelocity )( unsigned int t, float *xvel, float *yvel );
// SetTextTagVisibility
typedef void( __cdecl *GAME_SetTextTagVisibility )( unsigned int t, BOOL flag );
// SetTextTagPermanent
typedef void( __cdecl *GAME_SetTextTagPermanent )( unsigned int t, BOOL flag );
// SetTextTagLifespan
typedef void( __cdecl *GAME_SetTextTagLifespan )( unsigned int t, float *lifespan );
// SetTextTagFadepoint
typedef void( __cdecl *GAME_SetTextTagFadepoint )( unsigned int t, float *fadepoint );
// SetTextTagText
void SetTextTagText( int t, char *s, float *height );

GAME_CreateTextTag CreateTextTag;
GAME_SetTextTagPos SetTextTagPos;
GAME_SetTextTagColor SetTextTagColor;
GAME_SetTextTagVelocity SetTextTagVelocity;
GAME_SetTextTagVisibility SetTextTagVisibility;
GAME_SetTextTagPermanent SetTextTagPermanent;
GAME_SetTextTagLifespan SetTextTagLifespan;
GAME_SetTextTagFadepoint SetTextTagFadepoint;

float height = 0.022f,
heightOffset = 0.0f,
xvel = 0.0f,
yvel = 0.0f,
fadepoint = 0.1f,
lifespan = 0.42f;

void SetTextTagText( int t, char *s, float *height )
{
	DWORD dwAddress = GameDll + 0x26DDC0;

	__asm
	{
		MOV		ECX, GameDll
			MOV		ECX, DWORD PTR DS : [ ECX + 0xBE4238 ]
			LEA		ECX, [ ECX + 0x1C ]
			MOV		ECX, DWORD PTR DS : [ ECX ]
			MOV		EAX, height
			MOV		EAX, DWORD PTR DS : [ EAX ]
			PUSH	EAX
			PUSH	s
			PUSH	t
			CALL	dwAddress
	}
}




BOOL gamestarted = FALSE;
char buffer[ 450 ];
BOOL badbad = FALSE;

vector<int> unitaddress;

void ManabarWork( )
{
	if ( IsGame( ) && IsWindowActive( ) && IsNotMIX && ( IsKeyPressed( VK_MENU ) || IsKeyPressed( VK_LMENU ) ) )
	{


		int * unitsarray = 0;
		int UnitsCount = GetUnitCountAndUnitArray( &unitsarray );



		if ( UnitsCount > 0 && unitsarray )
		{


			if ( !gamestarted )
			{
				gamestarted = TRUE;
			}
			unitaddress.clear( );


			for ( int i = 0; i < UnitsCount; i++ )
			{
				if ( unitsarray[ i ] )
				{
					if ( IsNotBadUnit( unitsarray[ i ] ) )
					{
						if ( IsHero( unitsarray[ i ] ) )
						{

							unitaddress.push_back( unitsarray[ i ] );
						}
					}
				}
			}


			for ( UINT i = 0; i < unitaddress.size( ); i++ )
			{

				float unitmaxmp = GetUnitFloatStat( unitaddress[ i ], 3 );
				float unitmp = GetUnitFloatStat( unitaddress[ i ], 2 );
				if ( unitmaxmp < 100 )
					continue;
				if ( unitmp < 5 )
					continue;

				float unitmaxhp = GetUnitFloatStat( unitaddress[ i ], 1 );
				float unithp = GetUnitFloatStat( unitaddress[ i ], 0 );
				if ( unitmaxhp < 100 )
					continue;
				if ( unithp < 5 )
					continue;

				float unitx = *( float* ) ( 0x284 + unitaddress[ i ] );
				float unity = *( float* ) ( 0x288 + unitaddress[ i ] );

				UINT unitxdword = *( UINT* ) ( 0x284 + unitaddress[ i ] );
				UINT unitydword = *( UINT* ) ( 0x288 + unitaddress[ i ] );

				if ( unitx == 0.0f && unity == 0.0f )
					continue;
				if ( unitx > 10000.0f || unitx < -10000.0f )
					continue;
				if ( unity > 10000.0f || unity < -10000.0f )
					continue;
				if ( unitxdword == 0 || unitydword == 0 )
					continue;
				if ( unitxdword == 0xFFFFFFFF || unitydword == 0xFFFFFFFF )
					continue;

				int currenttexttag = CreateTextTag( );
				if ( currenttexttag < 1 )
					return;

				unsigned int manapercent = ( unsigned int ) ( ( unitmp / unitmaxmp ) * 100 );
				BYTE colorr = 155 + ( 100 - manapercent );
				BYTE colorg = 256 - ( 255 - ( int ) ( manapercent * 2.5f ) );
				BYTE colorb = 256 - ( 255 - ( int ) ( manapercent * 2.5f ) );
				BYTE tempcolor[ 3 ];
				tempcolor[ 0 ] = colorr;
				tempcolor[ 1 ] = colorg;
				tempcolor[ 2 ] = colorb;
				unsigned int hitpointpercent = ( unsigned int ) ( ( unithp / unitmaxhp ) * 100 );

				BYTE hpcolorr = 255 - ( int ) ( hitpointpercent * 2.5f );
				BYTE hpcolorg = ( int ) ( hitpointpercent * 2.5f );
				BYTE hpcolorb = 0;
				BYTE hptempcolor[ 3 ];
				hptempcolor[ 0 ] = hpcolorr;
				hptempcolor[ 1 ] = hpcolorg;
				hptempcolor[ 2 ] = hpcolorb;

				memset( buffer, 0, 450 );
				sprintf_s( buffer, 450, "|cFF00FF00HP: (|r|cFF%s %i|r|cFF00FF00/%i )|r|n|cFF9BFFFFMP: (|r|cFF%s %i|r|cFF9BFFFF/%i )|r", hexStr( hptempcolor, 3 ).c_str( ), ( int ) unithp, ( int ) unitmaxhp, hexStr( tempcolor, 3 ).c_str( ), ( int ) unitmp, ( int ) unitmaxmp );
				
				SetTextTagLifespan( currenttexttag, &lifespan );
				SetTextTagPos( currenttexttag, &unitx, &unity, &heightOffset );
				SetTextTagText( currenttexttag, buffer, &height );
				//SetTextTagColor( currenttexttag , 255 , 0 , 0 , 255 );
				//SetTextTagVelocity( currenttexttag , &xvel , &yvel );
				//SetTextTagFadepoint( currenttexttag , &fadepoint );
				SetTextTagPermanent( currenttexttag, FALSE );
				SetTextTagVisibility( currenttexttag, TRUE );

			}
		}

	}
	else
	{
		if ( gamestarted )
		{
			gamestarted = FALSE;
		}
	}

	unitaddress.clear( );


}

VOID CALLBACK ManaBarThread(
	HWND hwnd,        // handle to window for timer messages 
	UINT message,     // WM_TIMER message 
	UINT idTimer,     // timer identifier 
	DWORD dwTime )     // current system time 
{
	try
	{
		ManabarWork( );
	}
	catch ( ... )
	{

	}

}


HWND War3HWND = NULL;
UINT_PTR TimerID = NULL;
BOOL WINAPI DllMain( HINSTANCE hDLL, UINT reason, LPVOID reserved )
{
	//MessageBoxA( 0 , "test" , "test" , 0 );
	if ( reason == DLL_PROCESS_ATTACH )
	{
		GameDll = ( int ) GetModuleHandle( L"Game.dll" );
		_GetFloatStat = GameDll + 0x669B40;

		CreateTextTag = ( GAME_CreateTextTag ) ( GameDll + 0x1DEA90 );
		SetTextTagPos = ( GAME_SetTextTagPos ) ( GameDll + 0x1F6E30 );
		SetTextTagColor = ( GAME_SetTextTagColor ) ( GameDll + 0x1F6D30 );
		SetTextTagVelocity = ( GAME_SetTextTagVelocity ) ( GameDll + 0x1F6FC0 );
		SetTextTagVisibility = ( GAME_SetTextTagVisibility ) ( GameDll + 0x1F7020 );
		SetTextTagPermanent = ( GAME_SetTextTagPermanent ) ( GameDll + 0x1F6E00 );
		SetTextTagLifespan = ( GAME_SetTextTagLifespan ) ( GameDll + 0x1F6DC0 );
		SetTextTagFadepoint = ( GAME_SetTextTagFadepoint ) ( GameDll + 0x1F6D80 );
		srand( ( unsigned int ) time( NULL ) );

		War3HWND = FindWindow( L"Warcraft III", NULL );
		if ( War3HWND == NULL )
			War3HWND = FindWindow( NULL, L"Warcraft III" );
		TimerID = 1001 + ( rand( ) % 50 );

		ttt = SetTimer( War3HWND, TimerID, 400, ManaBarThread );
	}
	else if ( reason == DLL_PROCESS_DETACH )
	{
		KillTimer( War3HWND, TimerID );
	}
	return TRUE;
}