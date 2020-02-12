using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollFall : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {// playerと接触したら
            transform.Translate(0.065f, 0, 0);
        }
    }
}
