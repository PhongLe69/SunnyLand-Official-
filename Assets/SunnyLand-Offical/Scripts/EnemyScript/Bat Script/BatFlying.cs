using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlying : MonoBehaviour
{

    public GameObject target;
    public float rotationSpeed;
    public float speed;
    Rigidbody2D rg2d;

    // Use this for initialization
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        //InvokeRepeating("changePosition",5,5);
    }

    void changePosition()
    {
        float x = Random.Range(-9.0f, 9.0f);
        float y = Random.Range(-6.0f, 6.0f);
        target.transform.position = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * rotationSpeed);
        rg2d.velocity = transform.right * speed;
        float dist = Vector2.Distance(target.transform.position, transform.position);
        if (dist < 2)
        {
            changePosition();
        }
    }
}