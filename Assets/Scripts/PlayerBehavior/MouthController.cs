using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthController : MonoBehaviour
{
    public GameObject _allBody;
    public float _scaleSpeed = .6f;

    public VacuumBehavior _vacuumScript;


    // Start is called before the first frame update
    void Start()
    {
        _vacuumScript = GameManager.Instance._vacuumScript;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("object") && _vacuumScript._vacuumOn)
        {
            Aspirable _apirableObject = other.GetComponent<Aspirable>();
            if (_apirableObject)
            {

               //Debug.Log("ici");
                if (_apirableObject._mass <= _vacuumScript._mass )
                {
                    
                    Swallow(_apirableObject);
                    _allBody.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    _allBody.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                    if (_apirableObject != null && _vacuumScript._aspirableList.Contains(_apirableObject))
                    {
                        //_vaccuumScript._aspirableList.Remove(_apirableObject); 
                        //dans swallow

                    }


                    //Destroy(other.gameObject);
                    GameManager.Instance.UpdateNumberObject();
                }
            }


        }
    }


    public void Swallow(Aspirable _objectApire)
    {
        //_objectApire.EndAspiration();

        _vacuumScript.GainMass(_objectApire._mass);
        /*
        Debug.Log(
            "mass body : " + _vaccuumScript._mass +
            "\n new scale : " + Vector3.one * _vaccuumScript._mass +
            "\n swallow : " + _objectApire.name
            );
        */
        //Debug.Log("swallow : " + _objectApire.name);

        //_vaccuumScript._aspirableList.Remove(_objectApire);
        //Destroy(_objectApire);
        _vacuumScript.RemoveAspirable(_objectApire); //marche pas


        //_allBody.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        //FMODUnity.RuntimeManager.PlayOneShot("event:/boop");
        _objectApire.DestroySelf();
    }

    public void Spit(float _lostMass)
    {
        _vacuumScript._mass -= _lostMass;
        //_allBody.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }
}
