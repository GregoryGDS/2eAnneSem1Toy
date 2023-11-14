using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decreaseTime : MonoBehaviour
{
    Rigidbody _rb;
    public float _lostMass;
    public float _lostTaille;
    float _savem;
    float _savet;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _savem = _lostMass;
        _savet = _lostTaille;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb.mass > 10)
        {
            _lostMass *= 2f;
        }
        else
        {
            _lostMass = _savem;
        }


        if (transform.localScale.x > 5)
        {
            _lostTaille *= 4;
        }
        else
        {
            _lostTaille = _savet;
        }



        if (_rb.mass - _lostMass > 1)
        {
            _rb.mass -= _lostMass;
        }

        if (transform.localScale.x - _lostTaille > 1f)
        {
            transform.localScale -= new Vector3(_lostTaille, _lostTaille, _lostTaille);
        }




    }

}
