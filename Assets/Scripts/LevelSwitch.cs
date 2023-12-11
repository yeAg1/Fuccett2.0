using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchLevel(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchLevel(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchLevel(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchLevel(4);
        }
    }

    void SwitchLevel(int level)
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene(4);
                break;
            case 2:
                SceneManager.LoadScene(1);
                break;
            case 3:
                SceneManager.LoadScene(2);
                break;
            case 4:
                SceneManager.LoadScene(3);
                break;
        }
    }
}
