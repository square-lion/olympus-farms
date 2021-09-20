using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public Image[] logos;
    public Image background;

    public float delay;

    public float speed;

    private int curLogo;
    private bool fadeIn;

    void Start(){
        StartCoroutine(Logos());
    }

    IEnumerator Logos(){


        fadeIn = !fadeIn;


        // fade from opaque to transparent
        if (!fadeIn)
        {
            yield return new WaitForSeconds(1);
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
            {
                // set color with i as alpha
                logos[curLogo].color = new Color(1, 1, 1, i);
                yield return null;
            }
            logos[curLogo].color = new Color(1,1,1,0);
            curLogo++;
            background.color = new Color(77/255f,171/255f,233/255f,1);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * speed)
            {
                // set color with i as alpha
                logos[curLogo].color = new Color(1, 1, 1, i);
                yield return null;
            }
        }

        if(curLogo != logos.Length)
            StartCoroutine(Logos());
        else
            SceneManager.LoadScene("Menu");
    }
}

