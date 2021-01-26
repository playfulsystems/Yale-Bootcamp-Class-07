﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GroundCheck groundCheck;
    public GameObject highlight;
    public AudioClip smallSound;
    public AudioClip fastSound;
    public AudioClip footSound;
    float speed = 10f;
    float jumpForce = 5f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.tag = "Player";
    }

    void Update()
	{
        // if space pressed and grounded (from GroundCheck script on GroundCollider) is true
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.grounded)
		{
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
	}


    void FixedUpdate()
    {
        // add horizontal movement to move Vector
        float hInput = Input.GetAxis("Horizontal");
        Vector3 move = Vector3.right * hInput * speed * Time.deltaTime;

        // add vertical movement to the move Vector
        float vInput = Input.GetAxis("Vertical");
        move += Vector3.forward * vInput * speed * Time.deltaTime;

        // rotate character to direction its moving based on the move vector
        transform.LookAt(transform.position + move);

        // move character
        //transform.position += move;
        rb.MovePosition(transform.position + move);

        // update our Animator parameter curRunSpeed
        GetComponent<Animator>().SetFloat("curRunSpeed", move.magnitude);
    }

    // called from Animation Event specified in "Run" animation
    void Footstep()
	{
        GetComponent<AudioSource>().PlayOneShot(footSound, 0.5f);
    }

    // co-routine
    IEnumerator SpeedBoostRoutine()
	{
        yield return new WaitForSeconds(5f);

        highlight.SetActive(false);
        speed = 5f;
    }

    // one of the gameobjects must be a rigidbody, both must have colliders
    // one must have trigger checked on the collider
	void OnTriggerEnter(Collider col)
	{
        if (col.CompareTag("small"))
		{
            // destroy object colliding with
            Destroy(col.gameObject);

            // shrink player
            transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

            // play passed in sound
            GetComponent<AudioSource>().PlayOneShot(smallSound);
        }
        if (col.CompareTag("fast"))
        {
            // destroy object colliding with
            Destroy(col.gameObject);

            // set speed
            speed = 20f;

            // play passed in sound
            GetComponent<AudioSource>().PlayOneShot(fastSound);

            // active highlight
            highlight.SetActive(true);

            // co-routine to turn off powerup 5 sec later
            StartCoroutine(SpeedBoostRoutine());
        }
    }
}
