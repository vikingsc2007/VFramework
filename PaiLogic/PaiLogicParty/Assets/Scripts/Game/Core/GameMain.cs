using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework.Managers;
using Game.Wnds;
namespace Game.Core{
	public class GameMain : Framework.Basics.BaseMain {
		
		public override void BindGameUIBinder ()
		{
			Framework.Managers.UIManager.Instance.Bind<Game.Bind.GameWndBind> ();
			PerparFairyGUI();



	
//			Invoke ("DelayOpen", 3);


			DelayOpen ();
		}

		private void PerparFairyGUI(){
			FairyGUI.StageCamera.CheckMainCamera ();
			Framework.Managers.UIManager.Instance.uiRoot = FairyGUI.GRoot.inst.container.gameObject;
		}

		public void DelayOpen(){
			UIManager.Instance.OpenWnd<LoginWnd> ();
		}


		/**
		 * 加载自定义管理器
		 * （此时系统核心管理器已初始化完毕）
		 */
		public override void InitGameManager ()
		{
			
		}


	}
}