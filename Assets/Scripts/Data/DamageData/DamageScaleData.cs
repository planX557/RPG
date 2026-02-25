using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DamageScaleData 
{
    [Header("Damage")]
    public float physical = 1;
    public float elemental = 1;

    [Header("Chill")]
    public float chillDuration = 3;
    public float chillSlowMultiplier = 0.2f;

    [Header("Burn")]
    public float burnDuration = 3;
    public float burnDamageScale = 1;

    [Header("Shock")]
    public float shockDuration = 3;
    public float shockDamageScale = 1;
    public float shockCharge = 0.4f;
}
