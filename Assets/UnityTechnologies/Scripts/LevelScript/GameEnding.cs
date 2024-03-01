using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private float _imageDuration = 3f;
    [SerializeField] private float _timer;
    [SerializeField] private CanvasGroup _imageWin;
    [SerializeField] private CanvasGroup _imageOut;
    [SerializeField] private AudioClip caughtClip;
    [SerializeField] private AudioClip ScapeClip;
    [SerializeField] private AudioSource _audioSource;

    
    private bool _isPlayerExit;
    private bool _isPlayerCaught;

    private void Update()
    {
        if(_isPlayerExit)
        {
            EndLevel(_imageWin,false);
        }
        else if(_isPlayerCaught)
        {
            EndLevel(_imageOut,true);
        }
        
    }
    public void CaughtPlayer()
    {
        _isPlayerCaught = true;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.tag == "Player")
        {
            _isPlayerExit = true;
        }
    }
    private void EndLevel(CanvasGroup imageGroup,bool doRestart)
    {
        if(doRestart)
        {
            if(!_audioSource.isPlaying)
            _audioSource.PlayOneShot(caughtClip);
        }
        else
        {
            if(!_audioSource.isPlaying)
            _audioSource.PlayOneShot(ScapeClip);
        }
        _timer += Time.deltaTime;
        imageGroup.alpha = _timer/_fadeDuration;
        if(_timer > _fadeDuration + _imageDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                Application.Quit();
            }
            
        }
    }
}
