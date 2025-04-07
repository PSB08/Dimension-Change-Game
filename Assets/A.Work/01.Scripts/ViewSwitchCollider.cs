using UnityEngine;

public class ViewSwitchCollider : MonoBehaviour
{
    public bool passable3DMode;  //3D일 때 통과가 가능한지
    private Collider col;

    private void Awake() => col = GetComponent<Collider>();

    public void SetView(bool is2D)
    {
        col.enabled = is2D == !passable3DMode;
    }
}
