using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SpitAimController : MonoBehaviour
{

    public Transform _spawnPoint;
    public blobMissile _missile;

    private Rigidbody _rb;
    public float _cooldown;
    [SerializeField]
    private float _time;


    public Camera _camBrain;
    private Vector3 _directionAiming;


    // Start is called before the first frame update
    void Start()
    {
        //_rb = transform.GetComponent<Rigidbody>();
        _time = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_camBrain != null && Input.GetKey(KeyCode.Mouse1))
        {
            // Get the position of the screen center in viewport space (0.5, 0.5)
            //Vector3 screenCenterViewport = new Vector3(0.5f, 0.5f, _camBrain.nearClipPlane);

            // Convert the screen center from viewport space to world space
            //Vector3 screenCenterWorld = _camBrain.GetComponentInChildren<Camera>().ViewportToWorldPoint(screenCenterViewport);
            //qDebug.Log(screenCenterWorld);



            //Debug.DrawRay(_spawnPoint.position, GameManager.Instance._cameraScript.getDirHead().normalized * 3f, Color.blue);


            /*
            RaycastHit hit;
            Ray ray = _camLook.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(_camLook.position, _camLook.forward, 100f))
            {
                Transform objectHit = hit.transform;

                // Do something with the object that was hit by the raycast.
                Debug.DrawRay(_spawnPoint.position, objectHit.position - _spawnPoint.position * 3f, Color.blue);

            }
        */
        /*
            //GameManager.Instance._cameraScript.getDirHead()
            Vector3 rayDirection = _camBrain.transform.forward;

            // Create a ray from the camera position in the specified direction
            Ray ray = new Ray(_camBrain.transform.position, rayDirection);
            Debug.DrawRay(_spawnPoint.position, ray.direction.normalized * 500f, Color.red);
            
            //Debug.Log("ray.direction : " + ray.direction); //vector 3
            
            // RaycastHit variable to store information about the hit point
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Calculate the directional vector between point A and the hit point
                _directionAiming = hit.point - _spawnPoint.position;

                // Normalize the vector to get a unit vector (optional, depends on your use case)
                //_directionAiming.Normalize();

                // Display the results in the console

                //Debug.DrawRay(_spawnPoint.position, _directionAiming.normalized * 10f, Color.red);


                // Optionally, you can use the vector for further actions, such as moving an object in that direction
                // objectToMove.transform.Translate(directionVector * Time.deltaTime * moveSpeed);
            }

        }
        */

        if (_time <= 0f)
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

    public Vector3 getDirectionAiming() // pour blobMissible
    {
        return _directionAiming;
    }

    void Fire(blobMissile _obj)
    {
        float _m = GameManager.Instance._vacuumScript._mass;
        if (_m > 1f && transform.localScale.x > 1)
        {
            // mettre instantiate sur la head
            // mettre la head dans game manager, pour éviter de la remettre partout, on la met juste là
            blobMissile _atSpawn = Instantiate(_obj, _spawnPoint.position, _spawnPoint.rotation);

            _atSpawn._damage = _m / 10; // script placé sur Player

            GameManager.Instance._vacuumScript.LossMass(_atSpawn._damage);
        }


    }
}
