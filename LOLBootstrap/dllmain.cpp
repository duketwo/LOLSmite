#include <metahost.h>
#include <string>
#include <Windows.h>
#include <stdio.h>
#include <mscoree.h>



#pragma comment(lib, "mscoree.lib")

#import "mscorlib.tlb" raw_interfaces_only				\
    high_property_prefixes("_get","_put","_putref")		\
    rename("ReportEvent", "InteropServices_ReportEvent")

using namespace mscorlib;
using namespace std;



//
// Parses arguments used to invoke a managed assembly
//
struct ClrArgs
{
	static const LPCWSTR DELIM;

	ClrArgs(LPCWSTR command)
	{
		int i = 0;
		wstring s(command);
		wstring* ptrs[] = { &pwzAssemblyPath, &pwzTypeName, &pwzMethodName };

		while (s.find(DELIM) != wstring::npos && i < 3)
		{
			*ptrs[i++] = s.substr(0, s.find(DELIM));
			s.erase(0, s.find(DELIM) + 1);
		}

		if (s.length() > 0)
			pwzArgument = s;
	}

	wstring pwzAssemblyPath;
	wstring pwzTypeName;
	wstring pwzMethodName;
	wstring pwzArgument;
};

const LPCWSTR ClrArgs::DELIM = L"\t"; // delimiter

//
// Function to start the DotNet runtime and invoke a managed assembly
//
__declspec(dllexport) HRESULT ImplantDotNetAssembly(_In_ LPCTSTR lpCommand)
{
    HRESULT hr;
    ICLRMetaHost *pMetaHost = NULL;
    ICLRRuntimeInfo *pRuntimeInfo = NULL;
	ICLRRuntimeHost *pClrRuntimeHost = NULL;

	// build runtime
	hr = CLRCreateInstance(CLSID_CLRMetaHost, IID_PPV_ARGS(&pMetaHost));
	hr = pMetaHost->GetRuntime(L"v4.0.30319", IID_PPV_ARGS(&pRuntimeInfo));
    hr = pRuntimeInfo->GetInterface(CLSID_CLRRuntimeHost, IID_PPV_ARGS(&pClrRuntimeHost));

	// start runtime
	hr = pClrRuntimeHost->Start();	



	// parse the arguments
	ClrArgs args(lpCommand);

	// execute managed assembly
	DWORD pReturnValue;
	hr = pClrRuntimeHost->ExecuteInDefaultAppDomain(
		args.pwzAssemblyPath.c_str(), 
		args.pwzTypeName.c_str(), 
		args.pwzMethodName.c_str(), 
		args.pwzArgument.c_str(), 
		&pReturnValue);

	HDOMAINENUM hEnum = NULL;
	

    pMetaHost->Release();
    pRuntimeInfo->Release();

	//DWORD currentDomain = NULL;
	//pClrRuntimeHost->GetCurrentAppDomainId(&currentDomain);
	//pClrRuntimeHost->UnloadAppDomain(currentDomain, false);

    pClrRuntimeHost->Release();


    return hr;
}

// Entrypoint
BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}
