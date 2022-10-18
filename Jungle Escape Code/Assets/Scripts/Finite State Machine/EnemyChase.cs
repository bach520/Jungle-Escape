using UnityEngine;

public class EnemyChase : EnemyBaseState
{
    Transform targetPostion;
    private float stopRangeWhileChasing;

    public override void EnterState(EnemyStateManager enemy)
    {
        stopRangeWhileChasing = enemy.user.chaseRange + 1f;
        enemy.user.GetAnimator().SetBool("isIdling", false);
        enemy.user.GetAnimator().SetBool("isPatrolling", false);
        enemy.user.GetAnimator().SetBool("isChasing", true);
        enemy.user.Chase_Anim(true);
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.user.targetIsHidden) // if target is hidden, patrol
        {
            enemy.user.Chase_Anim(false);
            enemy.user.Idle_Anim(false);
            enemy.SwitchState(enemy.Patrol); // patrol
        }
        else // else check to chase, attack, idle when can't move, or patrol when too far
        {
            targetPostion = enemy.user.target.transform;
            if (enemy.user.GetDistanceToTarget() > stopRangeWhileChasing)
            {
                enemy.user.Chase_Anim(false);
                enemy.user.Idle_Anim(false);
                enemy.SwitchState(enemy.Patrol); // patrol
            }
            else if (enemy.user.TouchEdgeOrWall() || enemy.transform.position.x <= targetPostion.position.x + 0.1f  && enemy.transform.position.x >= targetPostion.position.x - 0.1f) // if enemy touches edge or wall or at same position with player
            {
                enemy.user.Chase_Anim(false);
                enemy.user.Idle_Anim(true); // idle
                enemy.transform.Translate(Vector2.zero);
                if (enemy.transform.position.x >= targetPostion.position.x) // if player behind rotate
                {
                    enemy.transform.eulerAngles = new Vector3(0, -180, 0);
                }
                else enemy.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (enemy.user.GetDistanceToTarget() <= enemy.user.attackDistance) // if in attack distance
            {
                enemy.user.Chase_Anim(false);
                enemy.SwitchState(enemy.Attack); // attack
            }
            // if enemy distance to player is larger than attack distance and within stop chasing range, turn and move towards player
            else if (enemy.user.GetDistanceToTarget() > enemy.user.attackDistance && enemy.user.GetDistanceToTarget() <= stopRangeWhileChasing)
            {
                enemy.user.Chase_Anim(true);
                enemy.user.Idle_Anim(false);

                if (enemy.transform.position.x > targetPostion.position.x) // if player is behind enemy
                {
                    enemy.transform.eulerAngles = new Vector3(0, -180, 0); // rotate
                    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPostion.position,
                        enemy.enemyStats.GetSpeed() * 1.5f * Time.deltaTime); // move to player position
                }
                else
                {
                    enemy.transform.eulerAngles = new Vector3(0, 0, 0);
                    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPostion.position,
                        enemy.enemyStats.GetSpeed() * 1.5f * Time.deltaTime); // move to player position
                }
            }
        }
    }
    public override void ExitState(EnemyStateManager enemy)
    {

    }
}
