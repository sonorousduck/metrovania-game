using System;
using System.Collections.Generic;

namespace CrowEngine
{
    public enum State
    {
        Paused,
        Playing,
        Stopped
    }
    public class AudioSource : Component
    {
        public State previousState;
        public State currentState;
        public string previousSong;
        public string currentSong;
        public bool repeat;
        public Queue<string> soundEffectsQueue;

        public AudioSource()
        {
            currentState= State.Stopped;
            previousState = State.Stopped;
            currentSong = "";
            previousSong = "";
            repeat = false;
            soundEffectsQueue = new Queue<string>();
        }
    }
}
