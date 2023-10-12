using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthController : MonoBehaviour
{
    public GameObject _allBody;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
;    }

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

    }
}
