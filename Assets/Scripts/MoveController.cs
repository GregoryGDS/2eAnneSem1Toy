using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("Move")]
    public float _speed;
    public float _force;
    /*
    public bool _isGround;
    public bool _isJumping;
    public float _gravity;
    */
    [SerializeField]    
    private float _horizontalMovement;
    [SerializeField]
    private float _verticalMovement;

    public Transform _orientation;
    Vector3 _moveDirection;
    public CameraType _moveType;

    Rigidbody _rb;

    public float _forceMultiplier = 1;
    public int _count = 2;

    public int _palier = 0;
    public int _previousPalier = 0;
    public float SmoothVelocity;
    public CharacterController _controller;

    [Header("Test")]
    public bool _isJump; // saut utilisé ?
    public float _jumpForce = 10f;

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
        //Vector3 _direction = new Vector3(_horizontalMovement, 0f, _verticalMovement).normalized;

        //retourne vector avec magnitude limité (clamp) à 1f
        //Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(_horizontalMovement, 0f, _verticalMovement), 1f);


        //Calcule direction 

        /*
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








        // permet d'avancer toujours dans la direction dans laquelle on regarde

        //_forceMultiplier = 1;
        //_count = 2;
        if (_rb.mass >= _count)
        {
            _forceMultiplier += 1.5f;
            _count += 2;

            _palier += 1;

        }

        // calcule mvt avec _moveDirection
        _moveDirection = _orientation.forward * _verticalMovement + _orientation.right * _horizontalMovement;
        //_moveDirection = transform.forward * _verticalMovement + transform.right * _horizontalMovement;

       
        
        _rb.AddForce(_moveDirection.normalized * _speed * _force * _forceMultiplier * Time.deltaTime);

        //_rb.velocity = _moveDirection.normalized * _speed * _force * _forceMultiplier * Time.deltaTime;

        /*
               if(_previousPalier != _palier)
               {
                   _previousPalier = _palier;
                   GameManager.instance._cameraScript.UpdateOrbitsRigs(0, 0.5f, 0.5f);
                   GameManager.instance._cameraScript.UpdateOrbitsRigs(1, 0.5f, 0.5f);
                   GameManager.instance._cameraScript.UpdateOrbitsRigs(2, 0.5f, 0.5f);
               }

        */
        GameManager.instance._cameraScript.UpdateOrbitsRigs(0, 10f, 0.5f);
        GameManager.instance._cameraScript.UpdateOrbitsRigs(1, 10f, 0.5f);
        GameManager.instance._cameraScript.UpdateOrbitsRigs(2, 10f, 0.5f);
    }



    // voir pour appliquer au body et pas au player
}
