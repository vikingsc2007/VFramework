using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class PTUICodeGenerator
{
    [MenuItem("Assets/PTUIFramework - Create UICode")]
    static public void CreateUICode()
    {
		m_Instance.CreateCode(Selection.activeGameObject);
    }

	[MenuItem("Tools/FairyGUI/Create All UICode", false, 0)]
	static public void CreateUICodeAll()
	{
		m_Instance.CreateAllCode();
	}
	private void CreateAllCode()
	{
		string path = GetUIPrefabPath ();
		if (Directory.Exists (path)) {
		
			string[] files = Directory.GetFiles (path);
			for (int i = 0; i < files.Length; i++) {
				string filePath = files [i];
				string fileName = filePath.Substring (filePath.LastIndexOf("/")+1);
				if (fileName.StartsWith ("UI") && fileName.EndsWith ("Wnd.prefab")) {
					GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/ArtRes/UIPrefab/" + fileName);
					CreateCode (prefab);
				}


			}

		
		}
	}

    private void CreateCode(GameObject go)
    {
        if (null != go && go.name.StartsWith("UI"))
		{  
			PrefabType prefabType = PrefabUtility.GetPrefabType(go);
			if (PrefabType.Prefab != prefabType)
			{
				return;
			}
            m_SelectGameObject = go;

			CreateUIComponentsCode();
        }
    }

  

    private void CreateUIComponentsCode()
    {
        if (null != m_SelectGameObject)
        {
			string strDlg = m_SelectGameObject.name.Substring(2,m_SelectGameObject.name.Length-2);
            string strFilePath = string.Format("{0}/{1}.cs", GetScriptsPath(), strDlg);
            if (File.Exists(strFilePath) == false)
            {
                StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
                StringBuilder strBuilder = new StringBuilder();

                strBuilder.AppendLine("using System;");
                strBuilder.AppendLine("using System.Collections.Generic;");
                strBuilder.AppendLine("using UnityEngine;");
				strBuilder.AppendLine();
				strBuilder.AppendLine ("namespace Game.Wnds{");

				strBuilder.Append ("\t").AppendFormat ("public class {0} : Game.Basics.BaseWnd", strDlg);
				strBuilder.AppendLine();
				strBuilder.Append ("\t").AppendLine("{");

				strBuilder.Append ("\t\t").Append ("public "+strDlg+" ()").Append(":base(typeof("+ strDlg +" )){");
				strBuilder.AppendLine();
				strBuilder.Append ("\t\t").AppendLine("}");
				strBuilder.AppendLine();


				strBuilder.Append ("\t\t").Append ("public override void OnOpen ()").AppendLine ("{");
				strBuilder.Append ("\t\t\t").AppendLine ("base.OnOpen ();");
				strBuilder.Append ("\t\t").AppendLine ("}");

				strBuilder.Append("\t").AppendLine("}");
				strBuilder.AppendLine ("}");
                sw.Write(strBuilder);
                sw.Flush();
                sw.Close();
            }

			string strResFilePath = Application.dataPath + "/Scripts/Game/Enums/WndAssetNames.cs";
			if (File.Exists (strResFilePath)) {
				StreamReader sr = new StreamReader (strResFilePath, Encoding.UTF8);

				string totalstring = sr.ReadToEnd ();
				sr.Close();
				string targetStr = string.Format("public const string {0} = \"UI{1}\";",strDlg,strDlg);
				if (totalstring.Contains (targetStr)) {
					Debug.Log (strDlg+ "resource name created");
				} else {
					totalstring = totalstring.Insert (totalstring.IndexOf ("public class WndAssetNames {\n") + 30, "\t"+targetStr+"\n");
					StreamWriter sw = new StreamWriter(strResFilePath, false, Encoding.UTF8);
					sw.Write (totalstring);
					sw.Flush ();
					sw.Close ();
				}
			}

			string strBindFilePath = Application.dataPath + "/Scripts/Game/Binds/GameWndBind.cs";
			if (File.Exists (strBindFilePath)) {
				StreamReader sr = new StreamReader (strBindFilePath, Encoding.UTF8);

				string totalstring = sr.ReadToEnd ();
				bool changed = false;
				sr.Close();
				string targetStr = string.Format ("\t\t\tAddSkin<{0}>(ThemesName.Default,ThemesBundleName.Default,WndAssetNames.{1});\n", strDlg, strDlg);
				if (totalstring.Contains (string.Format("AddSkin<{0}>",strDlg))) {
					Debug.Log (strDlg + " skin registered");
				} else {
					string headstring = "public override void RegisterSkins(){\n";
					totalstring = totalstring.Insert (totalstring.IndexOf (headstring) + headstring.Length,targetStr);
					changed = true;
				}


				string targetStr2 = string.Format ("\t\t\tRegister<{0}> (UIExclusion.Window,GameUILayer.Default);\n", strDlg);

				if (totalstring.Contains (string.Format("Register<{0}>",strDlg))) {
					Debug.Log (strDlg +" bind registered");
				} else {
					string headstring = "public override void RegisterAll (){\n";
					totalstring = totalstring.Insert (totalstring.IndexOf (headstring) + headstring.Length,targetStr2);
					changed = true;
				}

				if (changed) {
					StreamWriter sw = new StreamWriter (strBindFilePath, false, Encoding.UTF8);
					sw.Write (totalstring);
					sw.Flush ();
					sw.Close ();
				}
			}
		

            AssetDatabase.Refresh();
            Debug.Log("Success Create UIObject Code");
        }
    }

    private string GetScriptsPath()
    {
		var retDir = PTFrameworkConfig.UIScriptDir;
		if (!Directory.Exists (retDir)) {
			Directory.CreateDirectory (retDir);
		}
		return retDir;
    }

    private string GetScriptsTempPath()
    {
        return Application.dataPath + "/../../";
    }

	private string GetUIPrefabPath()
    {
		var retDir = PTFrameworkConfig.UIPrefabDir;
		if (!Directory.Exists (retDir)) {
			Directory.CreateDirectory (retDir);
		}
		return retDir;
    }

    private GameObject m_SelectGameObject = null;
    private Dictionary<string, Transform> m_dicNameToTrans = null;
    static private PTUICodeGenerator m_Instance = new PTUICodeGenerator();
}
