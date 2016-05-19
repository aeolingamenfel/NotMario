using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

/*
 * Lightweight XML-Like Reader
 * Ivan Mattie
 * 
 * Designed for plaintext game data reading using XML/HTML-esque
 * syntax, referred to from here on out as XMLlite
 * 
 * XMLlite Example:
 * 
 * test.txt =====
 * <name>Fred</name>
 * <age>24</age>
 * ==============
 * 
 * 
 */

namespace NotMario.Utility{
	public class DataReader
	{
		private enum nodeOpenerStates : int {READING_NAME=1, READING_ATTR_NAME, READING_ATTR};

		protected string filename;
		protected string fileData;

		public Dictionary<String, XMLLiteNode> data;


		// ------------------------------------------------------------------------
		/// <summary>
		///  DataReader automatically parses through a XMLLite file as soon as its initialized, 
		///  and stores it in a special dictionary in the data element.
		/// </summary>
		/// <param name="filename">File path. If relative, will go from project root.</param>
		// ------------------------------------------------------------------------
		public DataReader (string filename)
		{
			string dir = Path.GetDirectoryName (Assembly.GetEntryAssembly ().Location);

			this.filename = dir + "/" + filename;
			this.data = new Dictionary<String, XMLLiteNode> ();

			// TODO: may need to swap this out for a streamreader, not 
			//       sure if ReadAllText() is supported on target platforms.
			//       -IM
			this.fileData = System.IO.File.ReadAllText (this.filename);

			this.Parse ();
		}

		protected void Parse()
		{
			for (var x = 0; x < this.fileData.Length; x++) {
				char currentCharacter = this.fileData [x];

				if (currentCharacter == '<')
				if(!char.IsWhiteSpace(currentCharacter)) this.ParseNode (data, x, out x);
			}
		}

		protected XMLLiteNode ParseNode(Dictionary<String, XMLLiteNode> parent, int x, out int newX)
		{
			return this.ParseNode (parent, x, true, out newX);
		}

		protected XMLLiteNode ParseNode(Dictionary<String, XMLLiteNode> parent, int x, bool autoRegister, out int newX)
		{
			XMLLiteNode newNode = new XMLLiteNode ();
			char currentCharacter;

			this.ParseNodeOpeningStatement (x, out newNode._name, out newNode.attributes, out x);

			// If the attribute data is set to "list", use the lists variable of newNode
			if (newNode.attributes.ContainsKey ("data") && newNode.attributes ["data"].Equals ("list")) {
				currentCharacter = this.fileData [x];

				this.ParseNodeList (x, newNode.lists, out x);
			} 
			else // Otherwise, this is a unique-node set
			{
				// Figure Out Value //
				currentCharacter = this.fileData[x];
				while (currentCharacter != '<' && x < this.fileData.Length) {
					newNode._value += currentCharacter;

					x += 1;
					currentCharacter = this.fileData [x];
				}
				newNode._value = newNode._value.Trim ();

				// Go Through All Children Recursively //
				while (!(currentCharacter == '<' && x < this.fileData.Length - 1 && this.fileData [x + 1] == '/')) {
					if(!char.IsWhiteSpace(currentCharacter)) this.ParseNode (newNode.children, x, out x);

					x += 1;
					currentCharacter = this.fileData [x];
				}
			}


			// Skip End of Node //
			while (currentCharacter != '>' && x < this.fileData.Length) {
				x += 1;
				currentCharacter = this.fileData [x];
			}
			x += 1;

			// Finish Up Node and Store //
			if(autoRegister && parent != null) 
				parent.Add (newNode._name, newNode);

			newX = x;

			return newNode;
		}

		protected void ParseNodeList(int x, Dictionary<string, List<XMLLiteNode>> parent, out int newX)
		{
			List<XMLLiteNode> nodes = new List<XMLLiteNode> ();
			char currentCharacter = this.fileData [x];
			char nextCharacter = this.fileData [x + 1];
			string nodesSharedName = "";

			while (!(currentCharacter == '<' && nextCharacter == '/')) {
				if (currentCharacter == '<') {
					XMLLiteNode newNode = this.ParseNode (null, x, false, out x);

					nodes.Add (newNode);
				}

				x += 1;
				currentCharacter = this.fileData [x];
				nextCharacter = this.fileData [x + 1];
			}

			//Debug.Log ("Nodes Count: " + nodes.Count);
			nodesSharedName = nodes [0]._name;

			parent.Add (nodesSharedName, nodes);

			newX = x;
		}

		protected void ParseNodeOpeningStatement(int x, out string name, out Dictionary<string, string> attributes, out int newX)
		{
			name = "";
			attributes = new Dictionary<string, string> ();

			char currentCharacter = this.fileData [x];
			string currentAttributeName = "";
			string currentAttribute = "";
			bool openingParenFound = false;
			int state = (int)DataReader.nodeOpenerStates.READING_NAME;

			while (currentCharacter != '>') {
				if (currentCharacter != '<') {

					// States should generally go
					// READING_NAME => (READING_ATTR_NAME => READING_ATTR)x??
					switch (state)
					{
					case (int)DataReader.nodeOpenerStates.READING_NAME:
						if (currentCharacter == ' ') {
							state = (int)DataReader.nodeOpenerStates.READING_ATTR_NAME;
						} else {
							name += currentCharacter;
						}
						break;
					case (int)DataReader.nodeOpenerStates.READING_ATTR_NAME:
						if (currentCharacter == '=') {
							state = (int)DataReader.nodeOpenerStates.READING_ATTR;
						} else {
							currentAttributeName += currentCharacter;
						}
						break;
					case (int)DataReader.nodeOpenerStates.READING_ATTR:
						// If we are on the opening double-quote,
						// just remember that we started recording.
						if (currentCharacter == '\"' && !openingParenFound) {
							openingParenFound = true;

							// If we are on the closing double-quote,
							// finalize everything, store it, and then
							// reset.
						}else if(currentCharacter == '\"' && openingParenFound){
							// Store
							attributes.Add(currentAttributeName, currentAttribute);

							// Reset
							openingParenFound = false;
							currentAttribute = "";
							currentAttributeName = "";

							// If the next character is a space, skip it
							if (this.fileData.Length - 1 != x && this.fileData [x + 1] == ' ') {
								x += 1;
							}

							state = (int)DataReader.nodeOpenerStates.READING_ATTR_NAME;
						} else {
							currentAttribute += currentCharacter;
						}
						break;
					default:
						break;
					}
				}

				// Finish & loop
				x += 1;
				currentCharacter = this.fileData [x];
			}

			// skip closing ">" character
			x += 1;

			// write-back x
			newX = x;

			//Debug.Log ("name: " + name);
		}

		// ==== Static Interface ==== //

		/*
	 * ReadDirectory() reads the specified directory (from the 
	 * top level of the Unity project) and returns an array of the 
	 * files listed there, throwing out any files that are auto-generated
	 * (and therefore useless) by Unity.
	 * 
	 */
		public static string[] ReadDirectory(string directoryURL)
		{
			return DataReader.ReadDirectory (directoryURL, "meta");
		}

		public static string[] ReadDirectory(string directoryURL, string filter)
		{
			string[] filenames = System.IO.Directory.GetFiles(directoryURL);
			List<string> finalFilenames = new List<string> ();

			for (var f = 0; f < filenames.Length; f++) {
				string[] split   = filenames[f].Split(new char[]{'.'});
				string extension = split [split.Length - 1];

				if (extension.Trim ().ToLower ().Equals (filter)) {
					finalFilenames.Add(filenames[f]);
				}
			}

			return finalFilenames.ToArray ();
		}
	}
}