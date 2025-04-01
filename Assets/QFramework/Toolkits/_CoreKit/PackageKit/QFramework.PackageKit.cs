/****************************************************************************
 * Copyright (c) 2016 ~ 2022 liangxiegame UNDER MIT LICENSE
 * 
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
    public static class FrameworkMenuItems
    {
        public const string Preferences = "QFramework/Preferences... %e";
    }

    public static class FrameworkMenuItemsPriorities
    {
        public const int Preferences = 1;
    }
    

    public class SubWindow : EditorWindow, IMGUILayout
    {
        public string Id { get; set; }
        public bool Visible { get; set; }

        public Func<bool> VisibleCondition { get; set; }

        void IMGUIView.DrawGUI()
        {
        }

        IMGUILayout IMGUIView.Parent { get; set; }

        private FluentGUIStyle mStyle = new FluentGUIStyle(() => new GUIStyle());

        public FluentGUIStyle Style
        {
            get { return mStyle; }
            set { mStyle = value; }
        }

        Color IMGUIView.BackgroundColor { get; set; }


        private List<IMGUIView> mPrivateChildren = new List<IMGUIView>();

        private List<IMGUIView> mChildren
        {
            get { return mPrivateChildren; }
            set { mPrivateChildren = value; }
        }

        void IMGUIView.RefreshNextFrame()
        {
        }

        void IMGUIView.AddLayoutOption(GUILayoutOption option)
        {
        }

        void IMGUIView.RemoveFromParent()
        {
        }

        void IMGUIView.Refresh()
        {
        }

        public void Hide()
        {
            throw new NotImplementedException();
        }

        public IMGUILayout AddChild(IMGUIView view)
        {
            mChildren.Add(view);
            view.Parent = this;
            return this;
        }

        public void RemoveChild(IMGUIView view)
        {
            mChildren.Add(view);
            view.Parent = null;
        }

        public void Clear()
        {
            mChildren.Clear();
        }

        private void OnGUI()
        {
            mChildren.ForEach(view => view.DrawGUI());
        }

        public void Dispose()
        {
        }
    }

    public abstract class Window : EditorWindow, IDisposable
    {
        public IMGUIViewController ViewController { get; set; }


        private void OnGUI()
        {
            if (ViewController != null)
            {
                ViewController.View.DrawGUI();
            }

            RenderEndCommandExecutor.ExecuteCommand();
        }

        public void Dispose()
        {
            OnDispose();
        }


        protected abstract void OnDispose();
    }


    public static class MouseSelector
    {
        public static string GetSelectedPathOrFallback()
        {
            var path = string.Empty;

            foreach (var obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                }
            }

            return path;
        }
    }


    internal class ColorView : IMGUIAbstractView
    {
        public ColorView(Color color)
        {
            Color = new BindableProperty<Color>(color);
        }

        public BindableProperty<Color> Color { get; private set; }

        protected override void OnGUI()
        {
            Color.Value = EditorGUILayout.ColorField(Color.Value, LayoutStyles);
        }
    }


    internal class EnumPopupView : IMGUIAbstractView
    {
        public BindableProperty<Enum> ValueProperty { get; set; }

        public EnumPopupView(Enum initValue)
        {
            ValueProperty = new BindableProperty<Enum>(initValue);
            ValueProperty.Value = initValue;
            Style = new FluentGUIStyle(() => EditorStyles.popup);
        }

        protected override void OnGUI()
        {
            Enum enumType = ValueProperty.Value;
            ValueProperty.Value = EditorGUILayout.EnumPopup(enumType, Style.Value, LayoutStyles);
        }
    }


    public abstract class IMGUIViewController
    {
        public VerticalLayout View = new VerticalLayout();

        public abstract void SetUpView();
    }
}
#endif
