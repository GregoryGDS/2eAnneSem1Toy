using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    public float _countdown;

    public bool _vacuumOn;
    public float _swallowForce;

    public Transform _mouth;
    public GameObject _allBody;

<<<<<<< Updated upstream
=======
    public bool _spitOn;

    private FMOD.Studio.EventInstance event_fmod;
    public bool Soundstart = false;
>>>>>>> Stashed changes
    private void Start()
    {
        event_fmod = FMODUnity.RuntimeManager.CreateInstance("event:/Aspiration");
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0)){

            _vacuumOn = true;
            //Debug.Log("Aspire");
<<<<<<< Updated upstream

=======
            if (Soundstart == false)
            {
                event_fmod.start();
                Soundstart = true;
            }
>>>>>>> Stashed changes
        }
        else
        {
            _vacuumOn = false;

        }

        if (Input.GetKey(KeyCode.Mouse1)){

            //Debug.Log("Expire");
        }


        if (_vacuumOn)
        {
            _mouth.transform.localScale = new Vector3(0.70f, 0.65f, 0.1f);
            
        }
        else
        {
            _mouth.transform.localScale = new Vector3(0.3f, 0.1f, 0.1f);
            event_fmod.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Soundstart = false;
        }
    }


/*    public IEnumerator Swallow(float _wait)
    {
        Debug.Log("Aspire");

        yield return new WaitForSeconds(_wait);

    }*/


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
        _obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

}
