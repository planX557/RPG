using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Merchant : Object_NPC, IInteractable
{
    [Header("Quest & Dialogue")]
    [SerializeField] private DialogueLineSO firstDialogueLine;
    [SerializeField] private QuestDataSO[] quests;

    private Inventory_Player inventory;
    private Inventory_Merchant merchant;

    protected override void Awake()
    {
        base.Awake();
        merchant = GetComponent<Inventory_Merchant>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Z))
            merchant.FillShopList();
    }

    public override void Interact()
    {
        base.Interact();

        ui.merchantUI.SetupMerchantUI(merchant, inventory);
        ui.OpenDialogueUI(firstDialogueLine, new DialogueNpcData(rewardNpc, quests));
        //ui.OpenQuestUI(quests);

        //ui.OpenMerchantUI(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        inventory = player.GetComponent<Inventory_Player>();
        merchant.SetInventory(inventory);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        ui.HideAllTooltips();
        ui.OpenMerchantUI(false);
    }
}
