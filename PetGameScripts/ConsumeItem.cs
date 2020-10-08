using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ConsumeItem : MonoBehaviour
{
    Image image;
    public Sprite a, b, c;
    public GameObject Pet;
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void StartAnimation()
    {
        StartCoroutine("Consume");
    }

    IEnumerator Consume()
    {
        yield return new WaitForSeconds(1);
        image.sprite = b;
        yield return new WaitForSeconds(1);
        image.sprite = c;
        yield return new WaitForSeconds(1);
        image.sprite = a;
        gameObject.SetActive(false);
    }
}