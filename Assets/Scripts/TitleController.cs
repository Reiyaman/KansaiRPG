using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    AudioSource audioSource;
    //public AudioClip startSE;
    //public AudioClip titleSE;
    //public AudioClip retrySE;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    public void TitleButton()
    {
        SceneManager.LoadScene("Title");
        //audioSource.PlayOneShot(titleSE);

    }

    public void RetryButton()
    {
        SceneManager.LoadScene("Main");
        //sudioSource.PlayOneShot(retrySE);
    }

    public void GameOverWait()
    {
        Invoke("GameOver", 4f);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
