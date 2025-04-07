using DG.Tweening;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float speed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }
    
    
}
