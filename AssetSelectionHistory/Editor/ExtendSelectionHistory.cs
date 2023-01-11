using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 本文件在Project目录的Assets文件夹旁边添加了两个按钮
public class ExtendSelectionHistory : MonoBehaviour
{
    private static float BUTTON_WIDTH = 25.0f;

    //给Project里面加一些自定义的按钮之类的
    [InitializeOnLoadMethod]
    private static void Init()
    {
        // TODO: 目前还不能运行，因为触发选择事件需要panel那边处理，因此这边运行需要panel先开启..
        // EditorApplication.projectWindowItemOnGUI += (guid, rect) =>
        // {
        //     var ItemIsAssets = AssetDatabase.GUIDToAssetPath(guid) == "Assets";
 
        //     if (ItemIsAssets)
        //     {
        //         float padding = BUTTON_WIDTH / 5.0f;
        //         float requireWidth = 2 * (BUTTON_WIDTH + padding);
        //         if (rect.width > requireWidth)
        //         {
        //             GUI.enabled = AssetSelectionHistory.Instance.HasPrevious();
        //             rect.x = rect.x + rect.width - requireWidth;
        //             rect.width = BUTTON_WIDTH;
        //             if (GUI.Button(rect, "←"))
        //                 AssetSelectionHistory.Instance.GoPrevious();
                        
        //             GUI.enabled = AssetSelectionHistory.Instance.HasNext();
        //             rect.x += BUTTON_WIDTH + padding;
        //             if (GUI.Button(rect, "→"))
        //                 AssetSelectionHistory.Instance.GoNext();

        //             GUI.enabled = true;
        //         }
        //     }
        // };
    }
}
