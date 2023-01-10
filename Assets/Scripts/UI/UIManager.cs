using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages all the UI Methods
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Text Holders")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _gameOverText;

    [Header("Sprite Containers")]
    [SerializeField] private Sprite[] _livesSprites;

    //gaining access
    [Header("Display Image")]
    [SerializeField] private Image _livesImg;



    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
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
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverTextFlicker());
        }
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
}
