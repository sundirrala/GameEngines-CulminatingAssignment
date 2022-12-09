using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigBod;

    [SerializeField]
    [Range(0, 10)]
    private float MoveSpeed = 10.0f;
    public Animator animator;

    Vector3 movement;

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

        Vector3 currentPosition = transform.position;

        //rigBod.MovePosition(currentPosition + new Vector3(movement.x, movement.y, 0) * MoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collided with: " + collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player triggered with: " + collision.gameObject.name);

    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        SavePlayerData data = SaveSystem.LoadPlayer();

        Vector3 pos;
        pos.x = data.pos[0];
        pos.y = data.pos[1];
        pos.z = data.pos[2];
        transform.position = pos;

        Debug.Log("The player's position is " + pos);
    }
}
