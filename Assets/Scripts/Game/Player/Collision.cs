#region INCLUDES

using UnityEngine;


#endregion

public class Collision : MonoBehaviour
{
    #region VARIABLES

    
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]


    public float collisionRadius = 0.25f;

    public Vector2 bottomOffset, rightOffset, leftOffset;
    
    #endregion
    
    #region METHODS

    private void Update()
    {
        var position = transform.position;
        
        onGround = Physics2D.OverlapCircle((Vector2)position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)position + rightOffset, collisionRadius, groundLayer) 
            || Physics2D.OverlapCircle((Vector2)position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        var position = transform.position;
        
        Gizmos.DrawWireSphere((Vector2)position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)position + leftOffset, collisionRadius);
    }
    
    #endregion
}
