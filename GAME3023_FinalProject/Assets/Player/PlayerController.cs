using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float MoveSpeed = 2.0f;
    public Animator animator;

    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        //If player goes left
        if (movement.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        //If player goes right
        else if (movement.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        
        transform.position += new Vector3(movement.x, movement.y, 0) * MoveSpeed * Time.deltaTime;
    }

    
}
