using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ArcherElf : Enemy
{
    public bool CanBeCountered { get => canBeStunned; }
    public Enemy_ArcherElfBattleState archerElfBattleState { get; set; }

    [Header("Archer Elf Specifics")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowStartPoint;
    [SerializeField] private float arrowSpeed = 8f;


    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        deadState = new Enemy_DeadState(this, stateMachine, "idle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");

        archerElfBattleState = new Enemy_ArcherElfBattleState(this, stateMachine, "battle");
        battleState = archerElfBattleState;
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public override void SpecialAttack()
    {
        GameObject newArrow = Instantiate(arrowPrefab, arrowStartPoint.position, Quaternion.identity);
        newArrow.GetComponent<Enemy_ArcherElfArrow>().SetupArrow(arrowSpeed * facingDir, combat);
    }

    public void HandleCounter()
    {
        if (CanBeCountered == false)
            return;

        stateMachine.ChangeState(stunnedState);
    }
}
