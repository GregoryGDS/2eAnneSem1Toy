using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitAimController : MonoBehaviour
{

    public Transform _mouth;
    public blobMissile _missile;
    public GameObject _missileGrab;
    
    private Rigidbody _rb;
    public float _cooldown;
    [SerializeField]
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_time <= 0f)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                GameManager.instance._moveScript._moveType = CameraType.FreeSpit;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    //Fire(_missile);
                    Fire2(_missileGrab);
                    _time = _cooldown;
                }

            }
            else
            {
               GameManager.instance._moveScript._moveType = CameraType.TowardsSwallow;
               // desactive le suivi de cam de visée
            }

            
        }
        else
        {
            _time -= Time.deltaTime;
        }

    }


    void Fire(blobMissile _obj)
    {
        if (_rb.mass > 1f && transform.localScale.x > 1)
        {
            blobMissile _atSpawn = Instantiate(_obj, _mouth.position, _mouth.rotation);

            _atSpawn._damage = _rb.mass / 10; // script placé sur Player
            //Debug.Log(transform.GetComponent<Rigidbody>().mass);

            GameManager.instance._mouthScript.Spit(0.2f);
        }

    }

    void Fire2(GameObject _obj)
    {
        if (_rb.mass > 1f && transform.localScale.x > 1)
        {
            Instantiate(_obj, _mouth.position, _mouth.rotation);
            
            GameManager.instance._mouthScript.Spit(0.2f);
        }

    }
}
