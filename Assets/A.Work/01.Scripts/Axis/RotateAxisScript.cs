using UnityEngine;
using Unity.Cinemachine;

namespace Script.Axis
{
    public class RotateAxisScript : MonoBehaviour
    {
        [Header("Camera Settings")]
        public Camera mainCamera;
        public CinemachineCamera virtualCamera;

        private ViewSwitchCollider[] viewSwitchObjects;
        private ViewSwitchShow[] viewShowObjects;
        private bool is3DMode = true;

        private void Awake()
        {
            viewSwitchObjects = FindObjectsOfType<ViewSwitchCollider>();
            viewShowObjects = FindObjectsOfType<ViewSwitchShow>();
        }

        private void Start()
        {
            UpdateView();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleViewMode();
            }
        }

        private void ToggleViewMode()
        {
            is3DMode = !is3DMode;
            UpdateView();
        }

        private void UpdateView()
        {
            mainCamera.orthographic = !is3DMode;
            mainCamera.transform.position = is3DMode
                ? new Vector3(0, 5, -10)
                : new Vector3(0, 0, -10);
            
            virtualCamera.transform.rotation = is3DMode
                ? Quaternion.Euler(25, 0, 0)
                : Quaternion.identity;
            
            foreach (var switchable in viewSwitchObjects)
            {
                switchable.SetViewMode(is3DMode);
            }

            foreach (var showable in viewShowObjects)
            {
                showable.SetShowMode(is3DMode);
            }
        }
        
        
        
    }
}