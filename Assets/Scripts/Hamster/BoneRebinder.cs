using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoneRebinder : MonoBehaviour
{
    [SerializeField] private Transform _headPosition;
    [SerializeField] private Transform _root;
    [SerializeField] private List<Rigidbody> _rigidbodies;
    [SerializeField] private List<Joint> _joints;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void RebindeBones()
    {
        foreach (var item in _joints)
        {
            Destroy(item);
        }
        foreach (var item in _rigidbodies)
        {
            Destroy(item);
        }
        _headPosition.SetParent(_root);
        _animator.Rebind();
    }
}
