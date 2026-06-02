using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public static TargetFinder Instance { get; private set; }
    public Transform CurrentTarget { get; private set; }
    public bool OnTarget { get; private set; }
    
    private List<Transform> _targets;
    private Camera _camera;

    private bool _cd;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _camera = Camera.main;
        
        _targets = new List<Transform>();
        
        OnTarget = false;
        CurrentTarget = null;
        _cd = false;
    }

    private void Update()
    {
        if (CurrentTarget && Vector3.Distance(_camera.transform.position, CurrentTarget.position) > 20)
        {
            TargetLockOff();
        }
    }

    public void TargetLockOn()
    {
        CurrentTarget = TargetFind();
        
        if (CurrentTarget)
        {
            CurrentTarget.gameObject.GetComponent<TargetC>().ActiveTarget();
            OnTarget = true;
        }
    }

    public void TargetLockOff()
    {
        CurrentTarget.gameObject.GetComponent<TargetC>().NonActiveTarget();
        CurrentTarget = null;
        OnTarget = false;
    }
    
    public void AddTarget(Transform T)
    {
        if (!_targets.Contains(T))
        {
            _targets.Add(T);
        }
    }

    public void RemoveTarget(Transform T)
    {
        if (_targets.Contains(T))
        {
            _targets.Remove(T);
        }
    }

    public void TargetSelect(int i)
    {
        var tCycle = new List<Transform>();

        foreach (var t in _targets)
        {
            if (_camera.WorldToViewportPoint(t.position).x is < 0 or > 1)
                continue;
            tCycle.Add(t);
        }
        
        tCycle.Sort((a,b) => _camera.WorldToViewportPoint(a.position).x.CompareTo(_camera.WorldToViewportPoint(b.position).x));

        if (!_cd && tCycle.Count > 1)
        {
            var currenIndex = tCycle.FindIndex(a => a.transform == CurrentTarget);
            
            var nextIndex = currenIndex + i;
            if (nextIndex >= 0 && nextIndex < tCycle.Count)
            {
                CurrentTarget.gameObject.GetComponent<TargetC>().NonActiveTarget();
                
                CurrentTarget = tCycle[nextIndex];
                CurrentTarget.gameObject.GetComponent<TargetC>().ActiveTarget();
                
                _cd = true;
                StartCoroutine(ResetCd());
            }
        }
    }

    private Transform TargetFind()
    {
        var topDot = 0f;
        Transform pickedTarget = null;

        foreach (var target in _targets)
        {
            var targetDot =
                Vector3.Dot(_camera.transform.forward.normalized, (target.transform.position - _camera.transform.position).normalized);
            if (targetDot < .7f) continue;

            if (targetDot >= topDot)
            {
                topDot = targetDot;
                pickedTarget = target;
            }
        }
        
        return pickedTarget;
    }
    
    private IEnumerator ResetCd()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        _cd = false;
    }
}