using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using FairyGUI;

namespace FairyGUIEditor{
	public class UIPanelCreater{
		[MenuItem("Tools/FairyGUI/Add UI Prefabs", false, 0)]
		public static void CreatWndPrefabs(){
			EditorToolSet.LoadPackages();
			List<UIPackage> pkgs = UIPackage.GetPackages();
			StageCamera.CheckMainCamera();

			for (int i = 0; i < pkgs.Count; i++) {
				List<PackageItem> items = pkgs [i].GetItems ();

				foreach (PackageItem pi in items)
				{
					if (pi.type == PackageItemType.Component && pi.exported)
					{
						if (pi.name.Contains ("Wnd")) {
							GameObject panelObject = GameObject.Find ("UI" + pi.name);
							if (panelObject != null)
								GameObject.DestroyImmediate (panelObject);

							panelObject = new GameObject("UI"+pi.name);
							int layer = LayerMask.NameToLayer(StageCamera.LayerName);
							panelObject.layer = layer;
							FairyGUI.UIPanel panel = panelObject.AddComponent<FairyGUI.UIPanel>();
							panel.renderMode = RenderMode.ScreenSpaceCamera;
							panel.packageName = pkgs [i].name;
							panel.componentName = pi.name;
							panel.packagePath = pkgs [i].assetPath;

							UIConfig uiconf = panelObject.AddComponent<UIConfig>();

							PrefabUtility.CreatePrefab ("Assets/ArtRes/UIPrefab/UI" + pi.name +".prefab",panelObject,ReplacePrefabOptions.ReplaceNameBased);

							Debug.Log (pi.name);
							GameObject.DestroyImmediate (panelObject);
						}
							






					}
				}
			}

			GameObject.DestroyImmediate (StageCamera.main.gameObject);
			StageCamera.main = null;

		}
	}
}
