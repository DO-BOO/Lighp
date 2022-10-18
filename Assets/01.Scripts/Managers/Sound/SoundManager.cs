using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoSingleton<SoundManager>
{
	// Audio players components.
	[SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioSource musicSource;

	[SerializeField] private GameObject soundPref;

	private const float defaultRandomPitch = 0.05f;

    #region 효과음 재생
    //효과음 재생
    public void PlayEffect(AudioClip clip)
	{
		effectSource.clip = clip;
		effectSource.Play();
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

	#region 랜덤재생
	public void PlayRandomPitch(AudioClip clip, float pitchRange = defaultRandomPitch)
	{
		float randomPitch = Random.Range(-pitchRange, pitchRange);
		effectSource.pitch = 1 + randomPitch;
		effectSource.clip = clip;
		effectSource.Play();
	}

	//클립의 피치를 랜덤으로 변경해서 재생
	public void PlayRandomPitch(AudioClip[] clips, float pitchRange = defaultRandomPitch)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(-pitchRange, pitchRange);
		effectSource.pitch = 1 + randomPitch;
		effectSource.clip = clips[randomIndex];
		effectSource.Play();
	}
	#endregion
	#endregion

	#region 음악
	//음악 재생
	public void PlayMusic(AudioClip clip)
	{
		musicSource.clip = clip;
		musicSource.Play();
	}
    #endregion
}