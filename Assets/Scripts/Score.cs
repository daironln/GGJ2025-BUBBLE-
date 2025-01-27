using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : PersistentSingleton<Score>
{
    [SerializeField] private TextMeshProUGUI scoreText;
    int score = 0;

    public void AddScore(){
        score++;
        scoreText.SetText($"Basura: {score.ToString()}");
    }
}
