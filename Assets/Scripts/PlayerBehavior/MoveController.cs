using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Move")]
    public float _speed;
    public float _maxSpeedMagnitude;
    public float _force;

    [SerializeField]    
    private float _horizontalMovement;
    [SerializeField]
    private float _verticalMovement;

    public Transform _orientation;
    Vector3 _moveDirection;
    public CameraType _moveType;

    Rigidbody _rb;

    public float _forceMultiplier = 1;




    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_rb.freezeRotation = true; // sinon player tombe ?
    }
    private void Update()
    {
        // if clic droit maintenu change _moveType
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal"); //raw = -1/0/1
        _verticalMovement = Input.GetAxisRaw("Vertical");
  
        // calcule mvt avec _moveDirection
        _moveDirection = _orientation.forward * _verticalMovement + _orientation.right * _horizontalMovement;
        //_moveDirection = transform.forward * _verticalMovement + transform.right * _horizontalMovement;

        if(_rb.velocity.magnitude < _maxSpeedMagnitude)
        {
            
            _rb.AddForce(_moveDirection.normalized * _speed * _force * _forceMultiplier * Time.deltaTime);

            //_rb.velocity = _moveDirection.normalized * _speed * _force * _forceMultiplier * Time.deltaTime;
        }


    }
}
/*
 
         //Calcule direction 

        
          if(_direction.magnitude >= 0.1f)
          {
            Debug.Log("go");
            
            float _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref SmoothVelocity, 0.1f);

            transform.rotation = Quaternion.Euler(0f, _angle, 0f);

            Vector3 _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;



            //_rb.AddForce(_moveDir.normalized * _speed * Time.deltaTime);
            _rb.AddForce(_moveDir.normalized * _speed * _force * _forceMultiplier * Time.deltaTime);

        }
*/
