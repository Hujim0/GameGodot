using GodotGame.Dialogues;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GodotGame.Serialization
{
	public static class SerializationSystem
	{
		public const string PathToGodot = @"res:";
		public const string PathToLanguages = @"languages\";
		public const string PathToDialogues = @"dialogues\";
		public const string PathToSaves = @"saves\";

		public static string AbsolutePathToData = string.Empty;

		public static bool isPathsReady = false;

		#region getdata

		/// <summary>
		///     Gets all SubDirectory names from exact directory.
		/// </summary>
		/// <param name="path">
		///     <summary>
		///         Note: relative path from res:\\data\json\languages\.
		///             <code> 
		///                 Example: "{Language}\{scene}\{npcName}\{Main||Secondary}" 
		///             </code>
		///     </summary>
		/// </param>
		public static IEnumerable<string> GetDirectoryNames (string path) => 
			Directory.EnumerateDirectories($"{AbsolutePathToData}{PathToLanguages}{path}");

        #endregion

        #region save

        /// <summary>
        ///     Save at given path. 
        ///         <code></code>
        ///     Note: read note for path param.
        /// </summary>
        /// 
        /// <param name="data"></param>
        /// <param name="path">
        ///     <summary>
        ///         Note: relative path from res:\\data\json\languages. Example:
        ///             <code>
        ///                 "{Language}\dialogues\{scene}\{npcName}\file.json"
        ///             </code>
        ///     </summary>
        /// </param>
        public static void SaveLocalizationDataGeneric<T>(object data, string path)
		{
			if (!isPathsReady) GetPaths();

			string truePath = $"{AbsolutePathToData}{PathToLanguages}{path}";

			string dataInFile = JsonConvert.SerializeObject(data, Formatting.Indented);

			Godot.GD.Print(truePath);

			Godot.GD.Print(dataInFile);

			if(!File.Exists(truePath)) Directory.CreateDirectory(Path.GetDirectoryName(truePath));
			File.WriteAllText(truePath, dataInFile);
		}



		/// <summary>
		///     Save at given path. 
		///         <code></code>
		///     Note: read note for path param.
		/// </summary>
		/// 
		/// <param name="data"></param>
		/// <param name="path">
		///     <summary>
		///         Note: relative path from res:\\data\json\. Example:
		///             <code>
		///                 "Preferences.json"
		///             </code>
		///     </summary>
		/// </param>
		public static void SaveDataGeneric(object data, string path)
		{
			if (!isPathsReady) GetPaths();

			string truePath = $"{AbsolutePathToData}{path}";

			string dataInFile = JsonConvert.SerializeObject(data, Formatting.Indented);

			Godot.GD.Print(truePath);

			Godot.GD.Print(dataInFile);
			if (!File.Exists(truePath)) Directory.CreateDirectory(Path.GetDirectoryName(truePath));
			File.WriteAllText(truePath, dataInFile);
		}

		/// <summary>
		///     Deletes if file already exist and saves at given path. 
		///         <code></code>
		///     Note: read note for path param.
		/// </summary>
		/// 
		/// <param name="data"></param>
		/// <param name="path">
		///     <summary>
		///         Note: relative path from res:\\data\json\. Example:
		///             <code>
		///                 "Preferences.json"
		///             </code>
		///     </summary>
		/// </param>
		public static void ReplaceDataGeneric(object data, string path)
		{
			if (!isPathsReady) GetPaths();

			string truePath = $"{AbsolutePathToData}{path}";

			string dataInFile = JsonConvert.SerializeObject(data, Formatting.Indented);

			Godot.GD.Print(truePath);

			Godot.GD.Print(dataInFile);
			if (File.Exists(truePath)) File.Delete(truePath);
			File.WriteAllText(truePath, dataInFile);
		}

		#endregion

		#region load

		#region dialogues

		/// <summary>
		///     Gets all Dialogues from exact directory.
		/// </summary>
		/// <param name="path">
		///     <summary>
		///         Note: relative path from res:\\data\json\languages\.
		///             <code> 
		///                 Example: "{Language}\{scene}\{npcName}\{Main||Secondary}" 
		///             </code>
		///     </summary>
		/// </param>
		public static Dialogue[] LoadDialogues(string path)
		{
			string directory = path;

			if (!isPathsReady) GetPaths();

			string pathToDir = $"{AbsolutePathToData}{PathToLanguages}{directory}";

			Godot.GD.Print($"	--- Dialogue load ---");

			if (!Directory.Exists(pathToDir)) { Godot.GD.PrintErr($"Directory doesnt exist! {pathToDir}"); return null; }

			Godot.GD.Print($"Dialogue directory: \"{pathToDir}\"");

			string[] paths =  Directory.GetFiles(pathToDir, "*");

			Dialogue[] dialogues = new Dialogue[paths.Length];

			for (int i = 0; i < paths.Length; i++)
			{
				Godot.GD.Print($"-- File #{i+1}");

				dialogues[i] = LoadLocalizationDataGeneric<Dialogue>($@"{directory}\{Path.GetFileName(paths[i])}");

				dialogues[i].DebugThisDialogue();

/*				Godot.GD.Print("peenis");*/
			}

			return dialogues;
		}

		#endregion

		/// <summary>
		///     Note: relative path from res:\\data\json\languages
		///         <code> 
		///             Example for dialogue file: "{Language}\{scene}\{npcName}\{Main||Secondary}\{fileName}" 
		///         </code>
		/// </summary>
		/// 
		/// <typeparam name="T"></typeparam>
		/// <param name="path">
		///     <summary>
		///         Note: relative path from res:\\data\languages
		///             <code> 
		///                 Example for dialogue file: "{Language}\{scene}\{npcName}\{Main||Secondary}\{fileName}" 
		///             </code>
		///     </summary>
		/// </param>
		/// 
		/// <returns>
		///     Deserialized localization file from json.
		/// </returns>
		public static T LoadLocalizationDataGeneric<T>(string path)
		{
			if (!isPathsReady) GetPaths();

			string pathToFile = $"{AbsolutePathToData}{PathToLanguages}{path}";

			if (!File.Exists(pathToFile)) { Godot.GD.PrintErr($"Cant find file at {pathToFile}"); return default; }

			Godot.GD.Print($"Loaded localization file from \"{pathToFile}\"");

			return JsonConvert.DeserializeObject<T>(File.ReadAllText(pathToFile));
		}

		/// <summary>
		///     Note: relative path from res:\\data\json\
		///         <code> 
		///             Example for file: "Preferences.json" 
		///         </code>
		/// </summary>
		/// 
		/// <typeparam name="T"></typeparam>
		/// <param name="path">
		///     <summary>
		///         Note: relative path from res:\\data\json\
		///             <code> 
		///                 Example for file: "Preferences.json" 
		///             </code>
		///     </summary>
		/// </param>
		/// 
		/// <returns>
		///     Deserialized data file from json.
		/// </returns>
		public static T LoadDataGeneric<T>(string path)
		{
			if (!isPathsReady) GetPaths();

			string pathToFile = $"{AbsolutePathToData}{path}";

			if (!File.Exists(pathToFile)) { Godot.GD.PrintErr($"Cant find file at {pathToFile}"); return default; }

			Godot.GD.Print($"Loaded generic data from \"{pathToFile}\"");

			return JsonConvert.DeserializeObject<T>(File.ReadAllText(pathToFile));
		}

		/*        /// <summary>
				///     Note: relative path from res:\\
				/// </summary>
				/// <typeparam name="T"></typeparam>
				/// <param name="path">
				///     <summary>
				///         Note: relative path from res:\\data\
				///     </summary>
				/// </param>
				/// 
				/// <returns>
				///     Resource file from given path.
				/// </returns>
				public static T GetResource<T>(string path)
				{
					if (!isPathsReady) GetPaths();

					string pathToFile = Path.Combine(AbsolutePathToGodot, path);

					if (!File.Exists(pathToFile)) { Godot.GD.PrintErr($"Cant find file at {pathToFile}"); return default; }

					return Godot.GD.Load<T>($@"res://{path}");
				}*/

		#endregion

		public static void GetPaths()
		{
			AbsolutePathToData = $@"{Directory.GetParent(Path.GetFullPath(PathToGodot)).Parent.FullName}\data\json\";

			isPathsReady = true;
		}
	}
}
