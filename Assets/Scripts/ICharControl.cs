using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICharControl 
{
    void Boost();
    void Jump();

    MoveCharacterScript Daddy();
    void IAm(MoveCharacterScript parent);
}
