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

    private int helpCount = 0;


    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void Update()
    {
        spriteRenderer.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    public void showHelp(char button)
    {
        if (helpCount < 0)
            helpCount = 0;
        helpCount++;
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
            default:
                Debug.Log(button + " is not a recognized input");
                break;
        }
    }

    public void hideHelp()
    {
        helpCount--;
        if(helpCount <= 0)
            spriteRenderer.enabled = false;
    }
}
