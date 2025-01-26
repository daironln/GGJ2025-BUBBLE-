using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices;

public class Attack : MonoBehaviour
{
    private Animation _anim;
    private bool canAttack = true;
    private BubbleAttack bubbleAttack;


    private void Awake()
    {
        _anim = GetComponentInChildren<Animation>();
        bubbleAttack = GetComponent<BubbleAttack>();
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
                _anim.SetTrigger("attack");
                bubbleAttack.LaunchBubble();

                canAttack = false;

                Debug.Log("Ataque");
            }
        }

        //Cuando la animacion de ataque termina se activa la variable canAttack para que se pueda volver a atacar
        if (!_anim.IsAnimationPlaying("Attack"))
        {
            canAttack = true;
        }
    }
}
