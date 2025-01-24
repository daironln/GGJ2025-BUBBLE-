#region INCLUDES

using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

#endregion

public class Trail : MonoBehaviour
{
    
    #region VARIABLES
    private Movement _move;
    private Animation _anim;
    [SerializeField] private Transform trailParent;
    [SerializeField] private Color trailColor;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float trailInterval;
    [SerializeField] private float fadeTime;

    #endregion
    
    #region METHODS
    private void Start()
    {
        _anim = FindObjectOfType<Animation>();
        _move = FindObjectOfType<Movement>();
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void ShowTrail()
    {
        var s = DOTween.Sequence();

        for (var i = 0; i < trailParent.childCount; i++)
        {
            var currentTrail = trailParent.GetChild(i);
            
            s.AppendCallback(()=> currentTrail.position = _move.transform.position);
            s.AppendCallback(() => currentTrail.GetComponent<SpriteRenderer>().flipX = _anim.sr.flipX);
            s.AppendCallback(()=>currentTrail.GetComponent<SpriteRenderer>().sprite = _anim.sr.sprite);
            s.Append(currentTrail.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
            s.AppendCallback(() => FadeSprite(currentTrail));
            s.AppendInterval(trailInterval);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void FadeSprite(Component current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }
    
    #endregion

}
