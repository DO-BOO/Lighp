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

	//ȿ���� ���
	public void PlayEffect(AudioClip clip)
	{
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}

	//ȿ������ ������ ��ҿ��� ��� (���߿� Ǯ �̿��ϴ°ɷ� ����)
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

	//���� ���
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

	//Ŭ���� ��ġ�� �������� �����ؼ� ���
	public void PlayRandomPitch(AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(-defaultRandomPitch, defaultRandomPitch);
		EffectsSource.pitch = 1 + randomPitch;
		EffectsSource.clip = clips[randomIndex];
		EffectsSource.Play();
	}


}