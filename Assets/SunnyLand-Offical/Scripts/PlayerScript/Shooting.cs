using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject shootingItem;   // bắn vật phẩm  
    public Transform shootingPoint;   // điểm bắn
    public bool canShoot = true;     // 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (!canShoot)
            return;

        GameObject si = Instantiate(shootingItem, shootingPoint);
        si.transform.parent = null;
    }
}
