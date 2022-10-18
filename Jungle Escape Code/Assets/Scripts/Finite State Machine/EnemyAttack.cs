using UnityEngine;

public class EnemyAttack : EnemyBaseState
{
    private int damage;
    private Transform raycast;
    private float raycastLength;
    private float attackDistance;
    
    private RaycastHit2D hit;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.user.Idle_Anim(true);
        damage = enemy.user.GetCharacterStats().GetDamage();
        raycast = enemy.user.GetAttackRay();
        raycastLength = enemy.user.GetAttackRayLength();
        attackDistance = enemy.user.attackDistance;
    }
    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.user.targetIsHidden) // if target is hidden, patrol
        {
            enemy.user.Chase_Anim(false);
            enemy.user.Idle_Anim(false);
            enemy.SwitchState(enemy.Patrol); // patrol
        }// if target is hidden, patrol
        else // else attack or check to chase
        {
            Vector2 vec2 = enemy.user.faceRight ? Vector2.right : Vector2.left;
            hit = Physics2D.Raycast(raycast.position, vec2, attackDistance, LayerMask.GetMask("Player"));
            Debug.DrawRay(raycast.position, vec2 * raycastLength, Color.red);
            if (hit.collider != null) // if raycast hit player
            {
                Debug.DrawRay(raycast.position, vec2 * attackDistance, Color.green);
                Attack(enemy); // attack
            }
            else if (enemy.user.TouchEdgeOrWall() || enemy.user.GetDistanceToTarget() > attackDistance) // if enemy touches edge or wall, or does not reach attack distance
            {
                enemy.user.Idle_Anim(false);
                enemy.SwitchState(enemy.Chase); // chase
            } // if enemy touches edge or wall, or does not reach attack distance, chase

            if (enemy.transform.position.x > enemy.user.target.transform.position.x) // if player is behind enemy
            {
                enemy.transform.eulerAngles = new Vector3(0, -180, 0); // rotate
                hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength, LayerMask.GetMask("Player")); // create raycast in rotate direction
                Debug.DrawRay(raycast.position, Vector3.left * raycastLength, Color.red);
            } // if player is behind enemy
            else // if player in front
            {
                enemy.transform.eulerAngles = new Vector3(0, 0, 0);
                hit = Physics2D.Raycast(raycast.position, Vector2.right, raycastLength, LayerMask.GetMask("Player")); // create raycast in rotate direction
                Debug.DrawRay(raycast.position, Vector3.right * raycastLength, Color.red);
            } // if player in front
        }
    }
    public override void ExitState(EnemyStateManager enemy)
    {

    }
    void Attack(EnemyStateManager enemy)
    {
        // if player is outside attack distance, switch to chase
        if (enemy.user.GetDistanceToTarget() > attackDistance)
        {
            enemy.user.Idle_Anim(false);
            enemy.SwitchState(enemy.Chase); // chase
        }
        else if (enemy.user.GetDistanceToTarget() <= attackDistance && enemy.user.isOnCD == false) // if player is in attack distance and enemy is not on CD, attack
        {
            enemy.user.Attack_Anim();
            enemy.user.ResetCDTimer(); // reset CD timer
            enemy.shouldDamagePlayer = true;
        }
        
    }
}
