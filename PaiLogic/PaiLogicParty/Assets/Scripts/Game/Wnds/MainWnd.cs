using System;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

namespace Game.Wnds{
	public class MainWnd : Game.Basics.BaseWnd
	{
		public MainWnd ():base(typeof(MainWnd )){
		}

		public override void OnOpen (){
			base.OnOpen ();
			SetFrontAble (true);
			SetDragAble (true);
			FairyGUI.GButton btn = Wnd.GetChild ("n0").asButton;

			btn.onClick.Add (()=>{
				Framework.Managers.UIManager.Instance.OpenWnd<GameWnd>();

				SendMsg<GameWnd>("AAA");
				SendMsg("BBB");
			});


			FairyGUI.GButton btn2 = Wnd.GetChild ("n2").asButton;
			btn2.onClick.Add (()=>{
				Framework.Managers.UIManager.Instance.CloseWnd<MainWnd>();
				//				SwitchToLayerTop();
			});



		}


	}
}
