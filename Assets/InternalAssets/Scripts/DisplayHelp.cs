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

    Dictionary<char, int> countHelp = new Dictionary<char, int>();

    private float timer = 0f;
    
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;

        countHelp.Add('A', 0);
        countHelp.Add('B', 0);
        countHelp.Add('X', 0);
        countHelp.Add('Y', 0);
    }

    private void LateUpdate()
    {
        bool display = false;
        foreach(int value in countHelp.Values) {
            if (value > 0) display = true;
        }
        spriteRenderer.enabled = display;

        spriteRenderer.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    public void showHelp(char button)
    {
        spriteRenderer.enabled = true;
        countHelp[button]++;
        KeyValuePair<char, int> highestKey = new KeyValuePair<char, int>('A', 0);
        foreach(KeyValuePair<char, int> key in countHelp) {
            if (key.Value > highestKey.Value) highestKey = key;
        }
        switch (highestKey.Key)
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

    }

    public void hideHelp(char button)
    {
        countHelp[button]--;
        if (countHelp[button] < 0) countHelp[button] = 0;
        Debug.Log(button + ", " + countHelp[button]);
    }
}
