using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : Entity
{
    [Header("Quest Info")]
    public string questTargetId;

    public Entity_Stats stats { get; private set; }
    public Enemy_Health health {  get; private set; }
    public Entity_Combat combat { get; private set; }
    public Entity_VFX vfx { get; private set; }
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;
    public Enemy_StunnedState stunnedState;
    //public Enemy_ReaperSpellCastState reaperSpellCastState;//

    [Header("Battle Details")]
    public float battleMoveSpeed = 3f;
    public float attackDistance = 2f;
    public float attackCooldown = 0.5f;
    public bool canChasePlayer = true;
    [Space]
    public float battleTimeDuration = 5f;
    public float minRetreatDistance = 1f;
    public Vector2 retreatVelocity;

    [Header("Stunned State Details")]
    public float stunnedDuration = 1;
    public Vector2 stunnedVelocity = new Vector2(7, 7);
    [SerializeField] protected bool canBeStunned;

    [Header("Movement details")]
    public float idleTime = 2f;
    public float moveSpeed = 1.4f;
    [Range(0,2)]
    public float moveAnimSpeedMultiplier = 1f;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform PlayerCheck;
    [SerializeField] private float playerCheckDistance = 10f;
    public Transform player { get;private set; }
    public float activeSlowMultiplier { get; private set; } = 1;
    private float defaultAnimSpeed;

    public float GetMoveSpeed() => moveSpeed * activeSlowMultiplier;
    public float GetBattleMoveSpeed() => battleMoveSpeed * activeSlowMultiplier;

    protected override void Awake()
    {
        base.Awake();
        health = GetComponent<Enemy_Health>();
        stats = GetComponent<Entity_Stats>();
        combat = GetComponent<Entity_Combat>();
        vfx = GetComponent<Entity_VFX>();

        defaultAnimSpeed = anim.speed;
    }

    public void MakeUntargetable(bool cantBeTargeted)
    {
        if (cantBeTargeted == true)
            gameObject.layer = LayerMask.NameToLayer("UnTargetable");
        else
            gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    public virtual void SpecialAttack()
    {

    }

    protected override IEnumerator SlowDownEntityCo(float duration, float slowMultiplier)
    {
        activeSlowMultiplier = 1 - slowMultiplier;

        anim.speed = defaultAnimSpeed * activeSlowMultiplier;

        yield return new WaitForSeconds(duration);
        StopSlowDown();
    }

    public override void StopSlowDown()
    {
        activeSlowMultiplier = 1;
        anim.speed = defaultAnimSpeed;
        base.StopSlowDown();
    }

    public void EnableCounterWindow(bool enable) => canBeStunned = enable;

    public override void EntityDeath()
    {
        base.EntityDeath();

        stateMachine.ChangeState(deadState);
    }

    private void HandlePlayerDeath()
    {
        stateMachine.ChangeState(idleState);
    }

    public void TryEnterBattleState(Transform player)
    {
        if (CanBeInterrupted() == false)
            return;

        if (stateMachine.currentState == battleState)
            return;

        if(stateMachine.currentState == attackState)
            return;

        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    public virtual bool CanBeInterrupted() => true;

    public void DestroyGameObjectWithDelay(float delay = 10)
    {
        Destroy(gameObject, delay);
    }

    public Transform GetPlayerReference()
    {
        if (player == null)
            player = PlayerDetected().transform;

        return player;
    }

    public RaycastHit2D PlayerDetected()
    {
        RaycastHit2D hit = 
            Physics2D.Raycast(PlayerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);

        if (hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;
        
        return hit;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(PlayerCheck.position,
            new Vector3(PlayerCheck.position.x + (facingDir * playerCheckDistance), PlayerCheck.position.y));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(PlayerCheck.position,
            new Vector3(PlayerCheck.position.x + (facingDir * attackDistance), PlayerCheck.position.y));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(PlayerCheck.position,
            new Vector3(PlayerCheck.position.x + (facingDir * minRetreatDistance), PlayerCheck.position.y));
    }

    private void OnEnable()
    {
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= HandlePlayerDeath; 
    }
}
