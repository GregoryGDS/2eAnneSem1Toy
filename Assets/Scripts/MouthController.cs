using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MouthController : MonoBehaviour
{
    public GameObject _allBody;
    private bool _sound = false;
    private float timer;

<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
;    }
=======
    //comment mettre ce script dans le parent ? vu qu'il y a a juste le trigger ? 
    private FMOD.Studio.EventInstance event_fmod;
    // Start is called before the first frame update
    void Start()
    {
        event_fmod = FMODUnity.RuntimeManager.CreateInstance("event:/Avale");
    }
>>>>>>> Stashed changes

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on trigger");

        if (other.CompareTag("object"))
        {
            Debug.Log("on if");

            if (_allBody.GetComponent<Rigidbody>().mass >= other.GetComponent<Rigidbody>().mass)
            {
                Swallow(other.GetComponent<Rigidbody>().mass);
                _allBody.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _allBody.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                Destroy(other.gameObject);
                GameManager.instance.UpdateNumberObject();
            }

        }
    }


    public void Swallow(float _mass)
    {
        _allBody.GetComponent<Rigidbody>().mass += _mass;
        _allBody.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
<<<<<<< Updated upstream
=======

        event_fmod.start();
>>>>>>> Stashed changes

    }
}
