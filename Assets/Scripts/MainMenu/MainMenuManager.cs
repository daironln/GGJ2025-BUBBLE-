using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{   
    [SerializeField] private GameObject _abautUsPannel;
    private void StartGame()
    {
        GameManager.Instance.gameState = GameState.InPlay;
    }

    private void ContinueGame()
    {
        GameManager.Instance.gameState = GameState.InPlay;
    }


    private void QuitGame()
    {
        Application.Quit();
    }

    private void AboutUs()
    {
        _abautUsPannel.SetActive(true);
    }
}
