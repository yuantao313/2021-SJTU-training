using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.UI;
using Story;
public class StoryController : MonoBehaviour
{
    public GameObject Camera;

    private StoryFrame story;

    public Image storyImage;

    public Text storyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}

namespace Story
{
    class StoryFrame
    {
        public Frame[] frame;

        public Frame getFrame()
        {
            return this.frame[0];
        }
    }

    class Frame
    {
        public float time;
        public String text;
    }
}
