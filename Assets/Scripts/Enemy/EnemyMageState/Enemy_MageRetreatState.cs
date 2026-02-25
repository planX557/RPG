using UnityEngine;

public class Enemy_MageRetreatState : EnemyState
{
    private Enemy_Mage enemyMage;
    private Vector3 startPosition;
    private Transform player;

    public Enemy_MageRetreatState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        enemyMage = enemy as Enemy_Mage;
    }

    public override void Enter()
    {
        base.Enter();

        if (player == null)
            player = enemy.GetPlayerReference();

        startPosition = enemy.transform.position;

        rb.velocity = new Vector2(enemyMage.retreatSpeed * -DirectonToPlayer(), 0);
        enemy.HandleFlip(DirectonToPlayer());
        enemy.MakeUntargetable(true);
        enemy.vfx.DoImageEchoEffect(1);
    }

    public override void Update()
    {
        base.Update();

        bool reachedMaxDistance = Vector2.Distance(enemy.transform.position, startPosition) > enemyMage.retreatMaxDistance;

        if (reachedMaxDistance || enemyMage.CantMoveBackwards())        
            stateMachine.ChangeState(enemyMage.mageSpellCastState);      
    }

    public override void Exit()
    {
        base.Exit();
        enemy.vfx.StopImageEchoEffect();
        enemy.MakeUntargetable(false);
    }

    protected int DirectonToPlayer()
    {
        if (player == null)
            return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
