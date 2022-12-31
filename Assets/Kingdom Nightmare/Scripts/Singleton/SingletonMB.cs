using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMB<T> : MonoBehaviour where T:MonoBehaviour
{
 
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if(_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(T).ToString() + "(singleton)";
                    T _instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);

                }
        
            }
            return _instance;
        }
    }
    
    
}
