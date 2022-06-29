using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Tham chiếu tới mục tiêu
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        // Đặt vị trí của camera = vị trí của mục tiêu + vector(ở đây cần + -z để cam có thể nhìn được mục tiêu )
        // vì camera = mục tiêu ở cả x y và z
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
