using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Basics{
	public class BaseWnd :Framework.Basics.BaseWnd {
		public BaseWnd(System.Type WndType):base(WndType){
		
		}

		private FairyGUI.GComponent _wnd;

		public FairyGUI.GComponent Wnd{
			get{ 
				return _wnd;
			}
		}


		public override void SyncSortOrder ()
		{
			base.SyncSortOrder ();
			if (gameObject != null) {
				FairyGUI.UIPanel panel = gameObject.GetComponent<FairyGUI.UIPanel> ();
				if (panel != null) {
					panel.SetSortingOrder (SortOrder, true);
				}
			}
		}

		public override void SwitchToLayerTop ()
		{
			base.SwitchToLayerTop ();
			Framework.Managers.UIManager.Instance.SyncSortOrder (wndName);
		}

		public override void BindRealWnd(){
			base.BindRealWnd ();

			FairyGUI.UIPanel panel = gameObject.GetComponent<FairyGUI.UIPanel>();
			//			panel.container = FairyGUI.Stage.inst;
			FairyGUI.Stage.inst.AddChild(panel.container);
			_wnd = panel.ui;


		}


		public virtual void SetDragAble(bool able){
			_wnd.draggable = able;
		}



		private bool _setedfrontable = false;
		public virtual void SetFrontAble(bool able){
			if (able) {
				_wnd.onTouchBegin.Add (SwitchToLayerTop);
				_setedfrontable = true;
			} 
			else if(_setedfrontable) {
				_wnd.onTouchBegin.Remove (SwitchToLayerTop);
			}
		}










	}
}
