using UnityEngine;
using System.Collections;

public class EffectsManager : MonoBehaviour {
	
	public ParticleSystem[] effects;
	public AudioClip[] effectClips;

	AudioSource effectSource;

	private static EffectsManager _instanc;

	public static EffectsManager Instance
	{
		get{ return _instanc;}
	}

	void Start()
	{
		_instanc = this;
		if (effectClips == null)
			effectClips = new AudioClip[effects.Length];

		StopEffects ();
	}

	void Awake()
	{
		effectSource = GetComponent<AudioSource> ();
	}
	void StopEffects()
	{
		for(int i=0;i<effects.Length;i++)
		{
			effects [i].Stop ();
		}
	}
	public void PlayEffect(int index,Vector3 pos)
	{
		effects [index].transform.position = pos;
		if (effects [index].isPlaying)
			effects [index].Stop ();
		effects [index].Play ();
		if (effectClips [index] != null) {
			effectSource.clip = effectClips [index];
			effectSource.Play ();
		}
	}
}
