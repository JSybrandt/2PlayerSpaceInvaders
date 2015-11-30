using UnityEngine;
using System.Collections;

public class gobaby : MonoBehaviour {

    public string levelToLoad;





    void Start()
    {
 


    }

    void OnMouseExit()
    {
   

        // audio.PlayOneShot(soundhover);
    }

    void Update()
    {
        // float val = CrossPlatformInputManager.GetAxis(Steering_Axes);
        // print(val);

        if (Input.GetKeyDown("joystick button 0"))
        {
            loadLevel();
        }

    }


    private void loadLevel()
    {
            // LoadALevel(levelToLoad);
            Application.LoadLevel("Game");
    }
}
