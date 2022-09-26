using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Fox thePlayer;

    public SpriteRenderer theSR;
    public Sprite chestOpenSprite;

    public bool chestOpen;
    public GameObject hideKey;

    /*[Header("Draw Curve")]
    public AnimationCurve moveCurve;

    public Vector3 targetPos;
    public Vector3 targetScale;

    public float duration = 1f;*/



    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Fox>();
        //StartCoroutine(ScaleObject(targetPos, 10f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            chestOpen = true;
            theSR.sprite = chestOpenSprite;
            hideKey.gameObject.SetActive(true);
        }
    }

    /*public IEnumerator MoveObject(Vector3 targetPos, float duration)
    {
        float time = 0;
        float rate = 1 / duration;
        Vector3 startPos = transform.localPosition;
        while (time < 1)
        {
            time += rate * Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, moveCurve.Evaluate(time));
            yield return 0;
        }
        transform.localPosition = targetPos;
    }

    public IEnumerator ScaleObject(Vector3 targetPos, float duration)
    {
        float time = 0;
        float rate = 1 / duration;
        while (time < 1)
        {
            time += rate * Time.deltaTime;
            transform.localScale = targetScale * moveCurve.Evaluate(time);
            yield return 0;
        }
        transform.localScale = targetScale;
    }*/
}
