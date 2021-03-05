using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Doozy.Engine;
using Doozy.Engine.Nody;
using KanKikuchi.AudioManager;
using UniRx;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public List<string> eventName;
   public enum GameState
    {
        Title,
        Playing_Heart0,
        Playing_Heart1,
        Clear,
        Over
    }

    // public GameState currentState{ get; private set; } = GameState.Opening;
    public GameState currentState{ get; private set; } = GameState.Title;
    // public GameState currentState{ get; private set; } = GameState.Playing_Heart0;
    private int stage{ get; set; } = 1;
    private GraphController _graphController;

    private bool pauseFlag = false;

    private GameState oldState = GameState.Title;

    // 状態による振り分け処理
    public void dispatch(GameState state)
    {
        oldState = currentState;

        currentState = state;
        switch (state)
        {
            case GameState.Title:
                GameTitle();
                break;
            case GameState.Playing_Heart0:
                GameStart();
                break;
            case GameState.Playing_Heart1:
                break;
            case GameState.Clear:
                GameClear();
                break;
            case GameState.Over:
                //if (oldState == GameState.Playing)
                //{
                    GameOver();
                //}
                break;
        }
        Debug.Log(currentState);
    }

    private void Update()
    {
        if (currentState == GameState.Playing_Heart0 || currentState == GameState.Playing_Heart1)
        {
            if (Input.GetButtonDown("Retry"))
            {
                GameEventMessage.SendEvent(eventName[0]);
                dispatch(GameState.Playing_Heart0);
            }

            else if (Input.GetButtonDown("Pause"))
            {
                GameEventMessage.SendEvent(eventName[1]);
            }
        }
    }

    // オープニング処理
    void GameTitle()
    {
        currentState = GameState.Title;
        stage = 0;
        if (BGMManager.Instance.IsPlaying())
        {
            BGMSwitcher.FadeOutAndFadeIn(BGMPath.TITLE_BGM);
        }
        else
        {
            BGMManager.Instance.Play(BGMPath.TITLE_BGM);
        }
    }

    private void GameStart()
    {
        if (oldState == GameState.Title)
        {
            BGMSwitcher.FadeOutAndFadeIn(BGMPath.PAZZLE_BGM);
        }
    }
    private void GameClear()
    {
        GameEventMessage.SendEvent(eventName[3]);
        stage++;
    }
    private void GameOver()
    {
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            GameEventMessage.SendEvent(eventName[2]);
        });
    }
}
