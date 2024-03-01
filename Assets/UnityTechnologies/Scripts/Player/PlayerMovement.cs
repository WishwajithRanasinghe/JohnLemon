using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _movement;
    private Animator _animator;
    private Rigidbody _rBody;
    private Quaternion _rotation = Quaternion.identity;
    private AudioSource _audio;
    [SerializeField] private float _turnSpeed = 20f;
    
    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movement.Set(horizontal, 0f, vertical);
        _movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal,0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical,0f);

        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("IsWalking",isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,_movement,_turnSpeed * Time.deltaTime,0f);
        _rotation = Quaternion.LookRotation(desiredForward);

        PlayAudio(isWalking);
        
    }
    private void PlayAudio(bool isPlay)
    {
        if(isPlay)
        {
            if(!_audio.isPlaying)
            _audio.Play();
        }
        else
        {
            _audio.Stop();
        }
    }
    private void OnAnimatorMove()
    {
        _rBody.MovePosition(_rBody.position + _movement * _animator.deltaPosition.magnitude);
        _rBody.MoveRotation(_rotation);
    }
}
