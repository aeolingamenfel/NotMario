using System;
using System.Collections;
using System.Collections.Generic;

namespace NotMario.Utility{
	public class XMLLiteNode
	{
		public string _value;
		public string _name;
		public Dictionary<string, string> attributes;
		public Dictionary<string, XMLLiteNode> children;
		public Dictionary<string, List<XMLLiteNode>> lists;

		public XMLLiteNode ()
		{
			this._value = "";
			this._name = "";
			this.children = new Dictionary<string, XMLLiteNode> ();
			this.attributes = new Dictionary<string, string> ();
			this.lists = new Dictionary<string, List<XMLLiteNode>> ();
		}

		public string GetValue(){
			return this._value;
		}

		public bool HasChild(string childName)
		{
			return this.children.ContainsKey (childName);
		}

		public bool HasAttribute(string attributeName)
		{
			return this.attributes.ContainsKey (attributeName);
		}

		public string GetChildValue(string childName)
		{
			if (this.HasChild (childName)) {
				return this.children [childName]._value;
			} else {
				//Debug.LogWarning ("Attempt made to access child value from child '" + childName + "' when it doesn't exist.");

				return string.Empty;
			}
		}

		public string GetAttribute(string attributeName)
		{
			if (this.HasAttribute (attributeName)) {
				return this.attributes [attributeName];
			} else {
				//Debug.LogWarning ("Attempt made to access node attribute '" + attributeName + "', when it doesn't exist.");

				return string.Empty;
			}
		}

		public override string ToString()
		{
			return this._value;
		}
	}
}