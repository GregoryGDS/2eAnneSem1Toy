using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthController : MonoBehaviour
{
    public GameObject _allBody;

    //comment mettre ce script dans le parent ? vu qu'il y a a juste le trigger ? 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("object") && GameManager.instance._vacuumScript._vacuumOn)
        {

            if (_allBody.GetComponent<Rigidbody>().mass >= other.GetComponent<Rigidbody>().mass)
            {
                Swallow(other.GetComponent<Rigidbody>().mass);
                _allBody.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _allBody.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                Destroy(other.gameObject);
                GameManager.instance.UpdateNumberObject();
            }
        }


        if (other.CompareTag("object_nega") && GameManager.instance._vacuumScript._vacuumOn)
        {

            if (_allBody.GetComponent<Rigidbody>().mass >= other.GetComponent<Rigidbody>().mass)
            {
                Swallow(other.GetComponent<Rigidbody>().mass, true);
                _allBody.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _allBody.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                Destroy(other.gameObject);
                GameManager.instance.UpdateNumberObject();
            }

        }
    }


    public void Swallow(float _mass, bool _lost = false)
    {
        if (!_lost)
        {
            _allBody.GetComponent<Rigidbody>().mass += _mass;
            _allBody.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }
        else
        {
            if(_allBody.GetComponent<Rigidbody>().mass - _mass >= 1)
            {
                _allBody.GetComponent<Rigidbody>().mass -= _mass;
                _allBody.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            }

        }


    }

    public void Spit(float _lostMass)
    {
        _allBody.GetComponent<Rigidbody>().mass -= _lostMass;
        _allBody.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }
}
