using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    Transform targetPos;
    Player target;
    Vector3 moveDirection;
    [SerializeField] int damage = 10;
    [SerializeField] float speed = 10f;
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.Find("PlayerPrefab").transform;
        target = targetPos.GetComponent<Player>();
        moveDirection = targetPos.position - transform.position;
        transform.right = new Vector2(moveDirection.x, moveDirection.y);
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 9f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            target.StatsObject.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
