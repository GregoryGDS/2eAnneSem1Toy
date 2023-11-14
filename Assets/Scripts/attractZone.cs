using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attractZone : MonoBehaviour
{
    public Transform _centerObject;
    public float _speedAttraction;

    Rigidbody _rb;

    public CheckGroundMissile _scriptCheck;

    // Start is called before the first frame update
    void Start()
    {
        //_damage = _rb.mass;

    }
    // Update is called once per frame
    void Update()
    {



    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("object") && _scriptCheck._isActive)
        {
            Debug.Log("grab");
            Vector3 _dir = _centerObject.transform.position - other.transform.position;

            other.attachedRigidbody.velocity = _dir.normalized * _speedAttraction;
        }
    }




}
