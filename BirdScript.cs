using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdrigidbody;
    public float jumpPower = 20;
    public float duckPower = 10;
    public LogicScript logic;
    public bool isBirdAlive = true;
    public bool multi = false;
    public float charge = 0;
    public int mana = 100;
    public float manaTime = 0;
    public GameObject yourmom;
    public ParticleSystem particle;
    public ParticleSystem death;
    public TextMeshProUGUI globalText;
    public Sprite birdsprite1;
    public Sprite birdsprite2;
    public Sprite birdsprite3;
    public Sprite birdsprite4;
    public int multiplier = 1;
    public async void LoadSomeData()
    {
        Dictionary<string, string> savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "key" });
        logic.GetComponent<LogicScript>().maxScore = int.Parse(savedData["key"]);
        savedData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "skin" });
        if (savedData["skin"] == "red")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = birdsprite1;
        }
        else if (savedData["skin"] == "blue")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = birdsprite3;
        }
        else if (savedData["skin"] == "penguin")
        {
            multiplier = 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = birdsprite4;
            birdrigidbody.gravityScale = 2.25f;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = birdsprite2;
        }
    }
    public void getScore()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "global",
            StartPosition = 0,
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        Debug.Log("amogus retrieved");
        foreach (var item in result.Leaderboard)
        {
            globalText.SetText("wr: " + item.StatValue.ToString());
        }
    }
    public void sendScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest { Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "global", Value = score } } };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("yippee");
    }
    private void OnError(PlayFabError error)
    {
        Debug.LogWarning("haaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        Debug.LogError("error:");
        Debug.LogError(error.GenerateErrorReport());
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadSomeData();
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name == "Particle System")
            {
                particle = eachChild.GetComponent<ParticleSystem>();
                yourmom = eachChild.gameObject;
            }
            if (eachChild.name == "Particle System (1)")
            {
                death = eachChild.GetComponent<ParticleSystem>();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isBirdAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                birdrigidbody.velocity = Vector2.up * jumpPower;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                birdrigidbody.velocity = Vector2.down * duckPower + Vector2.right * duckPower / 3;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && mana >= 100)
            {
                yourmom.transform.localScale = new Vector3(1, 1, 1);
                mana -= 100;
                birdrigidbody.velocity = Vector2.up * jumpPower * 3;
                particle.Play();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                logic.noMana();
            }
            manaTime += Time.deltaTime * 4;
            if (Input.GetKeyDown(KeyCode.DownArrow) && mana >= 100)
            {
                yourmom.transform.localScale = new Vector3(1, -1, 1);
                mana -= 100;
                birdrigidbody.velocity = Vector2.down * duckPower * 3 + Vector2.right * duckPower / 3;
                particle.Play();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && mana < 100)
            {
                logic.noMana();
            }
            if (manaTime >= 1)
            {
                if (mana < 200)
                {
                    mana += 1;
                }
                manaTime = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    int i = 0;
    public async void data()
    {
        var point = new Dictionary<string, object>()
        {{"key", logic.GetComponent<LogicScript>().maxScore}};
        await CloudSaveService.Instance.Data.ForceSaveAsync(point);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "death" && i == 0)
        {
            isBirdAlive = false;
            logic.GetComponent<LogicScript>().gameOver();
            death.Play();
            i = 1;
            data();
            sendScore(logic.GetComponent<LogicScript>().playerScore);
            getScore();
        }
    }
}