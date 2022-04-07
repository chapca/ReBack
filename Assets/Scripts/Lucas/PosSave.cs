using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosSave : MonoBehaviour
{

    [Header("PosSytstem")]
    [SerializeField] GameObject player;
    private List<Vector3> posForGhost1 = new List<Vector3>();
    private List<Vector3> posForGhost2 = new List<Vector3>();
    private List<Vector3> posRecord = new List<Vector3>();
    [SerializeField] GameObject GhostGO1;
    [SerializeField] GameObject GhostGO2;

    [Header("Index(s)")]
    [SerializeField] bool isGhost1 = true;

    private bool canMove = false;
    private bool canMove2 = false;

    private int moveIndexFor1 = 0;
    private int moveIndexFor2 = 0;
    void Start()
    {

    }

    private void FixedUpdate()
    {
        SavePos();
        if (canMove)
        {
            GhostGO1.SetActive(true);
            MoveGhost1();
        }
        if (canMove2)
        {
            GhostGO2.SetActive(true);
            MoveGhost2();
        }
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

    public void MoveGhost1()
    {
        if (posForGhost1 != null)
        {
            GhostGO1.transform.position = posForGhost1[moveIndexFor1];
            moveIndexFor1++;
            if(moveIndexFor1 >= posForGhost1.Count)
            {
                moveIndexFor1 = 0;
            }
        }
    }
    public void MoveGhost2()
    {
        if (posForGhost2 != null)
        {
            GhostGO2.transform.position = posForGhost2[moveIndexFor2];
            moveIndexFor2++;
            if(moveIndexFor2 >= posForGhost2.Count)
            {
                moveIndexFor2 = 0;
            }
        }
    }

    public void CoppyPos()
    {
        if (isGhost1)
        {
            posForGhost1 = new List<Vector3>(posRecord);
            posRecord.Clear();
            canMove = true;
        }
        else
        {
            posForGhost2 = new List<Vector3>(posRecord);
            posRecord.Clear();
            canMove2 = true;
        }
    }
}
