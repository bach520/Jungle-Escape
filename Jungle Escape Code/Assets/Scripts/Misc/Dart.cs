using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    #region variables
    private Vector3 shootDirection;
    private int damage;
    #endregion
    
    public void SetUp(Vector3 _shootDirection, int _damage)
    {
        this.shootDirection = _shootDirection;
        damage = _damage;
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(shootDirection) - 90);
        Destroy(gameObject, 3f);
    }
    private void Update()
    {
        float moveSpeed = 5f;
        transform.position += shootDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 3)
        {
            Debug.Log("Found enemy");
            if(other.gameObject.GetComponent<Enemy>())
            {
                other.gameObject.GetComponent<Enemy>().Stats.TakeDamage(damage);
            }
            else if(other.gameObject.GetComponent<MonkeyManager>())
            {
                other.gameObject.GetComponent<MonkeyManager>().Stats.TakeDamage(damage);
                Debug.Log("Monkey hit");
            }
            else if (other.gameObject.GetComponent<FinalBossManager>())
            {
                other.gameObject.GetComponent<FinalBossManager>().Stats.TakeDamage(damage);
                Debug.Log("Boss hit");
            }
            Destroy(gameObject);
        }
    }

    private float GetAngleFromVector(Vector3 direction)
    {
        direction = direction.normalized;
        float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(n < 0) n += 360;
        return n;
    }
}
