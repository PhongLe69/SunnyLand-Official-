using UnityEngine;
using UnityEngine.Events;

public class ShootingAction : MonoBehaviour
{
    public UnityEvent action;  // sự kiện hành động

    public void Action()
    {
        action?.Invoke();
    }
}
