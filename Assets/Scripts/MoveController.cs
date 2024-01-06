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
        
    private float _horizontalMovement;
    private float _verticalMovement;

    public Transform _orientation;
    Vector3 _moveDirection;

    Rigidbody _rb;

    public float _forceMultiplier = 1;
    public int _count = 2;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_rb.freezeRotation = true; // sinon player tombe ?
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
        // permet d'avancer toujours dans la direction dans laquelle on regarde
        
        //_forceMultiplier = 1;
        //_count = 2;
        if (_rb.mass >= _count)
        {
            _forceMultiplier += 1.5f;
            _count += 2;
        }

        _rb.AddForce(_moveDirection.normalized * _speed * _force * _forceMultiplier * Time.deltaTime);
    }



    // voir pour appliquer au body et pas au player
}
