using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoSingleton<UIManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(Instance);

        joystickAttack = FindObjectOfType<JoystickAttack>();
        joystickMove = FindObjectOfType<JoystickMove>();
        onOffSwitch = OnOffSwitch.Instance;

        DataManager.Instance.Save();
    }

    public JoystickAttack joystickAttack;
    public JoystickMove joystickMove;

    public OnOffSwitch onOffSwitch;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
