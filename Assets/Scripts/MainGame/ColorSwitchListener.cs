using UnityEngine;

public abstract class ColorSwitchListener : MonoBehaviour {
    protected virtual void OnEnable() {
        //Call once to setup the material in the target state
        OnColorSwitch();
        PlayerAttribute.onColorSwitch += OnColorSwitch;
    }

    public abstract void OnColorSwitch();
    
    protected virtual void OnDisable() {
        PlayerAttribute.onColorSwitch -= OnColorSwitch;
    }
}
