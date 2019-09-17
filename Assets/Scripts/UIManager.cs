using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoSingleton<UIManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(Instance);
    }

    public JoystickMove joystickMove;
    public JoystickAttack joystickAttack;

    public OnOffSwitch onoffSwitch;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
