using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) {
			GameManager.Instance.ProcessInput (Input.mousePosition, TouchPhase.Began);
		} 
		else 
		{
			if (Input.GetMouseButton (0)) {
				GameManager.Instance.ProcessInput (Input.mousePosition, TouchPhase.Moved);
			} else {
				if (Input.GetMouseButtonUp (0)) {
					GameManager.Instance.ProcessInput (Input.mousePosition, TouchPhase.Ended);
				}
			}
		}

		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			switch (touch.phase) 
			{
			case TouchPhase.Began:
				GameManager.Instance.ProcessInput (touch.position, TouchPhase.Began);
				break;

			case TouchPhase.Ended:
				GameManager.Instance.ProcessInput (touch.position, TouchPhase.Ended);
				break;

			default:
				GameManager.Instance.ProcessInput (touch.position, TouchPhase.Moved);
				break;
				
			}
		}
	}
}
