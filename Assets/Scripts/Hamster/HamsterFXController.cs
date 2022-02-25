using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hamster))]
public class HamsterFXController : MonoBehaviour
{
    [SerializeField] private GameObject _spawnEffect;
    [SerializeField] private GameObject _dischargeEffect;
    [SerializeField] private GameObject _stunEffect;

    private Hamster _hamster;
    private HamsterMover _hamsterMover;
    private float _stunTimer = 0f;

    private void Awake()
    {
        _hamster = GetComponent<Hamster>();
        _hamsterMover = GetComponent<HamsterMover>();
    }

    private void Update()
    {
        if (_stunTimer > 0)
            _stunTimer -= Time.deltaTime;
        else
            _stunEffect.SetActive(false);
    }

    private void OnEnable()
    {
        _hamster.Discharged += InstantiateDischargeEffect;
        _hamster.Respauned += InstantiateSpawnEffect;
        _hamsterMover.Stuned += EnableStunEffect;
    }

    private void OnDisable()
    {
        _hamster.Discharged -= InstantiateDischargeEffect;
        _hamster.Respauned -= InstantiateSpawnEffect;
        _hamsterMover.Stuned -= EnableStunEffect;
    }

    public void InstantiateSpawnEffect()
    {
        Instantiate(_spawnEffect, transform.position,new Quaternion());
    }

    public void InstantiateDischargeEffect()
    {
        Instantiate(_dischargeEffect, transform.position,new Quaternion());
    }

    public void EnableStunEffect(float time)
    {
        _stunTimer = time;
        _stunEffect.SetActive(true);
    }
}
