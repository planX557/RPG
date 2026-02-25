using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject_Sword : SkillObject_Base
{
    protected Skill_SwordThrow swordManager;

    protected Transform playerTransform;
    protected bool shouleComeBack;
    protected float comeBackSpeed = 20;
    protected float maxAllowedDistance = 25;

    protected virtual void Update()
    {
        transform.right = rb.velocity;
        HandleComeBack();
    }

    public virtual void SetupSword(Skill_SwordThrow swordManager, Vector2 direction)
    {
        rb.velocity = direction;

        this.swordManager = swordManager;

        playerTransform = swordManager.transform.root;
        playerStats = swordManager.player.stats;
        damageScaleData = swordManager.damageScaleData;
    }

    public void GetSwordBackToPlayer() => shouleComeBack = true;

    protected void HandleComeBack()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        
        if (distance > maxAllowedDistance) 
            GetSwordBackToPlayer();
        
        if (shouleComeBack == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, comeBackSpeed * Time.deltaTime);

        if (distance < 0.5f)
            Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        StopSword(collision);
        DamageEnemiesInRadius(transform, 1);
    }

    protected void StopSword(Collider2D collision)
    {
        rb.simulated = false;
        transform.parent = collision.transform;
    }
}

