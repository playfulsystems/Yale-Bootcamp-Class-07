using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
	public TMP_Text dialogText;

	// a public array of strings we can set in the inspector
	public string[] dialog;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// choosing a random number to use as an index for our array
			int randIndex = Random.Range(0, dialog.Length);

			// setting the text
			dialogText.text = dialog[randIndex];

			// showing the dialog canvas
			GetComponentInChildren<Canvas>().enabled = true;
		}
		
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// hiding the dialog canvas
			GetComponentInChildren<Canvas>().enabled = false;
		}
	}
}
