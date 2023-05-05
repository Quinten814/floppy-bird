using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogicScript : MonoBehaviour
{
    public int playerScore = 0, maxScore;
    public Text scoreText;
    public Text bonusText;
    public TextMeshProUGUI maxText;
    public GameObject GameOverScreen;
    public GameObject grandflapper;
    public GameObject bird;
    public int i = 0;
    public Text manaText;
    public float newmovespeed;

    void grandmaster()
    {
        grandflapper.SetActive(true);
        bird.GetComponent<BirdScript>().isBirdAlive = false;
    }
    private void Start()
    {
        bonusText.text = "space = jump\nlshift = duck\nup arrow = fly\ndown arrow = dive";
    }
    // Update is called once per frame
    void Update()
    {
        newmovespeed += 0.0001f;
        if (bird.GetComponent<BirdScript>().isBirdAlive == true)
        {
            manaText.text = "mana: " + bird.GetComponent<BirdScript>().mana.ToString();
            if (Input.GetKeyDown(KeyCode.LeftShift) == true)
            {
                bonusText.text = "COOL!! \n +" + 10000 * bird.GetComponent<BirdScript>().multiplier  +" POINTS!";
                playerScore = playerScore + 10000 * bird.GetComponent<BirdScript>().multiplier;
            }
            playerScore = playerScore + bird.GetComponent<BirdScript>().multiplier;
            if (playerScore <= 9999)
            {
                scoreText.text = "points: " + playerScore.ToString() + "\ngrade: F";
            }
            if (playerScore >= 10000 && playerScore <= 19999)
            {
                scoreText.text = "points: " + playerScore.ToString() + "\ngrade: D";
            }
            if (playerScore >= 20000 && playerScore <= 49999)
            {
                scoreText.text = "points: " + playerScore.ToString() +"\ngrade: C";
            }
            if (playerScore >= 50000 && playerScore <= 99999)
            {
                scoreText.text = "points: " + playerScore.ToString() +"\ngrade: B";
            }
            if (playerScore >= 100000 && playerScore <= 999999)
            {
                scoreText.text = "points: ?????? \ngrade: A" ;
            }
            if (playerScore >= 1000000 && playerScore <= 9999999)
            {
                scoreText.text = "points: " + playerScore.ToString() + "\ngrade: S";
            }
            if (playerScore >= 10000000 && playerScore <= 99999999)
            {
                scoreText.text = "points: " + playerScore.ToString() + "\ngrade: FLAPPER";
            }
            if (playerScore >= 100000000)
            {
                scoreText.text = "points: " + playerScore.ToString() + "\ngrade: GF";
                grandmaster();
            }
        } else {
            if (playerScore >= 100000 && playerScore <= 999999)
            {
                scoreText.text = "points: " + playerScore.ToString() + "\ngrade: A" ;
            }
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        grandflapper.SetActive(false);
    }

    public void gameOver()
    {
        maxScore = Mathf.Max(maxScore, playerScore);
        maxText.SetText("high score: " + maxScore.ToString());
        GameOverScreen.SetActive(true);
    }

    public void noMana()
    {
        bonusText.text = "NOT ENOUGH MANA!";
    }
}
