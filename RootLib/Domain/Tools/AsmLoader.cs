using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tools
{
    public static class AsmLoader { 

        public static void LoadPlugins(string rootPath) 
        {
            String[] listeFiles = Directory.GetFiles(rootPath, "*.dll");
            foreach (String file in listeFiles)
            {

            }
        }

        public static void ProcessAsm(String fileName)
        {
            Console.WriteLine($"Process dll {fileName}");
        }

        public static Type[] loadAsm()
        {
            Type[] retTypes = new Type[2];
            string pathDll = @"C:\Users\orsys\Documents\workspaces\RootProject\LesImplementations\bin\Debug\net8.0\LesImplementations.dll";
            Assembly asmDyna = Assembly.LoadFrom(pathDll);

            InfoPluginAttribute? attr = asmDyna.GetCustomAttribute<InfoPluginAttribute>();
            if(attr != null )
            {
                Console.WriteLine($"asm {asmDyna.FullName} provide {attr.ServiceProvide}");
            }
            else
            {
                Console.WriteLine($"asm {asmDyna.FullName} pas compatible");
            }

            retTypes[0] = asmDyna.GetType("Domain.Mom.MomListener");
            retTypes[1] = asmDyna.GetType("Domain.Services.NotificationService");

            Console.WriteLine($"m : {retTypes[0].Name} n: {retTypes[1].Name}");

            return retTypes;
        }
    }
}
