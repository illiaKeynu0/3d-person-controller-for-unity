using UnityEngine;

public class TargetMarkerC : MonoBehaviour
{
    public RectTransform canvas, marker;
    
    private Camera _camera;
    private static Animator _animator;
    private static SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (TargetFinder.Instance.OnTarget)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas,
                _camera.WorldToScreenPoint(TargetFinder.Instance.CurrentTarget.position), _camera, out var point);
            marker.anchoredPosition = point;
        }
        else
        {
            _spriteRenderer.enabled = false;
            marker.anchoredPosition = new Vector2(0, 0);
        }
    }

    public static void MarkerActive()
    {
        _spriteRenderer.enabled = true;
        _animator.Play("TargetLock");
    }
}
