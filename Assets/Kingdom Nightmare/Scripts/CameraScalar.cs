using UnityEngine;
using System.Collections;

public class CameraScalar : MonoBehaviour {

    //inspector variables
     [SerializeField] private Vector2 _referenceResolution;
     [SerializeField] private float   _referenceCameraSize = 38.0f;
     [SerializeField] private float   _orthographicSize;

    //properties
    public float MaxOrthographicSize => _orthographicSize;
        //private fields
    private Camera _camera;
    public void Awake()   
    {
        _camera = GetComponent<Camera>();
        if (_camera == null) return;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = _referenceResolution.x / _referenceResolution.y;
        if (screenRatio <= targetRatio)
        {
            float diffrerenceInSize = targetRatio / screenRatio;
            _orthographicSize = _referenceCameraSize * diffrerenceInSize;

        }
        else
        {
            _orthographicSize = _referenceCameraSize;
        }
       _camera.orthographicSize = _orthographicSize;
	}

    
}
