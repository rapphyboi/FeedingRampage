using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    public void CreatePanic()
    {
        circleCollider.enabled = true;
        Invoke("DisableCollider", 0.1f);
    }

    private void DisableCollider()
    {
        circleCollider.enabled = false;
    }
}
