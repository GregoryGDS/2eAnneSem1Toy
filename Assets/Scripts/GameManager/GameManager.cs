using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    TowardsSwallow, // normal
    FreeSpit, // visée
}
public enum FoodType
{
    Normal,
    Runner,
}

public enum TypeObject
{
    Verre,
    Distributeur,
    Batiement
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int _spawnMax;
    public int _current;

    public SpawnerObject _spawnerScript;
    public ThirdPersonCamera _cameraScript;
    public VacuumBehavior _vacuumScript;
    public MoveController _moveScript;
    public SwallowBehavior _mouthScript; // SwallowBehavior
    public SpitAimController _spitAimScript; // SwallowBehavior

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Une instance Game Manager existe déjà.");
        }
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
