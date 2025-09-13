using UnityEngine;

public class DuplicateHornet : MonoBehaviour
{
    [SerializeField] Vector3 cloneHornetOffset = new Vector3(6f, 0f, 0f);
    [SerializeField] float cloneSlideSpeed = 2.0f;
    [SerializeField] float cloneRotationSpeed = 120f;

    void Start()
    {
        //finds hornet
        HornetSpinMove original = FindObjectOfType<HornetSpinMove>();

        //clones the mesh
        GameObject clone = Instantiate(
            original.gameObject,
            original.transform.position + cloneHornetOffset,
            original.transform.rotation,
            this.transform
        );
        clone.name = "HornetClone";

        //clones knobs
        Transform origA = original.KnobA;
        Transform origB = original.KnobB;

        GameObject knobC = Instantiate(origA.gameObject,
            origA.position + cloneHornetOffset, origA.rotation, this.transform);
        knobC.name = "KnobC";

        GameObject knobD = Instantiate(origB.gameObject,
            origB.position + cloneHornetOffset, origB.rotation, this.transform);
        knobD.name = "KnobD";

        //changes targets of cloned hornet
        HornetSpinMove hornet2 = clone.GetComponent<HornetSpinMove>();    
        hornet2.KnobA = knobC.transform;
        hornet2.KnobB = knobD.transform;
        hornet2.moveSpeed = cloneSlideSpeed;
        hornet2.rotateSpeed = cloneRotationSpeed;

    }
}
