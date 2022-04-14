using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    Animator animator;
    public bool isActive;
    [SerializeField] string managerName;
    PlateManager manager;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        manager = GameObject.Find(managerName).GetComponent<PlateManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isPushed", true);
            isActive = true;
            manager.Check();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isPushed", false);
            isActive = false;
        }
    }
}
