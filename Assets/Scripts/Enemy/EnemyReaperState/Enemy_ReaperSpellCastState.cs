using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ReaperSpellCastState : EnemyState
{
    private Enemy_Reaper enemyReaper;

    public Enemy_ReaperSpellCastState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyReaper = enemy as Enemy_Reaper;
    }   

    public override void Enter()
    {
        base.Enter();

        enemyReaper.SetVelocity(0, 0);
        enemyReaper.SetSpellCastPerformed(false);
        enemyReaper.SetSpellCastOnCooldown();
        enemyReaper.MakeUntargetable(false);

        stateTimer = 15f; // Safety timeout
    }

    public override void Update()
    {
        base.Update();

        if (enemyReaper.spellCastPerformed)
        {
            anim.SetBool("spellCast_Performed", true);
        }

        if (triggerCalled || (enemyReaper.spellCastPerformed && stateTimer < 0))
        {
            if (enemyReaper.ShouldTeleport())
                stateMachine.ChangeState(enemyReaper.reaperTeleportState);
            else
                stateMachine.ChangeState(enemyReaper.reaperBattleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemyReaper.StopSpellCast();
        anim.SetBool("spellCast", false);
        anim.SetBool("spellCast_Performed", false);
    }
}
