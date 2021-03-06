using System;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using Doozy.Engine.Nody;
using KanKikuchi.AudioManager;
using UniRx;

//外部サイトのスクリプトを参照して制作
//https://qiita.com/yaju/items/5a3b46104f4f0a767c7f

//ゲームの状態の管理ととそれに因んだ処理を行う
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
   
    public GameState currentState{ get; private set; } = GameState.Title;
    //現在のステージ
    private int stage{ get; set; } = 1;
    private GraphController _graphController;

    private bool pauseFlag = false;

    private GameState oldState = GameState.Title;

    // 状態による振り分け処理
    //切り替わった瞬間に一回だけ処理を行う
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
                GameOver();
                break;
        }
        Debug.Log(currentState);
    }

    private void Update()
    {
        //ゲームプレイ中に対応したボタンを押すとDoozy.Engineにメッセージを飛ばしてイベントを行う
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

    // タイトル処理
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
    
    //メインシーン処理
    private void GameStart()
    {
        if (oldState == GameState.Title)
        {
            BGMSwitcher.FadeOutAndFadeIn(BGMPath.PAZZLE_BGM);
        }
    }
    
    //クリア処理
    private void GameClear()
    {
        GameEventMessage.SendEvent(eventName[3]);
        stage++;
    }
    
    //ミス処理
    //1秒待機した後にイベントメッセージを送信
    private void GameOver()
    {
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            GameEventMessage.SendEvent(eventName[2]);
        });
    }
}
