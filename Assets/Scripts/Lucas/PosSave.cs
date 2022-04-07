using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosSave : MonoBehaviour
{

    [Header("PosSytstem")]
    [SerializeField] GameObject player;
    [SerializeField] Queue<Vector3> posForGhost1 = new Queue<Vector3>();
    [SerializeField] Queue<Vector3> posForGhost2 = new Queue<Vector3>();  

    [Header("Index(s)")]
    [SerializeField] bool isGhost1;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void SavePos()
    {
        if(isGhost1)
        {
            posForGhost1.Enqueue(player.transform.position);
        }
        else
        {
            posForGhost2.Enqueue(player.transform.position);
        }
    }

    public virtual void Move()
    {

    }
}
