using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can be extended for different types of controllers depending on platform
// Preprocessor directives (e.g. "#if UNITY_IOS") can be used to include only relevant code for each platform
// To learn more see https://docs.unity3d.com/Manual/PlatformDependentCompilation.html
public class InputController : MonoBehaviour {
	
	void Update () 
	{
#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    GameManager.Instance.ProcessInput(touch.position, TouchPhase.Began);
                    break;

                case TouchPhase.Ended:
                    GameManager.Instance.ProcessInput(touch.position, TouchPhase.Ended);
                    break;

                default:
                    GameManager.Instance.ProcessInput(touch.position, TouchPhase.Moved);
                    break;

            }
        }
#else
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
#endif
    }
}
