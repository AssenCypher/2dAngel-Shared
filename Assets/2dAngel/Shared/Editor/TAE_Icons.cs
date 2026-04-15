#if UNITY_EDITOR
using UnityEngine;

namespace TwoDAngel.Shared.Editor
{
    // Transitional compatibility wrapper.
    // Keep this while AP/APT/ALT still reference TAE_* names.
    public static class TAE_Icons
    {
        public static GUIContent Overview => TwoDA_Icons.Overview;
        public static GUIContent Native => TwoDA_Icons.Native;
        public static GUIContent Reflection => TwoDA_Icons.Reflection;
        public static GUIContent Bakery => TwoDA_Icons.Bakery;
        public static GUIContent Volumes => TwoDA_Icons.Volumes;
        public static GUIContent Audit => TwoDA_Icons.Audit;
        public static GUIContent About => TwoDA_Icons.About;
        public static GUIContent Visualize => TwoDA_Icons.Visualize;
        public static GUIContent EditBounds => TwoDA_Icons.EditBounds;
        public static GUIContent Open => TwoDA_Icons.Open;
        public static GUIContent Scan => TwoDA_Icons.Scan;
        public static GUIContent Refresh => TwoDA_Icons.Refresh;
        public static GUIContent Package => TwoDA_Icons.Package;
        public static GUIContent Warning => TwoDA_Icons.Warning;
        public static GUIContent Success => TwoDA_Icons.Success;
        public static GUIContent Missing => TwoDA_Icons.Missing;
        public static GUIContent Convert => TwoDA_Icons.Convert;
        public static GUIContent Safety => TwoDA_Icons.Safety;
        public static GUIContent History => TwoDA_Icons.History;
        public static GUIContent Revert => TwoDA_Icons.Revert;
        public static GUIContent Redo => TwoDA_Icons.Redo;
        public static GUIContent MakeScaled(string fallbackText, int size, params string[] iconNames) => TwoDA_Icons.MakeScaled(fallbackText, size, iconNames);
        public static GUIContent Make(string fallbackText, params string[] iconNames) => TwoDA_Icons.Make(fallbackText, iconNames);
    }
}
#endif
