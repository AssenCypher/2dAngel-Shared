#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TwoDAngel.Shared.Editor
{
    public static class TAE_Icons
    {
        public static GUIContent Overview => Make("Overview", "Grid.Default", "UnityEditor.SceneHierarchyWindow", "FilterByType");
        public static GUIContent Native => Make("Native", "Lighting", "LightingSettings", "PreMatCube");
        public static GUIContent Reflection => MakeScaled("Reflection", 14, "ReflectionProbe Gizmo", "ReflectionProbe Icon", "SceneViewFx", "PreMatSphere", "SceneViewOrtho");
        public static GUIContent Bakery => Make("Bakery", "PreMatCube", "BuildSettings.Editor.Small", "Lighting");
        public static GUIContent Volumes => Make("Volumes", "SceneViewFx", "PreMatCube", "FilterByType");
        public static GUIContent Audit => Make("Audit", "Search Icon", "FilterByType", "ViewToolZoom");
        public static GUIContent About => Make("About", "_Help", "console.infoicon", "Toolbar Plus More");

        public static GUIContent Visualize => MakeScaled("Visualize", 14, "SceneViewFx", "ViewToolOrbit", "SceneViewOrtho", "PreMatSphere");
        public static GUIContent EditBounds => Make("Edit Bounds", "ScaleTool", "RectTool", "MoveTool");

        public static GUIContent Open => Make("Open", "tab_next", "Folder Icon");
        public static GUIContent Scan => Make("Scan", "Search Icon", "ViewToolZoom");
        public static GUIContent Refresh => Make("Refresh", "TreeEditor.Refresh", "Refresh", "RotateTool");
        public static GUIContent Package => Make("Package", "Folder Icon", "Project");
        public static GUIContent Warning => Make("Warning", "console.warnicon", "console.erroricon");
        public static GUIContent Success => Make("Ready", "TestPassed", "Collab");
        public static GUIContent Missing => Make("Missing", "console.erroricon", "console.warnicon");
        public static GUIContent Convert => Make("Convert", "P4_AddedLocal", "tab_next", "Refresh");
        public static GUIContent Safety => Make("Safety", "Shield", "LockIcon-On", "console.warnicon", "Refresh");
        public static GUIContent History => Make("History", "Animation.Record", "Profiler.NextFrame", "Refresh");
        public static GUIContent Revert => Make("Revert", "Animation.PrevKey", "Toolbar Minus", "Refresh");
        public static GUIContent Redo => Make("Redo", "Animation.NextKey", "TreeEditor.Duplicate", "Refresh");


        public static GUIContent MakeScaled(string fallbackText, int size, params string[] iconNames)
        {
            GUIContent content = Make(fallbackText, iconNames);
            if (content.image == null || size <= 0)
            {
                return content;
            }

            Texture2D scaled = ScaleTexture(content.image, size, size);
            return scaled != null ? new GUIContent(scaled, fallbackText) : content;
        }

        public static GUIContent Make(string fallbackText, params string[] iconNames)
        {
            if (iconNames != null)
            {
                for (int i = 0; i < iconNames.Length; i++)
                {
                    Texture texture = TryFindTexture(iconNames[i]);
                    if (texture != null)
                    {
                        return new GUIContent(texture, fallbackText);
                    }
                }
            }

            return new GUIContent(fallbackText);
        }


        private static Texture2D ScaleTexture(Texture source, int width, int height)
        {
            if (source == null)
            {
                return null;
            }

            RenderTexture rt = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
            RenderTexture previous = RenderTexture.active;
            try
            {
                Graphics.Blit(source, rt);
                RenderTexture.active = rt;
                Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false)
                {
                    hideFlags = HideFlags.HideAndDontSave,
                    filterMode = FilterMode.Bilinear
                };
                texture.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
                texture.Apply(false, true);
                return texture;
            }
            catch
            {
                return null;
            }
            finally
            {
                RenderTexture.active = previous;
                RenderTexture.ReleaseTemporary(rt);
            }
        }

        private static Texture TryFindTexture(string iconName)
        {
            if (string.IsNullOrWhiteSpace(iconName))
            {
                return null;
            }

            Texture texture = null;
            try
            {
                texture = EditorGUIUtility.FindTexture(iconName);
            }
            catch
            {
                texture = null;
            }

            if (texture != null)
            {
                return texture;
            }

            string darkName = iconName.StartsWith("d_") ? iconName : "d_" + iconName;
            try
            {
                texture = EditorGUIUtility.FindTexture(darkName);
            }
            catch
            {
                texture = null;
            }

            return texture;
        }
    }
}
#endif
