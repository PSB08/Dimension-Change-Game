using DG.Tweening;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        #region Test

        transform.Translate(Vector3.right * (speed * Time.deltaTime));

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        
        
        #endregion
        
        
        
        
    }
    
    
}
