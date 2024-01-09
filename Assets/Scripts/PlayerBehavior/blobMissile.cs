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
        _rb = GetComponent<Rigidbody>();
        //_targetAim = GameObject.FindGameObjectWithTag("aimPoint").transform;

        Scale();
        Fire();
    }
    // Update is called once per frame
    void Update()
    {

        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            Destruct();
        }
        //Debug.DrawRay(transform.position, _dir * 500f, Color.yellow);

        _dir = GameManager.Instance._spitAimScript.getDirectionAiming().normalized;
        //Debug.DrawRay(transform.position, _dir * 500f, Color.blue);


    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("object"))
        {
            collision.gameObject.GetComponent<Rigidbody>(); // - mass
            Destruct();
        }
        
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
