using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SpawnerObject : MonoBehaviour 
{
    public GameObject[] _listObject;
    public float _x;
    public float _y;
    public float _z;
    private void Start()
    {
        
    }


    public void SpawRand()
    {
        int _choice = Random.Range(0, _listObject.Length);
        Debug.Log(_choice);

        Instantiate(_listObject[_choice], transform.position + new Vector3(Random.Range(-_x, _x), Random.Range(-_y, _y), Random.Range(-_z, _z)), _listObject[_choice].transform.rotation);

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(_x * 2, _y * 2, _z * 2));
    }
}
