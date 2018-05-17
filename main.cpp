#include <iostream>
#include <Windows.h>
#include <string>

bool WriteSerial(LPCSTR PortSpecifier, LPCSTR data)
{
	DCB dcb;
	DWORD byteswritten;

	HANDLE hPort = CreateFile(
		PortSpecifier,
		GENERIC_WRITE,
		0,
		NULL,
		OPEN_EXISTING,
		0,
		NULL);

	if(!GetCommState(hPort, &dcb))
		return false;

	dcb.BaudRate = CBR_9600;
	dcb.ByteSize = 8;
	dcb.Parity = NOPARITY;
	dcb.StopBits = ONESTOPBIT;

	if(!SetCommState(hPort, &dcb))
		return false;

	bool retVal = WriteFile(hPort, data, 1, &byteswritten, NULL);
	CloseHandle(hPort);
	return retVal;
}

int main()
{
	/*std::string port, data;

	std::cout << "Port: ";
	std::cin >> port;
	std::cout << "Data: ";
	std::cin >> data;
	std::cout << "\n\nPress a button to start...";*/
	std::cin.get();

	if(!WriteSerial("COM5", "AB"))
		std::cout << "\n\n\nError";
	else
		std::cout << "Success";

	std::cin.get();
	return 0;
}