using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Game.Wnds;
using Game.Enums;
using Framework.Enums;
namespace Game.Bind{
	
	public class GameWndBind : WndBinder {
		public override void RegisterAll (){
			Register<MainWnd> (UIExclusion.MessageBox,GameUILayer.Default);
			Register<GameWnd> (UIExclusion.Window,GameUILayer.Default);
			Register<LoginWnd> (UIExclusion.NoExclusion,GameUILayer.Bottom);

		}


		public override void RegisterSkins(){
			AddSkin<MainWnd>(ThemesName.Default,ThemesBundleName.Default,WndAssetNames.MainWnd);
			AddSkin<GameWnd>(ThemesName.Default,ThemesBundleName.Default,WndAssetNames.GameWnd);
			AddSkin<LoginWnd>(ThemesName.Default,ThemesBundleName.Default,WndAssetNames.LoginWnd);

		}
	}
}