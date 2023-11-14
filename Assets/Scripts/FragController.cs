using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragController : MonoBehaviour
{
    public GameObject _fragment;
    public Transform _group;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.CompareTag("spit"))
        {
            Debug.Log("inin");

            blobMissile _missile = collision.gameObject.GetComponent<blobMissile>();


            //transform.localScale -= new Vector3(_missile._damage, _missile._damage, _missile._damage);


            if ((GetComponent<Rigidbody>().mass - _missile._damage) > 1)
            {

                transform.localScale -= new Vector3(_missile._damage, _missile._damage, _missile._damage);

                GetComponent<Rigidbody>().mass -= _missile._damage;
                Instantiate(_fragment,
                    transform.position + new Vector3(
                        Random.Range(-transform.position.x + 5, transform.position.x + 5),
                        Random.Range(0, transform.position.y + 5),
                        Random.Range(-transform.position.z + 5, transform.position.z + 5)),
                    transform.rotation,
                    _group);
            }

            Debug.Log(_missile._damage);
            _missile.Destruct();

        }
    }
}
