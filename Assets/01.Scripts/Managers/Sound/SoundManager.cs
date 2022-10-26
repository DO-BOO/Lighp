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

    #region ȿ���� ���
    //ȿ���� ���
    public void PlayEffect(AudioClip clip)
	{
		effectSource.clip = clip;
		effectSource.Play();
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

	#region �������
	public void PlayRandomPitch(AudioClip clip, float pitchRange = defaultRandomPitch)
	{
		float randomPitch = Random.Range(-pitchRange, pitchRange);
		effectSource.pitch = 1 + randomPitch;
		effectSource.clip = clip;
		effectSource.Play();
	}

	//Ŭ���� ��ġ�� �������� �����ؼ� ���
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

	#region ����
	//���� ���
	public void PlayMusic(AudioClip clip)
	{
		musicSource.clip = clip;
		musicSource.Play();
	}
    #endregion
}