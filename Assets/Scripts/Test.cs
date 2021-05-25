using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button _spriteRenderer;
    [SerializeField] private Ease _ease;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) _spriteRenderer.transform.DOScale( 3f, 1f)
                .SetEase(_ease);
        else if (Input.GetKeyDown(KeyCode.W)) _spriteRenderer.transform.DOScale(1f, 1f)
                .SetEase(_ease);
    }
}
