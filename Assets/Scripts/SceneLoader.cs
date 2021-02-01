using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMenu() => SceneManager.LoadScene("Menu");
    public void LoadGame() => SceneManager.LoadScene("Game");
}
