#region INCLUDES
using System.Collections;
using UnityEngine;
using DG.Tweening;
#endregion

public class Movement : MonoBehaviour
{
    #region VARIABLES

    private Collision _coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private Animation _anim;

    [Space]
    [SerializeField] private float speed = -1;
    [SerializeField] private float jumpForce = -1;
    [SerializeField] private float slideSpeed = -1;
    [SerializeField] private float wallJumpLerp = -1;
    [SerializeField] private float dashSpeed = -1;

    [Space]
    public bool canMove;
    public bool wallGrab;
    [SerializeField] private bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]

    private bool _groundTouch;
    private bool _hasDashed;

    [SerializeField] private int side = 1;

    [Space]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    private BetterJumping _betterJumping;
    
    [Space]
    [SerializeField] private float raycastGarbWallCornerOffser = 2f;
    

    #endregion



    #region METHODS

    

    private void Awake()
    {
        _betterJumping = GetComponent<BetterJumping>();
        _coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animation>();
    }

    
    private void Update()
    {
        Move();


    }

    private void Move(){
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var xRaw = Input.GetAxisRaw("Horizontal");
        var yRaw = Input.GetAxisRaw("Vertical");
        var dir = new Vector2(x, y);

        Walk(dir);
        _anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if (_coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if(side != _coll.wallSide)
                _anim.Flip(side);//-1
                
            wallGrab = true;
            wallSlide = false;

            // GrabWallCorner(side);

        }

        if (Input.GetButtonUp("Fire3") || !_coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        if (_coll.onGround && !isDashing)
        {
            wallJumped = false;
            _betterJumping.enabled = true;
        }
        
        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if(x is > .2f or < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            var speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 3;
        }

        if(_coll.onWall && !_coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!_coll.onWall || _coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Jump"))
        {
            _anim.SetTrigger("jump");

            if (_coll.onGround)
                Jump(Vector2.up, false);
            if (_coll.onWall && !_coll.onGround)
                WallJump();
        }

        if (Input.GetButtonDown("Fire1") && !_hasDashed)
        {
            if(xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (_coll.onGround && !_groundTouch)
        {
            GroundTouch();
            _groundTouch = true;
        }

        if(!_coll.onGround && _groundTouch)
        {
            _groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        switch (x)
        {
            case > 0:
                side = 1;
                _anim.Flip(side);
                break;
            case < 0:
                side = -1;
                _anim.Flip(side);
                break;
        }
    }

    private void GrabWallCorner(int side){
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + raycastGarbWallCornerOffser), Vector2.right * side, 1.5f, _coll.groundLayer);

        //Reproducir animacion de agarre de esquina
        if (hit.collider == null)
        {
            _anim.SetTrigger("grabCornerWall");

            Debug.Log("Agarrando esquina de la pared");
        }
        
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + raycastGarbWallCornerOffser), Vector2.right * 1.5f * side, Color.red);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Dash(float x, float y)
    {
        if (Camera.main != null)
        {
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
            FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        }

        _hasDashed = true;

        _anim.SetTrigger("dash");

        var velocity = Vector2.zero;
        var dir = new Vector2(x, y);

        velocity += dir.normalized * dashSpeed;
        rb.velocity = velocity;
        StartCoroutine(DashWait());
    }
    
    private void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }
    
    private void WallJump()
    {
        if ((side == 1 && _coll.onRightWall) || side == -1 && !_coll.onRightWall)
        {
            side *= -1;
            _anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        var wallDir = _coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }
    private void GroundTouch()
    {
        _hasDashed = false;
        isDashing = false;

        side = _anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }
    private void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    private int ParticleSide()
    {
        var particleSide = _coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    private void WallSlide()
    {
        //No flip
        if(_coll.wallSide != side)
            _anim.Flip(side);//-1

        if (!canMove)
            return;

        var velocity = rb.velocity;
        var pushingWall = (velocity.x > 0 && _coll.onRightWall) || (velocity.x < 0 && _coll.onLeftWall);
        var push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        rb.velocity = !wallJumped ? new Vector2(dir.x * speed, rb.velocity.y) : Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        var particle = wall ? wallJumpParticle : jumpParticle;

        var velocity = rb.velocity;
        velocity = new Vector2(velocity.x, 0);
        velocity += dir * jumpForce;
        rb.velocity = velocity;

        particle.Play();
    }

    private IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (_coll.onGround)
            _hasDashed = false;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator DashWait()
    {
        FindObjectOfType<Trail>().ShowTrail();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }


    private IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    
    #endregion
}
