using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework.Basics;

namespace Framework.Managers{

	public class UIManager :  Framework.Basics.BaseManager<UIManager> {
		public WndBinder UIBinder{private set; get;}

		private class LayerWnds{
			public Dictionary<string,BaseWnd> wnds= new Dictionary<string, BaseWnd>();


			public void Sort(int Basicindex){
				if (wnds.Count > 0) {
					List< BaseWnd> lst = new List<BaseWnd>();

					foreach (BaseWnd basewnd in wnds.Values) {
						lst.Add (basewnd);
					}
//					lst.Sort ();

					lst.Sort ((BaseWnd s1,BaseWnd s2)=> {
						return s1.SortOrder.CompareTo(s2.SortOrder);
					});
					for (int i = 0; i < lst.Count; i++) {
						lst [i].SortOrder = i + 1 + Basicindex;
					}
				}
			}
		}
		private class ExclusionWnds{
			public Dictionary<string,BaseWnd> wnds= new Dictionary<string, BaseWnd>();
		}


		private Dictionary<int,ExclusionWnds> exclusionWnds = null;
		private Dictionary<string,LayerWnds> layerWnds = null;
		private Dictionary<string,BaseWnd> openedWnds = null;

		private GameObject _uiRoot;
		public GameObject uiRoot{
			set{ 
				_uiRoot = value;
				CreateLayerAvatar (Framework.Enums.GameUILayer.Bottom);
				CreateLayerAvatar (Framework.Enums.GameUILayer.Down);
				CreateLayerAvatar (Framework.Enums.GameUILayer.Default);
				CreateLayerAvatar (Framework.Enums.GameUILayer.Up);
				CreateLayerAvatar (Framework.Enums.GameUILayer.Top);
			}
			get{
				return _uiRoot;
			}
		}



		public override void Init(){
			base.Init ();
			exclusionWnds = new Dictionary<int, ExclusionWnds> (); 
			layerWnds = new Dictionary<string,LayerWnds> ();
			openedWnds = new Dictionary<string,BaseWnd> ();
			CreateAllLayers ();
		}

		private GameObject CreateLayerAvatar(string name){
			GameObject gameobject = new GameObject();
			gameobject.name = name;
			gameobject.transform.SetParent (_uiRoot.transform);
			gameobject.transform.localPosition = Vector3.zero;
			return gameobject;
		}
		public GameObject GetLayerAvatar(BaseWnd wnd){
			
			return GetLayerAvatar(UIBinder.GetLayer(wnd.wndName));
		}

		public int GetLayerIndex(BaseWnd wnd){
			return GetLayerIndex (UIBinder.GetLayer (wnd.wndName));
//			return GetLayerAvatar(UIBinder.GetLayer(wnd.wndName)).transform;
		}
		public int GetLayerIndex(string  layerName){
			switch (layerName) {
			case Framework.Enums.GameUILayer.Bottom:
				return 0;
			case Framework.Enums.GameUILayer.Down:
				return 10000;
			case Framework.Enums.GameUILayer.Default:
				return 20000;
			case Framework.Enums.GameUILayer.Up:
				return 30000;
			case Framework.Enums.GameUILayer.Top:
				return 40000;
			default:
				return 20000;
			}
			//			return GetLayerAvatar(UIBinder.GetLayer(wnd.wndName)).transform;
		}

		public int GetLayerCount(BaseWnd wnd){
			string layer = UIBinder.GetLayer (wnd.wndName);
			return layerWnds [layer].wnds.Count;
			//			return GetLayerAvatar(UIBinder.GetLayer(wnd.wndName)).transform;
		}

		public GameObject GetLayerAvatar(string layername){
			if (layerWnds.ContainsKey (layername)) {
				if (_uiRoot != null) {
					Transform trans = _uiRoot.transform.Find (layername);
					if (trans == null)
						return CreateLayerAvatar (layername);
					else
						return trans.gameObject;
				}
			}
			return null;
		}


		private void CreateAllLayers(){
			layerWnds.Add (Framework.Enums.GameUILayer.Bottom,new LayerWnds());
			layerWnds.Add (Framework.Enums.GameUILayer.Down,new LayerWnds());
			layerWnds.Add (Framework.Enums.GameUILayer.Default,new LayerWnds());
			layerWnds.Add (Framework.Enums.GameUILayer.Up,new LayerWnds());
			layerWnds.Add (Framework.Enums.GameUILayer.Top,new LayerWnds());
		}




		public void Bind<T>() where T : WndBinder,new(){
			UIBinder = new T();
			UIBinder.RegisterAll ();
			UIBinder.RegisterSkins ();
		}


	


		/**
		 * 当前是否有T类型Wnd打开
		 */ 
		public bool IsWndOpen<T>() where T : BaseWnd{
			return IsWndOpen(typeof(T).Name);
		}

		public bool IsWndOpen(string wndName){
			return openedWnds.ContainsKey (wndName) ;
		}



		public void SendMsg(string msgCode,params object[] param){
			foreach (BaseWnd wnd in openedWnds.Values) {
				wnd.OnRecvMsg (msgCode,param);
			}
		}
		/**
		 * 获取当前打开的T类型Wnd 没有打开的返回null
		 */ 
		public T GetWnd<T>() where T : BaseWnd {
			if (IsWndOpen<T> ()) {
				return openedWnds [typeof(T).Name] as T;
			}
			return null;
		}
			

		/**
		 * 打开T类型Wnd
		 */ 
		public T OpenWnd<T>(object[] data = null,string skinName = Framework.Enums.ThemesName.Default) where T:BaseWnd ,new(){
			return OpenWnd (new T(), data,skinName) as T;
		}


		/**
		 * 关闭T类型Wnd
		 */ 
		public void CloseWnd<T>() where T:BaseWnd{
			CloseWnd (typeof(T).Name);
		}
			

		private void CloseExclusionWnd(ExclusionWnds exclutionWnds){
			bool found = false;
			foreach  (BaseWnd wnd in exclutionWnds.wnds.Values){
				found = true;
				CloseAndRemoveWndNow (wnd);
				break;
			}

			if(found){
				CloseExclusionWnd(exclutionWnds);
			}
		}
			

		public BaseWnd OpenWnd(BaseWnd wnd,object[] openData = null,string skinName = Framework.Enums.ThemesName.Default){
			
			//已经打开
			if (openedWnds.ContainsKey (wnd.wndName) && !openedWnds [wnd.wndName].readyDestroyed)
				return null;

			//如果有互斥窗口 关闭互斥窗口  负数不互斥
			int exclusion = UIBinder.GetExclusion (wnd.wndName);
			if (exclusion >= 0) {
				if (exclusionWnds.ContainsKey (exclusion)) {
					CloseExclusionWnd(exclusionWnds[exclusion]);
					exclusionWnds.Remove (exclusion);
				}
			}


			string layer = UIBinder.GetLayer (wnd.wndName);
			if (!layerWnds.ContainsKey (layer)) {
				Debug.LogError("Use Invailed Layer");
				return null;
			}

			SkinData data = UIBinder.GetSkin (wnd.wndName,skinName);

			layerWnds [layer].Sort (GetLayerIndex(layer));
			wnd.Hide ();
			wnd.Create (openData,data.assetBundleName,data.assetName,(bool success,BaseWnd newwnd)=>{
					
					if(success)
					{
						newwnd.OnOpen ();
						newwnd.Show ();
					}else{
						LayerWnds layerwnd = layerWnds [UIBinder.GetLayer (newwnd.wndName)];
						layerwnd.wnds.Remove (newwnd.wndName);

						ExclusionWnds exclutionsWnd = exclusionWnds[UIBinder.GetExclusion (wnd.wndName)];
						exclutionsWnd.wnds.Remove (wnd.wndName);


						openedWnds.Remove (wnd.wndName);
					}


			});


			layerWnds [layer].wnds.Add (wnd.wndName,wnd);

			if (!exclusionWnds.ContainsKey (exclusion)) {
				exclusionWnds.Add (exclusion, new ExclusionWnds ());
			}

			exclusionWnds[exclusion].wnds.Add (wnd.wndName, wnd);


			openedWnds.Add (wnd.wndName, wnd);
			return wnd;

		}

		public void CloseWnd(string wndName){
			if (IsWndOpen (wndName)) {
				CloseAndRemoveWndNow (openedWnds[wndName]);
			}
		}


		public void SyncSortOrder<T>() where T:BaseWnd{
			string wndName = typeof(T).Name;
			SyncSortOrder (wndName);
		}
		public void SyncSortOrder(string wndName){
			if (openedWnds.ContainsKey (wndName)) {
				string layer = UIBinder.GetLayer (wndName);
				layerWnds [layer].Sort (GetLayerIndex(layer));
			}
		}


		public void CloseAllWnd(string layer = null){
			if (layer == null) {
				CloseAllWnd (Enums.GameUILayer.Bottom);
				CloseAllWnd (Enums.GameUILayer.Down);
				CloseAllWnd (Enums.GameUILayer.Default);
				CloseAllWnd (Enums.GameUILayer.Up);
				CloseAllWnd (Enums.GameUILayer.Top);
			} else if(layerWnds.ContainsKey(layer)) {
				CloseAndRemoveLayerWndNow (layerWnds [layer]);
			}else{
				Debug.LogError("CloseAllWnd:Invailed Layer");
			}
		}

		private void CloseAndRemoveLayerWndNow(LayerWnds layer){
			bool found = false;
			foreach  (BaseWnd wnd in layer.wnds.Values){
				found = true;
				CloseAndRemoveWndNow (wnd);
				break;
			}

			if(found){
				CloseAndRemoveLayerWndNow(layer);
			}
		}

		private void CloseAndRemoveWndNow(BaseWnd wnd){
			wnd.Hide ();
			LayerWnds layer = layerWnds [UIBinder.GetLayer (wnd.wndName)];
			layer.wnds.Remove (wnd.wndName);

			ExclusionWnds exclutions = exclusionWnds[UIBinder.GetExclusion (wnd.wndName)];
			exclutions.wnds.Remove (wnd.wndName);


			openedWnds.Remove (wnd.wndName);

			wnd.OnClose ();



			wnd.Dispose ();
		}


		public static string GetWndLayer<T>() where T:BaseWnd{
			return UIManager.Instance.UIBinder.GetLayer<T> ();
		}
			
	}

}	