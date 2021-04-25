using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;

    public Animator animator;
    public float moveSpeed;

    public GameObject hitEffect;
    private Vector3 moveDirection;

    public float rangeToChasePlayer;

    public GameObject[] deathSplatters;

    public bool shootPlayer;

    public GameObject bullet;

    public Transform firePoint;

    public float shootRange;

    public float fireRate = 1;

    private float fireCounter;

    public SpriteRenderer theBody;
    public int health = 150;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (theBody.isVisible)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

            if (PlayerController.instance.transform.position.x >= 0)
            {
                           Debug.Log(PlayerController.instance.transform.position.x + " - " + screenPoint.x);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else{
                transform.localScale = Vector3.one;
            }

            moveDirection.Normalize();

            theRB.velocity = moveDirection * moveSpeed;




            if (shootPlayer && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= shootRange)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;

                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                }
            }
        }
        animator.SetBool("isMoving", moveDirection != Vector3.zero);

    }

    public void DamageEnemy(int damage)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(
                deathSplatters[Random.Range(0, deathSplatters.Length)],
                transform.position,
                Quaternion.Euler(0f, 0f, Random.Range(0, 4) * 90f));
        }
    }
}
