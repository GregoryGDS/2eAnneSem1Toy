using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitAimController : MonoBehaviour
{

    public Transform _mouth;
    public blobMissile _missile;

    private Rigidbody _rb;
    public float _cooldown;
    [SerializeField]
    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        //_rb = transform.GetComponent<Rigidbody>();
        _time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_time <= 0f)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                GameManager.Instance._moveScript._moveType = CameraType.FreeSpit;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Fire(_missile);
                    _time = _cooldown;
                }

            }
            else
            {
               GameManager.Instance._moveScript._moveType = CameraType.TowardsSwallow;
               // desactive le suivi de cam de visée
            }

            
        }
        else
        {
            _time -= Time.deltaTime;
        }

        //controle pour après tire, repasse la masse à 1 si < 1 // marche pas si dans Fire()
        if (GameManager.Instance._vacuumScript._mass < 1)
        {
            GameManager.Instance._vacuumScript._mass = 1;
        }


    }


    void Fire(blobMissile _obj)
    {
        float _m = GameManager.Instance._vacuumScript._mass;
        Transform _h = GameManager.Instance._cameraScript._head;
        if (_m > 1f && transform.localScale.x > 1)
        {
            // mettre instantiate sur la head
            // mettre la head dans game manager, pour éviter de la remettre partout, on la met juste là
            blobMissile _atSpawn = Instantiate(_obj, _h.position, _h.rotation);

            _atSpawn._damage = _m / 10; // script placé sur Player

            GameManager.Instance._vacuumScript.LossMass(_atSpawn._damage);
        }


    }
}
