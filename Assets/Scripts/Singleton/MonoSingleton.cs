﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    //스크립트 이름의 새 게임오브젝트 생성
                    GameObject newObj = new GameObject(typeof(T).FullName);
                    _instance = newObj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T;
        Initialize();
    }

    protected virtual void Initialize() { }
}
