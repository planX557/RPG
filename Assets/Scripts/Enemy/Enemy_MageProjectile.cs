using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MageProjectile : MonoBehaviour
{
    private Entity_Combat combat;
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator anim;

    [SerializeField] private float arcHeight = 2f;
    [SerializeField] private LayerMask whatCanCollideWith;

    public void SetupProjectile(Transform target, Entity_Combat combat)
    {
        this.combat = combat;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        anim.enabled = false;

        Vector2 velocity = CalculateBallisticVelocity(transform.position, target.position);
        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatCanCollideWith) != 0)
        {
            combat.PerformAttackOnTarget(collision.transform);

            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            anim.enabled = true;
            col.enabled = false;
            Destroy(gameObject, 2);
        }
    }

    private Vector2 CalculateBallisticVelocity(Vector2 start,  Vector2 end)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);

        float displacementX = end.x - start.x;
        float displacementY = end.y - start.y;

        float peakHeight = Mathf.Max(arcHeight, end.y - start.y + 0.1f);

        float timeToApex = Mathf.Sqrt((2 * peakHeight) / gravity);
        float timeFromApex = Mathf.Sqrt((2 * (peakHeight - displacementY)) / gravity);

        float totalTime = timeToApex + timeFromApex;

        float velocityX = displacementX / totalTime;
        float velocityY = Mathf.Sqrt(2 * gravity * peakHeight);

        return new Vector2(velocityX, velocityY);
    }
}


