using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour {
    [SerializeField] AudioSource hitBit;
    [SerializeField] AudioSource winState;
    [SerializeField] AudioSource noWinState;
    [SerializeField] AudioSource buttonClick;
    [SerializeField] AudioSource hitGhost;

    public void PlayHitBit() {
        hitBit.Play();
    }

    public void PlayWinState() {
        winState.Play();
    }

    public void PlayNoWinState() {
        noWinState.Play();
    }

    public void PlayButtonClick() {
        buttonClick.Play();
    }

    public void PlayHitGhost() {
        hitGhost.Play();
    }
}
