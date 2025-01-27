using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{   
    [SerializeField] private GameObject _abautUsPannel;
    public void StartGame()
    {
        GameManager.Instance.gameState = GameState.InPlay;

        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        GameManager.Instance.gameState = GameState.InPlay;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void AboutUs()
    {

        _abautUsPannel.GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        _abautUsPannel.transform.DOBlendableLocalMoveBy(new Vector3(0f, 3000f, 0f), 0.8f).SetEase(Ease.OutElastic);
    }

    public void DeAboutUs()
    {
        _abautUsPannel.GetComponent<CanvasGroup>().DOFade(0f, 0.5f);
        _abautUsPannel.transform.DOBlendableLocalMoveBy(new Vector3(0f, -3000f, 0f), 0.8f).SetEase(Ease.OutBounce);
    }
}
