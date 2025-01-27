using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Quit(){
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 1){
                Time.timeScale = 0;

                pausePanel.SetActive(true);

            }else{
                Time.timeScale = 1;

                pausePanel.SetActive(false);

            }

        }
    }
}
