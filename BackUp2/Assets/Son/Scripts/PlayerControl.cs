using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    //Animation States
    const string DANCE = "dance";
    const string POINT = "pointing";
    const string WAIT = "wait";
    const string TALKING = "talking";
    const string SAMBADANCE = "sambadance";
    const string THINK = "think";
    const string CHICKENDANCE = "chickendance";

    private string currentState;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        Invoke("StartGame", 2f);
    }

    public void StartGame()
    {
        GameManager.Instance.isDance = true;
        Debug.Log("isDance T");

    }
    void Update()
    {
    
        if (GameManager.Instance.isDance)
        {
            ChangeAnimationState(DANCE);
        }
        if (GameManager.Instance.isDecidedTalking)
        {
            ChangeAnimationState(TALKING);
        }
        if (GameManager.Instance.isDecidedThinking)
        {
            ChangeAnimationState(THINK);
        }
        if (GameManager.Instance.isSambaDance)
        {
            ChangeAnimationState(SAMBADANCE);
        }
        if (GameManager.Instance.isChickenDance)
        {
            ChangeAnimationState(CHICKENDANCE);
        }
        
        if (GameManager.Instance.isWait)
        {
            ChangeAnimationState(WAIT);
        }

    }


    void ChangeAnimationState(string newState)
    {
            
        if (currentState == newState) return;

        _animator.Play(newState);
        currentState = newState;

        if (newState == POINT)
            _animator.applyRootMotion = true;
        else
            _animator.applyRootMotion = false;

        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

    }
}
