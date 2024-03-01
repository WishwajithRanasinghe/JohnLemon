using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    private NavMeshAgent _navMesh;
    [SerializeField] private Transform[] _wayPoints;
    private int _currentWayPointIndex;
    private void Start()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _navMesh.SetDestination(_wayPoints[0].position);        
    }

    // Update is called once per frame
    private void Update()
    {
        if(_navMesh.remainingDistance < _navMesh.stoppingDistance)
        {
            _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;
            _navMesh .SetDestination(_wayPoints[_currentWayPointIndex].position);
        }
        
    }
}
