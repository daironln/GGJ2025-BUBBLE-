#region INCLUDES

using UnityEngine;
using DG.Tweening;

#endregion

public class UILevite : MonoBehaviour {

    [SerializeField] private float length = 10f;
    [SerializeField] private float time = 0.5f;

    private void OnEnable() {
        Up();
    }

    private void Up() {
        if(!gameObject.activeInHierarchy) return;
        
        transform.DOBlendableLocalMoveBy(new Vector3(0f, length, 0f), time).OnComplete(Down);
        
    }

    private void Down() {
        transform.DOBlendableLocalMoveBy(new Vector3(0f, -length, 0f), time).OnComplete(Up);

    }
}