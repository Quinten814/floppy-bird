using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EasyScript : MonoBehaviour
{
    public int gameScene;

    public void startGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
