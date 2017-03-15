using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Enums{
	public class WndAssetNames {
		public const string MainWnd = "UIMainWnd";
	public const string GameWnd = "UIGameWnd";
	public const string LoginWnd = "UILoginWnd";
	}


	public class UIExclusion{
		public const int NoExclusion = -1;
		public const int Default = 0;
		public const int Window = 1000;
		public const int MessageBox = 2000;
		public const int GameBackground = 3000;
	}

}