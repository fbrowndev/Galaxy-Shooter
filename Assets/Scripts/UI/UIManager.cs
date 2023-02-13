using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Manages all the UI Methods
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Text Holders")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private TMP_Text _ammoText;

    [Header("Sprite Containers")]
    [SerializeField] private Sprite[] _livesSprites;

    //gaining access
    [Header("Display Image")]
    [SerializeField] private Image _livesImg;

    [SerializeField] private Slider _slider;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is null.");
        }
    }

    private void Update()
    {
        ExitGame();
    }

    #region Public Methods
    /// <summary>
    /// Handles all updates for player score
    /// </summary>
    /// <param name="playerScore"></param>
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    /// <summary>
    /// handles all updates for current player lives
    /// </summary>
    /// <param name="currentLives"></param>
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverTextFlicker());
    }

    /// <summary>
    /// Handling all logic for the Thrust bar component
    /// </summary>
    /// <param name="thrust"></param>
    public void SetMaxThrust(int thrust)
    {
        _slider.maxValue = thrust;
        _slider.value = thrust;
    }

    public void SetThrust(float thrust)
    {
        _slider.value = thrust;
    }

    /// <summary>
    /// Handling display for ammo count
    /// </summary>
    /// <returns></returns>
    public void AmmoCheck(int ammoTotal)
    {
        _ammoText.text = ammoTotal.ToString() + " / 15";
    }
    #endregion


    IEnumerator GameOverTextFlicker()
    {
        while(true)
        {
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.6f);
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.6f);
        }
    }

    void ExitGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
