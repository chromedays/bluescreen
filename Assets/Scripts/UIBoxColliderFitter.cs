using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(RectTransform))]
public class UIBoxColliderFitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var rect = GetComponent<RectTransform>();
        var collider = GetComponent<BoxCollider2D>();
        collider.size = rect.rect.size;
        collider.offset = new Vector2(
            (0.5f - rect.pivot.x) * rect.rect.width,
            (0.5f - rect.pivot.y) * rect.rect.height);
    }
}
