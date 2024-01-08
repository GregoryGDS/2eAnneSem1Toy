using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObject : MonoBehaviour 
{
    public GameObject[] _listObject;
    public Collider _collider;

    public float _maxX;
    public float _minX;

    public float _maxY;
    public float _minY;

    public float _maxZ;
    public float _minZ;

    public Transform _groupParent;
    private void Start()
    {
        _maxX = _collider.bounds.max.x;
        _minX = _collider.bounds.min.x;

        _maxY = _collider.bounds.max.y;
        _minY = _collider.bounds.min.y;

        _maxZ = _collider.bounds.max.z;
        _minZ = _collider.bounds.min.z;
    }


    public void SpawRand()
    {
        int _choice = Random.Range(0, _listObject.Length);

        Instantiate(_listObject[_choice], 
            transform.position + new Vector3(
                Random.Range(_minX, _maxX), 
                Random.Range(_minY, _maxY), 
                Random.Range(_minZ, _maxZ)), 
            _listObject[_choice].transform.rotation,
            _groupParent
            );

    }

}
