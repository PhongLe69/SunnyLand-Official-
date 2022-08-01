using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    private float timeOfFirstButton;
    private bool firstButtonPressed;
    private bool reset;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            waitTime = 0.5f;
            effector.rotationalOffset = 0;
        }

        if (Input.GetKey(KeyCode.X))
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        /*if (Input.GetKeyDown(KeyCode.S) && firstButtonPressed)
        {
            if (Time.time - timeOfFirstButton < 0.5f)
            {
                effector.rotationalOffset = 180f;
            }
            else
            {
                Debug.Log("Too late");
            }

            reset = true;
        }

        if (Input.GetKeyDown(KeyCode.S) && !firstButtonPressed)
        {
            firstButtonPressed = true;
            timeOfFirstButton = Time.time;
        }

        if (reset)
        {
            firstButtonPressed = false;
            reset = false;
        }*/
    }
}
