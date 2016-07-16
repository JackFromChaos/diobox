using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XmlNodeIdGenerator
{
	public class XmlNodeIdGen
	{
		public string sourceFile;
		public string destFile;
		public int keyLength;
		public string attributeName;
		public List<string> ignoreElement;
		
		protected bool _changed;
		protected List<string> _allKeys;
		public void Gen()
		{
			_allKeys = new List<string>();
			_changed = false;
			XmlDocument doc = new XmlDocument();
			doc.Load(sourceFile);
			recursiveGen(doc.LastChild as XmlElement);
			if (_changed || destFile != sourceFile) 
				doc.Save(destFile);
		}

		private void recursiveGen(XmlElement xmlElement)
		{
			if (!checkIgnore(xmlElement))
			{
				var key = xmlElement.GetAttribute(attributeName);
				if (string.IsNullOrEmpty(key))
				{
					key = generatePassword(keyLength);
					if (xmlElement.Attributes.Count > 0)
					{
						var attr=xmlElement.OwnerDocument.CreateAttribute(attributeName);
						attr.Value = key;
						xmlElement.Attributes.InsertBefore(attr,xmlElement.Attributes[0]);
					}
					else
						xmlElement.SetAttribute(attributeName, key);
					_changed = true;
				}
				if(_allKeys.Contains(key))
					Console.WriteLine("Error! Duplicate key {0}!",key);
				_allKeys.Add(key);
			}
			foreach(var child in xmlElement)
			{
				XmlElement e = child as XmlElement;
				if (e != null)
					recursiveGen(e);
			}
		}

		private bool checkIgnore(XmlElement xmlElement)
		{
			if (ignoreElement == null)
				return false;
			return ignoreElement.Contains(xmlElement.Name);
		}
		Random _random = new Random();
		string generatePassword(int limit = 8)
		{
			char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
			char[] consts = new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
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
