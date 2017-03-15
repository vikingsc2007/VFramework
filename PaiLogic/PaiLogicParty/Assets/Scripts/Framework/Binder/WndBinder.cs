using System;
using System.Collections;
using System.Collections.Generic;



public abstract class WndBinder  {
	


	private class WndBindValue{
		public int exclusion{  get;private set;}
		public string layer{  get;private set;}

		public Dictionary<string,SkinData> skins;

		public WndBindValue(int ex,string ly){
			exclusion = ex;
			layer = ly;
			skins = new Dictionary<string,SkinData>();
		}


		public void AddSkin(string skinName,string assetBundleName,string assetName){
			if(skins.ContainsKey(skinName)){
				UnityEngine.Debug.LogError ("Add Skin Fault.."+skinName);
			}
			skins.Add (skinName, new SkinData (assetBundleName, assetName));
		}
	}


	public WndBinder(){
		binded = new Dictionary<string, WndBindValue>();
	}


	private Dictionary<string,WndBindValue> binded;


	public abstract void RegisterAll ();
	public abstract void RegisterSkins ();


	public bool Register<T>(int exclusion,string layer) where T:Framework.Basics.BaseWnd{
		if (!binded.ContainsKey (typeof(T).Name)) {
			binded.Add (typeof(T).Name, new WndBindValue (exclusion, layer ));
			return true;
		}
		return false;
	}



	public int GetExclusion<T>() where T:Framework.Basics.BaseWnd{
		if (binded.ContainsKey (typeof(T).Name)) {
			
			return binded[typeof(T).Name].exclusion;
		}
		return -1;
	}
	public string GetLayer<T>() where T:Framework.Basics.BaseWnd{
		if (binded.ContainsKey (typeof(T).Name)) {
			
			return binded[typeof(T).Name].layer;
		}
		return null;
	}


	public string GetLayer(string wndName){
		if (binded.ContainsKey (wndName)) {

			return binded[wndName].layer;
		}
		return null;
	}


	public int GetExclusion(string wndName){
		if (binded.ContainsKey (wndName)) {

			return binded[wndName].exclusion;
		}
		return -1;
	}



	public void AddSkin<T>(string skinName,string assetBundleName ,string assetName) where T:Framework.Basics.BaseWnd{
		if (!binded.ContainsKey (typeof(T).Name)) {
			UnityEngine.Debug.LogError (string.Format("can't add skin because {0} not Binded",typeof(T).Name));
			return;
		}
		binded [typeof(T).Name].AddSkin (skinName, assetBundleName, assetName);
	}

	public SkinData GetSkin(string wndName ,string skinName){
		if (!binded.ContainsKey (wndName)) {
			UnityEngine.Debug.LogError (string.Format("can't get skin because {0} not Binded",wndName));
			return null;
		}

		if (!binded [wndName].skins.ContainsKey (skinName)) {
			UnityEngine.Debug.LogError (string.Format("can't get skin because skin {0} not added",skinName));
			return null;
		}
		return binded [wndName].skins [skinName];
	}



		

}


public class SkinData{
	public string assetBundleName{  get;private set;}
	public string assetName{  get;private set;}

	public SkinData(string assetBundle, string asset){
		assetBundleName = assetBundle;
		assetName = asset;
	}
}