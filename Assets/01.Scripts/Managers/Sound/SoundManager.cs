using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoSingleton<SoundManager>
{
	// Audio players components.
	private AudioSource EffectsSource;
    public AudioSource MusicSource;
	[SerializeField] private GameObject soundPref;

	public float defaultRandomPitch = 0.05f;

	//효과음 재생
	public void PlayEffect(AudioClip clip)
	{
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}

	//효과음을 지정한 장소에서 재생 (나중에 풀 이용하는걸로 수정)
	public void PlayEffect(AudioClip clip, Vector3 pos)
    {
		GameObject obj = GameObject.Instantiate(soundPref);
		AudioSource audio = obj.GetComponent<AudioSource>();
		obj.transform.position = pos;
		audio.clip = clip;
		obj.SetActive(true);
		audio.Play();
		RemovePrefab(obj, clip.length);
    }

	private IEnumerator RemovePrefab(GameObject obj, float time)
    {
		yield return new WaitForSeconds(time);
		Object.Destroy(obj);
    }

	//음악 재생
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}
	public void PlayRandomPitch(AudioClip clip)
	{
		float randomPitch = Random.Range(-defaultRandomPitch, defaultRandomPitch);
		EffectsSource.pitch = 1 + randomPitch;
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}

	//클립의 피치를 랜덤으로 변경해서 재생
	public void PlayRandomPitch(AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(-defaultRandomPitch, defaultRandomPitch);
		EffectsSource.pitch = 1 + randomPitch;
		EffectsSource.clip = clips[randomIndex];
		EffectsSource.Play();
	}


}