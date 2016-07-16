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
			InputArguments arguments = new InputArguments(args);
			string source = arguments["src"];
			string dest = source;
			if(arguments.Contains("dst"))
				dest = arguments["dst"];
			int keyLength = 8;
			if (arguments.Contains("keylen"))
				keyLength = int.Parse(arguments["keylen"]);
			string attribute = "gid";
			if (arguments.Contains("attr"))
				attribute = arguments["attr"];
			XmlDocument doc = new XmlDocument();
			//doc.Load(
			Console.WriteLine(source);
			
			var node = doc.CreateElement("root");
			node.SetAttribute("text", "Привет ромашки!");
			doc.AppendChild(node);
			doc.Save(@"D:\Projects\DioBox\Tools\test.xml");

			Console.WriteLine(Resources.ReadMe);
			//Console.WriteLine(arguments["src"]);
			//string source = null;

			/*for (var i = 0; i < 10; i++)
				Console.WriteLine(generatePassword(8));*/
		}
		static Random _random = new Random();
		static string generatePassword(int limit = 8)
		{
			char []vowels = new char[]{'a', 'e', 'i', 'o', 'u'};
			char []consts = new char[]{'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z'};
			StringBuilder word = new StringBuilder();
			for (int i = 0; i < limit; i++) 
				if (i % 2 == 0) 
					word.Append(consts[_random.Next(consts.Length)]);
				else
					word.Append(vowels[_random.Next(vowels.Length)]);
			return word.ToString();
		}
		/*
//php
function generatePassword( $limit = 8 ) {
  $vowels = array('a', 'e', 'i', 'o', 'u');
  $const = array('b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z');

  $word = '';

  for ($i = 0; $i < $limit; $i++) {
    if ($i % 2 == 0) { // even = vowels
      $word .= $const[rand(0, 20)];  
    } else {
		$word .= $vowels[rand(0, 4)]; 
    
    } 
  }
  return $word;
}
		 */
	}
}
