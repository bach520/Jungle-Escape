using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject user;
    private GameObject target;

    Rigidbody2D rigidBody;
    private int damage;

    private Vector2 throwPos;
    private Vector2 targetPos;

    private float time;
    private float currentTime;
    private float distanceX, distanceY,
        nextX,
        baseY,
        height;
    bool reachTarget = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        user = transform.parent.gameObject;
        target = GameObject.FindGameObjectWithTag("Player");
        damage = user.GetComponent<MonkeyManager>().Stats.GetDamage();

        throwPos = user.transform.Find("ShootPoint").position;
        targetPos = target.transform.position;
        distanceX = targetPos.x - throwPos.x;
        distanceY = targetPos.y - throwPos.y;
        time = 0.6f + Mathf.Abs(distanceX) * 0.1f; // time increases as distance increases
        transform.parent = null;
    }
    private void Update()
    {
        currentTime = currentTime + Time.deltaTime;
        float veloX = distanceX / time;
        float veloY = distanceY / time + 4.9f * time - 9.8f * currentTime;

        rigidBody.velocity = new Vector2(veloX, veloY);
        transform.rotation = LookAtTarget(transform.right - transform.position);
    }

    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg); // rotate projectile in z axis
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            target.GetComponent<Player>().StatsObject.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
