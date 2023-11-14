using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundMissile : MonoBehaviour
{
    public float _speedMissile;
    public bool _isActive;

    public float _lifeTime = 3f;
    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * _speedMissile, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            Destruct();
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("ground"))
        {
            _isActive = true;
            _rb.velocity = Vector3.zero;
        }
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }
}
