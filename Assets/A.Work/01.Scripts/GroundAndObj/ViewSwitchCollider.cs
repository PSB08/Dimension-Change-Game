using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ViewSwitchCollider : MonoBehaviour
{
    [Tooltip("3D 모드에서 통과 가능한가?")] public bool passableIn3D;

    private Collider objCollider;

    private void Awake()
    {
        objCollider = GetComponent<Collider>();
    }

    public void SetViewMode(bool is3DMode)
    {
        // 3D 모드일 때 통과 가능하면 => 충돌 OFF (enabled = false)
        // 2D 모드일 때 통과 가능하지 않으면 => 충돌 ON (enabled = true)
        objCollider.enabled = is3DMode != passableIn3D;
    }

}