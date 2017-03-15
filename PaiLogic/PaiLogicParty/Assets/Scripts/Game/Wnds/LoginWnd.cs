using System;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

namespace Game.Wnds{
	public class LoginWnd : Game.Basics.BaseWnd
	{
		public FairyGUI.GButton btn;



		public LoginWnd ():base(typeof(LoginWnd )){
		}

		public override void OnOpen (){
			base.OnOpen ();

			btn = Wnd.GetChild ("n4").asButton;
			btn.onClick.Add (()=>{
				if(!Framework.Managers.UIManager.Instance.IsWndOpen<MainWnd>())
					Framework.Managers.UIManager.Instance.OpenWnd<MainWnd>(new object[]{1});
//				SwitchToLayerTop();
			});
				







		}


		public override void OnCreate (Action oncreated)
		{
//			base.OnCreate (action);

			PTGame.AssetBundles.PTResourceManager.Instance.LoadAssetAsync<GameObject> ("GAF_Example","demo2",(bool arg1, GameObject arg2) => {
				if(arg1){
					GameObject go = GameObject.Instantiate(arg2);
					//						GAF.Core.GAFMovieClip mvc = go.GetComponent<GAF.Core.GAFMovieClip>();
					//						mvc.setSequence("stand_left",true);
					GoWrapper warpper = new GoWrapper(go);
					Wnd.GetChild("n1").asGraph.SetNativeObject(warpper);
					OnLoadDemoSuccess(oncreated);
				}

			});



		}





		private void OnLoadDemoSuccess(Action oncreated){
			PTGame.AssetBundles.PTResourceManager.Instance.LoadAssetAsync<GameObject> ("DB_Example","Swordsman",(bool arg1, GameObject arg2) => {
				if(arg1){
					GameObject go = GameObject.Instantiate(arg2);
					//						GAF.Core.GAFMovieClip mvc = go.GetComponent<GAF.Core.GAFMovieClip>();
					//						mvc.setSequence("stand_left",true);
					GoWrapper warpper = new GoWrapper(go);
					Wnd.GetChild("n6").asGraph.SetNativeObject(warpper);
					oncreated();
				}

			});
		}



			
	}




}
