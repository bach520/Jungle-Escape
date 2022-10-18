using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField]
    public Enemy user;

    public StatsObject enemyStats;
    EnemyBaseState currentState; // reference to active state
    public EnemyPatrol Patrol = new EnemyPatrol();
    public EnemyChase Chase = new EnemyChase();
    public EnemyAttack Attack = new EnemyAttack();
    public bool shouldDamagePlayer = false;
    public float damageTimer = 0.25f;

    // Start is called before the first frame update
    protected void Start()
    {
        currentState = Patrol;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    protected void Update()
    {
        currentState.UpdateState(this);
        if(shouldDamagePlayer)
        {
            if(damageTimer <= 0)
            {
                user.target.GetComponent<Player>().StatsObject.TakeDamage(user.Stats.GetDamage());
                damageTimer = 0.25f;
                shouldDamagePlayer = false;
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }
        }
    }
    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
    
}
