using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;
    private int _currentTarget = 0;
    private NavMeshAgent _agent;
    private bool _reverse = false;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints.Count > 0 && wayPoints[_currentTarget] != null)
        {
            _agent.SetDestination(wayPoints[_currentTarget].position);

            float distance = Vector3.Distance(transform.position, wayPoints[_currentTarget].position);

            if (distance < 1.0f)
            {
                if (_reverse == true)
                {
                    _currentTarget--;
                    if (_currentTarget == 0)
                    {
                        _reverse = false;
                        _currentTarget = 0;
                    }
                }
                else
                {
                    _currentTarget++;
                    if (_currentTarget == wayPoints.Count)
                    {
                        _reverse = true;
                        _currentTarget--;
                    }
                }
            }
        }
    }
}
