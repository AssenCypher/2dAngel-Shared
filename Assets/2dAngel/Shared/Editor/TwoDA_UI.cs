#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace TwoDAngel.Shared.Editor
{
    public enum TwoDA_NavVisual
    {
        Sidebar = 0,
        Tab = 1
    }

    public static class TwoDA_UI
    {
        public const float SidebarWidth = 132f;
        public const float SidebarButtonHeight = 34f;
        public const float TabButtonHeight = 27f;
        public const float CompactBreakpoint = 720f;
        public const float UltraCompactBreakpoint = 520f;

        private static GUIStyle titleStyle;
        private static GUIStyle subtitleStyle;
        private static GUIStyle navButtonStyle;
        private static GUIStyle navButtonActiveStyle;
        private static GUIStyle tabButtonStyle;
        private static GUIStyle tabButtonActiveStyle;
        private static GUIStyle cardTitleStyle;
        private static GUIStyle cardSubtitleStyle;
        private static GUIStyle valueStyle;
        private static GUIStyle labelStyle;
        private static GUIStyle badgeStyle;
        private static GUIStyle centerMiniStyle;

        public static bool UseSidebar(float width)
        {
            return width >= CompactBreakpoint;
        }

        public static bool UseIconOnlyNav(float width)
        {
            return width < UltraCompactBreakpoint;
        }

        public static void Header(string title, string subtitle, string version, Action drawRightSide)
        {
            EnsureStyles();
            using (new EditorGUILayout.VerticalScope("box"))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    using (new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Label(title ?? string.Empty, titleStyle);
                        if (!string.IsNullOrWhiteSpace(subtitle))
                        {
                            GUILayout.Space(2f);
                            GUILayout.Label(subtitle, subtitleStyle);
                        }
                    }

                    GUILayout.FlexibleSpace();

                    using (new EditorGUILayout.VerticalScope(GUILayout.Width(196f)))
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            GUILayout.FlexibleSpace();
                            DrawBadge("v" + version, State.Neutral);
                        }

                        GUILayout.Space(2f);
                        drawRightSide?.Invoke();
                    }
                }
            }
        }

        public static bool NavButton(bool selected, GUIContent icon, string label, string tooltip, bool iconOnly, float height = 34f, float width = 0f, TwoDA_NavVisual visual = TwoDA_NavVisual.Sidebar)
        {
            EnsureStyles();
            GUIContent content = iconOnly
                ? new GUIContent(icon != null ? icon.image : null, tooltip)
                : new GUIContent(label ?? string.Empty, icon != null ? icon.image : null, tooltip);

            GUIStyle style = GetNavStyle(selected, visual);
            Vector2 previousIconSize = EditorGUIUtility.GetIconSize();
            EditorGUIUtility.SetIconSize(visual == TwoDA_NavVisual.Sidebar ? new Vector2(14f, 14f) : new Vector2(13f, 13f));
            try
            {
                if (width > 0f)
                {
                    return GUILayout.Button(content, style, GUILayout.Height(height), GUILayout.Width(width));
                }

                return GUILayout.Button(content, style, GUILayout.Height(height), GUILayout.ExpandWidth(true));
            }
            finally
            {
                EditorGUIUtility.SetIconSize(previousIconSize);
            }
        }

        public static void Card(string title, string subtitle, Action body)
        {
            EnsureStyles();
            using (new EditorGUILayout.VerticalScope("box"))
            {
                if (!string.IsNullOrWhiteSpace(title))
                {
                    GUILayout.Label(title, cardTitleStyle);
                }

                if (!string.IsNullOrWhiteSpace(subtitle))
                {
                    GUILayout.Space(2f);
                    GUILayout.Label(subtitle, cardSubtitleStyle);
                }

                if (!string.IsNullOrWhiteSpace(title) || !string.IsNullOrWhiteSpace(subtitle))
                {
                    GUILayout.Space(6f);
                }

                body?.Invoke();
            }
        }

        public static void DrawBadge(string text, State state, GUILayoutOption width = null)
        {
            EnsureStyles();
            GUIContent gc = new GUIContent(text ?? string.Empty);
            float calculatedWidth = Mathf.Max(42f, badgeStyle.CalcSize(gc).x + 14f);
            Rect rect = width == null
                ? GUILayoutUtility.GetRect(calculatedWidth, 20f, GUILayout.ExpandWidth(false))
                : GUILayoutUtility.GetRect(calculatedWidth, 20f, width, GUILayout.Height(20f));
            EditorGUI.DrawRect(rect, GetStateColor(state));
            GUI.Label(rect, gc, badgeStyle);
        }

        public static void DrawMiniStat(string label, string value)
        {
            EnsureStyles();
            using (new EditorGUILayout.VerticalScope("box", GUILayout.MinHeight(52f)))
            {
                GUILayout.FlexibleSpace();
                GUILayout.Label(value ?? string.Empty, valueStyle);
                GUILayout.Space(1f);
                GUILayout.Label(label ?? string.Empty, centerMiniStyle);
                GUILayout.FlexibleSpace();
            }
        }

        public static void DrawKeyValue(string key, string value, float keyWidth = 168f)
        {
            EnsureStyles();
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Label(key ?? string.Empty, labelStyle, GUILayout.Width(keyWidth));
                EditorGUILayout.SelectableLabel(value ?? string.Empty, EditorStyles.label, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            }
        }

        public static void Divider(float top = 6f, float bottom = 6f)
        {
            GUILayout.Space(top);
            Rect rect = EditorGUILayout.GetControlRect(false, 1f);
            EditorGUI.DrawRect(rect, EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.08f) : new Color(0f, 0f, 0f, 0.12f));
            GUILayout.Space(bottom);
        }

        public static ResponsiveRow Row(float width)
        {
            return new ResponsiveRow(width);
        }

        private static GUIStyle GetNavStyle(bool selected, TwoDA_NavVisual visual)
        {
            if (visual == TwoDA_NavVisual.Tab)
            {
                return selected ? tabButtonActiveStyle : tabButtonStyle;
            }

            return selected ? navButtonActiveStyle : navButtonStyle;
        }

        private static void EnsureStyles()
        {
            if (titleStyle == null)
            {
                titleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 15,
                    wordWrap = true
                };
            }

            if (subtitleStyle == null)
            {
                subtitleStyle = new GUIStyle(EditorStyles.miniLabel)
                {
                    wordWrap = true,
                    richText = true
                };
            }

            if (navButtonStyle == null)
            {
                navButtonStyle = new GUIStyle(EditorStyles.miniButton)
                {
                    alignment = TextAnchor.MiddleLeft,
                    fixedHeight = SidebarButtonHeight,
                    padding = new RectOffset(13, 11, 6, 6),
                    margin = new RectOffset(4, 4, 3, 3),
                    fontStyle = FontStyle.Bold,
                    imagePosition = ImagePosition.ImageLeft,
                    contentOffset = new Vector2(0f, -0.5f)
                };
            }

            if (navButtonActiveStyle == null)
            {
                navButtonActiveStyle = new GUIStyle(navButtonStyle);
                navButtonActiveStyle.normal = new GUIStyleState
                {
                    background = MakeSolidTexture(EditorGUIUtility.isProSkin ? new Color(0.20f, 0.24f, 0.30f, 1f) : new Color(0.84f, 0.88f, 0.95f, 1f)),
                    textColor = EditorGUIUtility.isProSkin ? new Color(0.96f, 0.98f, 1f, 1f) : new Color(0.10f, 0.14f, 0.20f, 1f)
                };
                navButtonActiveStyle.hover = navButtonActiveStyle.normal;
                navButtonActiveStyle.active = navButtonActiveStyle.normal;
            }

            if (tabButtonStyle == null)
            {
                tabButtonStyle = new GUIStyle(EditorStyles.toolbarButton)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fixedHeight = TabButtonHeight,
                    padding = new RectOffset(10, 10, 5, 4),
                    fontStyle = FontStyle.Bold,
                    imagePosition = ImagePosition.ImageLeft
                };
            }

            if (tabButtonActiveStyle == null)
            {
                tabButtonActiveStyle = new GUIStyle(tabButtonStyle);
                tabButtonActiveStyle.normal = new GUIStyleState
                {
                    background = MakeSolidTexture(EditorGUIUtility.isProSkin ? new Color(0.22f, 0.28f, 0.36f, 1f) : new Color(0.82f, 0.87f, 0.95f, 1f)),
                    textColor = EditorGUIUtility.isProSkin ? new Color(0.96f, 0.98f, 1f, 1f) : new Color(0.10f, 0.14f, 0.20f, 1f)
                };
                tabButtonActiveStyle.hover = tabButtonActiveStyle.normal;
                tabButtonActiveStyle.active = tabButtonActiveStyle.normal;
            }

            if (cardTitleStyle == null)
            {
                cardTitleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 12,
                    wordWrap = true
                };
            }

            if (cardSubtitleStyle == null)
            {
                cardSubtitleStyle = new GUIStyle(EditorStyles.wordWrappedMiniLabel)
                {
                    richText = true
                };
            }

            if (valueStyle == null)
            {
                valueStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 18
                };
            }

            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(EditorStyles.label)
                {
                    richText = false
                };
            }

            if (badgeStyle == null)
            {
                badgeStyle = new GUIStyle(EditorStyles.miniButtonMid)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Bold,
                    padding = new RectOffset(8, 8, 3, 3),
                    stretchWidth = false
                };
            }

            if (centerMiniStyle == null)
            {
                centerMiniStyle = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
                {
                    wordWrap = true
                };
            }
        }

        private static Texture2D MakeSolidTexture(Color color)
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.hideFlags = HideFlags.HideAndDontSave;
            tex.SetPixel(0, 0, color);
            tex.Apply();
            return tex;
        }

        private static Color GetStateColor(State state)
        {
            switch (state)
            {
                case State.Ready:
                    return new Color(0.24f, 0.54f, 0.36f, 0.95f);
                case State.Warning:
                    return new Color(0.88f, 0.64f, 0.16f, 0.95f);
                case State.Missing:
                    return new Color(0.74f, 0.24f, 0.24f, 0.95f);
                default:
                    return new Color(0.34f, 0.40f, 0.46f, 0.95f);
            }
        }

        public enum State
        {
            Neutral,
            Ready,
            Warning,
            Missing
        }

        public sealed class ResponsiveRow : IDisposable
        {
            private readonly float contentWidth;
            private readonly GUIStyle buttonStyle;
            private readonly GUIStyle miniButtonStyle;
            private readonly GUIStyle toolbarButtonStyle;
            private float cursorX;
            private float gap = 6f;
            private float pad = 2f;
            private bool rowOpen;

            internal ResponsiveRow(float contentWidth)
            {
                this.contentWidth = Mathf.Max(180f, contentWidth);
                buttonStyle = new GUIStyle(GUI.skin.button);
                miniButtonStyle = new GUIStyle(EditorStyles.miniButton);
                toolbarButtonStyle = new GUIStyle(EditorStyles.toolbarButton);
                BeginRow();
            }

            public ResponsiveRow Gap(float value)
            {
                gap = Mathf.Max(0f, value);
                return this;
            }

            private void BeginRow()
            {
                if (rowOpen)
                {
                    return;
                }

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(pad);
                cursorX = pad;
                rowOpen = true;
            }

            private void EndRowInternal()
            {
                if (!rowOpen)
                {
                    return;
                }

                GUILayout.FlexibleSpace();
                GUILayout.Space(pad);
                EditorGUILayout.EndHorizontal();
                rowOpen = false;
            }

            public void NewRow()
            {
                EndRowInternal();
                BeginRow();
            }

            public bool Button(string text)
            {
                return Button(new GUIContent(text), buttonStyle);
            }

            public bool Button(GUIContent content)
            {
                return Button(content, buttonStyle);
            }

            public bool MiniButton(string text)
            {
                return Button(new GUIContent(text), miniButtonStyle);
            }

            public bool ToolbarButton(GUIContent content)
            {
                return Button(content, toolbarButtonStyle);
            }

            public bool Button(GUIContent content, GUIStyle style)
            {
                float width = Mathf.Ceil(style.CalcSize(content).x + 8f);
                EnsureSpace(width);
                bool pressed = GUILayout.Button(content, style, GUILayout.Width(width), GUILayout.Height(22f));
                cursorX += width;
                return pressed;
            }

            private void EnsureSpace(float width)
            {
                float available = Mathf.Max(0f, contentWidth - cursorX - pad);
                if (width > available && cursorX > pad)
                {
                    NewRow();
                }
                else if (cursorX > pad)
                {
                    GUILayout.Space(gap);
                    cursorX += gap;
                }
            }

            public void Dispose()
            {
                EndRowInternal();
            }
        }
    }
}
#endif
