using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Reference")]
    public Transform _orientation;
    public Transform _player;
    public Transform _playerBody;
    public Rigidbody _rb;

    public float _rotationSpeed;

    [Range(0.1f,3f)]
    public float _mouseSensitivity = 1f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        

    }

    void Update()
    {
        // rotation / orientation de la cam sur XZ
        Vector3 _viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z); //new Vector3(transform.position.x, _player.position.y, transform.position.z)
        _orientation.forward = _viewDir.normalized;

        // rotation Player
        //float _horizontalInput = Input.GetAxis("Horizontal");
        //float _verticalInput = Input.GetAxis("Vertical");

        Vector3 _dir = _orientation.forward; //* _verticalInput + _orientation.right * _horizontalInput
        //pk right ? 

        if (_dir != Vector3.zero)
        {
            _playerBody.forward = Vector3.Slerp(_playerBody.forward, _dir.normalized, _rotationSpeed * _mouseSensitivity * Time.deltaTime);
        }
    }
}
