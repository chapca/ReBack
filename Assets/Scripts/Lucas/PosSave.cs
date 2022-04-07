using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosSave : MonoBehaviour
{

    [Header("PosSytstem")]
    [SerializeField] GameObject player;
    [SerializeField] List<Vector3> posForGhost1 = new List<Vector3>();
    [SerializeField] List<Vector3> posForGhost2 = new List<Vector3>();
    [SerializeField] List<Vector3> posRecord = new List<Vector3>();

    [Header("Index(s)")]
    [SerializeField] bool isGhost1 = true;
    void Start()
    {

    }

    private void FixedUpdate()
    {
        SavePos();
    }

    public void SavePos()
    {
        if (isGhost1)
        {
            posRecord.Add(player.transform.position);
            if (GameManager.instance.GetHasRespawned())
            {
                CoppyPos();
                isGhost1 = false;
                GameManager.instance.SetHasRespawned(false);
            }
        }
        else
        {
            posRecord.Add(player.transform.position);
            if (GameManager.instance.GetHasRespawned())
            {
                CoppyPos();
                isGhost1 = true;
                GameManager.instance.SetHasRespawned(false);
            }
        }
    }

    public virtual void Move()
    {

    }

    public void CoppyPos()
    {
        if (isGhost1)
        {
            posForGhost1 = new List<Vector3>(posRecord);
            posRecord.Clear();
        }
        else
        {
            posForGhost2 = new List<Vector3>(posRecord);
            posRecord.Clear();
        }
    }
}
