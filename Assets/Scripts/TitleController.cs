using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip startSE;
    public AudioClip titleSE;
    public AudioClip retrySE;

    
    public GameObject operation;
   

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
        audioSource.PlayOneShot(startSE);
        SceneManager.LoadScene("Main");
    }

    public void OptionButton()
    {
        operation.SetActive(true);
        audioSource.PlayOneShot(titleSE);

    }

    public void TitleButton()
    {
        audioSource.PlayOneShot(titleSE);
        SceneManager.LoadScene("Title");
        //audioSource.PlayOneShot(titleSE);

    }

    public void RetryButton()
    {
        audioSource.PlayOneShot(retrySE);
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

    public void gameClear()
    {
        SceneManager.LoadScene("StaffRoll");
    }

    public void ReturnButton()
    {
        operation.SetActive(false);
        audioSource.PlayOneShot(titleSE);
    }

}
