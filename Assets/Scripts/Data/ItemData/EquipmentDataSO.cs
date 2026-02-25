using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName = "RPG SetUp/Item Data/Equipment item", fileName = "Equipment data -")]
public class EquipmentDataSO : ItemDataSO
{
    [Header("ItemModifiers")]
    public ItemModifier[] modifiers;
}

[Serializable]
public class ItemModifier
{
    public StatType statType;
    public float value;
}
