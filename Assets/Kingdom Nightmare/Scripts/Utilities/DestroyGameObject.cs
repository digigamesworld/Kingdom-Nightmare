using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField]private float time;
    private void Start()
    {
        if(time>0)
        {
            Invoke("DestroyThisGameObject", time);
        }
    }
    public void DestroyThisGameObject()
    {
        Destroy(gameObject);
    }

}
