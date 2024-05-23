using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("檢測參數")]
    public Vector2 bottomOffset;
    public float checkRadius;
    public LayerMask groundLayer;
    [Header("狀態")]
    public bool isGround;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //檢測地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
}
