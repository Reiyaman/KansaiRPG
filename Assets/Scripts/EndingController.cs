using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    Vector3 Staffrollposition;
    public RectTransform rectTransform;
    public float endPos;
    public GameObject titleButton;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Staffrollposition = rectTransform.anchoredPosition;
        player.gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(rectTransform.anchoredPosition.y < endPos)
        {
            Staffrollposition.y += 1f;
            rectTransform.anchoredPosition = Staffrollposition;
        }

        else
        {
            titleButton.SetActive(true);
        }
    }
}
