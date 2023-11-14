using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    
    [SerializeField]
    public CinemachineFreeLook _camLook;

    [Header("Param Cam")]
    public int _radiusSwallow = 3;
    public int _heightSwallow = 6;
    public int _radiusSpit = 12;
    public int _heightSpit = 12;
    public float _offSetCam;

    private Vector3 _viewDir;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    void Update()
    {
        //_orientation = vers où la caméra regarde 

        //calcule rotation de la caméra au joueur pour trouver où doit aller le .forward

        // rotation / orientation de la cam sur XZ

        if (GameManager.instance._moveScript._moveType == CameraType.TowardsSwallow)
        {
            _viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z); //new Vector3(transform.position.x, _player.position.y, transform.position.z)
            //_camLook.m_Orbits[0].m_Radius = _radiusSwallow;
            //_camLook.m_Orbits[0].m_Height = _heightSwallow;
        }

        if (GameManager.instance._moveScript._moveType == CameraType.FreeSpit)
        {
            _offSetCam = transform.position.y;
            _viewDir = _player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z); //new Vector3(transform.position.x, _player.position.y, transform.position.z)
            //_camLook.m_Orbits[0].m_Radius = _radiusSpit;
            //_camLook.m_Orbits[0].m_Height = _heightSpit;
        }

        _orientation.forward = _viewDir.normalized;

        // rotation Player (tout)
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");

        Vector3 _dir = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
        //pk right ? 

        if (_viewDir != Vector3.zero)
        {
            //_playerBody.forward = Vector3.Slerp(_playerBody.forward, _viewDir.normalized, _rotationSpeed * _mouseSensitivity * Time.deltaTime);
            _playerBody.forward = Vector3.Slerp(_playerBody.forward, _viewDir.normalized, _rotationSpeed * _mouseSensitivity * Time.deltaTime);
        }

        //_camLook.transform.rotation = _player.rotation;
    }

    public void UpdateOrbitsRigs(int _index, float _radius,float _height)
    {
        //freeLookState.CorrectedPosition will give you the freeLook's current position, including any freeLook movement

        var distance = Vector3.Distance(_camLook.State.CorrectedPosition, _player.position);

        //_camLook.m_Orbits[_index].m_Radius = _player.localScale.z + _player.localScale.z * 2f;
        //_camLook.m_Orbits[_index].m_Height = _player.localScale.y + _player.localScale.y * 0.6f;

        switch (_index)
        {
            case 0:
                //Debug.Log(_index + " top \n" + _camLook.m_Orbits[0].m_Radius);
                _camLook.m_Orbits[_index].m_Radius = _player.localScale.z + _player.localScale.z * 6f;
                _camLook.m_Orbits[_index].m_Height = _player.localScale.y + _player.localScale.z * 4.5f;
                break;
            case 1:
                //Debug.Log(_index + " middle \n" + _camLook.m_Orbits[1].m_Radius);
                _camLook.m_Orbits[_index].m_Radius = _player.localScale.z + _player.localScale.z * 3f;
                _camLook.m_Orbits[_index].m_Height = _player.localScale.y + _player.localScale.z * 3f;
                break;
            case 2:
                //Debug.Log(_index + " bottom \n" + _camLook.m_Orbits[2].m_Radius);
                _camLook.m_Orbits[_index].m_Radius = _player.localScale.z + _player.localScale.z * 1f;
                
                if (GameManager.instance._moveScript._moveType == CameraType.FreeSpit)
                {
                    _camLook.m_Orbits[_index].m_Height = ((_player.localScale.z * 0.6f) * -1f )- 1f;
                }
                else
                {
                    _camLook.m_Orbits[_index].m_Height = _player.localScale.y * 0.3f;
                }
                break;
            default:
                return;
        }

        
    }


    /*
        _horizontalMovement = Input.GetAxisRaw("Horizontal"); //raw = -1/0/1
        _verticalMovement = Input.GetAxisRaw("Vertical");
        Vector3 _direction = new Vector3(_horizontalMovement, 0f, _horizontalMovement).normalized;
        
        if(_direction.magnitude >= 0.1f)
        {
            float _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref SmoothVelocity, 0.1f);

            transform.rotation = Quaternion.Euler(0f, _targetAngle, 0f);
        }

     */
}
