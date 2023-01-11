using UnityEngine;
using UnityEditor;

public class SelectionHistoryMiniPanel : EditorWindow
{
    private AssetSelectionHistory _history = new AssetSelectionHistory();

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
        GUI.enabled = _history.HasPrevious();
        if (GUILayout.Button("←", buttonLayout))
            _history.GoPrevious();
            
        GUI.enabled = _history.HasNext();
        if (GUILayout.Button("→", buttonLayout))
            _history.GoNext();

        GUILayout.EndHorizontal();
        GUI.enabled = true;
    }

    void OnSelectionChange()
    {
        _history.TriggerSelectionChange();
        Repaint();
    }
}
