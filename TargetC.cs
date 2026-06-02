using UnityEngine;

public class TargetC : MonoBehaviour
{
    private Camera _camera;
    private LayerMask _layerMask;
    
    private bool _isVisible;

    private void Start()
    {
        _camera = Camera.main;
        _layerMask = LayerMask.GetMask("Obstructions");
    }

    private void Update()
    {
        _isVisible = !Physics.Raycast(transform.position, _camera.transform.position - transform.position, out _, Vector3.Distance(transform.position, _camera.transform.position), _layerMask);
        
        if (_isVisible)
        {
            TargetFinder.Instance.AddTarget(transform);
        }
        else
        {
            TargetFinder.Instance.RemoveTarget(transform);
        }
    }

    public void ActiveTarget()
    {
        gameObject.layer = 8;
    }

    public void NonActiveTarget()
    {
        gameObject.layer = 6;
    }
}