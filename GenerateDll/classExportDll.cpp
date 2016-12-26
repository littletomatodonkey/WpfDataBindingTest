#include<vector>
#include<iostream>
#include<string>

using namespace std;

typedef struct
{
	unsigned char Port;
	unsigned long Id;
	unsigned char Ctrl;
	unsigned char pData[8];
}HSCAN_MSG;

extern "C"
{
	class __declspec(dllexport) TestFcnDllClass
	{
	public:
		static void printInfo(const char *fn)
		{
			cout << "name is : " << fn << endl;
		}

		static int add(int a, int b)
		{
			return a + b;
		}

		double minus( double a, double b )
		{
			return a - b;
		}

		double addMnius(int a, int b, int c)
		{
			return minus(add(a, b), c);
		}

		int HSCAN_SendCANMessage(unsigned char nDevice, unsigned char nPort, HSCAN_MSG *msg, int nLength)
		{
			int res = 0;
			for (int i = 0; i < nLength; i++)
			{
				res += msg[i].Port;
			}
			return res;
		}

		HSCAN_MSG* GenerateMsg( )
		{
			HSCAN_MSG msg;
			msg.Id = 10;
			msg.Port = 200;
			return &msg;
		}
		
		
	};

}
