using UnityEngine;

namespace QFramework
{
    public class FrameworkModule : ConsoleModule
    {
        public override string Title { get; set; } = "Framework";

        public override void DrawGUI()
        {
            base.DrawGUI();

            if (GUILayout.Button("Clear All Data & Quit"))
            {
                Quit();
            }
        }
        
        public static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit ();
#endif
        }
    }
}