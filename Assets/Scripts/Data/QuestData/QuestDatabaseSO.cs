using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "RPG SetUp/Quest Data/Quest Database", fileName = "QUEST DATABASE")]

public class QuestDatabaseSO : ScriptableObject
{
    public QuestDataSO[] allQuests;


    public QuestDataSO GetQuestById(string  id)
    {
        return allQuests.FirstOrDefault(q => q != null && q.questSaveId == id);
    }

#if UNITY_EDITOR
    [ContextMenu("Auto-fill with all QuestDataSO")]
    public void CollectItemsData()
    {
        string[] guids = AssetDatabase.FindAssets("t:QuestDataSO");

        allQuests = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<QuestDataSO>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(q => q != null)
            .ToArray();

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }
#endif
}
