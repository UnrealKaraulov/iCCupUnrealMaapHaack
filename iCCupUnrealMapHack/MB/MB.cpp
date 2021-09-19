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

BOOL IsMIX = TRUE;



char hexmap[ ] = { '0', '1', '2', '3', '4', '5', '6', '7',
'8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };



typedef unsigned int( __cdecl * pGetPlayerColor )( int whichPlayer );
pGetPlayerColor GetPlayerColor;

typedef int( __cdecl * pPlayer )( int number );
pPlayer Player;



int         PLAYER_COLOR_RED = 0;
int         PLAYER_COLOR_BLUE = 1;
int         PLAYER_COLOR_CYAN = 2;
int         PLAYER_COLOR_PURPLE = 3;
int         PLAYER_COLOR_YELLOW = 4;
int         PLAYER_COLOR_ORANGE = 5;
int         PLAYER_COLOR_GREEN = 6;
int         PLAYER_COLOR_PINK = 7;
int         PLAYER_COLOR_LIGHT_GRAY = 8;
int         PLAYER_COLOR_LIGHT_BLUE = 9;
int         PLAYER_COLOR_AQUA = 10;
int         PLAYER_COLOR_BROWN = 11;


const char * GetPlayerColorString( int player )
{
	int c = GetPlayerColor( player );
	if ( c == PLAYER_COLOR_RED )
		return "|cffFF0202";
	else if ( c == PLAYER_COLOR_BLUE )
		return "|cff0031FF";
	else if ( c == PLAYER_COLOR_CYAN )
		return "|cff1BE5B8";
	else if ( c == PLAYER_COLOR_PURPLE )
		return "|cff530080";
	else if ( c == PLAYER_COLOR_YELLOW )
		return "|cffFFFC00";
	else if ( c == PLAYER_COLOR_ORANGE )
		return "|cffFE890D";
	else if ( c == PLAYER_COLOR_GREEN )
		return "|cff1FBF00";
	else if ( c == PLAYER_COLOR_PINK )
		return "|cffE45AAF";
	else if ( c == PLAYER_COLOR_LIGHT_GRAY )
		return "|cff949596";
	else if ( c == PLAYER_COLOR_LIGHT_BLUE )
		return "|cff7DBEF1";
	else if ( c == PLAYER_COLOR_AQUA )
		return "|cff0F6145";
	else if ( c == PLAYER_COLOR_BROWN )
		return "|cff4D2903";
	else
		return "|cffFFFFFF";

}



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
int IsGameOffset1 = 0xBE6530;
int IsGameOffset2 = 0xBE6530;
int IsWindowActiveOffset = 0xB673EC;
int WarcraftGlobalClassOffset = 0xBE6350;
int UnitClassOffset = 0xA4A704;
int SetTextTagTextOffset = 0x26DDC0;
int GlobalPlayerOffset = 0xBE4238;

BOOL IsGame( ) // my offset + public
{
	return *( int* ) ( GameDll + IsGameOffset1 ) > 0 || *( int* ) ( GameDll + IsGameOffset2 ) > 0 /* && !IsLagScreen( )*/;
}

BOOL IsWindowActive( BOOL SkipNoCheck = FALSE )
{
	return *( BOOL* ) ( GameDll + IsWindowActiveOffset );
}


// Get unit count and units array
UINT GetUnitCountAndUnitArray( int ** unitarray )
{
	int GlobalClassOffset = *( int* ) ( GameDll + WarcraftGlobalClassOffset );
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
		int xaddr = GameDll + UnitClassOffset;
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



#pragma optimize("",off)

int _GetFloatStat = 0;
float GetUnitFloatStat( int unitaddr, unsigned int statNum )
{
	float result = 0;
	__asm
	{
		PUSH statNum;
		LEA EAX, result;
		PUSH EAX;
		MOV ECX, unitaddr;
		CALL _GetFloatStat;
	}
	return result;
}
#pragma optimize("",on)

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

float height = 0.021f,
heightOffset = 0.0f,
xvel = 0.0f,
yvel = 0.0f,
fadepoint = 0.1f,
lifespan = 0.33f;


#pragma optimize("",off)

void SetTextTagText( int hTextTag, char* string, float* height )
{
	DWORD pSetTextTagText = GameDll + SetTextTagTextOffset;
	__asm
	{
		MOV		ECX, GameDll;
		MOV		EAX, GlobalPlayerOffset;
		MOV		ECX, DWORD PTR DS : [ ECX + EAX ];
		LEA		ECX, [ ECX + 0x1C ];
		MOV		ECX, DWORD PTR DS : [ ECX ];

		MOV		EAX, height;
		MOV		EAX, DWORD PTR DS : [ EAX ];
		PUSH	EAX;
		PUSH	string;
		PUSH	hTextTag;
		CALL	pSetTextTagText;
	}

	return;
}

#pragma optimize("",on)



int pGameClass1 = 0;

int GetUnitAddressFloatsRelated( int unitaddr, int step )
{
	int offset1 = unitaddr + step;
	int offset2 = *( int* ) pGameClass1;

	if ( *( int* ) offset1 &&  offset2 )
	{
		offset1 = *( int* ) offset1;
		offset2 = *( int* ) ( offset2 + 0xC );
		if ( offset2 )
		{
			return *( int* ) ( ( offset1 * 8 ) + offset2 + 4 );
		}
	}
	return 0;
}


float GetUnitHPregen( int unitaddr )
{
	int offset1 = GetUnitAddressFloatsRelated( unitaddr, 0xA0 );
	if ( offset1 )
	{
		return *( float* ) ( offset1 + 0x7C );
	}
	return 0.0f;
}

float GetUnitMPregen( int unitaddr )
{
	int offset1 = GetUnitAddressFloatsRelated( unitaddr, 0xC0 );
	if ( offset1 )
	{
		return *( float* ) ( offset1 + 0x7C );
	}
	return 0.0f;
}

UINT GetUnitOwnerSlot( int unitaddr )
{
	return *( UINT* ) ( unitaddr + 88 );
}


BOOL gamestarted = FALSE;
char buffer[ 512 ];
BOOL badbad = FALSE;

vector<int> unitaddress;

BOOL allwaysdraw = FALSE;
BOOL ALTPRESSED = FALSE;

typedef int( __fastcall * p_GetTypeInfo )( int unit_item_code, int unused );
p_GetTypeInfo GetTypeInfo = NULL;

typedef int( __fastcall * GetUnitLevel )( int unitaddr, int unused );
GetUnitLevel mGetUnitLevel;


char * DefaultString = "Unnamed";

int GetObjectClassID( int unit_or_item_addr )
{
	return *( int* ) ( unit_or_item_addr + 0x30 );
}


char * GetObjectNameByID( int clid )
{
	if ( clid > 0 )
	{
		int v3 = GetTypeInfo( clid, 0 );
		int v4, v5;
		if ( v3 && ( v4 = *( int * ) ( v3 + 40 ) ) != 0 )
		{
			v5 = v4 - 1;
			if ( v5 >= ( unsigned int ) 0 )
				v5 = 0;
			return ( char * ) *( int * ) ( *( int * ) ( v3 + 44 ) + 4 * v5 );
		}
		else
		{
			return DefaultString;
		}
	}
	return DefaultString;
}


std::vector<int> whitelist;

DWORD __stdcall Test( int all )
{
	if ( all == 0 )
		whitelist.clear( );
	else
		whitelist.push_back( all );
	return 0;
}


bool IsUnitWhiteList( int unitaddr )
{
	int unittypeid = *(int*)(unitaddr + 0x30);
	for ( int i : whitelist )
	{
		if ( i == unittypeid )
		{
			return true;
		}
	}

	return false;
}


void ManabarWork( )
{
	if ( IsMIX )
		return;

	if ( IsGame( ) && IsWindowActive( ) )
	{

		if ( IsKeyPressed( VK_MENU )  )
		{
			if ( IsKeyPressed( VK_CONTROL ) && !ALTPRESSED )
			{
				allwaysdraw = !allwaysdraw;
				ALTPRESSED = TRUE;
			}
			else
			{
				ALTPRESSED = FALSE;
			}
		}

		if ( allwaysdraw || IsKeyPressed( VK_MENU ) )
		{

			int * unitsarray = 0;
			int UnitsCount = GetUnitCountAndUnitArray( &unitsarray );



			if ( UnitsCount > 0 && unitsarray > 0 )
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
							if ( IsHero( unitsarray[ i ] ) || IsUnitWhiteList( unitsarray[i]) )
							{
								unitaddress.push_back( unitsarray[ i ] );
							}
						}
					}
				}


				for ( UINT i = 0; i < unitaddress.size( ); i++ )
				{
					int UnitOwnerSlot = GetUnitOwnerSlot( unitaddress[ i ] );

					if ( UnitOwnerSlot < 0 || UnitOwnerSlot > 15 )
						continue;

					int UnitLevel = mGetUnitLevel( unitaddress[ i ], 0 );
					if ( UnitLevel == 0 && !IsUnitWhiteList( unitaddress[ i ] ) )
						continue;

					const char * PlayerColorStr = GetPlayerColorString( Player( UnitOwnerSlot ) );

					float unitmaxmp = GetUnitFloatStat( unitaddress[ i ], 3 );
					float unitmp = GetUnitFloatStat( unitaddress[ i ], 2 );
					if ( unitmaxmp < 100 && !IsUnitWhiteList( unitaddress[ i ] ) )
						continue;
					if ( unitmp < 5 && !IsUnitWhiteList( unitaddress[ i ] ) )
						continue;

					float unitmaxhp = GetUnitFloatStat( unitaddress[ i ], 1 );
					float unithp = GetUnitFloatStat( unitaddress[ i ], 0 );
					if ( unitmaxhp < 100 && !IsUnitWhiteList( unitaddress[ i ] ) )
						continue;
					if ( unithp < 5 && !IsUnitWhiteList( unitaddress[ i ] ) )
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
					{
						return;
					}
					float unitregenhp = GetUnitHPregen( unitaddress[ i ] );

					float unitregenmp = GetUnitMPregen( unitaddress[ i ] );


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


					memset( buffer, 0, 512 );
					sprintf_s( buffer, 512, "%s(%i)%.30s ( %i )|r|n|cFF00FF00HP: (|r|cFF%s %i|r|cFF00FF00/%i|r |cFF00FF00+%.2f|r|cFF00FF00)|r|n|cFF9BFFFFMP: (|r|cFF%s %i|r|cFF9BFFFF/%i|r |cFF00FFFF+%.2f|r|cFF9BFFFF)|r", PlayerColorStr, UnitOwnerSlot + 1,GetObjectNameByID( GetObjectClassID ( unitaddress[ i ] )), UnitLevel, hexStr( hptempcolor, 3 ).c_str( ), ( int ) unithp, ( int ) unitmaxhp, unitregenhp, hexStr( tempcolor, 3 ).c_str( ), ( int ) unitmp, ( int ) unitmaxmp, unitregenmp );
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



bool IsWork = false;

VOID CALLBACK ManaBarThread(
	HWND hwnd,        // handle to window for timer messages 
	UINT message,     // WM_TIMER message 
	UINT idTimer,     // timer identifier 
	DWORD dwTime )     // current system time 
{
	if ( IsWork )
		return;

	IsWork = true;
	try
	{
		ManabarWork( );
	}
	catch ( ... )
	{

	}

	IsWork = false;
}

HWND war3hwnd = NULL;

void Init28a( )
{
	IsGameOffset1 = 0xD753E0;
	IsGameOffset2 = 0xD72C20;
	WarcraftGlobalClassOffset = 0xD75200;
	IsWindowActiveOffset = 0xCDFE74;
	UnitClassOffset = 0xB80FF0;


	SetTextTagTextOffset = 0x291A20;

	GlobalPlayerOffset = 0xD730F0;

	pGameClass1 = GameDll + 0xD72F58;

	_GetFloatStat = GameDll + 0x68EB30;

	CreateTextTag = ( GAME_CreateTextTag )( GameDll + 0x202840 );
	SetTextTagPos = ( GAME_SetTextTagPos )( GameDll + 0x21ACB0 );
	SetTextTagColor = ( GAME_SetTextTagColor )( GameDll + 0x21ABB0 );
	SetTextTagVelocity = ( GAME_SetTextTagVelocity )( GameDll + 0x21AE40 );
	SetTextTagVisibility = ( GAME_SetTextTagVisibility )( GameDll + 0x21AEA0 );
	SetTextTagPermanent = ( GAME_SetTextTagPermanent )( GameDll + 0x21AC80 );
	SetTextTagLifespan = ( GAME_SetTextTagLifespan )( GameDll + 0x21AC40 );
	SetTextTagFadepoint = ( GAME_SetTextTagFadepoint )( GameDll + 0x21AC00 );

	GetPlayerColor = ( pGetPlayerColor )( GameDll + 0x207A30 );
	Player = ( pPlayer )( GameDll + 0x215BE0 );
	GetTypeInfo = ( p_GetTypeInfo )( GameDll + 0x34BFD0 );

	mGetUnitLevel = ( GetUnitLevel )( GameDll + 0x68CA10 );
	war3hwnd = *( HWND* )( GameDll + 0xD47C78 );
}



void Init28b( )
{
	IsGameOffset1 = 0xD7A438;
	IsGameOffset2 = 0xD77C78;
	WarcraftGlobalClassOffset = 0xD7A258;
	IsWindowActiveOffset = 0xCE4E74;

	UnitClassOffset = 0xB845A4;


	SetTextTagTextOffset = 0x294050;

	GlobalPlayerOffset = 0xD78148;

	pGameClass1 = GameDll + 0xD77FB0;

	_GetFloatStat = GameDll + 0x6912A0;

	CreateTextTag = ( GAME_CreateTextTag )( GameDll + 0x204E20 );
	SetTextTagPos = ( GAME_SetTextTagPos )( GameDll + 0x21D290 );
	SetTextTagColor = ( GAME_SetTextTagColor )( GameDll + 0x21D190 );
	SetTextTagVelocity = ( GAME_SetTextTagVelocity )( GameDll + 0x21D420 );
	SetTextTagVisibility = ( GAME_SetTextTagVisibility )( GameDll + 0x21D480 );
	SetTextTagPermanent = ( GAME_SetTextTagPermanent )( GameDll + 0x21D260 );
	SetTextTagLifespan = ( GAME_SetTextTagLifespan )( GameDll + 0x21D220 );
	SetTextTagFadepoint = ( GAME_SetTextTagFadepoint )( GameDll + 0x21D1E0 );

	GetPlayerColor = ( pGetPlayerColor )( GameDll + 0x20A010 );
	Player = ( pPlayer )( GameDll + 0x2181C0 );
	GetTypeInfo = ( p_GetTypeInfo )( GameDll + 0x34E680 );

	mGetUnitLevel = ( GetUnitLevel )( GameDll + 0x68F180 );
	war3hwnd = *( HWND* )( GameDll + 0xD4CCC8 );
}


void Init28c( )
{
	IsGameOffset1 = 0xD7A438;
	IsGameOffset2 = 0xD77C78;
	WarcraftGlobalClassOffset = 0xD7A258;
	IsWindowActiveOffset = 0xCE4E74;

	UnitClassOffset = 0xB845A4;


	SetTextTagTextOffset = 0x294050;

	GlobalPlayerOffset = 0xD78148;

	pGameClass1 = GameDll + 0xD77FB0;

	_GetFloatStat = GameDll + 0x6913A0;

	CreateTextTag = ( GAME_CreateTextTag )( GameDll + 0x204E20 );
	SetTextTagPos = ( GAME_SetTextTagPos )( GameDll + 0x21D290 );
	SetTextTagColor = ( GAME_SetTextTagColor )( GameDll + 0x21D190 );
	SetTextTagVelocity = ( GAME_SetTextTagVelocity )( GameDll + 0x21D420 );
	SetTextTagVisibility = ( GAME_SetTextTagVisibility )( GameDll + 0x21D480 );
	SetTextTagPermanent = ( GAME_SetTextTagPermanent )( GameDll + 0x21D260 );
	SetTextTagLifespan = ( GAME_SetTextTagLifespan )( GameDll + 0x21D220 );
	SetTextTagFadepoint = ( GAME_SetTextTagFadepoint )( GameDll + 0x21D1E0 );

	GetPlayerColor = ( pGetPlayerColor )( GameDll + 0x20A010 );
	Player = ( pPlayer )( GameDll + 0x2181C0 );
	GetTypeInfo = ( p_GetTypeInfo )( GameDll + 0x34E680 );

	mGetUnitLevel = ( GetUnitLevel )( GameDll + 0x68F280 );
	war3hwnd = *( HWND* )( GameDll + 0xD4CCC8 );
}

void Init284( )
{
	IsGameOffset1 = 0xD314D0;
	IsGameOffset2 = 0xD2ED10;
	WarcraftGlobalClassOffset = 0xD312F0;
	IsWindowActiveOffset = 0xCA2E74;

	UnitClassOffset = 0xB678D4;


	SetTextTagTextOffset = 0x2BDD40;

	GlobalPlayerOffset = 0xD2F1E0;

	pGameClass1 = GameDll + 0xD2F048;

	_GetFloatStat = GameDll + 0x6BB2A0;

	CreateTextTag = ( GAME_CreateTextTag )( GameDll + 0x22EB50 );
	SetTextTagPos = ( GAME_SetTextTagPos )( GameDll + 0x246FD0 );
	SetTextTagColor = ( GAME_SetTextTagColor )( GameDll + 0x246ED0 );
	SetTextTagVelocity = ( GAME_SetTextTagVelocity )( GameDll + 0x247160 );
	SetTextTagVisibility = ( GAME_SetTextTagVisibility )( GameDll + 0x2471C0 );
	SetTextTagPermanent = ( GAME_SetTextTagPermanent )( GameDll + 0x246FA0 );
	SetTextTagLifespan = ( GAME_SetTextTagLifespan )( GameDll + 0x246F60 );
	SetTextTagFadepoint = ( GAME_SetTextTagFadepoint )( GameDll + 0x246F20 );

	GetPlayerColor = ( pGetPlayerColor )( GameDll + 0x233D40 );
	Player = ( pPlayer )( GameDll + 0x241EF0 );
	GetTypeInfo = ( p_GetTypeInfo )( GameDll + 0x378570 );

	mGetUnitLevel = ( GetUnitLevel )( GameDll + 0x6B9180 );
	war3hwnd = *( HWND* )( GameDll + 0xD03D60 );
}

void Init285( )
{
	IsGameOffset1 = 0xD328D0;
	IsGameOffset2 = 0xD30110;
	WarcraftGlobalClassOffset = 0xD326F0;
	IsWindowActiveOffset = 0xCA3E74;

	UnitClassOffset = 0xB68904;


	SetTextTagTextOffset = 0x2BDED0;

	GlobalPlayerOffset = 0xD305E0;

	pGameClass1 = GameDll + 0xD30448;

	_GetFloatStat = GameDll + 0x6BB400;

	CreateTextTag = ( GAME_CreateTextTag )( GameDll + 0x22ECE0 );
	SetTextTagPos = ( GAME_SetTextTagPos )( GameDll + 0x247140 );
	SetTextTagColor = ( GAME_SetTextTagColor )( GameDll + 0x247040 );
	SetTextTagVelocity = ( GAME_SetTextTagVelocity )( GameDll + 0x2472D0 );
	SetTextTagVisibility = ( GAME_SetTextTagVisibility )( GameDll + 0x247330 );
	SetTextTagPermanent = ( GAME_SetTextTagPermanent )( GameDll + 0x247110 );
	SetTextTagLifespan = ( GAME_SetTextTagLifespan )( GameDll + 0x2470D0 );
	SetTextTagFadepoint = ( GAME_SetTextTagFadepoint )( GameDll + 0x247090 );

	GetPlayerColor = ( pGetPlayerColor )( GameDll + 0x233ED0 );
	Player = ( pPlayer )( GameDll + 0x242080 );
	GetTypeInfo = ( p_GetTypeInfo )( GameDll + 0x378720 );

	mGetUnitLevel = ( GetUnitLevel )( GameDll + 0x6B92E0 );
	war3hwnd = *( HWND* )( GameDll + 0xD05160 );
}


void Init27b( )
{
	IsGameOffset1 = 0xD6AA98;
	IsGameOffset2 = 0xD6AA98;
	IsWindowActiveOffset = 0xCD5E74;
	WarcraftGlobalClassOffset = 0xD6A8B8;
	UnitClassOffset = 0xB79D34;
	SetTextTagTextOffset = 0x68B4F0;
	GlobalPlayerOffset = 0xD687A8;
	pGameClass1 = GameDll + 0xD68610;

	_GetFloatStat = GameDll + 0x687270;
	CreateTextTag = ( GAME_CreateTextTag ) ( GameDll + 0x1FC4D0 );
	SetTextTagPos = ( GAME_SetTextTagPos ) ( GameDll + 0x214AC0 );
	SetTextTagColor = ( GAME_SetTextTagColor ) ( GameDll + 0x2149C0 );
	SetTextTagVelocity = ( GAME_SetTextTagVelocity ) ( GameDll + 0x214C50 );
	SetTextTagVisibility = ( GAME_SetTextTagVisibility ) ( GameDll + 0x214CB0 );
	SetTextTagPermanent = ( GAME_SetTextTagPermanent ) ( GameDll + 0x214A90 );
	SetTextTagLifespan = ( GAME_SetTextTagLifespan ) ( GameDll + 0x214A50 );
	SetTextTagFadepoint = ( GAME_SetTextTagFadepoint ) ( GameDll + 0x214A10 );

	GetPlayerColor = ( pGetPlayerColor ) ( GameDll + 0x2016F0 );
	Player = ( pPlayer ) ( GameDll + 0x20F890 );
	GetTypeInfo = ( p_GetTypeInfo ) ( GameDll + 0x344760 );

	mGetUnitLevel = ( GetUnitLevel ) ( GameDll + 0x685150 );
	war3hwnd = *( HWND* )( GameDll + 0xD3D560 );
}

void Init27( )
{
	IsGameOffset1 = 0xBE6530;
	IsGameOffset2 = 0xBE6530;
	IsWindowActiveOffset = 0xB673EC;
	WarcraftGlobalClassOffset = 0xBE6350;
	UnitClassOffset = 0xA4A704;
	SetTextTagTextOffset = 0x26DDC0;
	GlobalPlayerOffset = 0xBE4238;
	pGameClass1 = GameDll + 0xBE40A8;

	_GetFloatStat = GameDll + 0x669B40;
	CreateTextTag = ( GAME_CreateTextTag ) ( GameDll + 0x1DEA90 );
	SetTextTagPos = ( GAME_SetTextTagPos ) ( GameDll + 0x1F6E30 );
	SetTextTagColor = ( GAME_SetTextTagColor ) ( GameDll + 0x1F6D30 );
	SetTextTagVelocity = ( GAME_SetTextTagVelocity ) ( GameDll + 0x1F6FC0 );
	SetTextTagVisibility = ( GAME_SetTextTagVisibility ) ( GameDll + 0x1F7020 );
	SetTextTagPermanent = ( GAME_SetTextTagPermanent ) ( GameDll + 0x1F6E00 );
	SetTextTagLifespan = ( GAME_SetTextTagLifespan ) ( GameDll + 0x1F6DC0 );
	SetTextTagFadepoint = ( GAME_SetTextTagFadepoint ) ( GameDll + 0x1F6D80 );

	GetPlayerColor = ( pGetPlayerColor ) ( GameDll + 0x1E3CA0 );
	Player = ( pPlayer ) ( GameDll + 0x1F1E70 );
	GetTypeInfo = ( p_GetTypeInfo ) ( GameDll + 0x327020 );

	mGetUnitLevel = ( GetUnitLevel ) ( GameDll + 0x667A20 );

	war3hwnd = *( HWND* )( GameDll + 0xBDAB88 );
}

void Init26( )
{
	IsGameOffset1 = 0xAB62A4;
	IsGameOffset2 = 0xAB7E98;
	IsWindowActiveOffset = 0xA9E7A4;
	WarcraftGlobalClassOffset = 0xAB4F80;
	UnitClassOffset = 0x931934;
	SetTextTagTextOffset = 0x426CF0;
	GlobalPlayerOffset = 0xAB65F4;
	pGameClass1 = GameDll + 0xAB7788;

	_GetFloatStat = GameDll + 0x27AE90;
	CreateTextTag = ( GAME_CreateTextTag ) ( GameDll + 0x3BC580 );
	SetTextTagPos = ( GAME_SetTextTagPos ) ( GameDll + 0x3BC610 );
	SetTextTagColor = ( GAME_SetTextTagColor ) ( GameDll + 0x3BC6A0 );
	SetTextTagVelocity = ( GAME_SetTextTagVelocity ) ( GameDll + 0x3BC700 );
	SetTextTagVisibility = ( GAME_SetTextTagVisibility ) ( GameDll + 0x3BC760 );
	SetTextTagPermanent = ( GAME_SetTextTagPermanent ) ( GameDll + 0x3BC7C0 );
	SetTextTagLifespan = ( GAME_SetTextTagLifespan ) ( GameDll + 0x3BC820 );
	SetTextTagFadepoint = ( GAME_SetTextTagFadepoint ) ( GameDll + 0x3BC850 );

	GetPlayerColor = ( pGetPlayerColor ) ( GameDll + 0x3C1240 );
	Player = ( pPlayer ) ( GameDll + 0x3BBB30 );
	GetTypeInfo = ( p_GetTypeInfo ) ( GameDll + 0x32C880 );

	mGetUnitLevel = ( GetUnitLevel ) ( GameDll + 0x2A0210 );
	war3hwnd = *( HWND* )( GameDll + 0xAD147C );

}

UINT_PTR TimerID = NULL;

DWORD __stdcall Login( int password )
{
	if ( *( int* ) password == 0x26a )
	{
		Init26( );
		IsMIX = FALSE;
	}
	else if ( *( int* ) password == 0x27a )
	{
		Init27( );
		IsMIX = FALSE;
	}
	else if ( *( int* )password == 0x27b )
	{
		Init27b( );
		IsMIX = FALSE;
	}
	else if ( *( int* )password == 0x28a )
	{
		Init28a( );
		IsMIX = FALSE;
	}
	else if ( *( int* )password == 0x28b )
	{
		Init28b( );
		IsMIX = FALSE;
	}
	else if ( *( int* )password == 0x28c )
	{
		Init28c( );
		IsMIX = FALSE;
	}
	else if ( *( int* )password == 0x284 )
	{
		Init284( );
		IsMIX = FALSE;
	}
	else if ( *( int* )password == 0x285 )
	{
		Init285( );
		IsMIX = FALSE;
	}

	

	srand( ( unsigned int )time( NULL ) );
	TimerID = 401 + ( rand( ) % 500 );

	ttt = SetTimer( war3hwnd, TimerID, 295 + ( rand( ) % 10 ), ManaBarThread );


	return 0;
}


BOOL WINAPI DllMain( HINSTANCE hDLL, UINT reason, LPVOID reserved )
{
	//MessageBoxA( 0 , "test" , "test" , 0 );
	if ( reason == DLL_PROCESS_ATTACH )
	{
		GameDll = ( int ) GetModuleHandle( L"Game.dll" );
		IsWork = false;
	}
	else if ( reason == DLL_PROCESS_DETACH )
	{
		if ( !KillTimer( 0, TimerID ) )
		{
			if ( !KillTimer( war3hwnd, TimerID ) )
			{
				SetTimer( 0, TimerID, -1, 0 );
				SetTimer( war3hwnd, TimerID, -1, 0 );
			}
		}
		Sleep( 1000 );
		unitaddress.clear( );
	}
	return TRUE;
}