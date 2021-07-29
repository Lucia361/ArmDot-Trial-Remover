using dnlib.DotNet;
using dnlib.DotNet.Writer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmDotTrialRemover
{
    class Program
	{
		public static ModuleDefMD module;
		static void Main(string[] args)
        {
			
			Console.Title = "ArmDot 2021.16 │ Trial Remover by 0x29A";

			string path = args[0];

		

			module = ModuleDefMD.Load(path);
			
			
			
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("[+] Assembly loaded!");



			RemoveTrial(module);





			ModuleWriterOptions moduleWriterOptions = new ModuleWriterOptions(module);
			moduleWriterOptions.MetaDataLogger = DummyLogger.NoThrowInstance;
			string filename = string.Concat(new string[]
			{
				Path.GetDirectoryName(path),
				"\\",
				Path.GetFileNameWithoutExtension(path),
				"_Trial_Removed",
				Path.GetExtension(path)
			});
			if (module.IsILOnly)
			{
				module.Write(filename, moduleWriterOptions);
			}
			else
			{
				NativeModuleWriterOptions nativeModuleWriterOptions = new NativeModuleWriterOptions(module);
				nativeModuleWriterOptions.MetaDataLogger = DummyLogger.NoThrowInstance;
				module.NativeWrite(filename, nativeModuleWriterOptions);
			}
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("[+] File saved");
			Console.WriteLine(filename);

			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Press key to exit...");
			Console.Read();


		}


		public static void RemoveTrial(ModuleDef module)
		{

			//so easy
			foreach (MethodDef md in module.GlobalType.Methods)
			{
				if (md.Name != ".cctor") continue;
				module.GlobalType.Remove(md);
				break;
			}
			//so easy
		}




	}
}
