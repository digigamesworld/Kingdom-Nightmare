using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //private variables
    private Transform target;
    private int currentLevel = 1;
    //inspectore variables
    [SerializeField] TowersSpecs towersSpecs;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, towersSpecs.Range.y);
    }

}
