using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Basics{
	public class WndSkin{
		string skinName;
		string assetBundleName;
		string assetName;

	}
	/**
	 * 面板打开流程
	 * OnCreate 面板创建
	 * OnOpen 面板打开
	 * OnShow 面板展示
	 * OnHide 面板隐藏
	 * OnClose 面板关闭
	 * OnDispose 面板移除
	 */
	public class BaseWnd {

		public string wndName;
		private bool _visible = false;
		private bool _destroyed = false;

		public BaseWnd(System.Type WndType){
			wndName = WndType.Name;
		}







		GameObject tmpGo;
		object[] openData;
		public void Create(object[] data , string assetbundleName,string assetName,System.Action<bool,BaseWnd> created){
			openData = data; 
			PTGame.AssetBundles.PTResourceManager.Instance.LoadAssetAsync<GameObject> (assetbundleName, assetName, (bool success, GameObject go) => {
				if (success) {
					tmpGo = GameObject.Instantiate(go);
					tmpGo.name = wndName;

//					
//					panel.SetSortingOrder(Framework.Managers.UIManager.Instance.GetLayerIndex(this),true);
					if(SortOrder == 0)
						SortOrder = Framework.Managers.UIManager.Instance.GetLayerIndex(this) + Framework.Managers.UIManager.Instance.GetLayerCount(this);
					else
						SortOrder = SortOrder;
					BindRealWnd();

					tmpGo.transform.SetParent (Framework.Managers.UIManager.Instance.GetLayerAvatar (this).transform);
					SyncVisable();
					SyncDispose();
					OnCreate (()=>{
						created (true, this);
					});

				} else {
					created (false, null);
				}
			});
		}
			


		public GameObject gameObject{
			get{ 
				return tmpGo;
			}
		}


		private int _sortOrder = 0;
		public int SortOrder{
			get{
				return _sortOrder;
			}
			set{
				_sortOrder = value;
				SyncSortOrder ();
			}
		}

		public virtual void SwitchToLayerTop(){
			_sortOrder = int.MaxValue;
		}



		public virtual void SyncSortOrder(){

		}


		public virtual void BindRealWnd(){

		}


		public virtual void SetDragAble(bool able){

		}

		public virtual void SetFrontAble(bool able){

		}





		public void Dispose(){

			_destroyed = true;
			SyncDispose ();
			OnDispose ();
		}


		public void Show(){
			_visible = true;
			SyncVisable ();
			OnShow ();
		}


		public void Hide(){
			_visible = false;
			SyncVisable ();
			OnHide ();
		}

		public virtual void OnCreate(System.Action oncreated){
			oncreated ();
		}

		public virtual void OnOpen(){

		}

		public virtual void OnShow(){

		}


		public virtual void OnHide(){
			
		}



		public virtual void OnClose(){

		}



		public virtual void OnDispose(){

		}





		public void ChangeTheme(string ThemeName){
			
		}



		private void SyncVisable(){
			if(tmpGo!= null)
				tmpGo.SetActive (_visible);
		}


		private void SyncDispose(){
			if (tmpGo != null && _destroyed) {
				GameObject.Destroy (tmpGo);
				tmpGo = null;
			}
		}

		public bool readyDestroyed{
			get{ 
				return _destroyed;
			}
		}
			



		public virtual void OnRecvMsg(string MessageCode , params object[] aParams){
			Debug.Log ("OnRecvMsg Wnd: " + wndName + " Message: " + MessageCode + " ");
		}

		public void SendMsg<T>(string MessageCode , params object[] aParams) where T:BaseWnd{
			if(Framework.Managers.UIManager.Instance.IsWndOpen<T>())
				Framework.Managers.UIManager.Instance.GetWnd<T> ().OnRecvMsg (MessageCode,aParams);
		}


		public void SendMsg(string MessageCode , params object[] aParams){
			Framework.Managers.UIManager.Instance.SendMsg (MessageCode, aParams);
		}
	}




}