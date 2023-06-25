using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class DissolverManager : MonoBehaviour
{
    [Header("Characters skin")]
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    private List<Material> materials = new List<Material>();
    [Header("Objects")]
    public MeshRenderer[] meshRenderers;
    private float dissolveAmount;
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
        if(meshRenderers.Length>0) {
            foreach(MeshRenderer mesh in meshRenderers) {
                for(int i = 0; i<mesh.materials.Length; i++) {
                    materials.Add(mesh.materials[i]);
                }
            }
        }
        if(materials.Count > 0) {
            dissolveAmount=materials[0].GetFloat("_DissolveAmount");
        }
    }

    public float getDissolverAmount() {
        return dissolveAmount;
    }
    // Update is called once per frame
    public IEnumerator startDissolving() {
        if(materials.Count > 0) {
            dissolveAmount = materials[0].GetFloat("_DissolveAmount");
            while(materials[0].GetFloat("_DissolveAmount") < 1) {
                dissolveAmount+=dissolveRate;
                for(int i = 0; i<materials.Count; i++) {
                    materials[i].SetFloat("_DissolveAmount", dissolveAmount);
                }
                dissolveAmount=1f;
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

    public IEnumerator undoDissolving() {
        if(materials.Count>0) {
            dissolveAmount= materials[0].GetFloat("_DissolveAmount");
            while(materials[0].GetFloat("_DissolveAmount")>0) {
                dissolveAmount-=dissolveRate;
                for(int i = 0; i<materials.Count; i++) {
                    materials[i].SetFloat("_DissolveAmount", dissolveAmount);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

    public void setDissolveAmount(float amount) {
        if(materials.Count>0) {
            for(int i = 0; i<materials.Count; i++) {
                materials[i].SetFloat("_DissolveAmount", amount);
            }
            dissolveAmount=amount;
        }
    }
}
