using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public float _patrolTime = 10f;
    public float _aggroRange = 10f;
    public Transform[] waypoints;

    private int _index;
    private float _speed;
    private float _agentSpeed;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;

    private void Awake()
    {
        //_animator=
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent != null)
        {
            _agentSpeed = _navMeshAgent.speed;
        }
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, _patrolTime);
        }
    }

    private void Patrol()
    {
        _index = _index == waypoints.Length - 1 ? 0 : _index + 1;
    }

    private void Tick()
    {
        _navMeshAgent.destination = waypoints[_index].position;
        _navMeshAgent.speed = _agentSpeed / 5;

        if ((_playerTransform != null) && (UnityEngine.Vector3.Distance(transform.position, _playerTransform.position) < _aggroRange))
        {
            _navMeshAgent.destination= _playerTransform.position;
            _navMeshAgent.speed = _agentSpeed;
        }
    }
}
