using UnityEngine;
using System.Collections;

public class CameraScalar : MonoBehaviour {

    //inspector variables
     [SerializeField] private Vector2 _referenceResolution;
     [SerializeField] private float   _referenceCameraSize = 38.0f;
     [SerializeField] private float   _orthographicSize;
     [SerializeField] private Camera _mainCam;
     [SerializeField]private Camera _UICam;
    //properties
    public float MaxOrthographicSize => _orthographicSize;
        //private fields
    private Camera _camera;
    public void Awake()   
    {
       
        if (_mainCam == null) return;
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
        _mainCam.orthographicSize = _orthographicSize;
        if(_UICam != null) _UICam.orthographicSize = _orthographicSize;
    }

    
}
