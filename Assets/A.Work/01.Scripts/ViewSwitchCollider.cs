using UnityEngine;

public class ViewSwitchCollider : MonoBehaviour
{
    [Tooltip("3D일 때 통과가 가능한가")]
    public bool passable3DMode; 
    private Collider objCollider;

    private void Awake() => objCollider = GetComponent<Collider>();

    public void SetView(bool is2D)
    {
        objCollider.enabled = is2D == !passable3DMode;
    }
}
