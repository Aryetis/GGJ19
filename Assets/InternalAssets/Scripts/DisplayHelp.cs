using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHelp : MonoBehaviour
{
    [SerializeField]
    private Sprite buttonA;

    [SerializeField]
    private Sprite buttonB;

    [SerializeField]
    private Sprite buttonX;

    [SerializeField]
    private Sprite buttonY;

    private float timer = 0f;
    
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                spriteRenderer.enabled = false;
            }
        }
        spriteRenderer.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    public void showHelp(char button)
    {
        spriteRenderer.enabled = true;
        switch (button)
        {
            case 'A':
                spriteRenderer.sprite = buttonA;
                break;
            case 'B':
                spriteRenderer.sprite = buttonB;
                break;
            case 'X':
                spriteRenderer.sprite = buttonX;
                break;
            case 'Y':
                spriteRenderer.sprite = buttonY;
                break;
        }

        timer = 1;

    }

    public void hideHelp(char button)
    {
        timer -= 0.5f;
    }
}
