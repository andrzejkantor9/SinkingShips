using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SinkingShips.Editor
{
#if UNITY_EDITOR
    public class CreateFolders : EditorWindow
    {
        #region Config
        private static List<string> _folders = new List<string>
        {
            "_AssetPacks",
            "_Packages",
            "_Test",
            "Animations",
            "Audio",
            "Audio/Music",
            "Audio/Sfx",
            "Editor",
            "Editor/InspectorPresets",
            "Materials",
            "Meshes",
            "Prefabs",
            "Prefabs/UI",
            "Prefabs/UI/HUD",
            "Prefabs/UI/Menus",
            "Prefabs/UI/Widgets",
            "Scenes",
            "Scenes/Sandboxes",
            "ScriptableObjects",
            "Scripts",
            "Shaders",
            "Textures",
        };
        #endregion

        #region Events & Statics
        //private static string projectName = "PROJECT_NAME";
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region Engine & Contructors
        //private void OnGUI()
        //{
        //    EditorGUILayout.LabelField("Insert the Project name used as the root folder");
        //    projectName = EditorGUILayout.TextField("Project Name: ", projectName);
        //    this.Repaint();
        //    GUILayout.Space(70);
        //    if (GUILayout.Button("Generate!"))
        //    {
        //        CreateAllFolders();
        //        this.Close();
        //    }
        //}
        #endregion

        #region Events & Statics
        [MenuItem("Assets/Create Default Folders")]
        private static void SetUpFolders()
        {
            //CreateFolders window = ScriptableObject.CreateInstance<CreateFolders>();
            //window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
            //window.ShowPopup();

            CreateAllFolders();
        }

        private static void CreateAllFolders()
        {
            foreach (string folder in _folders)
            {
                if (!Directory.Exists("Assets/" + folder))
                {
                    //Directory.CreateDirectory("Assets/" + projectName + "/" + folder);
                    //File.Create("Assets/" + projectName + "/" + folder + "/.gitkeep");

                    string directoryPath = "Assets/" + folder;
                    Directory.CreateDirectory(directoryPath);
                    File.Create(directoryPath + "/.gitkeep");
                }
            }

            AssetDatabase.Refresh();
        }
        #endregion
    }
#endif
}
