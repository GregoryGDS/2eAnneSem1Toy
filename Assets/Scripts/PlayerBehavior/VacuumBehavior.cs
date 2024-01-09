using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBehavior : MonoBehaviour
{
    public GameObject _playerBody;

    public float _mass = 1f;
    public float _scaleSpeed;

    [Header("Vacuum Parameter")]
    public Transform _swallowPosition;

    public bool _vacuumOn;    
    public float _maxAngle = 30f;
    public float _aspirationForce = 10f;
    public List<Aspirable> _aspirableList = new List<Aspirable>();
    public float _range = 10f;
    public float _rangeSave;

    public SphereCollider _triggerRange;

    [Header("Aim Parameter")]
    public bool _spitOn;

    private void Start()
    {
        //_triggerRange = GetComponent<SphereCollider>(); //prend le 1er
        _rangeSave = _range;
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * _mass, _scaleSpeed * Time.deltaTime);

        //Debug.Log("_triggerRange.radius : "+ _triggerRange.radius);
        if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        {
            _vacuumOn = true;
            //Debug.Log("Aspire");
        }
        else
        {
            _vacuumOn = false;
        }


        if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {

            _spitOn = true;
            //Debug.Log("Aspire");
        }
        else
        {
            _spitOn = false;
        }

    }

    private void FixedUpdate()
    {
        if (_vacuumOn)
        {
            //Debug.Log("in _vacuumOn");

            if (_aspirableList.Count != 0)// passe le bug du remove
            {
                //Debug.Log("in _aspirableList.Count != 0 : "+ _aspirableList.Count);
                Debug.Log("before foreach vacuum : " + _aspirableList.Count);
                foreach (Aspirable _aspirable in _aspirableList)
                {
                    //Debug.Log("in foreach");
                    if(_aspirable != null) //controle si bien dans liste, test pour les cas ou veut y accéder alors que null
                    {
                        //Add force towards mouth 
                        Vector3 _dir = _aspirable.transform.position - _swallowPosition.transform.position;
                        //verif range ? 
                        float _angleRange = 1f - Mathf.Clamp01(Vector3.Angle(_swallowPosition.transform.forward, _dir) / _maxAngle);
                        _aspirable._rb.AddForce(-_dir.normalized * _aspirationForce * _angleRange);
                    }
                    else
                    {
                        Debug.LogWarning("NOPE");
                    }

                }
            }
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("range enter : " + other.name);
        Aspirable aspirable = other.GetComponent<Aspirable>(); //Check tag
        if (aspirable != null && !aspirable._isDestroy)
        {
            float massToAspire = aspirable._mass;

            if (massToAspire <= _mass)
            {
                _aspirableList.Add(aspirable);
                //Debug.Log("enter range in mass : " + aspirable.name);

                aspirable.StartAspiration();//stop course agent runner // verif si dans range
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Aspirable aspirable = other.GetComponent<Aspirable>();
        if (aspirable != null && _aspirableList.Contains(aspirable))
        {
            _aspirableList.Remove(aspirable);

            aspirable.EndAspiration();
        }
    }

    public void LossMass(float _lostMass)
    {
        _mass -= _lostMass;
        //_allBody.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }

    public void GainMass(float _massAdd)
    {
        _mass += _massAdd;
        //transform.localScale = new Vector3(_mass, _mass, _mass);
    }

    public void RemoveAspirable(Aspirable _obj)
    {
        Debug.Log("in  removeAspirable");
        //Debug.Log("remove obj : " + _obj.name);

        //Debug.Log("contains before remove : " + _aspirableList.Contains(_obj));

        if (_obj != null && _aspirableList.Contains(_obj))
        {
            Debug.Log("in  removeAspirable : check null et contains");
            //_obj._isDestroy = true;//empeche de l'enregistrer à nouveaux dans la liste => fait avant remove et suppression pour être certain
            _aspirableList.Remove(_obj);
            Debug.Log("contains after remove : " + _aspirableList.Contains(_obj) + "\n _isDestroy : " + _obj._isDestroy);
            _obj.EndAspiration();
        }


    }


    private void OnDrawGizmos()
    {
        // angle vacuum
        Vector3 rightDir = Quaternion.AngleAxis(_maxAngle, Vector3.up) * _swallowPosition.transform.forward;
        Vector3 leftDir = Vector3.Reflect(rightDir, _swallowPosition.transform.right);
        Vector3 downDir = Quaternion.AngleAxis(_maxAngle, Vector3.right) * _swallowPosition.transform.forward;
        Vector3 upDir = Vector3.Reflect(downDir, _swallowPosition.transform.up);

        //Debug.Log("_range gizmos : "+ _range  * transform.localScale.x);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_swallowPosition.transform.position, rightDir.normalized * _range  * transform.localScale.x);
        Gizmos.DrawRay(_swallowPosition.transform.position, leftDir.normalized * _range * transform.localScale.x);
        Gizmos.DrawRay(_swallowPosition.transform.position, downDir.normalized * _range * transform.localScale.x);
        Gizmos.DrawRay(_swallowPosition.transform.position, upDir.normalized * _range * transform.localScale.x);
    }
}
