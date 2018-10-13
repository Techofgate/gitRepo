using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public GameObject[] AutoAudios;

    public GameObject[] Audios;
    public float[] DeltaTimes;

    int index = 0;

    private void Start()
    {
        if (AutoAudios.Length <= 0)
            Debug.LogError("自动音频数量为0");
    }

    public void InstantiateAutoAudio()
    {
        GameObject audio = Instantiate(AutoAudios[index], transform);
        Destroy(audio, 0.5f);

        index++;
        if (index >= AutoAudios.Length)
            index = 0;
    }
	public void InstantiateAudio(int _index)
	{
		GameObject audio = Instantiate(Audios[_index], transform);
		Destroy(audio, DeltaTimes[_index]);
	}


}
