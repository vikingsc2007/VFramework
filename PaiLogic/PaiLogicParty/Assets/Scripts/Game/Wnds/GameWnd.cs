using System;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

namespace Game.Wnds{
	public class GameWnd : Game.Basics.BaseWnd
	{
		public GameWnd ():base(typeof(GameWnd )){
		}

		public override void OnOpen (){
			base.OnOpen ();
			SetFrontAble (true);
			SetDragAble (true);
			ani_index = 1;
			FairyGUI.GButton btn = Wnd.GetChild ("n4").asButton;
			btn.onClick.Add (()=>{
				
				ani_index = (ani_index >= mvc.armature.animation.animationNames.Count ? 1 :ani_index+1);
				mvc.armature.animation.Play(mvc.armature.animation.animationNames[ani_index-1]);

//				Framework.Managers.UIManager.Instance.CloseWnd<GameWnd>();
				//				SwitchToLayerTop();
			});
		}
		DragonBones.UnityArmatureComponent mvc;
		int ani_index = 0;
		public override void OnCreate (Action oncreated)
		{
			//			base.OnCreate (action);
			PTGame.AssetBundles.PTResourceManager.Instance.LoadAssetAsync<GameObject> ("DB_Example","ubbie",(bool arg1, GameObject arg2) => {
				if(arg1){
					GameObject go = GameObject.Instantiate(arg2);
					mvc = go.GetComponent<DragonBones.UnityArmatureComponent>();

					GoWrapper warpper = new GoWrapper(go);
					Wnd.GetChild("n1").asGraph.SetNativeObject(warpper);
					oncreated();
				}

			});
		}


		public override void OnRecvMsg (string MessageCode, params object[] aParams)
		{
			base.OnRecvMsg (MessageCode, aParams);
		}
	}
}
