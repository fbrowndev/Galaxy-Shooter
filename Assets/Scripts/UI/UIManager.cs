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


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    #region Public Methods
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    #endregion

}
