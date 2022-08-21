using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator doorAnim;
    private bool isFindPlayer;
    private float DoorAnimBlend;
    private void Start()
    {
        isFindPlayer = false;
        doorAnim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (isFindPlayer)
        {            
            DoorAnimBlend += Time.deltaTime;
            if(DoorAnimBlend >= 1)
            {
                DoorAnimBlend = 1f;
            }
            DoorOpen();
        }
        else
        {
            DoorAnimBlend -= Time.deltaTime;
            if (DoorAnimBlend <= 0)
            {
                DoorAnimBlend = 0f;
            }
            DoorClose();
        }
    }
    private void DoorOpen()
    {
        if (DoorAnimBlend <= 1)
        {
            doorAnim.SetFloat("Blend", DoorAnimBlend);
        }
    }
    private void DoorClose()
    {
        if (DoorAnimBlend >= 0)
        {
            doorAnim.SetFloat("Blend", DoorAnimBlend);
        }
    }
    void OnTriggerStay(Collider _other)
    {
        if (_other.tag == "Player")
        {
            isFindPlayer = true;
        }
    }
    void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Player")
        {
            isFindPlayer = false;
        }
    }
}