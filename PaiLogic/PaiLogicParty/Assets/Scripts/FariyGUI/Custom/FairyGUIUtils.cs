using FairyGUI;
using UnityEngine;
using FairyGUI.Utils;
using System.Linq;
using System;
using System.Collections;
using DG.Tweening;

namespace FairyGUIEx
{
    public static class FairyGUIUtils
    {
		#if UNITY_EDITOR
		private const string kSimulateAssetBundles = "SimulateAssetBundles";
		#endif
        public const string FairyguiABParent = "fairygui_";

        ///<summary>
        ///是否从assetbundle加载fairygui资源
        ///</summary>
        public static bool FairyguiLoadAB 
        {
            get
            {
                #if !UNITY_EDITOR
                return true;
                #else

				return UnityEditor.EditorPrefs.GetBool (kSimulateAssetBundles, true) ? false : true;
                #endif			
            }
        }

        public static GObject CreateObject(string pkgName, string resName, GComponent parent)
        {
            AddPackage(pkgName);

            GObject obj = UIPackage.CreateObject(pkgName, resName);
            obj.asCom.fairyBatching = true;
            parent.AddChild(obj);

            return obj;
        }

        public static GObject CreateObject(string pkgName, string resName, GComponent parent, int childIndex)
        {
            GObject obj = CreateObject(pkgName, resName, parent);
            parent.SetChildIndex(obj, childIndex);
            return obj;
        }

        public static void DestroyObject(GObject gObj)
        {
            gObj.Dispose();
        }

        public static UIPackage AddPackage(string pkgName)
        {
            UIPackage pkg = UIPackage.GetByName(pkgName);
            if (pkg == null)
            {
                if (!FairyguiLoadAB)
                {
					#if UNITY_EDITOR
                    //1. try to load from Resources
					string path = "Assets/AssetBundleRes/FairyGUI/FairyGUI_" + pkgName+"/"+pkgName;
                    Debug.Log(">>>> add package from AssetDatabase: " + path);
                    pkg = UIPackage.AddPackageFromAssetDatabase(path);
					#endif
                }
                else
                {
                    //todo: 考虑描述文件、资源文件分别打包
//                    AssetBundle ab = GameUtils.GetLoadedAssetBundle(FairyguiABParent+pkgName);
//                    if (ab == null)
//                        ab = GameUtils.LoadAssetBundle(FairyguiABParent+pkgName);
					AssetBundle ab =PTGame.AssetBundles.PTResourceManager.Instance.LoadAssetBundle(FairyguiABParent+pkgName);
//					PTGame.AssetBundles.PTResourceManager.Instance
                    pkg = UIPackage.AddPackage(ab);
                    //加载完package就理解卸载assetbundle，因为一个assetbundle对应一个package，assetbundle会额外占用内存
//                    GameUtils.UnLoadAssetBundle(FairyguiABParent+pkgName, false);
//					PTGame.AssetBundles.PTResourceManager.Instance.UnloadAssetBundle (FairyguiABParent+pkgName, false);
                }
            }
            return pkg;
        }

        public static void RemovePackage(string pkgName, bool force)
        {
            UIPackage.RemovePackage(pkgName, force);
            if (FairyguiLoadAB)
            {
//                GameUtils.UnLoadAssetBundle(FairyguiABParent+pkgName, force);
				PTGame.AssetBundles.PTResourceManager.Instance.UnloadAssetBundle (FairyguiABParent + pkgName, force);
            }
        }

        ///<summary>
        ///卸载除了exceptPkgNames指定的其他所有packages
        ///</summary>
        public static void RemoveAllPackagesExcept(bool force, params string[] exceptPkgNames)
        {
            UIPackage[] allPkgs = UIPackage.GetPackages().ToArray();
            foreach (UIPackage pkg in allPkgs)
            {
                if (Array.Exists(exceptPkgNames, pName => pName.Equals(pkg.name)))
                    continue;

                FairyGUIUtils.RemovePackage(pkg.name, force);
            }
        }

        ///<summary>
        ///使用自定义扩展的UBBParser
        ///</summary>
        public static void UBBWith(this GTextField gText, UBBParser parser, string text = null)
        {
            gText.UBBEnabled = true;
            UBBParser oldParser = UBBParser.inst;
            UBBParser.inst = parser;
            if (string.IsNullOrEmpty(text))
            {
                //重新set一下，使用新的ubbparser解析
                gText.text = gText.text;
            }
            else
            {
                gText.text = text;
            }
            
            //reset default UBBParser
            UBBParser.inst = oldParser;
        }

//        ///<summary>
//        ///循环旋转，不用tween，因为visible=false时，tween仍然执行
//        ///</summary>
//        public static void LoopRotate(this GObject gObj, float step)
//        {
//            CoroutineHelper.Instance.StartCoroutine(StartLoopRotate(gObj, step));
//        }
//
//        private static System.Collections.IEnumerator StartLoopRotate(GObject gObj, float step)
//        {
//            while(gObj.visible && !gObj.displayObject.isDisposed)
//            {
//                gObj.rotation += step;
//                yield return 0;
//            }
//        }
//
//        ///<summary>
//        ///更新顶部栏标签名
//        ///</summary>
//        public static void UpdateTopLabel(this GComponent topCom, string label)
//        {
//            topCom.GetChild("title").text = label;
//        }
//
//        ///<summary>
//        ///显示弹框
//        ///</summary>
//        /// <param name="title">null，则没有标题</param>
//        /// <param name="content">弹框内容</param>
//        /// <param name="confirm">确认按钮的显示内容</param>
//        /// <param name="cancel">取消按钮的显示内容</param>
//        public static GObject ShowTipsPanel(string title, string content, string confirm, string cancel, EventCallback0 confirmAction, EventCallback0 cancelAction)
//        {
//            string resName = string.IsNullOrEmpty(title) ? "panel_tips_noTitle" : "panel_tips";
//
//            GObject panel = FairyGUIUtils.CreateObject(PkgName.Common, resName, PageMgr.Instance.rootView);
//            panel.size = panel.parent.size;
//            panel.AddRelation(panel.parent, RelationType.Size);
//
//            if (!string.IsNullOrEmpty(title))
//                panel.asCom.GetChild("title").asTextField.text = title;
//            panel.asCom.GetChild("content").asTextField.text = content;
//            
//            GButton cancelBtn = panel.asCom.GetChild("button_cancel").asButton;
//            cancelBtn.GetChild("cancel_btn_text").asTextField.text = cancel;
//            if (cancelAction != null)
//            {
//                cancelBtn.onClick.Add(()=>{
//					panel.Dispose();
//					cancelAction();
//				});
//            }
//            else 
//            {
//                cancelBtn.onClick.Add(()=>panel.Dispose());
//            }
//
//            GButton confirmBtn = panel.asCom.GetChild("button_confirm").asButton;
//            confirmBtn.GetChild("confirm_btn_text").asTextField.text = confirm;
//            if (confirmAction != null)
//            {
//                confirmBtn.onClick.Add(()=>{
//					panel.Dispose();
//					confirmAction();
//				});
//            }
//            else
//            {
//                confirmBtn.onClick.Add(()=>panel.Dispose());
//            }
//
//            return panel;
//        }
//
//
//		private static GObject _openedConfirmTipsPanel;
//		public static GObject ShowConfirmTipsPanel(string title, string content, EventCallback0 confirmAction = null)
//		{
//			if (null != _openedConfirmTipsPanel)
//			{
//				_openedConfirmTipsPanel.Dispose();
//			}
//
//			const string resName = "panel_confirm_tips";
//
//			GObject panel = FairyGUIUtils.CreateObject(PkgName.Common, resName, PageMgr.Instance.rootView);
//			panel.size = panel.parent.size;
//			panel.AddRelation(panel.parent, RelationType.Size);
//
//			if (string.IsNullOrEmpty(title))
//			{
//				panel.asCom.GetChild("content").asTextField.text = content;
//
//				panel.asCom.GetChild("content2").visible = false;
//				panel.asCom.GetChild("title").visible = false;
//			}
//			else
//			{
//				panel.asCom.GetChild("content").visible = false;
//
//				panel.asCom.GetChild("content2").asTextField.text = content;
//				panel.asCom.GetChild("title").asTextField.text = title;
//			}
//
//			GButton confirmBtn = panel.asCom.GetChild("button_confirm").asButton;
//
//			if (confirmAction != null)
//			{
//				confirmBtn.onClick.Add(()=>{
//					confirmAction();
//					panel.Dispose();
//				});
//			}
//			else		
//			{
//				confirmBtn.onClick.Add (panel.Dispose);
//			}
//
//			_openedConfirmTipsPanel = panel;
//
//			_openedConfirmTipsPanel.sortingOrder = GObjectSortingOrder.Tips;
//
//			return panel;
//		}
//
//		public static GObject ShowConfirmTipsPanel(string content, EventCallback0 confirmAction = null)
//		{
//			return ShowConfirmTipsPanel(null, content, confirmAction);
//		}
//
//        ///<summary>
//        ///显示转菊花弹框
//        ///</summary>
//        public static GObject ShowWaitingPanel(string content)
//        {
//            string resName = string.IsNullOrEmpty(content) ? "panel_waiting_noTitle" : "panel_waiting";
//            
//            GObject panel = FairyGUIUtils.CreateObject(PkgName.Common, resName, PageMgr.Instance.rootView);
//            panel.size = panel.parent.size;
//            panel.AddRelation(panel.parent, RelationType.Size);
//
//            if (!string.IsNullOrEmpty(content))
//                panel.asCom.GetChild("text").asTextField.text = content;
//
//            GObject flower = panel.asCom.GetChild("img_waiting");
//            flower.SetPivot(0.5f, 0.5f);
//            flower.LoopRotate(4);
//
//            return panel;
//        }
//
//		public static GObject ShowToastTips(string content)
//		{
//			string resName = "toast_tips";
//
//			GObject panel = FairyGUIUtils.CreateObject (PkgName.Common, resName, PageMgr.Instance.rootView);
//			panel.size = panel.parent.size;
//			panel.AddRelation(panel.parent, RelationType.Size);
//
//			//GObject toast_bg = panel.asCom.GetChild ("toast_bg");
//			GTextField toast_text = panel.asCom.GetChild ("toast_text").asTextField;
//
//			if (content != null && !string.IsNullOrEmpty(content))
//				toast_text.text = content;
//			
//			return panel;
//		}
//
//		public static void ShowToast(string content)
//		{
//			GObject panel = FairyGUIUtils.CreateObject (PkgName.Common, "Toaster", PageMgr.Instance.rootView);
//
//            panel.sortingOrder = GObjectSortingOrder.Toast;
//
//			panel.size = panel.parent.size;
//			panel.AddRelation(panel.parent, RelationType.Size);
//
//			GComponent component = panel.asCom;
//
//			GTextField textField = component.GetChild("content").asTextField;
//
//			textField.text = content;
//
//			component.GetChild("background").width = textField.actualWidth + 10;
//
//			component.y = 0;
//			component.x = 0;
//
//			component.scale = new Vector2(0.5f, 0.5f);
//
//			component.TweenScale(new Vector2(1.0f, 1.0f), 0.1f).OnComplete(
//				() => CoroutineHelper.Instance.StartCoroutine (FadeToast (component))).SetEase(Ease.OutBack);
//		}
//
//		private static IEnumerator FadeToast(GComponent component)
//		{
//			yield return new WaitForSeconds(1);
//
//			component.TweenFade (0, 0.5f).OnComplete (component.Dispose);
//		}
    }
}