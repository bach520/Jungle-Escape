using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossManager : Character
{
    [SerializeField] GameObject fireBallObject;
    [SerializeField] Transform firePoint;
    [SerializeField] Collider2D firePointCollider;
    [SerializeField] Player target;
    float longAtkRange = 1f;
    Vector3 targetPos;
    float attackCDTime;
    float meleeAtkCDTime;
    bool isMelee = false;
    public bool die = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        targetPos = target.transform.position;
        attackCDTime = Stats.TimeBetweenAttacks/2;
        meleeAtkCDTime = Stats.TimeBetweenAttacks;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Stats.GetCurrentHealth() <= 0)
        {
            die = true;
            Destroy(gameObject);
        }
        targetPos = target.transform.position;

        //Debug.Log(Vector2.Distance(transform.position, targetPos));
        if (Vector2.Distance(transform.position, targetPos) > longAtkRange) // if player too far, do range atk
        {
            isMelee = false;
        }
        else isMelee = true; // else do melee atk
        if (!isMelee)
        {
            if (attackCDTime <= 0)
            {
                _animator.SetTrigger("attack");
                attackCDTime = Stats.TimeBetweenAttacks / 2;
            }
            else
            {
                attackCDTime -= Time.deltaTime;
            }
        }
        else
        {
            if (meleeAtkCDTime <= 0)
            {
                _animator.SetTrigger("attack");
                firePointCollider.enabled = true;
                meleeAtkCDTime = Stats.TimeBetweenAttacks;
            }
            else
            {
                firePointCollider.enabled = false;
                meleeAtkCDTime -= Time.deltaTime;
            }
        }
    }
    void AttackPlayer()
    {
        if (!isMelee)
        {
            Instantiate(fireBallObject, firePoint.position, Quaternion.identity);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, Vector2.right, 1f, LayerMask.GetMask("Player"));
            Debug.DrawRay(firePoint.position, Vector2.right * 1f, Color.red);
            
            if(hit.collider != null)
            {
                target.StatsObject.TakeDamage(Stats.GetDamage());
            }
        }
    }
}
