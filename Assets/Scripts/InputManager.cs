using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameManager gameManager;
	
	void Update () {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            gameManager.ProcessInput(Input.mousePosition, TouchPhase.Began);
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                gameManager.ProcessInput(Input.mousePosition, TouchPhase.Ended);
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    gameManager.ProcessInput(Input.mousePosition, TouchPhase.Moved);
                }
            }
        }

#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    gameManager.ProcessInput(touch.position, TouchPhase.Began);
                    break;

                case TouchPhase.Ended:
                    gameManager.ProcessInput(touch.position, TouchPhase.Ended);
                    break;

                case TouchPhase.Canceled:
                    gameManager.ProcessInput(touch.position, TouchPhase.Ended);
                    break;

                default:
                    gameManager.ProcessInput(touch.position, TouchPhase.Moved);
                    break;
            }
        }
#endif
    }
}
