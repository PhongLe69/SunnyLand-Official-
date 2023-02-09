using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public new BoxCollider2D collider2D;

    protected Animator animator;

    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;
    
    private float lastXVal;

    private Vector2 objectPosition;

    private float speedModifier;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        //coroutineAllowed = true;
        lastXVal = transform.position.x;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }

        if (transform.position.x < lastXVal)
        {
            //Update lastXVal
            lastXVal = transform.position.x;
            spriteRenderer.flipX = true;
        }
        else if (transform.position.x > lastXVal)
        {
            //Update lastXVal
            lastXVal = transform.position.x;
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            coroutineAllowed = true;
            collider2D.enabled = false;
            animator.SetBool("isFly", true);
            animator.SetBool("isIdle", false);
        }
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;


        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        animator.SetBool("isFly", false);
        animator.SetBool("isIdle", true);

        tParam = 0f;

        routeToGo += 1; 
        collider2D.enabled = true;
        

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        //coroutineAllowed = true;

    }

    
}
