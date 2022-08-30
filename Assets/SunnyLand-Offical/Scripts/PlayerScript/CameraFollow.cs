using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Tham chiếu tới mục tiêu
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;
    public Vector3 minValues, maxValues;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;  
    }

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        // Define minimum x,y,z values and maximum x,y,z values


        // Đặt vị trí của camera = vị trí của mục tiêu + vector(ở đây cần + -z để cam có thể nhìn được mục tiêu )
        // vì camera = mục tiêu ở cả x y và z
        Vector3 targetPosition = target.position + offset;
        // Verify if the targetPosition is out of bound or not
        // Limit it to the min and max values 
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
