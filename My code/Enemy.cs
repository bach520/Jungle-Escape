using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    //public
    #region
    public float chaseRange = 2f;
    //public float stoppingDistance = 0.5f;
    public float attackDistance = 0.5f;

    public bool faceRight;
    public bool touchEdge;
    public bool backTouchEdge;
    public bool touchWall;
    public bool targetIsHidden;
    public bool isOnCD = false;

    public GameObject target;
    public Transform groundDetection;
    public Transform backGroundDetection;
    public Transform CwallDetection;
    public Collider2D wallDetection;
    #endregion

    //private
    #region
    [SerializeField]
    private float attackCDTime = 3f;
    private float CDtimer;
    [SerializeField]
    private Transform attackRayPoint;
    [SerializeField]
    private float attackRayLength = 0.5f;
    private float distanceToTarget;
    #endregion

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if(this.Stats.GetCurrentHealth() <= 0)
        {
            DestroyEnemy();
        }
        targetIsHidden = target.GetComponent<Player>().IsHidden;
        distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
        faceRight = FaceRight();
        // if ground detection does not collides with ground, enemy reaches edge
        touchEdge = !Physics2D.OverlapCircle(groundDetection.position, 0.1f, LayerMask.GetMask("Ground"));
        // if back ground detection does not collides with ground, enemy behind is edge
        backTouchEdge = !Physics2D.OverlapCircle(groundDetection.position, 0.1f, LayerMask.GetMask("Ground"));
        // if wall detection collides with wall, enemy touches wall
        //touchWall = wallDetection.IsTouchingLayers(LayerMask.GetMask("Ground"));
        touchWall = Physics2D.OverlapCircle(CwallDetection.position, 0.1f, LayerMask.GetMask("Ground"));

        if (isOnCD)
        {
            Cooldown();
        }
    }

    public StatsObject GetCharacterStats() => characterStats;
    public Animator GetAnimator() => _animator;
    public float GetAttackCDTime() => attackCDTime;
    public Transform GetAttackRay() => attackRayPoint;
    public float GetAttackRayLength() => attackRayLength;
    public float GetDistanceToTarget() => distanceToTarget;
    public void AIWalk()
    {
        transform.Translate(Vector2.right * characterStats.GetSpeed() * 0.5f * Time.deltaTime);
    }
    public void AIChase()
    {
        transform.Translate(Vector2.right * characterStats.GetSpeed() * 1.5f * Time.deltaTime);
    }
    public void AITurn()
    {
        transform.eulerAngles = new Vector3(0, -180, 0); // rotate
    }
    public void Patrol_Anim(bool patrol)
    {
        _animator.SetBool("isPatrolling", patrol);
    }
    public void Chase_Anim(bool chase)
    {
        _animator.SetBool("isChasing", chase);
    }
    public void Attack_Anim()
    {
        _animator.SetTrigger("attack");
    }
    public void Idle_Anim(bool idle)
    {
        _animator.SetBool("isIdling", idle);
    }
    public bool TouchEdgeOrWall()
    {
        if (touchEdge || touchWall) return true;
        else return false;
    }
    public bool FaceRight()
    {
        if (groundDetection.position.x < backGroundDetection.position.x) return false;
        else return true;
    }
    public void Cooldown()
    {
        CDtimer -= Time.deltaTime;
        if (CDtimer <= 0)
        {
            isOnCD = false;
            CDtimer = attackCDTime;
        }
    }
    public void ResetCDTimer()
    {
        CDtimer = attackCDTime;
        isOnCD = true;
    }

    public void DestroyEnemy()
    {
        target.GetComponent<Player>().PlayerInventory.TotalCollectables += 5;
        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }
}
