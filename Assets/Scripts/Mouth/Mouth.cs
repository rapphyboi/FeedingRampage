using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private float scaleIndex = 0.5f;
    public void CreatePanic()
    {
        circleCollider.enabled = true;
        Invoke("DisableCollider", 0.1f);
    }

    private void DisableCollider()
    {
        circleCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Vector3 foodSize = collision.transform.localScale;
            if(transform.parent.localScale.x > foodSize.x )
            {
                transform.parent.localScale += foodSize * scaleIndex;
                GetComponentInChildren<SoundEmitter>();
            }
            else
            {
                Debug.Log("DEAD BOY");
            }
            
        }
    }
}
