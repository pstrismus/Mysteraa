using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpAn : MonoBehaviour
{
    [SerializeField]CharacterCont ccj;

    private void jump()
    {
        ccj.Jump();
    }
}
