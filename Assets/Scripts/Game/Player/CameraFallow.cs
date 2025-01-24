
#region INCLUDES

using UnityEngine;


#endregion

public class CameraFallow : MonoBehaviour {

    #region VARIABLES

    
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.3f;

    #endregion
    
    #region METHODS


    private Vector3 _velocity;

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref _velocity, smoothTime);
    }
    
    #endregion
}