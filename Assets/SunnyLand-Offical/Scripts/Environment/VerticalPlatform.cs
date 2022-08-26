using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalMethods;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;

    private float timeOfFirstButton;

    private bool firstButtonPressed = false;
    private bool reset;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.X))
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
        }*/

      /* if (Input.GetKeyDown(KeyCode.S) && firstButtonPressed)
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

        if (IsDoublePressed(KeyCode.S, 0.2f))
        {
            StartCoroutine(FlipEffector(0.3f));
        }
    }

    public bool IsDoublePressed(KeyCode keyCode, float pressInterval)
    {
        if (Input.GetKeyDown(keyCode) && !firstButtonPressed)
        {
            firstButtonPressed = true;

            //After 'pressInterval' amount of time, firstButtonPressed = false
            StartCoroutine(DelayedInvoke(pressInterval, () => firstButtonPressed = false));
        }
        else if (Input.GetKeyDown(keyCode) && firstButtonPressed)
        {
            return true;
        }

        return false;
    }

    IEnumerator FlipEffector(float flipDuration)
    {
        effector.rotationalOffset = 180f;

        yield return new WaitForSeconds(flipDuration);

        effector.rotationalOffset = 0f;
    }
}
