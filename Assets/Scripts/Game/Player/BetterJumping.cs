
#region INCLUDES

using UnityEngine;


#endregion

public class BetterJumping : MonoBehaviour
{
    #region VARIABLES
    private Rigidbody2D _rb;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    
    #endregion

    #region METHODS
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (_rb.velocity.y)
        {
            case < 0:
                _rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
                break;
            case > 0 when !Input.GetButton("Jump"):
                _rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
                break;
        }
    }
    
    #endregion
}
