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

    // Update is called once per frame
    void Update()
    {

    }

    public void ScoreDisplay(int score)
    {
        _scoreText.text = "Score: " + score;
    }
}
