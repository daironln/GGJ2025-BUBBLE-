using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices;

public class Attack : MonoBehaviour
{
    public Animator _anim;
    private bool canAttack = true;
    private BubbleAttack bubbleAttack;

    [SerializeField]
    private float cullDown = 0.8f;

    private PlayerAudioController playerAudioController;


    private void Awake()
    {
        // _anim = GetComponentInChildren<Animator>();
        bubbleAttack = GetComponent<BubbleAttack>();
        playerAudioController = GetComponent<PlayerAudioController>();
    }

    private void Update()
    {
        AttackButton();
    }

    //Esta funcion hace que al presionar el boton de ataque se active la animacion de ataque y no se pueda atacar de nuevo hasta que se termine la animacion
    public void AttackButton()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (canAttack)
            {
                _anim.SetBool("attack", true);
                bubbleAttack.LaunchBubble();

                playerAudioController.PlayAttackSound();

                canAttack = false;

                Debug.Log("Ataque");
            }
        }

        //Cuando la animacion de ataque termina se activa la variable canAttack para que se pueda volver a atacar
        if (cullDown > 0)
        {
            cullDown -= Time.deltaTime;
        }
        else
        {
            canAttack = true;

            _anim.SetBool("attack", false);


            cullDown = 0.5f;
        }
    }
}
