using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XmlNodeIdGenerator.Properties;

namespace XmlNodeIdGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			XmlNodeIdGen gen = new XmlNodeIdGen();
			InputArguments arguments = new InputArguments(args);
			gen.sourceFile = arguments["src"];
			if (string.IsNullOrEmpty(gen.sourceFile) && args.Length > 0 && args[0][0] != '-')
				gen.sourceFile = args[0];

			gen.destFile = gen.sourceFile;
			if(arguments.Contains("dst"))
				gen.destFile = arguments["dst"];
			gen.keyLength = 8;
			if (arguments.Contains("keylen"))
				gen.keyLength = int.Parse(arguments["keylen"]);
			gen.attributeName = "gid";
			if (arguments.Contains("attr"))
				gen.attributeName = arguments["attr"];
			if(arguments.Contains("ignore"))
				gen.ignoreElement = arguments["ignore"].Split(',').ToList();
			if (string.IsNullOrEmpty(gen.sourceFile))
			{
				Console.WriteLine("Ошибка! Источник не определен!");
				Console.WriteLine(Resources.ReadMe);
				return;
			}
			if (arguments.Contains("help") || arguments.Contains("h"))
				Console.WriteLine(Resources.ReadMe);
			gen.Gen();
		}
	}
}
