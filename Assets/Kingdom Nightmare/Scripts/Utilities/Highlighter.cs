using UnityEngine;
public class Highlighter : MonoBehaviour
{
    private int _defaultLayerMask;
    private int _hightlightLayerMask;
    private void Awake()
    {
        _defaultLayerMask = gameObject.layer;
        _hightlightLayerMask = LayerMask.NameToLayer("Highlight");
    }
    private void OnMouseEnter()
    {
        transform.gameObject.layer = _hightlightLayerMask;
    }
    private void OnMouseExit()
    {
        transform.gameObject.layer = _defaultLayerMask;
    }
 
}
