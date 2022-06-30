using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public float speed; // tốc độ

    private void Update()
    {
        transform.Translate(transform.right * transform.localScale.x * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)  // kích hoạt phần tử 2D
    {
        if (collision.tag == "Player")
            return;

        // Trigger the custom action on the other object IF IT EXISTS  
        // kích hoạt hành động lên đối tượng khác nếu ĐỐI TƯỢNG ĐÓ TỒN TẠI
        if (collision.GetComponent<ShootingAction>())
            collision.GetComponent<ShootingAction>().Action();
        // Destroy (sau đó là phá hủy)
        Destroy(gameObject);

    }
}
