using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLanguage : MonoBehaviour
{
    [SerializeField]
    private Transform McPosition;

    void Update()
    {
        if(McPosition.position.x <= 500)
        {
            transform.Translate(0, 2.0f, 0);
        }
    }
}
