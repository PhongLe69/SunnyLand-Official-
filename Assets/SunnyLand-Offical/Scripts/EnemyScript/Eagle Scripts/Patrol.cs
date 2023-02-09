using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float waitTime;
    public Transform[] moveSpots;

    private int randomSpot;
    private float lastXVal;


    void Start()
    {
        StartCoroutine(Move());
        //moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void Update()
    {
        if (transform.position.x < lastXVal)
        {
            //Update lastXVal
            lastXVal = transform.position.x;
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x > lastXVal)
        {
            //Update lastXVal
            lastXVal = transform.position.x;
            spriteRenderer.flipX = true;
        }
    }

IEnumerator Move()
    {
        while (true)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
            while (Vector3.Distance(transform.position, moveSpots[randomSpot].position) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}