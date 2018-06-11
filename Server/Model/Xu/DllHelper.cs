using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ETModel
{
    public static class DllHelperEx
    {
        public static Assembly GetHotfixAssembly(string path)
        {
            byte[] dllBytes = File.ReadAllBytes(path + "/Hotfix.dll");
#if __MonoCS__
			byte[] pdbBytes = File.ReadAllBytes(path + "/Hotfix.dll.mdb");
#else
            byte[] pdbBytes = File.ReadAllBytes(path + "/Hotfix.pdb");
#endif
            Assembly assembly = Assembly.Load(dllBytes, pdbBytes);
            return assembly;
        }
    }
}
