using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCount : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;

    //Non-serialze cached objects
    List<GameObject> hearts = new List<GameObject>();

    private int remainingHealth;



    private void OnEnable()
    {
        PlayerHealth.INSTANCE.OnHealthChange += OnHealthChange;
    }

    private void OnDisable()
    {
        PlayerHealth.INSTANCE.OnHealthChange -= OnHealthChange;
    }

    private void Start()
    {
        OnHealthChange();

        for (int i = 0; i < remainingHealth; i++)
            hearts.Add(Instantiate(heartPrefab, this.gameObject.transform));
    }


    void OnHealthChange()
    {
        remainingHealth = PlayerHealth.INSTANCE.Health;

        //Hide the accessive hearts
        for (int i = remainingHealth; i<hearts.Count; i++)
            if (hearts[i].activeInHierarchy)
                hearts[i].SetActive(false);
    }

}
