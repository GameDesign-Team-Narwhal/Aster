using UnityEngine;
using System.Collections;

//script which randomly plays songs from the audioclips it is provided.  When one ends, it plays another.  You can call PlayNewSong() to
//stop the current song and start a new one.
//The same song won't play two times in a row.
[RequireComponent (typeof(AudioSource))]
public class Jukebox : MonoBehaviour { 
    public AudioClip[] tracks;


    AudioSource audioSource;
    int lastSongIndex = -1; //start at invalid value, so any clip can be played

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        //if the song has ended, start a new one
	    if(!audioSource.isPlaying)
        {
            PlayNewSong();
        }
	}

    public void PlayNewSong()
    {
        if(tracks.Length == 0)
        {
            Debug.LogWarning("This Jukebox does not have any songs to play!");
            return;
        }

        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        //find new song index different from old one
        int newSongIndex;
        do
        {
            newSongIndex = Mathf.FloorToInt(Random.Range(0, tracks.Length - .05f));
        }
        while (tracks.Length > 1 && newSongIndex == lastSongIndex);

        audioSource.clip = tracks[newSongIndex];
        audioSource.Play();

        lastSongIndex = newSongIndex;
    }
}
