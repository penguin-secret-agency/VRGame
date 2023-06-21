using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class DissolverManager : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    private List<Material> materials = new List<Material>();
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;
    // Start is called before the first frame update
    void Start()
    {
        if(skinnedMeshRenderers.Length > 0) {
            foreach(SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers) {
                for(int i = 0; i<skinnedMeshRenderer.materials.Length; i++) {
                    materials.Add(skinnedMeshRenderer.materials[i]);
                }
            }
        }
    }

    // Update is called once per frame
    public IEnumerator startDissolving() {
        if(materials.Count > 0) {
            float counter = 0;
            while(materials[0].GetFloat("_DissolveAmount") < 1) {
                counter+=dissolveRate;
                for(int i = 0; i<materials.Count; i++) {
                    materials[i].SetFloat("_DissolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }
}
