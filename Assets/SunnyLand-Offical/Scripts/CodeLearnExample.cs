using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Có thể sử dụng được tất cả các function liên quan đến quản lí cảnh
using UnityEngine;

public class CodeLearnExample : MonoBehaviour
{
    Rigidbody2D rb;  // tạo biến 
    public GameObject winText; // khởi tạo biến winText cho UI
    float xInput;
    float zInput;

    public float speed; 


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject);  // Vật sẽ bị phá hủy ngay khi trò chơi bắt đầu
        Destroy(gameObject, 3f);  // Vật sẽ bị phá hủy sau khi bắt đầu được 3 giây 

        rb = GetComponent<Rigidbody2D>();  // dùng biến để lấy các thành phần bên trong Rigidbody (trong Start)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Destroy(gameObject);  // Vật sẽ bị phá hủy sau khi nhấn nút “space”
            rb.AddForce(Vector3.up * 500);  // Vật bị tác động 1 lực hướng lên sau khi nhấn “space”
        }

        rb.velocity = Vector3.forward * 20f;  // thêm vận tốc cho rb, làm cho vật tiến về phía trước

        #region Switch Scenes
        // File -> Build Settings -> Add Open Scenes -> để đặt tên cho Scenes hiện tại
        // File -> New Scenes -> chọn vị trí + đặt tên của cảnh 2 -> Save
        // File -> Build Settings -> Kéo thả Level2 vào ô Scenes In Build -> thì mới có thể load được cảnh
        if (Input.GetKeyDown(KeyCode.R))  // Nếu nút R được nhấn xuống
        {
            SceneManager.LoadScene("Level2"); // Thì trình quản lí cảnh sẽ load cảnh Level2
        }
        #endregion
        
        // Di chuyển trái phải (giá trị của GetAxis -1 -> 1)
        xInput = Input.GetAxis("Horizontal");  // Nhận được giá trị của trục hoành khi nhấn nút <- -> hoặc a-d 
        // Di chuyển lên xuống
        zInput = Input.GetAxis("Vertical");  // Nhận được giá trị của trục tung khi nhấn nút "lên-xuống" hoặc w-s

        //rb.AddForce(xInput * speed, 0, zInput * speed);
        rb.AddForce(new Vector3(xInput * speed, 0, zInput * speed));

    }

    private void OnMouseDown()  // Nhấp chuột
    {
        Destroy(gameObject);  // Vật sẽ bị phá hủy sau khi nhấp chuột vào vật
    }

    private void OnCollisionEnter(Collision collision)  // Phát hiện va chạm 3D
    {
        if(collision.gameObject.tag == "Enemy") // Kiểm tra đối tượng mà vật thể va chạm có được gắng thẻ "Enemy" hay không.
        {
            Destroy(gameObject);            // Nếu có thì vật "không" gắn tag Enemy bị phá hủy
            Destroy(collision.gameObject);  // Nếu có thì vật "có" gắn tag Enemy bị phá hủy

            // Khi vật chạm nhau, 1 trong 2 sẽ biến mất.
            // Sau đó, dòng Text của UI hiện lên (Nhớ disable UI có text đó để nó biến mất và tham chiếu đến vật
            winText.SetActive(true);  // Thiết lập winText thành true(enable) và hiển thị lên màn hình. 
        }
    }


    
}
