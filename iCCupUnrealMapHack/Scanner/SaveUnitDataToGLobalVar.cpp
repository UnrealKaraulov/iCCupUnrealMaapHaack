#pragma region Headers
#define _WIN32_WINNT 0x0501 
#define WINVER 0x0501 
#define NTDDI_VERSION 0x05010000
//#define BOTDEBUG
#define WIN32_LEAN_AND_MEAN
#define IsKeyPressed(CODE) (GetAsyncKeyState(CODE) & 0x8000) > 0

#include <Windows.h>
#include <vector>
using namespace std;

union DWFP
{
	DWORD dw;
	float fl;
};



HANDLE ttt = NULL;
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

vector<UnitInfo> unittempvector;

int unitglobaldataaddr = 0;
int unitglobaldatavectorsize = 0;

void SetTlsForMe( )
{
	UINT32 Data = *( UINT32 * ) ( GameDll + 0xACEB4C );
	UINT32 TlsIndex = *( UINT32 * ) ( GameDll + 0xAB7BF4 );
	if ( TlsIndex )
	{
		UINT32 v5 = **( UINT32 ** ) ( *( UINT32 * ) ( *( UINT32 * ) ( GameDll + 0xACEB5C ) + 4 * Data ) + 44 );
		if ( !v5 || !( *( LPVOID * ) ( v5 + 520 ) ) )
		{
			Sleep( 800 );
			SetTlsForMe( );
			return;
		}
		TlsSetValue( TlsIndex , *( LPVOID * ) ( v5 + 520 ) );
	}
	else
	{
		Sleep( 900 );
		SetTlsForMe( );
		return;
	}
}


BOOL IsWindowActive( BOOL SkipNoCheck = FALSE )
{
	return *( BOOL* ) ( GameDll + 0xA9E7A4 );
}

BOOL IsGame( ) // my offset + public
{
	return ( *( int* ) ( ( UINT32 ) GameDll + 0xACF678 ) > 0 || *( int* ) ( ( UINT32 ) GameDll + 0xAB62A4 ) > 0 )/* && !IsLagScreen( )*/;
}


// Pure code: Get unit count and units array
UINT GetUnitCountAndUnitArray( int ** unitarray )
{
	int GlobalClassOffset = *( int* ) ( GameDll + 0xAB4F80 );
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
		int xaddr = GameDll + 0x931934;
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

int faceoffest1;
int faceoffest2;
int faceoffest3;
int faceoffest4;
int faceoffest5;

__declspec( naked ) DWFP __cdecl GetUnitFacingReal( int unitaddr )
{
	faceoffest5 = unitaddr;

	if ( !faceoffest5 )
	{
		__asm
		{
			sub esp , 0x08;
			mov eax , 0;
			add esp , 0x08;
			ret;
		}
	}

	__asm
	{
		mov ecx , 0x100001;
		sub esp , 0x08;
		push esi;
		call faceoffest1;
		mov eax , faceoffest5;
		mov esi , eax;
		mov edx , [ esi + 0x10 ];
		mov ecx , [ esi + 0x0C ];
		call faceoffest2;
		xor ecx , ecx;
		cmp[ eax + 0x0C ] , 0x2B61676C;
		setne cl;
		sub ecx , 0x01;
		and ecx , eax;
		mov eax , ecx;
		mov edx , [ esi ];
		mov eax , [ edx + 0x000000B8 ];
		mov ecx , esi;
		call eax;
		mov edx , [ eax ];
		mov edx , [ edx + 0x1C ];
		lea ecx , [ esp + 0x08 ];
		push ecx;
		mov ecx , eax;
		call edx;
		push faceoffest3;
		mov edx , eax;
		lea ecx , [ esp + 0x08 ];
		call faceoffest4;
		mov eax , [ esp + 0x04 ];
		pop esi;
		add esp , 0x08;
		ret;
	}
}



float __cdecl GetUnitFacing( int unitaddr )
{
	return GetUnitFacingReal( unitaddr ).fl;
}

typedef int( __fastcall * GetUnitLevel )( int unitaddr , int unused );
GetUnitLevel mGetUnitLevel;

int _GetFloatStat = 0;
float GetUnitFloatStat( int unitaddr , DWORD statNum )
{
	float result = 0;
	__asm
	{
		PUSH statNum;
		LEA EAX , result
			PUSH EAX
			MOV ECX , unitaddr
			CALL _GetFloatStat
	}
	return result;
}
bool gamestarted = false;

void SaveUnitGlobalWork( )
{
	try
	{
		if ( IsGame( ) )
		{
			if ( !gamestarted || IsWindowActive( ) == FALSE )
			{
				gamestarted = true;
				if ( unitglobaldata.size( ) > 0 )
				{
					unitglobaldata.clear( );
				}
				if ( unittempvector.size( ) > 0 )
				{
					unittempvector.clear( );
				}
				unitglobaldataaddr = ( int ) &unitglobaldata[ 0 ];
				unitglobaldatavectorsize = 0;
			}


			if ( unittempvector.size( ) > 0 )
			{
				unittempvector.clear( );
			}

			int * unitsarray = 0;
			int UnitsCount = GetUnitCountAndUnitArray( &unitsarray );

			if ( UnitsCount > 0 && unitsarray )
			{
				for ( int i = 0; i < UnitsCount; i++ )
				{
					if ( unitsarray[ i ] )
					{
						if ( IsNotBadUnit( unitsarray[ i ] ) )
						{
							if ( IsHero( unitsarray[ i ] ) )
							{
								UnitInfo tmpunit = UnitInfo( );
								tmpunit.unitaddr = unitsarray[ i ];
								tmpunit.unitfacing = GetUnitFacing( unitsarray[ i ] );
								tmpunit.unitlevel = mGetUnitLevel( unitsarray[ i ] , unitsarray[ i ] );
								tmpunit.unithp = GetUnitFloatStat( unitsarray[ i ] , 0 );
								tmpunit.unitmaxhp = GetUnitFloatStat( unitsarray[ i ] , 1 );
								tmpunit.unitmp = GetUnitFloatStat( unitsarray[ i ] , 2 );
								tmpunit.unitmaxmp = GetUnitFloatStat( unitsarray[ i ] , 3 );
								unittempvector.push_back( tmpunit );
							}

						}

					}
					else
						break;
				}

			}


			if ( unitglobaldata.size( ) > 0 )
			{
				unitglobaldata.clear( );
			}

			if ( unittempvector.size( ) > 0 )
			{
				unitglobaldata.insert( unitglobaldata.begin( ) , unittempvector.begin( ) , unittempvector.end( ) );
				unittempvector.clear( );
			}
			if ( unittempvector.size( ) > 0 )
			{
				unittempvector.clear( );
			}
			unitglobaldataaddr = ( int ) &unitglobaldata[ 0 ];
			unitglobaldatavectorsize = ( int ) unitglobaldata.size( );
		}
		else
		{
			if ( gamestarted )
			{
				unitglobaldatavectorsize = 0;
				gamestarted = false;
				if ( unitglobaldata.size( ) > 0 )
				{
					unitglobaldata.clear( );
				}

			}

		}
	}
	catch ( ... )
	{

	}

	Sleep( 400 );
}

DWORD WINAPI SaveUnitsDataToGlobal( LPVOID x )
{
	try
	{
		Sleep( 1900 );

		//unitglobaldataaddr = ( int ) &unitglobaldata[ 0 ];
		SetTlsForMe( );

		while ( true )
		{

			SaveUnitGlobalWork( );

			Sleep( 50 );
		}

	}
	catch ( ... )
	{
		Sleep( 10000 );
		ttt = CreateThread( 0 , 0 , SaveUnitsDataToGlobal , 0 , 0 , 0 );
	
	}
	return 0;
}


BOOL WINAPI DllMain( HINSTANCE hDLL , UINT reason , LPVOID reserved )
{
	if ( reason == DLL_PROCESS_ATTACH )
	{
		ttt = CreateThread( 0 , 0 , SaveUnitsDataToGlobal , 0 , 0 , 0 );
		unitglobaldataaddr = 504030;
		unitglobaldatavectorsize = 403020;
		GameDll = ( int ) GetModuleHandle( L"Game.dll" );
		faceoffest1 = GameDll;
		faceoffest1 += 0x3BDCB0;
		faceoffest2 = GameDll;
		faceoffest2 += 0x3FA30;
		faceoffest3 = GameDll;
		faceoffest3 += 0xAAE60C;
		faceoffest4 = GameDll;
		faceoffest4 += 0x6EEE20;
		_GetFloatStat = GameDll + 0x27AE90;
		mGetUnitLevel = ( GetUnitLevel ) ( GameDll + 0x2A0210 );
	}
	else if ( reason == DLL_PROCESS_DETACH )
	{
		TerminateThread( ttt , 0 );
		unitglobaldataaddr = 0;
		unitglobaldatavectorsize = 0;
		unitglobaldata.clear( );
		unittempvector.clear( );
		GameDll = 0;
		faceoffest1 = 0;
		faceoffest2 = 0;
		faceoffest3 = 0;
		faceoffest4 = 0;
		_GetFloatStat = 0;
		mGetUnitLevel = 0;
	}
	return TRUE;
}