using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnim : MonoBehaviour
{
    [SerializeField] float hurtAnimDuration = 0.5f;

    Animator animator;

    private void OnEnable()
    {
        PlayerHealth.INSTANCE.OnTakeDamage += OnTakeDamage;
    }

    private void OnDisable()
    {
        PlayerHealth.INSTANCE.OnTakeDamage -= OnTakeDamage;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTakeDamage()
    {
        StartCoroutine(OnTakeDamage_Corou(hurtAnimDuration));
    }

    IEnumerator OnTakeDamage_Corou(float hurtAnimDuration)
    {
        float timeLeft = hurtAnimDuration;

        animator.SetTrigger("isHurting");

        while (timeLeft >0)
        {
            foreach (AnimatorControllerParameter animPara in animator.parameters)
            {
                if (animPara.type == AnimatorControllerParameterType.Bool)
                    animator.SetBool(animPara.name, false);
            }

            yield return null;
            timeLeft -= Time.deltaTime;
        }
    }
}
