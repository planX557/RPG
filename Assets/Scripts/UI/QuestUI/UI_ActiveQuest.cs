using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActiveQuest : MonoBehaviour
{
    private Player_QuestManager questManager;
    private UI_ActiveQuestSlot[] questSlots;


    private void Awake()
    {
        questManager = Player.instance.questManager;
        questSlots = GetComponentsInChildren<UI_ActiveQuestSlot>(true);
    }

    private void OnEnable()
    {
        List<QuestData> quests = questManager.activeQuests;

        foreach (var slot in questSlots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = 0; i < quests.Count; i++)
        {
            questSlots[i].gameObject.SetActive(true);
            questSlots[i].SetupActiveQuestSlot(quests[i]);
        }

        if (quests.Count > 0)
            questSlots[0].SetupPreviewBTN();
    }
}
