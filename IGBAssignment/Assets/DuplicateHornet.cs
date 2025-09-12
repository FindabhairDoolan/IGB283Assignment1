using UnityEngine;

public class HornetMeshDuplicator : MonoBehaviour
{
    [SerializeField] Vector3 clonePointA = new Vector3(-4, 1, 0);
    [SerializeField] Vector3 clonePointB = new Vector3(0, 1, 0);
    [SerializeField] float cloneSlideSpeed = 2.0f;
    [SerializeField] float cloneRotationSpeed = 120f;

    void Start()
    {
        //find the mesh to copy
        HornetSpinMove original = FindObjectOfType<HornetSpinMove>();     

        //duplicate at the offset
        GameObject clone = Instantiate(
            original.gameObject,
            original.transform.position + new Vector3(6, 0, 0),
            original.transform.rotation,
            this.transform
        );

        clone.name = original.gameObject.name + "_Clone";

        //configure
        HornetSpinMove mover = clone.GetComponent<HornetSpinMove>();
        mover.Configure(
            clonePointA, 
            clonePointB,    
            cloneSlideSpeed,              
            cloneRotationSpeed                    
        );
    }
}
