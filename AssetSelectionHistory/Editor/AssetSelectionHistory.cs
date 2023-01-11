/* 

  Asset Selection History
  @polemical, Unity Forum

  2023-01-11 - 2.0.0 by AllocBlock
  - Changed
    - move editor part to separate file, this file only contain class for history saving

  2020-08-20 - 1.0.2

  - Fixed
    - Different projects would use the same history.

  2020-08-20 - 1.0.1

  - Fixed
    - Entering Play Mode would result in the History being lost.

  - Added
    - History is now saved to EditorPrefs and restored when reopening Unity or as necessary.
    - Ability to reselect the indicated selection (i.e. for after deselection).
    - Ability to remove current selection from the list.

  - Changed
    - The button that previously was used to clear the history now opens a menu.
    - Selecting the same objects as a selection already added to history will 
      now re-select that in the list instead of adding a duplicate, up to LIMIT_DUPLICATES 
      number of most recent selections, or 0 (allow), -1 (check all).
      - By default, this only checks for duplicates in the 100 most recent selections.

  2020-08-19 - Initial Release
  
  - Inspired by https://forum.unity.com/threads/can-we-get-a-back-button-in-the-project-panel.723560/

*/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetSelectionHistory
{
    private static int MAX_HISTORY_NUM = 100; // 0: disable, -1: check all, otherwise up to # most recent.

    private Object[][] _historys = new Object[MAX_HISTORY_NUM][]; // 支持多选
    private int _curHistoryIndex = -1;
    private int _curHeadHistoryIndex = -1;
    private bool _isSelfTriggeredChange = false;

    // EditorWindow有OnSelectionChange，在里面调用这里的TriggerSelectionChange
    public void TriggerSelectionChange()
    {
        Object[] currentSelection = Selection.objects;
        if (currentSelection == null || currentSelection.Length == 0) return;
        if (_isSelfTriggeredChange)
        {
            _isSelfTriggeredChange = false;
            EditorGUIUtility.PingObject(currentSelection[0]); // 等价于在窗口里点击了目标
            EditorUtility.FocusProjectWindow();
        }
        else
            AddHistory(currentSelection);
    }

    void ClearHistory(bool clear = false)
    {
        _curHistoryIndex = _curHeadHistoryIndex = -1;
    }

    void AddHistory(Object[] selection)
    {
        if (Selection.assetGUIDs == null || Selection.assetGUIDs.Length == 0) return;
        if (selection == null || selection.Length == 0) return;

        _curHeadHistoryIndex = _curHistoryIndex + 1;
        if (_curHistoryIndex >= 0) // 已有记录
        {
            if (isSame(selection, _historys[_curHistoryIndex])) return;
        }
        _historys[_curHeadHistoryIndex] = selection;
        _curHistoryIndex = _curHeadHistoryIndex;
    }

    public bool HasPrevious()
    {
        return _curHistoryIndex > 0;
    }

    public bool HasNext()
    {
        return _curHistoryIndex < _curHeadHistoryIndex;
    }

    public bool GoPrevious()
    {
        if (_curHistoryIndex > 0) _curHistoryIndex--;
        else return false;
        _isSelfTriggeredChange = true;
        Selection.objects = _historys[_curHistoryIndex];
        return true;
    }

    public bool GoNext()
    {
        if (_curHistoryIndex < _curHeadHistoryIndex) _curHistoryIndex++;
        else return false;
        _isSelfTriggeredChange = true;
        Selection.objects = _historys[_curHistoryIndex];
        return true;
    }

    bool isSame(Object[] v1, Object[] v2)
    {
        if (v1 == v2) return true;
        if (v1.Length != v2.Length) return false;
        for (int i = 0; i < v1.Length; ++i)
            if (v1[i] != v2[i]) return false;
        return true;
    }
}