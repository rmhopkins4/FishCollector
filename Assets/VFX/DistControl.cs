using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DistControl : MonoBehaviour
{

    UnityEngine.Rendering.VolumeProfile volumeProfile;
    UnityEngine.Rendering.Universal.ChromaticAberration chromAb;

    private void Start()
    {
    volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;

    if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

    // You can leave this variable out of your function, so you can reuse it throughout your class.

    if (!volumeProfile.TryGet(out chromAb)) throw new System.NullReferenceException(nameof(chromAb));
    }

    private void FixedUpdate()
    {

        chromAb.intensity.Override(0);
    }
    public void nearThing(float d)
    {
        chromAb.intensity.Override(Mathf.Pow(2, -.2f * (d - 5)));
    }
}