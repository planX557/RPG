public class Enemy_ReaperTeleportState : EnemyState
{
    private Enemy_Reaper enemyReaper;

    public Enemy_ReaperTeleportState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyReaper = enemy as Enemy_Reaper;
    }

    public override void Enter()
    {
        base.Enter();
        enemyReaper.MakeUntargetable(true);
    }

    public override void Update()
    {
        base.Update();

        if (enemyReaper.teleportTrigger)
        {
            enemyReaper.transform.position = enemyReaper.FindTeleportPoint();
            enemyReaper.SetTeleportTrigger(false);
        }

        if (triggerCalled)
        {
            if (enemyReaper.CanDoSpellCast())
            {
                stateMachine.ChangeState(enemyReaper.reaperSpellCastState);
            }
            else
            {
                stateMachine.ChangeState(enemyReaper.reaperBattleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemyReaper.MakeUntargetable(false);
    }
}
