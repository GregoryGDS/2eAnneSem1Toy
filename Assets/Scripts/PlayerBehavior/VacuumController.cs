using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    public float _countdown;

    public bool _vacuumOn;
    public float _swallowForce;

    public Transform _mouth;
    public GameObject _allBody;

    public bool _spitOn;

    private void Start()
    {
        
    }

    private void Update()
    {

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

        if (Input.GetKey(KeyCode.Mouse1)){

            //Debug.Log("Expire");
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("object"))
        {
            if (_vacuumOn && _allBody.GetComponent<Rigidbody>().mass >= other.GetComponent<Rigidbody>().mass)
            {
                Vector3 _dir = _mouth.position - new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
                other.GetComponent<Rigidbody>().AddForce(_dir * _swallowForce);
            }
            else
            {
                Stop(other);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("object"))
        {
            Stop(other);
        }
    }

    void Stop(Collider _obj)
    {
        /*
        _obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        */
    }

}
