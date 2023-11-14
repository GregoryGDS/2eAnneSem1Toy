using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerBehavior : MonoBehaviour
{

    public Transform _toAvoid; // Le player à éviter
    public float _mindist = 20f; // La distance minimum à laquelle il reste
    public float _speed;
    public float _minSpeed;
    public float _maxSpeed;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("StayAway", .5f, .5f);

    }

    // Update is called once per frame
    void Update()
    {
    }


    public void StayAway()
    {
        // Calculer la direction pour s'éloigner
        Vector3 _distToAvoid = transform.position - _toAvoid.position;

        // Calculer la distance actuelle entre l'objet et l'objet à éviter
        float _distCurrent = _distToAvoid.magnitude;


        float _dynamicSpeed = Mathf.Clamp(_distCurrent / _mindist, 0f, 1f) * (_maxSpeed - _minSpeed) + _minSpeed;


        // Si la distance actuelle inférieure à la distance minimale, nouvelle destination
        if (_distCurrent < _mindist)
        {

            Vector3 _newDist = transform.position + _distToAvoid.normalized * _mindist;
            _agent.SetDestination(_newDist);
            Debug.DrawRay(transform.position, _newDist);

        }

        _agent.speed = _dynamicSpeed;

    }
}
