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

        //_mouth.transform.rotation = Quaternion.Euler(GameManager.instance._cameraScript._orientation.forward);

        
        if (_vacuumOn || _spitOn)
        {
            _mouth.transform.localScale = new Vector3(0.70f, 0.65f, 0.1f);
        }
        else
        {
            _mouth.transform.localScale = new Vector3(0.3f, 0.1f, 0.1f);
        }
    }


/*    public IEnumerator Swallow(float _wait)
    {
        Debug.Log("Aspire");

        yield return new WaitForSeconds(_wait);

    }*/


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("object") || other.CompareTag("object_nega"))
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
        if (other.CompareTag("object") || other.CompareTag("object_nega"))
        {
            Stop(other);
        }
    }

    void Stop(Collider _obj)
    {
        
        _obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        
    }

}
