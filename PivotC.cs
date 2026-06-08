using UnityEngine;

public class PivotController : MonoBehaviour
{
    public Transform player;
    private Camera _camera;

    private bool _wasTargeting, _isRecoveringRotation;
    private float _vRotation, _hRotation;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.position = player.position;
        
        if (_wasTargeting && !TargetFinder.Instance.OnTarget)
        {
            _isRecoveringRotation = true;

            _vRotation = Mathf.DeltaAngle(0, transform.eulerAngles.x);
            _hRotation = transform.eulerAngles.y;
        }
        
        if (TargetFinder.Instance.OnTarget)
        {
            var targetViewPos = _camera.WorldToViewportPoint(TargetFinder.Instance.CurrentTarget.position);
            var dir = TargetFinder.Instance.CurrentTarget.position - player.position;

            if (!Mathf.Approximately(targetViewPos.y, 0.67f))
            {
                _vRotation -= (targetViewPos.y - 0.67f) * 2;
            }

            _hRotation = Mathf.MoveTowardsAngle(_hRotation,
                Quaternion.LookRotation(dir, transform.up).eulerAngles.y, 200 * Time.deltaTime);
        }
        else
        {
            if (_isRecoveringRotation)
            {
                switch (_vRotation)
                {
                    case > 70:
                        _vRotation = Mathf.MoveTowards(_vRotation, 70, 1);
                        break;
                    case < -20:
                        _vRotation = Mathf.MoveTowards(_vRotation, -20, 1);
                        break;
                    default:
                        _isRecoveringRotation = false;
                        break;
                }
            }
            else
            {
                _vRotation -= PlayerInput.Instance.MouseInput.y;
                _hRotation += PlayerInput.Instance.MouseInput.x;
            }
        }
        
        _vRotation = Mathf.Clamp(_vRotation, -20, 70);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_vRotation, _hRotation, 0), 15);

        _wasTargeting = TargetFinder.Instance.OnTarget;
    }
}