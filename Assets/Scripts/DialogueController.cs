#region INCLUDES

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class DialogueController : MonoBehaviour {

    private bool _canSpeakOrray = false;
    private int _textIndex = 0;
    
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    [SerializeField] private Button next;
    
    [SerializeField] private float textSpeed = 0.2f;

    [SerializeField] private GameObject nextIndicator;
    [SerializeField] private GameObject tutorialPanel;

    private string[] text = new string[] {
        "Hola, objetivo 6. Las aguas estan muy contamindas.",
        "Nos gustaria que hicieras una Biorremedacion.",
        "Usa click derecho para atrapar basura con tu jabolina.",
        "Usa click izquierdo para dashear y llegar a lugares mas lejanos.",
        "Con shift en una pared puedes escalarla.",
        "Ten en cuenta que ahora mismo eres inmortal por falta de tiempo (Es una Jam)."
    };


    private void Start(){
        _textIndex = 0;

        tutorialPanel.SetActive(true);
        WriteText(text[_textIndex], dialogueTxt);
        NextDialogue(text);
    
    }

    private void NextDialogue(string[] text) {
        next.onClick.AddListener(() => {
            if (_textIndex == text.Length - 1) {
                nextIndicator.SetActive(false);
                tutorialPanel.SetActive(false);
            }
            
            if (_canSpeakOrray && _textIndex < text.Length - 1) {
                WriteText(text[++_textIndex], dialogueTxt);
            }

        });
    }

    private void WriteText(string text, TextMeshProUGUI textUI) {
        StartCoroutine(Write(text, textUI));
    }
    private IEnumerator Write(string text, TextMeshProUGUI textUI) {
        textUI.SetText(" ");
        nextIndicator.SetActive(false);
        _canSpeakOrray = false;
        
        foreach (var character in text) {
            textUI.text += character;
            yield return new WaitForSeconds(textSpeed);
        }

        _canSpeakOrray = true;
        nextIndicator.SetActive(true);
    }


}