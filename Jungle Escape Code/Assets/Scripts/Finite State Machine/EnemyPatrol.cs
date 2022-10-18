using UnityEngine;

public class EnemyPatrol : EnemyBaseState
{
    private Transform playerPos;
    private float distanceToTarget;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.user.Patrol_Anim(true);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform; //get player position
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        if (!enemy.user.targetIsHidden) // if target is not hidden
        {
            if (enemy.user.GetDistanceToTarget() < enemy.user.chaseRange) // if back and front not empty (can walk) and in range
            {
                enemy.user.Patrol_Anim(false);
                enemy.SwitchState(enemy.Chase); // chase
            }
            else if (enemy.user.GetDistanceToTarget() <= enemy.user.attackDistance) // if in attack distance
            {
                enemy.user.Patrol_Anim(false);
                enemy.SwitchState(enemy.Attack); // attack
            }
        } // if target is not hidden, detect to chase/attack
        // else if enemy is hidden or front not empty, patrol
        if (!enemy.user.TouchEdgeOrWall()) // if front not empty
        {
            enemy.user.transform.Translate(Vector2.right * enemy.enemyStats.GetSpeed() * .5f * Time.deltaTime); // move/patrol
        }
        else // if front empty
        {
            if (enemy.user.faceRight) // if face right, rotate char
            {
                enemy.transform.eulerAngles = new Vector3(0, -180, 0);
            } // if face left, return to normal rotation
            else enemy.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public override void ExitState(EnemyStateManager enemy)
    {

    }
}
