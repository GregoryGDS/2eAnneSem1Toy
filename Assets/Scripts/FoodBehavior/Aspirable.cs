using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Aspirable : MonoBehaviour
{
    public FoodType _type = FoodType.Normal;
    public float _mass = 1f;
    public Rigidbody _rb;
    public NavMeshAgent _agent;
    public bool _isAspirated;

    public bool _isDestroy; // siva être détruit, pour ne pas être à nouveaux ajouté

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        if (_agent.enabled)
        {
            _rb.isKinematic = false;
        }
        
    }


    public void StartAspiration()
    {
        if (_type == FoodType.Runner && _agent.enabled)
        {
            _agent.enabled = false;
            _rb.isKinematic = true;
        }

        _isAspirated = true;

        /*
        if (_agent.enabled)
        {
            _rb.isKinematic = true;
        }
        */
    }

    public void EndAspiration()
    {
        if (_type == FoodType.Runner && _agent.enabled)
        {
            _agent.enabled = true;
            _rb.isKinematic = false;

        }

        _isAspirated = false;

        /*
        if (_agent.enabled)
        {
            _rb.isKinematic = false;
        }
        */
    }


    public void DestroySelf()
    {
        
        _isDestroy = true;

        Destroy(gameObject);

        //desactive et dans game master après 1s supprimer les objet desactivé ? 

        Debug.Log("in DestroySelf, count list : " + GameManager.Instance._vacuumScript._aspirableList.Count);
        
    }
}
