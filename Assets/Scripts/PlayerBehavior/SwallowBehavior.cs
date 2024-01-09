using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwallowBehavior : MonoBehaviour
{
    public VacuumBehavior _vacuumScript;

    [Header("Mouth/Swallow Parameter")]
    public float _mouthAngle;
    public float _rGizmo ;

    // Start is called before the first frame update
    void Start()
    {
        //_mouthAngle = GameManager.Instance._vacuumScript._maxAngle * 2;
        _vacuumScript = GameManager.Instance._vacuumScript;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("object")) // && _vacuumScript._vacuumOn
        {
            //Debug.Log("enter collision");
            Aspirable _aspirableObject = collision.gameObject.GetComponent<Aspirable>();
            if (_aspirableObject != null && !_aspirableObject._isDestroy && _vacuumScript._aspirableList.Contains(_aspirableObject))
            {
                // verif si :
                // - est un objet aspirable
                // - dans liste des objets aspirable/mangeable
                // - s'il n'est pas en cours de destruction / déjà mangé

                Vector3 dir = collision.contacts[0].point - transform.position;

                float _impactAngle = Vector3.Angle(transform.forward, dir);

                float _angleDiffMouth = Mathf.Clamp01(_impactAngle / _mouthAngle); // entre 0 et 1

                //Debug.Log("angle impact : " + _impactAngle + "\nangle diff : " + _angleDiffMouth);

                if (_angleDiffMouth < 1f) //si < 1 = dans la range
                {
                    //stop sa course pour éviter de pousser le player
                    _aspirableObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    _aspirableObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                    Swallow(_aspirableObject);



                    GameManager.Instance.UpdateNumberObject(); // pour les tests avec les obj instantié en random
                }
            }
        }



    }



    public void Swallow(Aspirable _objectApire)
    {
        //Debug.Log("in swallow");
        _objectApire.EndAspiration();

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

        //Debug.Log("before destroySelf");

        _objectApire.DestroySelf();
    }


    private void OnDrawGizmos()
    {
        Vector3 rightDir = Quaternion.AngleAxis(_mouthAngle, Vector3.up) * transform.forward;
        Vector3 leftDir = Vector3.Reflect(rightDir, transform.right);
        Vector3 downDir = Quaternion.AngleAxis(_mouthAngle, Vector3.right) * transform.forward;
        Vector3 upDir = Vector3.Reflect(downDir, transform.up);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rightDir.normalized *  _rGizmo  * transform.localScale.x);
        Gizmos.DrawRay(transform.position, leftDir.normalized *  _rGizmo  * transform.localScale.x);
        Gizmos.DrawRay(transform.position, downDir.normalized *  _rGizmo  * transform.localScale.x);
        Gizmos.DrawRay(transform.position, upDir.normalized *  _rGizmo  * transform.localScale.x);

    }
}
