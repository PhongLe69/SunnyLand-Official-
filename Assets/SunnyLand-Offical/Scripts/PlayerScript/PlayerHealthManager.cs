using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    //[SerializeField] int baseHealth = 4;

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
        PlayerHealth.INSTANCE.TakeDamage(1);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Return))
            LoseLife();*/
    }
}
