using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxBehaviour : MonoBehaviour
{
    [SerializeField]
    private string textContent = "Your totem seems cool !";

    [SerializeField]
    private float textDuration = 4;

    private float timer=0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = textContent;
        timer = textDuration;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                transform.parent.gameObject.SetActive(false);
            }

        }
    }

    public void displayText(string text, float time)
    {
        transform.parent.gameObject.SetActive(true);
        GetComponent<Text>().text = text;
        timer = time;
    }
}
