using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Fade fade;

    void Start()
    {
        fade.FadeOut(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.P))
        {
            //Initiate.Fade("Scene0_map", Color.black, 1.0f);
            fade.FadeIn(1f , () => SceneManager.LoadScene("Scene0_map"));
            

        }
    }

    
}
