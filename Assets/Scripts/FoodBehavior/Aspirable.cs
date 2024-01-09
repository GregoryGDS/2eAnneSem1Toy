using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Aspirable : MonoBehaviour
{
    public FoodType _type = FoodType.Normal;
    public float _mass = 1f;
    public float _massMax = 1f;

    public Rigidbody _rb;
    public NavMeshAgent _agent;
    public bool _isAspirated;

    public bool _isDestroy; // siva être détruit, pour ne pas être à nouveaux ajouté

    public Vector3 _newScale;
    public float _scaleSpeed = 1.5f;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
        if (_agent && _agent.enabled)
        {
            _rb.isKinematic = false;
        }
        _massMax = _mass;
        _newScale = transform.localScale; // initiale
    }

    private void Update()
    {

        transform.localScale = Vector3.MoveTowards(transform.localScale, _newScale, _scaleSpeed * Time.deltaTime);


    }

    public void lossMass(float _loss)
    {
        _mass -= _loss;
    }

    public void StartAspiration()
    {
        if (_type == FoodType.Runner && (_agent &&_agent.enabled))
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
        if (_type == FoodType.Runner && (_agent && _agent.enabled))
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

        //Debug.Log("in DestroySelf, count list : " + GameManager.Instance._vacuumScript._aspirableList.Count);
        
    }


    public void Rescale()
    {
        float _newScaleX = (transform.localScale.x * _mass) / _massMax;
        float _newScaleY = (transform.localScale.y * _mass) / _massMax;
        float _newScaleZ = (transform.localScale.z * _mass) / _massMax;

        _newScale = new Vector3(_newScaleX, _newScaleY, _newScaleZ);
        Debug.Log(gameObject.name + " / _newScale : " + _newScale);
    }
}
