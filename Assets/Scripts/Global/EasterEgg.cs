using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    [SerializeField]
    AudioClip son;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AudioSource asource = gameObject.AddComponent<AudioSource>();
            asource.clip = son;
            asource.Play();
            Destroy(asource,son.length);
        }
    }
}
