using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text scorePointsText;
    public Text highScorePointsText;
    public List<Image> heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject player;
    EntityStatus playerStatus;

    public static HUDManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        playerStatus = player.GetComponent<EntityStatus>();
        UpdateHearts();
        SetHighScorePoints(PlayerPrefs.GetInt("HighScore", 0));
    }

    void Update()
    {
        
    }

    public void SetScorePoints(int score)
    {
        scorePointsText.text = score.ToString();
    }

    public void SetHighScorePoints(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
        highScorePointsText.text = score.ToString();
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < playerStatus.hp)
            {
                heartImages[i].sprite = fullHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
}
