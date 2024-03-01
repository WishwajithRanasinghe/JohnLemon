using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private bool _isPlayerInRange;
    [SerializeField] private Transform _player;
    [SerializeField] private GameEnding _endScript;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == ("Player"))
        {
            _isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.transform.tag == ("Player"))
        {
            _isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if(_isPlayerInRange)
        {
            Vector3 direction = _player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position,direction);
            RaycastHit hit;


            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.transform.tag ==("Player"))
                {
                    _endScript.CaughtPlayer();
                }

            }
        }
    }

}
