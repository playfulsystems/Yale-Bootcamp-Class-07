using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKinematicForward : MonoBehaviour
{
    float speed = 4f;

    void FixedUpdate()
    {
        Vector3 move = transform.forward * speed * Time.deltaTime;
        GetComponent<Rigidbody>().MovePosition(transform.position + move);

        Debug.Log(Time.deltaTime);
    }
}
