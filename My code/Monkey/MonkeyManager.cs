using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyManager : Character
{
    private float attackDistance = 5f;
    private GameObject target;
    [SerializeField] private Transform projectile;
    [SerializeField] private Transform shootPoint;
    bool attackOnCD = false;
    float attackCDTime;
    float CDTimer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player");
        attackCDTime = Stats.TimeBetweenAttacks;
        CDTimer = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Stats.GetCurrentHealth() <= 0)
        {
            DestroyEnemy();
        }

        float distanceToTarget = Vector2.Distance(shootPoint.position, target.transform.position);
        if (target.transform.position.x <= transform.position.x) // rotate to face target
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (attackOnCD) // if attack on CD, run CD
        {
            Cooldown();
        }
        else if (distanceToTarget <= attackDistance) // else if attack not on CD and target in range
        {
            ResetCDTimer(); // reset cd timer
            // play throw animation
            _animator.SetTrigger("attack");
        }
    }
    public void Cooldown()
    {
        CDTimer -= Time.deltaTime; // countdown timer
        if (CDTimer <= 0) // if timer reaches 0
        {
            attackOnCD = false; // set attack not on CD
            CDTimer = attackCDTime; // reset CDTimer to start new countdown
        }
    }
    public void ResetCDTimer()
    {
        CDTimer = attackCDTime;
        attackOnCD = true;
    }
    void ThrowProjectile()
    {
        Instantiate(projectile, shootPoint.position, Quaternion.identity, this.transform); // throw projectile
    }
    void DestroyEnemy()
    {
        target.GetComponent<Player>().PlayerInventory.TotalCollectables += 10;
        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }
}
