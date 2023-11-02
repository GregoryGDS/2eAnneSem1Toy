using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobMissile : MonoBehaviour
{
    public float _damage;
    public float _speed;
    Rigidbody _rb;
    public float _lifeTime = 1f;
    public bool _isDead;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_damage = _rb.mass;

        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead)
        {
            _lifeTime -= Time.deltaTime;

            if (_lifeTime <= 0)
            {
                Destruct();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("mouth"))
        {
            Destruct();
        }
    }

    void Destruct()
    {
        Destroy(gameObject);
    }
}
