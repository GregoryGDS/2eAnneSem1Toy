using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    TowardsSwallow,
    FreeSpit,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int _spawnMax;
    public int _current;

    public SpawnerObject _spawnerScript;
    public ThirdPersonCamera _cameraScript;
    public VacuumController _vacuumScript;
    public MoveController _moveScript;


    void Start()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        if (_current < _spawnMax)
        {
            _spawnerScript.SpawRand();
            _current++;
        }
    }


    public void UpdateNumberObject()
    {
        _current -= 1;
    }

}
