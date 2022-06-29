using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Vector2 playerInitPosition;

    void start()
    {
        playerInitPosition = FindObjectOfType<Fox>().transform.position;
    }

    public void Restart()
    {
        // 1- Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // 2- Reset the player's position, (reset the score counter, reapear coins in game.)
        // Save the playe's initial position when game start
        // When respawing simply reposition the player to that init position
        // Reset the player's movement speed
        /*FindObjectOfType<Fox>().ResetPlayer();
        FindObjectOfType<Fox>().transform.position = playerInitPosition;*/
    }
}
