using UnityEngine;
using UnityEditor;

public class SelectionHistoryMiniPanel : EditorWindow
{
    [MenuItem("Window/Selection History Mini Panel")]
    static void Init()
    {
        SelectionHistoryMiniPanel window = EditorWindow.GetWindow<SelectionHistoryMiniPanel>("History");
        window.minSize = new Vector2(50, 20);
        window.Show();
    }

    void OnGUI()
    {
        var rowLayout = new GUILayoutOption[]
        {
            GUILayout.ExpandWidth(true),
            GUILayout.ExpandHeight(true),
        };
        var buttonLayout = new GUILayoutOption[]
        {
            GUILayout.ExpandWidth(true),
            GUILayout.ExpandHeight(true),
        };

        GUILayout.BeginHorizontal(rowLayout);
        GUI.enabled = AssetSelectionHistory.Instance.HasPrevious();
        if (GUILayout.Button("←", buttonLayout))
            AssetSelectionHistory.Instance.GoPrevious();
            
        GUI.enabled = AssetSelectionHistory.Instance.HasNext();
        if (GUILayout.Button("→", buttonLayout))
            AssetSelectionHistory.Instance.GoNext();

        GUILayout.EndHorizontal();
        GUI.enabled = true;
    }

    void OnSelectionChange()
    {
        Repaint();
    }
}
