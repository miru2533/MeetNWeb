﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : PersistentSingleton<PlayerDataManager>
{
    #region getseter
    public int Stemina
    {
        get {
            _stemina = PlayerPrefs.GetInt("Stemina");
            return _stemina;
        }
        set
        {
            _stemina = value;
            PlayerPrefs.SetInt("Stemina", value);
            PlayerPrefs.Save();
            MoneyUI.SetSteminaUI(_stemina, MaxStemina);

            // 모든 스태미너 풀로 채워짐
            if (_stemina > MaxStemina)
            {
                _stemina = MaxStemina;
            }
        }
    }

    public int Gold
    {
        get
        {
            _gold = PlayerPrefs.GetInt("Gold");
            return _gold;
        }
        set
        {
            _gold = value;
            PlayerPrefs.SetInt("Gold", value);
            PlayerPrefs.Save();
            MoneyUI.SetGoldUI(_gold);
        }
    }

    public int Cash
    {
        get
        {
            _cash = PlayerPrefs.GetInt("Cash");
            return _cash;
        }
        set
        {
            _cash = value;
            PlayerPrefs.SetInt("Cash", value);
            PlayerPrefs.Save();
            MoneyUI.SetCashUI(_cash);
        }
    }

    public DateTime SteminaUpdateTime
    {
        get
        {
            _steminaUpdateTime = DateTime.Parse(PlayerPrefs.GetString("SteminaTime"));
            return _steminaUpdateTime;
        }
        set
        {
            _steminaUpdateTime = value;
            PlayerPrefs.SetString("SteminaTime", value.ToString());
            PlayerPrefs.Save();
        }
    }

    public PlayerMoneyUI MoneyUI
    {
        get
        {
            if (_PlayerMoneyUI == null)
                _PlayerMoneyUI = FindObjectOfType<PlayerMoneyUI>();
            return _PlayerMoneyUI;
        }
    }
    #endregion

    public const int MaxStemina = 5;
    public DateTime currentTime;
    public TimeSpan steminaChargeTime;

    private int _stemina = 0;
    private int _gold = 0;
    private int _cash = 0;
    private DateTime _steminaUpdateTime;
    //public const long SteminaChargeTime = 100000000;
    private PlayerMoneyUI _PlayerMoneyUI = null;

    protected override void Initialize()
    {
        steminaChargeTime = new TimeSpan(0, 0, 10);

        FirstSetting();
        MoneyUI.SetSteminaUI(Stemina, MaxStemina);
        MoneyUI.SetGoldUI(Gold);
        MoneyUI.SetCashUI(Cash);
        /*
        if(!ES3.KeyExists("FirstRun"))
        {
            FirstSetting();
            ES3.Save<bool>("FirstRun", true);
        }
        else
        {

        }
        */
    }

    private void FirstSetting()
    {
        PlayerPrefs.DeleteAll();
        Stemina = 1;
        SteminaUpdateTime = DateTime.Now;
        Gold = 1000;
        Cash = 500;
    }

    private void Update()
    {
        currentTime = DateTime.Now;
        if (MoneyUI != null)
        {
            MoneyUI.SetTimeUI();
        }

        if (!IsSteminaFull())
        {
            TimeSpan diffTime = currentTime - SteminaUpdateTime;
            if (diffTime > steminaChargeTime)
            {
                Stemina += 1;
                SteminaUpdateTime += steminaChargeTime;
            }
            /*
            int diffHour = (int)((currentTime - steminaFirstConsumeTime).TotalHours);
            if (diffHour > MaxStemina)
            {
                diffHour = MaxStemina;
            }
            */
        }
    }

    public bool IsSteminaFull()
    {
        return Stemina >= MaxStemina;
    }
}
