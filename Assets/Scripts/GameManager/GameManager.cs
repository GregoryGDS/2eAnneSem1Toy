using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int _spawnMax;
    public int _current;

    public SpawnerObject _script;

    void Start()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        if (_current < _spawnMax)
        {
            _script.SpawRand();
            _current++;
        }
    }


    public void UpdateNumberObject()
    {
        _current -= 1;
    }

}
