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

    private Dictionary<char, int> countInputs = new Dictionary<char, int>();

    private int ACount = 0;
    private int BCount = 0;
    private int XCount = 0;
    private int YCount = 0;
    
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
        countInputs.Add('A', 0);
        countInputs.Add('B', 0);
        countInputs.Add('X', 0);
        countInputs.Add('Y', 0);
    }

    private void Update()
    {
        spriteRenderer.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    public void showHelp(char button)
    {
        spriteRenderer.enabled = true;
        countInputs[button]++;
        int highestCount = 0;
        char highestKey = '!';
        foreach(char key in countInputs.Keys)
        {
            if(countInputs[key] > highestCount)
            {
                highestKey = key;
            }
        }

        switch (highestKey)
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

        StartCoroutine(HideHelptAfterTime(1));

    }

    IEnumerator HideHelptAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (char key in countInputs.Keys)
        {
            countInputs[key] = 0;
        }
        spriteRenderer.enabled = false;
    }

    public void hideHelp(char button)
    {
        countInputs[button]--;
        foreach (char key in countInputs.Keys)
        {
            if (countInputs[key]>0)
            {
                return;
            }
        }
        spriteRenderer.enabled = false;
    }
}
