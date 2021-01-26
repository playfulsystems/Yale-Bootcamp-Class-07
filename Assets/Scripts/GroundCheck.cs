using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
	public bool grounded = false;

	// when collider triggers with ground set grounded to true
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("ground"))
		{
			grounded = true;
		}
	}

	// when collider triggers with ground set grounded to false
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("ground"))
		{
			grounded = false;
		}
	}

}
