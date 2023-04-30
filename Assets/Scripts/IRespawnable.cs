using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRespawnable
{
    Transform RespawnPoint { get; set; }
}
