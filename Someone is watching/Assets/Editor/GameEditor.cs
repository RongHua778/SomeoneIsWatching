using UnityEngine;
using UnityEditor;


public class GameEditor : EditorWindow
{

    string myString = "1";

    [MenuItem("Window/GameEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<GameEditor>();
    }

    private void OnGUI()
    {
        GUILayout.Label("SetDay",EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Day", myString);
        if (GUILayout.Button("SetDay"))
        {
            
            Debug.Log("DaySet");
        }
        GUILayout.Label("SetTime", EditorStyles.boldLabel);
        if (GUILayout.Button("Fast"))
        {
            Time.timeScale = 3f;
        }
        if (GUILayout.Button("Slow"))
        {
            Time.timeScale = 1f;
        }

    }
}
