using UnityEngine;

public class HitTrap : Trap
{
    [SerializeField] private float _pausedTime = 2.5f;
    [SerializeField] private float _minSaveTime;
    [SerializeField] private float _maxSaveTime;

    protected float _currentTime = 0;

    private const string _hit = "Hit";

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime>_pausedTime)
        {
            _currentTime = 0;
            _animator.SetTrigger(_hit);
        }
        if (_currentTime > _minSaveTime && _currentTime < _maxSaveTime)
            _runPermissionForAI = true;
        else
            _runPermissionForAI = false;
    }
}
