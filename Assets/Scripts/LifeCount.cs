using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaning;

    /*private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }*/

    // 4 lives - 4 images (0, 1, 2, 3)
    // 3 lives - 3 images (0, 1, 2, [3])
    // 2 lives - 2 images (0, 1, [2], [3])
    // 1 live - 1 image (0, [1], [2], [3])
    // 0 live - 0 image [0, 1, 2, 3] LOSE
    public void LoseLife()
    {
        // If no lives remaining do nothing
        if (livesRemaning == 0)
            return;

        // Decrease the value of liveRemaining
        livesRemaning--;
        // Hild one of the life image
        lives[livesRemaning].enabled = false;

        // If we run out of lives we lose the game.
        if(livesRemaning == 0)
        {
            FindObjectOfType<Fox>().Die();
        }

    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Return))
            LoseLife();*/
    }
}
