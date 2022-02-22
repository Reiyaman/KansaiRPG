using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{

   

    public Fungus.Flowchart flowchart = null;
    // public String sendMessage = "";

    private void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag != "Enemy")
        {

            if(collision.gameObject.tag == "BOSS")
            {
                flowchart.SendFungusMessage("boss");
            }

            else
            {
                return;
            }
        }

        else if (collision.gameObject.GetComponent<EnemyController>().enemynumber == 0)
        {
            flowchart.SendFungusMessage("slime");
        }

        else if(collision.gameObject.GetComponent<EnemyController>().enemynumber == 1)
        {
            flowchart.SendFungusMessage("pink");
        }

        else if (collision.gameObject.GetComponent<EnemyController>().enemynumber == 2)
        {
            flowchart.SendFungusMessage("chest");
        }

        else if (collision.gameObject.GetComponent<EnemyController>().enemynumber == 3)
        {
            flowchart.SendFungusMessage("skeleton");
        }

        else if (collision.gameObject.GetComponent<EnemyController>().enemynumber == 4)
        {
            flowchart.SendFungusMessage("turtleshell");
        }

        else if (collision.gameObject.GetComponent<EnemyController>().enemynumber == 5)
        {
            flowchart.SendFungusMessage("lance");
        }

        else if (collision.gameObject.GetComponent<EnemyController>().enemynumber == 6)
        {
            flowchart.SendFungusMessage("goblin");
        } 

    }
}