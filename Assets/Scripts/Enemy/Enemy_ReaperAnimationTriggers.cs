using UnityEngine;

public class Enemy_ReaperAnimationTriggers : Enemy_AnimationTriggers
{
    Enemy_Reaper enemyReaper;

    protected override void Awake()
    {
        base.Awake();
        enemyReaper = GetComponentInParent<Enemy_Reaper>();
    }

    private void TeleportTrigger()
    {
        enemyReaper.SetTeleportTrigger(true);
    }
}
