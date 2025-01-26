
#region INCLUDES
using UnityEngine;



#endregion
public class Animation : MonoBehaviour
{
    #region VARIABLES
    
    private Animator _anim;
    private Movement _move;
    private Collision _coll;
    [HideInInspector]
    public SpriteRenderer sr;

    private static readonly int OnGround = Animator.StringToHash("onGround");
    private static readonly int OnWall = Animator.StringToHash("onWall");
    private static readonly int OnRightWall = Animator.StringToHash("onRightWall");
    private static readonly int WallGrab = Animator.StringToHash("wallGrab");
    private static readonly int WallSlide = Animator.StringToHash("wallSlide");
    private static readonly int CanMove = Animator.StringToHash("canMove");
    private static readonly int IsDashing = Animator.StringToHash("isDashing");
    private static readonly int HorizontalAxis = Animator.StringToHash("HorizontalAxis");
    private static readonly int VerticalAxis = Animator.StringToHash("VerticalAxis");
    private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
    
    #endregion
    
    #region METHODS

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _coll = GetComponentInParent<Collision>();
        _move = GetComponentInParent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _anim.SetBool(OnGround, _coll.onGround);
        _anim.SetBool(OnWall, _coll.onWall);
        _anim.SetBool(OnRightWall, _coll.onRightWall);
        _anim.SetBool(WallGrab, _move.wallGrab);
        _anim.SetBool(WallSlide, _move.wallSlide);
        _anim.SetBool(CanMove, _move.canMove);
        _anim.SetBool(IsDashing, _move.isDashing);

    }

    //GetCurrentAnimatorStateInfo(0) devuelve el estado actual de la animacion
    //IsName("nombre de la animacion") compara el nombre de la animacion con el estado actual
    public bool IsAnimationPlaying(string name)
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    public void SetHorizontalMovement(float x,float y, float yVel)
    {
        _anim.SetFloat(HorizontalAxis, x);
        _anim.SetFloat(VerticalAxis, y);
        _anim.SetFloat(VerticalVelocity, yVel);
    }

    public void SetTrigger(string trigger)
    {
        _anim.SetTrigger(trigger);
    }

    public void Flip(int side)
    {

        if (_move.wallGrab || _move.wallSlide)
        {
            switch (side)
            {
                case -1 when sr.flipX:
                case 1 when !sr.flipX:
                    return;
            }
        }

        var state = (side != 1);
        sr.flipX = state;
    }
    
    #endregion
}
