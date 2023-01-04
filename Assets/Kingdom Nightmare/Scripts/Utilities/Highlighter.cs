using UnityEngine;

public class Highlighter : MonoBehaviour
{
    private int _defaultLayerMask;
    private int _hightlightLayerMask;
    private Transform[] _childtranses;
    private void Awake()
    {
        _defaultLayerMask = gameObject.layer;
        _hightlightLayerMask = LayerMask.NameToLayer("Highlight");
        _childtranses = transform.GetComponentsInChildren<Transform>();
    }
    private void OnMouseEnter()
    {
    for(int i=0; i< _childtranses.Length;i++)
            _childtranses[i].gameObject.layer = _hightlightLayerMask;
    }
    private void OnMouseExit()
    {
        for (int i = 0; i < _childtranses.Length; i++)
            _childtranses[i].gameObject.layer = _defaultLayerMask;
    }
 
}
