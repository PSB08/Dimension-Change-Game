using UnityEngine;

public class ViewSwitchShow : MonoBehaviour
{
    [Tooltip("3D 모드에서 볼 수 있는가?")] public bool showableIn3D;

    private MeshRenderer objRenderer;

    private void Awake()
    {
        objRenderer = GetComponent<MeshRenderer>();
    }

    public void SetShowMode(bool is3DMode)
    {
        objRenderer.enabled = is3DMode == showableIn3D;
    }
}
