#include<vector>
#include<iostream>

using namespace std;

extern "C" __declspec(dllexport) int _stdcall g_Add(int a, int b)
{
	return a + b;
}

extern "C" __declspec(dllexport) int _stdcall g_Minus(int a, int b)
{
	return a - b;
}

extern "C" __declspec(dllexport) int _stdcall g_Multi(int a, int b)
{
	return a * b;
}

extern "C" __declspec(dllexport) int _stdcall g_Div(int a, int b)
{
	return a / b;
}

extern "C" __declspec(dllexport) int _stdcall g_DotProd(int a[], int b[], int num)
{
	int res = 0;
	for (int i = 0; i < num; i++)
	{
		res += a[i] * b[i];
	}
	return res;
}

extern "C" __declspec(dllexport) void _stdcall g_Char(char *s, int n)
{
	for (int i = 0; i < n; i++)
	{
		cout << s[i];
	}
	cout << endl;
	//return s;
}

