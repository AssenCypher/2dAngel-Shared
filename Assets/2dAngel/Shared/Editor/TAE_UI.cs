#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace TwoDAngel.Shared.Editor
{
    // Transitional compatibility wrapper.
    // Keep this while AP/APT/ALT still reference TAE_* names.
    public enum TAE_NavVisual
    {
        Sidebar = TwoDA_NavVisual.Sidebar,
        Tab = TwoDA_NavVisual.Tab
    }

    public static class TAE_UI
    {
        public const float SidebarWidth = TwoDA_UI.SidebarWidth;
        public const float SidebarButtonHeight = TwoDA_UI.SidebarButtonHeight;
        public const float TabButtonHeight = TwoDA_UI.TabButtonHeight;
        public const float CompactBreakpoint = TwoDA_UI.CompactBreakpoint;
        public const float UltraCompactBreakpoint = TwoDA_UI.UltraCompactBreakpoint;

        public enum State
        {
            Neutral = TwoDA_UI.State.Neutral,
            Ready = TwoDA_UI.State.Ready,
            Warning = TwoDA_UI.State.Warning,
            Missing = TwoDA_UI.State.Missing
        }

        public readonly ref struct ResponsiveRow
        {
            private readonly TwoDA_UI.ResponsiveRow row;
            public ResponsiveRow(TwoDA_UI.ResponsiveRow row) { this.row = row; }
            public ResponsiveRow Gap(float gap) => new ResponsiveRow(row.Gap(gap));
            public bool Button(GUIContent content, params GUILayoutOption[] options) => row.Button(content, options);
            public bool Button(string label, params GUILayoutOption[] options) => row.Button(label, options);
            public void Dispose() => row.Dispose();
        }

        public static bool UseSidebar(float width) => TwoDA_UI.UseSidebar(width);
        public static bool UseIconOnlyNav(float width) => TwoDA_UI.UseIconOnlyNav(width);
        public static void Header(string title, string subtitle, string version, Action drawRightSide) => TwoDA_UI.Header(title, subtitle, version, drawRightSide);
        public static bool NavButton(bool selected, GUIContent icon, string label, string tooltip, bool iconOnly, float height = 34f, float width = 0f, TAE_NavVisual visual = TAE_NavVisual.Sidebar)
            => TwoDA_UI.NavButton(selected, icon, label, tooltip, iconOnly, height, width, visual == TAE_NavVisual.Tab ? TwoDA_NavVisual.Tab : TwoDA_NavVisual.Sidebar);
        public static void Card(string title, string subtitle, Action body) => TwoDA_UI.Card(title, subtitle, body);
        public static void DrawBadge(string text, State state, GUILayoutOption width = null) => TwoDA_UI.DrawBadge(text, (TwoDA_UI.State)state, width);
        public static void DrawMiniStat(string label, string value) => TwoDA_UI.DrawMiniStat(label, value);
        public static void DrawKeyValue(string key, string value, float keyWidth = 168f) => TwoDA_UI.DrawKeyValue(key, value, keyWidth);
        public static void Divider(float top = 6f, float bottom = 6f) => TwoDA_UI.Divider(top, bottom);
        public static ResponsiveRow Row(float width) => new ResponsiveRow(TwoDA_UI.Row(width));
    }
}
#endif
