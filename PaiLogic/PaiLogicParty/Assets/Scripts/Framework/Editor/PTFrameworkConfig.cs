using UnityEngine;
using System.Collections;

public class PTFrameworkConfig  {


	public static string UIScriptDir {
		get {
			return Application.dataPath + "/Scripts/Game/Wnds";
		}
	}

	public static string UIFactoryFilePath {
		get {
			return  string.Format("{0}/{1}.cs", Application.dataPath + "/PTGameData/Framework/Script", "PTUIFactory");
		}
	}

	public static string UIPrefabDir {
		get {
			return Application.dataPath + "/ArtRes/UIPrefab";
		}
	}
}

