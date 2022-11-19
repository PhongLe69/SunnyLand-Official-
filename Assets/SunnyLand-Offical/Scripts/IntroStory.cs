using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroStory : MonoBehaviour
{
    private void OnEnable()
    {
        // Only specifying the sceneName of sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene("Introduce", LoadSceneMode.Single);
    }
}
