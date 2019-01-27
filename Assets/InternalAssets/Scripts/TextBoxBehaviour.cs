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

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = textContent;
        StartCoroutine(DisappearTextAfterTime(textDuration));
    }

    IEnumerator DisappearTextAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        transform.parent.gameObject.SetActive(false);
    }
}
