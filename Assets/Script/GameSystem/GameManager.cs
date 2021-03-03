using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Doozy.Engine;
using Doozy.Engine.Nody;
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
    public GameState currentState{ get; private set; } = GameState.Playing_Heart0;
    private int stage{ get; set; } = 1;
    private GraphController _graphController;

    private bool pauseFlag = false;
    // [SerializeField] private GameObject panel;
    // [SerializeField] private Text text;
    // [SerializeField] private GameObject gameClearCanvasPrefab;
    // private GameObject gameClearCanvasClone;
    // [SerializeField] private GameObject gameOverCanvasPrefab;
    // private GameObject gameOverCanvasClone;
    // private Button[] buttons;

    private void Start()
    {
        // Sound.LoadBgm("Title", "TitleBGM");
        // Sound.LoadBgm("Main", "MainBGM");
        // Sound.LoadBgm("BossWarning", "BossWarningBGM");
        // Sound.LoadBgm("Boss", "BossBGM");
        // Sound.LoadSe("PlayerAttack1", "PlayerAttackSE1");
        // Sound.LoadSe("PlayerAttack2", "PlayerAttackSE2");
        // Sound.LoadSe("PlayerAttack3", "PlayerAttackSE3");
        // Sound.LoadSe("PlayerDamage1", "PlayerDamageSE1");
        // Sound.LoadSe("PlayerDamage2", "PlayerDamageSE2");
        // Sound.LoadSe("PlayerWin", "PlayerWinSE");
        // Sound.LoadSe("PlayerDead", "PlayerDeadSE");
        // Sound.LoadSe("TitleButton", "TitleButtonSE");
        // Sound.LoadSe("BlockChange", "BlockChangeSE");
        // Sound.LoadSe("GameClear", "GameClearBGM");
        // Sound.LoadSe("GameOver", "GameOverBGM");
        // //以下未実装
        // Sound.LoadSe("Devil", "DevilSE");
        // Sound.LoadSe("Bug", "BugSE");
        // Sound.LoadSe("Gunner", "GunnerSE");
        // Sound.LoadSe("GunnerShot", "GunnerShotSE");
        // Sound.LoadSe("Dragon", "DragonSE");//ドラゴンの断末魔
        // Sound.LoadSe("DragonFire", "DragonFireSE");
        // Sound.LoadSe("EnemyAttack", "EnemyAttackSE");//敵の体当たり
        // dispatch(GameState.Opening);
        
    }

    // 状態による振り分け処理
    public void dispatch(GameState state)
    {
        GameState oldState = currentState;

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
        // Time.timeScale = 1f;
        //Sound.StopBgm();
        // Sound.PlayBgm("Title");
    }

    void GameStart()
    {
        // if (gameOverCanvasClone)
        // {
        //     Destroy(gameOverCanvasClone);
        // }
        // else if (gameClearCanvasClone)
        // {
        //     Destroy(gameClearCanvasClone);
        // }
        //Sound.StopBgm();
        // Sound.PlayBgm("Main");
    }

    public void JudgeMove()
    {
        if (pauseFlag == false)
        {
            Time.timeScale = 0f;
            pauseFlag = true;
        }
        else if (pauseFlag == true)
        {
            Time.timeScale = 1f;
            pauseFlag = false;
        }
    }

    public void GameClear()
    {
        GameEventMessage.SendEvent(eventName[3]);
        stage++;
        dispatch(GameState.Playing_Heart0);
        // gameClearCanvasClone = Instantiate(gameClearCanvasPrefab);
        //後の処理はgameClearCanvasCloneで処理される。
    }
    public void GameOver()
    {
        // gameOverCanvasClone = Instantiate(gameOverCanvasPrefab);
        //後の処理はgameOverCanvasCloneで処理される。
        Observable.Timer(TimeSpan.FromSeconds(1)).Subscribe(_ =>
        {
            GameEventMessage.SendEvent(eventName[2]);
            dispatch(GameState.Playing_Heart0);
        });
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    //ここからタイトルのボタン
    public void StartGame()
    {
        // NowLoading.sceneName = "PlayerScene";
        SceneManager.LoadScene("NowLoading");//Unityでの遷移の設定はまだしてない?
        //dispatch(GameState.Playing);
    }
    public void ExplainImage()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("終わり");
    }
    //ここまでタイトルのボタン
    public void TitleGame()
    {
        // NowLoading.sceneName = "TitleScene";
        SceneManager.LoadScene("NowLoading");
    }
}
