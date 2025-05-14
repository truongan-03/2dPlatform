using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    

    public enum Particles
    {
        die
    }

    [System.Serializable]
    public class ParticleEntry
    {
        public Particles type;
        public ParticleSystem prefab;
    }

    public List<ParticleEntry> particleList;
    private Dictionary<Particles, ParticleSystem> particleDict;



    private void Awake()
    {
        

        particleDict = new Dictionary<Particles, ParticleSystem>();
        foreach (var entry in particleList)
        {
            if (entry.prefab != null && !particleDict.ContainsKey(entry.type))
            {
                particleDict.Add(entry.type, entry.prefab);
            }
        }
    }

    public void PlayParticle(Particles type, Vector3 position)
    {
        if (particleDict.ContainsKey(type))
        {
            ParticleSystem prefab = particleDict[type];
            ParticleSystem ps = Instantiate(prefab, position, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy particle có loại: {type}");
        }
    }
}
