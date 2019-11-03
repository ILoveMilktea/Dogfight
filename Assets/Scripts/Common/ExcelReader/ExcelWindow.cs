using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 이제 여기서 Tool 부분 만들어야 함.
/// </summary>
public class ExcelWindow : EditorWindow
{
    [MenuItem("Tool/ExcelReader")]
    static public void CreateWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(ExcelWindow));
        window.minSize = new Vector2(80f, 60f);
        window.maxSize = new Vector2(100f, 100f);
    }

    private void OnGUI()
    {
        Repaint();

        GUILayout.BeginArea(new Rect(10, 10, 80, 60));
        {
            if (GUILayout.Button("Set Tables", GUILayout.Height(30)))
            {
                ExcelReader.SetTables();
            }

            GUILayout.BeginVertical();
            {
                foreach (var worksheet in ExcelReader.m_Worksheets)
                {

                }
            }

        }
        GUILayout.EndArea();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

}