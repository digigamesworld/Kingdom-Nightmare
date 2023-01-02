using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we darw ghost path with this class in the editor
public class DrawPath : MonoBehaviour {
    
    //private fields
    private List<Transform> _pathNodes = new List<Transform>();//all the path nodes transform on the path
    private Transform[] nodesArray;
    public List<Transform> PathNodes
    {
        get
        {
            _pathNodes.Clear();
            var transformList = FindObjectOfType<DrawPath>().GetComponentsInChildren<Transform>();
            foreach (Transform tr in transformList)
            {
                if (tr != transform)
                {
                    _pathNodes.Add(tr);//add transform in the nodesArray to path node except this gameObject  transform
                }

            }
            return _pathNodes;
        }
    }

    private void OnDrawGizmos()
    {
       //every time we creare a new path node array will be updated 
        nodesArray = GetComponentsInChildren<Transform>();
        _pathNodes.Clear();
        foreach (Transform tr in nodesArray)
        {
            if(tr != transform)
            {
                _pathNodes.Add(tr);//add transform in the nodesArray to path node except this gameObject  transform
            }
   
        }
        //if there is a node
        if(_pathNodes != null)
        {
            //and if there is at least two nodes on path
            if(_pathNodes.Count>1)
            {
                for (int i = 1; i < _pathNodes.Count; i++)
                {
                    Vector3 position = _pathNodes[i].position;
                    Vector3 lastNodePos = _pathNodes[i - 1].position;
                    Gizmos.DrawLine(lastNodePos, position);
                    Gizmos.DrawWireSphere(position, 0.2f);
                }
            }
        }

    }

}
