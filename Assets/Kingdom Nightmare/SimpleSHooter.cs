using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSHooter : MonoBehaviour
{
    public int index;
    public Transform startPos;
    public Transform targetPos;
    private void Start()
    {
        InvokeRepeating(nameof(Shoot), 0, 0.3f);
    }
    private void Shoot()
    {
       
            var pre = ProjectilePool.Instance.GetObjectFromPool(index);
            pre.transform.position = startPos.position;
            pre.Shoot(new Vector2(0, 50), 100, startPos, targetPos.position);
        
    }
}
