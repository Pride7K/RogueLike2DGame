using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private Vector2 moveInput;

    public Transform gunHand;

    private Camera camera;

    public Animator anim;


    public Rigidbody2D theRB;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        
        moveInput.Normalize();
        //transform.position += new Vector3(
        //  moveInput.x* Time.deltaTime * moveSpeed,
        //moveInput.y* Time.deltaTime * moveSpeed,
        //0f);

        theRB.velocity = moveInput * moveSpeed;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = camera.WorldToScreenPoint(transform.localPosition);

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunHand.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunHand.localScale = Vector3.one;
        }


        // rotate gun arm
        Vector2 offset = new Vector2(
            mousePosition.x - screenPoint.x,
            mousePosition.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunHand.rotation = Quaternion.Euler(0, 0, angle);

        anim.SetBool("isMoving",moveInput != Vector2.zero);

    }
}
