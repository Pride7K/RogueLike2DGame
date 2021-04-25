using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject impactEffect;
    public float speed = 7.5f;
    public Rigidbody2D theRB;

    public int bulletDamage = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            other.GetComponent<EnemyController>().DamageEnemy(bulletDamage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
