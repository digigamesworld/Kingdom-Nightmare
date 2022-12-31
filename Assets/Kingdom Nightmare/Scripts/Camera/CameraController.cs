using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //inspector variables
    [SerializeField] float           _zoomSpeed = 3.0f;
    [SerializeField] float           _panMargin = 50.0f;
    [SerializeField] float           _panSpeed = 5.0f;
    [SerializeField] Vector2         _orthographicSize = new(20.0f, 38.0f);
    [SerializeField] Transform       _cameraOrigin;
    [SerializeField] float           _mapRadious = 30.0f;
    [SerializeField] Camera          _camera;
    //private fields
    private Vector3 _cameraOriginPos;
    //properties
    private CameraScalar CameraS=> GetComponent<CameraScalar>();
    private Vector2 OrthographicSize
    { get 
        {
            if (CameraS == null) return _orthographicSize;
            else return new Vector2(_orthographicSize.x,CameraS.MaxOrthographicSize);
         }
    }

    private void Start()
    {
        _cameraOriginPos = transform.position;
        Debug.Log(_cameraOriginPos);
    }
    private void Update()
    {
        SetZoom();
       // DoPan();
    
    }
    public void DoPan()
    {


        Vector2 mousePos = Input.mousePosition;

        if (mousePos.x < _panMargin)
        {
            transform.position -= _panSpeed * Time.deltaTime * transform.right;

                 
        }
        else if(mousePos.x > Screen.width - _panMargin)
        {
            transform.position += _panSpeed * Time.deltaTime * transform.right;

        }
        if (mousePos.y < _panMargin)
        {
            transform.position -= _panSpeed * Time.deltaTime * transform.up;


        }
        else if (mousePos.y > Screen.height - _panMargin)
        {
            transform.position += _panSpeed * Time.deltaTime * transform.up;

        }
         transform.position = ClampCameraPosition(transform.position);
    }
    private void SetZoom()
    {

        float mouseWheelY = Input.mouseScrollDelta.y;
        _camera.orthographicSize -= mouseWheelY * _zoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, OrthographicSize.x, OrthographicSize.y);
        // transform.position =  ClampCameraPosition(transform.position);
    }


    private Vector3 ClampCameraPosition(Vector3 targetPosition)
    {
        float cameraHeight = _camera.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * _camera.aspect;
    
        float minPosX = _cameraOriginPos.x - _mapRadious  ;
        float maxPosX = _cameraOriginPos.x + _mapRadious ;
        float minPosY = _cameraOriginPos.y - _mapRadious ;
        float maxPosY = _cameraOriginPos.y + _mapRadious ;
        Debug.Log("minPosX " + minPosX + "maxPosX" + maxPosX + "minPosY" + minPosY + "maxPosY" + maxPosY);
        float clampedPosX = Mathf.Clamp(targetPosition.x, minPosX, maxPosY);
        float clampedPosY = Mathf.Clamp(targetPosition.y, minPosY, maxPosY);
        
        return new Vector3(clampedPosX, clampedPosY, _cameraOriginPos.z);

    }

}
