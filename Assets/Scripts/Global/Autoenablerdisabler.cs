using UnityEngine;
using System.Collections;

public class Autoenablerdisabler : MonoBehaviour {

    public GameObject tutolayer,helpicon;
 

         public IEnumerator Start()
    {
        yield return StartCoroutine(wait4sec(4f));
        yield return StartCoroutine(Mystart(2f));
    }

    IEnumerator wait4sec(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutolayer.SetActive(false) ;

    }

    IEnumerator Mystart(float delay)
    {
        yield return new WaitForSeconds(delay);
        helpicon.SetActive(true);

    }
   
}
