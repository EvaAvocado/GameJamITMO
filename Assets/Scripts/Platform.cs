using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
    
    public void EnableCollider()
    {
        _collider.enabled = true;
    }
}
