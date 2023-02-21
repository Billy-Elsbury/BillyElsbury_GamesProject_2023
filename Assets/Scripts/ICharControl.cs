using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICharControl 
{
    void boost();
    void jump();

    MoveCharacterScript daddy();
    void iAm(MoveCharacterScript parent);
}
