using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG SetUp/Item Data/Item effect/Heal effect", fileName = "Item effect data - heal")]
public class ItemEffect_Heal : ItemEffect_DataSO
{
    [SerializeField] private float healPercent = 0.1f;
    public override void ExecuteEffect()
    {
        Player player = FindFirstObjectByType<Player>();
        float healAmount = player.stats.GetMaxHealth() * healPercent;

        player.health.IncreaseHealth(healAmount);
    }
}
