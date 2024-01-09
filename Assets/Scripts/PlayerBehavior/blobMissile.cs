using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class blobMissile : MonoBehaviour
{
    public float _damage;
    public float _speed;
    Rigidbody _rb;
    public float _lifeTime = 1f;
    public bool _isDead;

    public CinemachineFreeLook _camLook;
    public Transform _targetAim;

    private Vector3 _dir;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("in bullet");

        _rb = GetComponent<Rigidbody>();
        _targetAim = GameObject.FindGameObjectWithTag("aimPoint").transform;
        _dir = _targetAim.position - transform.position;
        Fire();
        Scale();
        Debug.DrawRay(transform.position, _dir * 500f, Color.yellow);

    }
    // Update is called once per frame
    void Update()
    {
        
        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            Destruct();
        }

        //Debug.DrawRay(transform.position, _dir * 500f, Color.blue);


        Debug.DrawRay(transform.position, _dir * 500f, Color.yellow);
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("object"))
        {
            collision.gameObject.GetComponent<Rigidbody>(); // - mass
            Destruct();
        }
        
    }

    public void SetDir(Vector3 _d)
    {
        _dir = _d;
    }

    public void Destruct()
    {
        Destroy(gameObject);
    }

   public void Scale()
    {
        // scale le missile pour être proportionnelle à la taille du player
        
        Vector3 _scalePlayer = GameManager.Instance._vacuumScript.transform.localScale;
        transform.localScale = new Vector3(
            transform.localScale.x * _scalePlayer.x,
            transform.localScale.y * _scalePlayer.y,
            transform.localScale.z * _scalePlayer.z
        );
    }


    public void Fire()
    {
        _rb.AddForce(_dir * _speed, ForceMode.Impulse);
    }

}
