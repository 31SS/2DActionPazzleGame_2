using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine;
using Doozy.Engine.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    private const float INTERVAL = 1.0f;
    private GameManager _gameManager;
    public SceneLoader sceneLoader;
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        if (_gameManager.currentState == GameManager.GameState.Playing_Heart0 || _gameManager.currentState == GameManager.GameState.Playing_Heart1)
        {
            if (Input.GetButtonDown("Retry"))
            {
                // GameEventMessage.SendEvent(_gameManager.eventName[0]);
                _gameManager.dispatch(GameManager.GameState.Playing_Heart0);
                FadeManager.Instance.LoadScene(SceneManager.GetActiveScene().name, INTERVAL);
            }

            else if (Input.GetButtonDown("Pause"))
            {
                GameEventMessage.SendEvent(_gameManager.eventName[1]);
            }
        }
    }
}
