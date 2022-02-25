using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    protected bool _runPermissionForAI = true;
    protected Animator _animator;

    public event UnityAction Stoped;

    private const string _stop = "Stop";

    private void Awake()
    {
        transform.TryGetComponent(out Animator animator);
        _animator = animator;
    }

    public void StopTrapAnimation()
    {
        if (_animator != null)
            _animator.SetBool(_stop, true);
        else
            Stoped?.Invoke();
    }

    public bool RunPermissionForAI => _runPermissionForAI;
}
