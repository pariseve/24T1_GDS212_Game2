using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArchitectTest : MonoBehaviour
{
    public TextMeshProUGUI text;
    TextArchitect architect;

    [TextArea(3,10)]
    public string speak;
    public int charactersPerFrame = 1;
    public float speed = 1f;
    public bool useEncap = true;
    // Start is called before the first frame update
    void Start()
    {
        architect = new TextArchitect(speak);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            architect = new TextArchitect(speak, "", charactersPerFrame, speed, useEncap);
        }

        text.text = architect.currentText;
    }
}
