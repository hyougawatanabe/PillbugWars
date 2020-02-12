using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCtrl : MonoBehaviour
{
    public void DeleteExplosion()
    {
        Destroy(gameObject);
    }
}
