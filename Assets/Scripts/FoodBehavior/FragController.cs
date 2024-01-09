using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragController : MonoBehaviour
{
    public GameObject _prefabFrag;
    public Transform _group;

    public Aspirable _aspirableScript;

    public float bounceImpulse = 2f;

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
        if (collision.gameObject.CompareTag("spit"))
        {
            Debug.Log("hit");

            blobMissile _missile = collision.gameObject.GetComponent<blobMissile>();


            //transform.localScale -= new Vector3(_missile._damage, _missile._damage, _missile._damage);

            Debug.Log(_aspirableScript._mass - _missile._damage);
            if ((_aspirableScript._mass - _missile._damage) > 1f)
            {

                //transform.localScale -= new Vector3(_missile._damage, _missile._damage, _missile._damage);

                _aspirableScript.lossMass(_missile._damage);

                Vector3 dir = collision.contacts[0].point - transform.position;


                //Todo add impulse force
                Vector3 bounceDir = collision.transform.position - transform.position;
                dir.y = 0f;
                Debug.DrawRay(transform.position, bounceDir * 10, Color.red);
                //Todo reflect velocity ?
                // bounceDir = Vector3.Reflect(collision.rigidbody.velocity, bounceDir.normalized);
                // collision.rigidbody.velocity = bounceDir * bounceImpulse;

                GameObject _a = Instantiate(_prefabFrag,

                    collision.contacts[0].point,
                    Quaternion.identity,
                    _group
                    );

                _a.GetComponent<Rigidbody>().AddForce(dir * bounceImpulse, ForceMode.Impulse);



            }

            //Debug.Log(_missile._damage);
            _missile.Destruct();

        }
    }
}
