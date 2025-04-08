using Unity.Cinemachine;
using UnityEngine;

public class RotateAxisScript : MonoBehaviour
{
    private ViewSwitchCollider[] viewSwitchObjects;

    public Camera mainCamera;
    public CinemachineCamera vcam;
    private bool is3DMode = true;

    private void Awake()
    {
        viewSwitchObjects = FindObjectsOfType<ViewSwitchCollider>();
    }

    private void Start()
    {
        ApplyView(); // 시작할 때 현재 뷰에 맞게 설정
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))  //쿨타임 만들기
        {
            SwitchView();
        }
    }

    private void SwitchView()
    {
        is3DMode = !is3DMode;
        ApplyView();
    }

    private void ApplyView()
    {
        mainCamera.orthographic = !is3DMode;
        mainCamera.transform.position = is3DMode ? new Vector3(0, 5, -10) : new Vector3(0, 0, -10);

        if (is3DMode)
        {
            vcam.transform.rotation = Quaternion.Euler(25, 0, 0);
        }
        else
        {
            vcam.transform.rotation = Quaternion.identity;
        }

        foreach (var obj in viewSwitchObjects)
        {
            obj.SetView(is3DMode);
        }
    }
    
    
    
}
