using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoneRebinder : MonoBehaviour
{
    [SerializeField] private Transform _headPosition;
    [SerializeField] private Transform _root;

    [SerializeField] private List<Transform> _headBones;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void RebindeBones()
    {
        foreach (var bone in _headBones)
        {
            bone.SetParent(_headPosition);
        }
        _headPosition.SetParent(_root);
        _animator.Rebind();
    }
}
