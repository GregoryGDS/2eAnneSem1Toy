using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitAimController : MonoBehaviour
{

    public Transform _mouth;
    public blobMissile _missile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            GameManager.instance._moveScript._moveType = CameraType.FreeSpit;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                Fire(_missile);
            }

        }
        else
        {
            GameManager.instance._moveScript._moveType = CameraType.TowardsSwallow;
        }
    }


    void Fire(blobMissile _obj)
    {
        blobMissile _atSpawn = Instantiate(_obj, _mouth.position, _mouth.rotation);

        _atSpawn._damage = transform.GetComponent<Rigidbody>().mass/10;
        //Debug.Log(transform.GetComponent<Rigidbody>().mass);

    }
}
