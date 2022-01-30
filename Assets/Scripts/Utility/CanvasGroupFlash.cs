using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFlash : MonoBehaviour {
    public CanvasGroup group;
    public float flashPeriod = 1f;
    public Vector2 flashAlphaMinMax = new Vector2(0.2f, 1f);
    
    // Update is called once per frame
    void Update() {
        group.alpha = Mathf.Lerp(flashAlphaMinMax.x, flashAlphaMinMax.y, Mathf.PingPong(Time.time, flashPeriod));
    }
}
