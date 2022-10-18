using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region variables
    [SerializeField]
    private GameObject DartgunEndPoint;
    [SerializeField]
    private WeaponBase weaponUsed;
    [SerializeField]
    private Player user;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private Transform spearAttackPos;
    [SerializeField]
    private Transform pfBullet;
    [SerializeField]
    private float spearAttackRange;
    [SerializeField]
    private float dartAttackRange = 4.0f;
    [SerializeField]
    private float timebetweenAttack;
    [SerializeField]
    private float startTimeBetweenAttack;
    private bool attacked = false;
    #endregion

    private void Start()
    {
        timebetweenAttack = 0;
        if(user.PlayerInventory.WeaponUsed != null)
        weaponUsed = user.PlayerInventory.WeaponUsed;
        startTimeBetweenAttack = user.PlayerInventory.WeaponUsed.GetAtkSpeed();
    }

    private void Update()
    {
        if(timebetweenAttack < 0)
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                if(GetComponent<Animator>().GetBool("usingBlowGun"))
                {
                    RangedAttack();
                    timebetweenAttack = startTimeBetweenAttack;
                }
                else
                {
                    SpearAttack();
                    timebetweenAttack = startTimeBetweenAttack;
                }
            }
        }
        else
        {
            timebetweenAttack -= Time.deltaTime;
        }
    }

    #region attacks
    public void RangedAttack()
    {
        if(user.PlayerInventory.TotalDarts <= 0)
        {
            Debug.Log("Not Enough Darts");
        }
        else if(Distance() > dartAttackRange)
        {
            Debug.Log(Distance());
            Debug.Log("Distance Too Far");
        }
        else
        {
            FireBullet();
            user.PlayerInventory.TotalDarts -= 1;
        }
    }

    public void FireBullet()
    {
        Transform bullet = Instantiate(pfBullet, DartgunEndPoint.transform.position, Quaternion.identity);
        Vector3 shootDirection = (GetMousePosition() - DartgunEndPoint.transform.position).normalized;
        bullet.GetComponent<Dart>().SetUp(shootDirection, (int)weaponUsed.GetDamage());
    }

    public void SpearAttack()
    {
        user.GetComponent<AudioSource>().clip = user.AudioList[0];
        user.GetComponent<AudioSource>().Play();
        // play spear attack animation
        Collider2D[] enemies = Physics2D.OverlapCircleAll(spearAttackPos.position, spearAttackRange, enemyLayer);
        for(int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].GetComponent<Enemy>() != null)
            {
                enemies[i].GetComponent<Enemy>().Stats.TakeDamage((int)weaponUsed.GetDamage());
            }
            else if(enemies[i].GetComponent<FinalBossManager>() != null)
            {
                enemies[i].GetComponent<FinalBossManager>().Stats.TakeDamage((int)weaponUsed.GetDamage());
            }
        }
        GetComponent<Animator>().SetTrigger("attackWithSpear");
        return;
    }
    #endregion


    #region get mouse world position
    public Vector3 GetMousePosition()
    {
        Vector3 vec = GetMouseWorldWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public Vector3 GetMouseWorldWithZ(Vector3 screenPosition, Camera worldCam)
    {
        Vector3 worldPosition = worldCam.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    #endregion
    
    public float Distance()
    {
        return Vector2.Distance(Input.mousePosition, user.DartStart);
    }

    public void ChangeWeapon(WeaponBase weaponToUse)
    {
        timebetweenAttack = weaponToUse.GetAtkSpeed();
        weaponUsed = weaponToUse;
        if(weaponUsed.GetID() == 1)
        {
            GetComponent<Animator>().SetBool("usingSpear", true);
            GetComponent<Animator>().SetBool("usingBlowGun", false);
        }
        else if(weaponUsed.GetID() == 2)
        {
            GetComponent<Animator>().SetBool("usingBlowGun", true);
            GetComponent<Animator>().SetBool("usingSpear", false);
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spearAttackPos.position, spearAttackRange);
    }

}
