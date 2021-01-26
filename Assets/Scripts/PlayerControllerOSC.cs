using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOSC : MonoBehaviour
{
    public GroundCheck groundCheck;
    public AudioClip footSound;
    float speed = 10f;
    float jumpForce = 5f;
    Rigidbody rb;
    float hInput;
    float vInput;
    public OSC osc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.tag = "Player";

        // setting the address to look at (remember the first /)!
        osc.SetAddressHandler("/3/xy", GetMovement);
    }

    void GetMovement(OscMessage message)
	{
        // making the data go from -1 to 1 instead of 0 to 1
        float x = message.GetFloat(0) * 2f - 1f;
        float y = message.GetFloat(1) * 2f - 1f;

        Debug.Log(x + "," + y);

        // using the OSC data to move our character
        hInput = x;
        vInput = -y;
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
        //float hInput = Input.GetAxis("Horizontal");
        Vector3 move = Vector3.right * hInput * speed * Time.deltaTime;

        // add vertical movement to the move Vector
        //float vInput = Input.GetAxis("Vertical");
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

}
