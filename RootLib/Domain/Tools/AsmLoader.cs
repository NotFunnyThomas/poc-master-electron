using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Tools
{
    public static class AsmLoader { 

        public static void LoadPlugins(string rootPath, IServiceCollection services) 
        {
            String[] listeFiles = Directory.GetFiles(rootPath, "*.dll");
            foreach (String file in listeFiles)
            {
                ProcessAsm(file, services);
            }
        }

        public static void ProcessAsm(String fileName, IServiceCollection services)
        {
            Console.WriteLine($"Process dll {fileName}");
            Assembly asmDyna = Assembly.LoadFrom(fileName);

            InfoPluginAttribute? attr = asmDyna.GetCustomAttribute<InfoPluginAttribute>();
            if (attr != null)
            {
                Console.WriteLine($"asm {asmDyna.FullName} provide {attr.ServiceProvide}");
                Type? tempType = asmDyna.GetType(attr.ServiceImplementationName);
                if (tempType != null)
                {
                    Object? testInterfaceDeamon = tempType.GetInterface("Domain.IServiceDeamon");
                    if (testInterfaceDeamon != null)
                    {
                        services.AddKeyedSingleton(typeof(IServiceDeamon), attr.ServiceProvide, tempType);
                    }          
                }              
            }
            else
            {
                Console.WriteLine($"asm {asmDyna.FullName} pas compatible");
            }
            //asmDyna = null;
        }

        public static Type[] loadAsm()
        {
            Type[] retTypes = new Type[2];
            Assembly asmDyna = Assembly.LoadFrom(@"C:\Users\orsys\Documents\workspaces\RootProject\LesImplementations\bin\Debug\net8.0\LesImplementations.dll");

            retTypes[0] = asmDyna.GetType("Domain.Mom.MomListener");
            retTypes[1] = asmDyna.GetType("Domain.Services.NotificationService");

            InfoPluginAttribute? attr = asmDyna.GetCustomAttribute<InfoPluginAttribute>();
            if (attr != null)
            {
                Console.WriteLine($"asm {asmDyna.FullName} provide {attr.ServiceProvide}");
            }
            else
            {
                Console.WriteLine($"asm {asmDyna.FullName} pas compatible");
            }

            Console.WriteLine($"m : {retTypes[0].Name} n: {retTypes[1].Name}");

            return retTypes;
        }
    }
}
