using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region GameMode
    [SerializeField] private Item[] _gameMode;
    #endregion
    [SerializeField] private IuManager _iuManager;
    [SerializeField] private Button _restart;
    public GameMode GameMode { get; private set; }
    public event Action<Item> ChangeMode;
    private int lvl = 0;
    private void Awake()
    {
        _iuManager.SendCheck += ReceivingMessages;
        _restart.onClick.AddListener(() => RestartGame());
    }
    void Start()
    {
        ChangeGameMode();
    }

    private void ChangeGameMode()
    {
        if (PlayerPrefs.HasKey("GameMode"))
            lvl = PlayerPrefs.GetInt("GameMode");
        ChangeMode?.Invoke(_gameMode[lvl]);
        GameMode = (GameMode)lvl;
        
    }
    private void ReceivingMessages(string message)
    {
        if (message == "win")
        {
            if (lvl < 2)
            {
                lvl++;
                PlayerPrefs.SetInt("GameMode", lvl);
                ChangeGameMode();
            }
            else
            {
                Debug.Log("GameOver");
                _iuManager.GameOver();
            }
        }
    }

    private void RestartGame()
    {
        PlayerPrefs.DeleteKey("GameMode");
        SceneManager.LoadScene("Game");
    }
}
