using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    AudioSource audioSource;
    //public AudioClip startSE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Main");
        //audioSource.PlayOneShot(startSE);
    }

    public void OptionButton()
    {

    }


}
