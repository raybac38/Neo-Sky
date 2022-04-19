using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DataBoxControler : MonoBehaviour
{
    public uint id;
    public NetworkIdentity networkIdentity;
    public void Awake()
    {
        id = networkIdentity.netId;
    }
}
