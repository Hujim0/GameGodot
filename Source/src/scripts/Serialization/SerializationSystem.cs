using GodotGame.Dialogues;
using Newtonsoft.Json;
using System.IO;

namespace GodotGame.Serialization
{
	public static class SerializationSystem
	{
		public const string PathToGodot = @"res:";
		public const string PathToLanguages = @"languages\";
		public const string PathToDialogues = @"dialogues\";

		public static string AbsolutePathToLanguages = string.Empty;
		public static string AbsolutePathToData = string.Empty;

		public static bool isPathsReady = false;

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
		///         Note: relative path from res:\\data\languages. Example:
		///             <code>
		///                 "{Language}\dialogues\{scene}\{npcName}\file.json"
		///             </code>
		///     </summary>
		/// </param>
		public static void SaveLocalizationDataGeneric<T>(object data, string path)
		{
			if (!isPathsReady) GetPaths();

			string truePath = $"{AbsolutePathToLanguages}{path}";

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
		///         Note: relative path from res:\\data. Example:
		///             <code>
		///                 "Preferences.json"
		///             </code>
		///     </summary>
		/// </param>
		public static void SaveDataGeneric<T>(object data, string path)
		{
			if (!isPathsReady) GetPaths();

			string truePath = $"{AbsolutePathToData}{path}";

			string dataInFile = JsonConvert.SerializeObject(data, Formatting.Indented);

			Godot.GD.Print(truePath);

			Godot.GD.Print(dataInFile);
			if (!File.Exists(truePath)) Directory.CreateDirectory(Path.GetDirectoryName(truePath));
			File.WriteAllText(truePath, dataInFile);
		}


		#endregion

		#region load

		#region dialogues

		/// <summary>
		///     Gets all Dialogues from exact directory.
		/// </summary>
		/// <param name="directory">
		///     <summary>
		///         Note: relative path from res:\\data\languages\{ru||en}. No "\" at the end.
		///             <code> 
		///                 Example: "{Language}\{scene}\{npcName}\{Main||Secondary}" 
		///             </code>
		///     </summary>
		/// </param>
		public static Dialogue[] LoadDialogues(string directory)
		{
			if (!isPathsReady) GetPaths();

			string pathToDir = $"{AbsolutePathToLanguages}{directory}";

			Godot.GD.Print(pathToDir);

			if (!Directory.Exists(pathToDir)) return null;

			string[] paths =  Directory.GetFiles(pathToDir, "*");

			Dialogue[] dialogues = new Dialogue[paths.Length];

			for (int i = 0; i < paths.Length; i++)
			{
                dialogues[i] = LoadLocalizationDataGeneric<Dialogue>($@"{directory}\{Path.GetFileName(paths[i])}");
				Godot.GD.Print(dialogues[i].panels[0].txt);
			}

			return dialogues;
		}

		#endregion

		/// <summary>
		///     Note: relative path from res:\\data\languages
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

			string pathToFile = $"{AbsolutePathToLanguages}{path}";

			Godot.GD.Print(pathToFile);

			if (!File.Exists(pathToFile)) { Godot.GD.PrintErr($"Cant find file at {pathToFile}"); return default; }

			return JsonConvert.DeserializeObject<T>(File.ReadAllText(pathToFile));
		}
		/// <summary>
		///     Note: relative path from res:\\data\
		///         <code> 
		///             Example for file: "Preferences.json" 
		///         </code>
		/// </summary>
		/// 
		/// <typeparam name="T"></typeparam>
		/// <param name="path">
		///     <summary>
		///         Note: relative path from res:\\data\
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

			AbsolutePathToLanguages = $@"{AbsolutePathToData}{PathToLanguages}";

			isPathsReady = true;
		}
	}
}
