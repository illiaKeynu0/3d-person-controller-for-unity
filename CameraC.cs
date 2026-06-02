using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _pivot;
    private LayerMask _collisionMask;
    
    private float _originalCameraDistance;
    
    private void Awake()
    {
        _collisionMask = LayerMask.GetMask("Obstructions");
    }

    private void Start()
    {
        _pivot = GameObject.FindGameObjectWithTag("CameraPivot").GetComponentInParent<Transform>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _originalCameraDistance = 6f;
    }
    
    private void LateUpdate()
    {
        var isCameraBlocked = Physics.Raycast(_pivot.position, -transform.forward, out var hit, _originalCameraDistance, _collisionMask);
        
        if (isCameraBlocked)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.MoveTowards(transform.localPosition.z, -hit.distance,  50 * Time.deltaTime));
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.MoveTowards(transform.localPosition.z,-_originalCameraDistance , 50 * Time.deltaTime));
        }
    }
}