using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Reference")]
    public Transform _orientation;
    public Transform _player;
    public Transform _head;

    public Transform _viseurCamPoint;

    public Transform _playerBody;
    public Rigidbody _rb;

    public float _rotationSpeed;


    public Dictionary<string, List<float>> _camDefaultSize = new Dictionary<string, List<float>>(); // list rig normal
    public Dictionary<string, List<float>> _camDefaultSizeVise = new Dictionary<string, List<float>>(); // list rig visé

    [Range(0.1f,3f)]
    public float _mouseSensitivity = 1f;
    
    [SerializeField]
    public CinemachineFreeLook _camLook;

    public GameObject _aimPointCanvas;

    [Header("Param Cam")]
    public int _radiusSwallow = 3;
    public int _heightSwallow = 6;
    public int _radiusSpit = 12;
    public int _heightSpit = 12;
    public float _offSetCam;

    private Vector3 _viewDir;
    private Vector3 _viewDirHead;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        // key => {radius, heigh} 
        _camDefaultSize.Add("Top", new List<float> { 13, 7 });
        _camDefaultSize.Add("Mid", new List<float> { 8, 3 });
        _camDefaultSize.Add("Bot", new List<float> { 4, 1.5f });

        _camDefaultSizeVise.Add("Top", new List<float> { 2, 2 });
        _camDefaultSizeVise.Add("Mid", new List<float> { 2, 1 });
        _camDefaultSizeVise.Add("Bot", new List<float> { 2, 0.3f });
    }

    void FixedUpdate()
    {

        AdjustOrbits();


        // rotation / orientation de la cam sur XZ pas Y
        _viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z); //new Vector3(transform.position.x, _player.position.y, transform.position.z)

        if (GameManager.Instance._moveScript._moveType == CameraType.TowardsSwallow)
        {
            //_viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z); //new Vector3(transform.position.x, _player.position.y, transform.position.z)

           
            //_viewDirHead = _viewDir;

        }

        if (GameManager.Instance._moveScript._moveType == CameraType.FreeSpit)
        {
            //_offSetCam = transform.position.y;

            //_viewDir = _player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z); //new Vector3(transform.position.x, _player.position.y, transform.position.z)
            _viewDirHead = _viseurCamPoint.position - new Vector3(transform.position.x, transform.position.y, transform.position.z); //new Vector3(transform.position.x, _player.position.y, transform.position.z)
        }

        _orientation.forward = _viewDir.normalized; // pour MoveController

        //Debug.DrawRay(_orientation.position, _viewDirHead.normalized * 2f, Color.blue);

        if (_viewDir != Vector3.zero)
        {
            //_playerBody.forward = Vector3.Slerp(_playerBody.forward, _viewDir.normalized, _rotationSpeed * _mouseSensitivity * Time.deltaTime);
            _player.forward = Vector3.Slerp(_player.forward, _viewDir.normalized, _rotationSpeed * _mouseSensitivity * Time.deltaTime);
        }

        if (_viewDirHead != Vector3.zero)
        {
            _head.forward = Vector3.Slerp(_head.forward, _viewDirHead.normalized, _rotationSpeed * _mouseSensitivity * Time.deltaTime);
        }

    }

    public Vector3 getDirHead()
    {
        return _viewDirHead;
    }

    void AdjustOrbits()
    {
        /*
         * 0 => top
         * 1 => middle
         * 2 => bottom
        */

        float scaleFactor = ((_player.localScale.x + _player.localScale.y + _player.localScale.z) / 3f) ;//au cas ou les 3 scale ne sont pas identique 



        if (GameManager.Instance._moveScript._moveType == CameraType.FreeSpit) //mode visée
        {
            _camLook.LookAt = _viseurCamPoint;
            _aimPointCanvas.SetActive(true);

            _camLook.m_Orbits[0].m_Radius = _camDefaultSizeVise["Top"][0] * scaleFactor;
            _camLook.m_Orbits[0].m_Height = _camDefaultSizeVise["Top"][1] * scaleFactor;

            _camLook.m_Orbits[1].m_Radius = _camDefaultSizeVise["Mid"][0] * scaleFactor;
            _camLook.m_Orbits[1].m_Height = _camDefaultSizeVise["Mid"][1] * scaleFactor;

            _camLook.m_Orbits[2].m_Radius = _camDefaultSizeVise["Bot"][0] * scaleFactor;
            _camLook.m_Orbits[2].m_Height = _camDefaultSizeVise["Bot"][1] * scaleFactor;
        }
        else
        {
            _camLook.LookAt = _player;
            _aimPointCanvas.SetActive(false);

            _camLook.m_Orbits[0].m_Radius = _camDefaultSize["Top"][0] * scaleFactor;
            _camLook.m_Orbits[0].m_Height = _camDefaultSize["Top"][1] * scaleFactor;

            _camLook.m_Orbits[1].m_Radius = _camDefaultSize["Mid"][0] * scaleFactor;
            _camLook.m_Orbits[1].m_Height = _camDefaultSize["Mid"][1] * scaleFactor;

            _camLook.m_Orbits[2].m_Radius = _camDefaultSize["Bot"][0] * scaleFactor;
            _camLook.m_Orbits[2].m_Height = _camDefaultSize["Bot"][1] * scaleFactor;
        }



        //Debug.Log("_camLook.m_Orbits[2].m_Radius after : " + _camLook.m_Orbits[2].m_Radius);


    }
}
