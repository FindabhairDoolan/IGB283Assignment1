using UnityEngine;

public class DuplicateHornet : MonoBehaviour
{
    [SerializeField] Vector3 cloneHornetOffset = new Vector3(6f, 0f, 0f);
    [SerializeField] float cloneSlideSpeed = 2.0f;
    [SerializeField] float cloneRotationSpeed = 120f;

    private GameObject clone;

    void Start()
    {
        //finds hornet
        HornetSpinMove original = FindObjectOfType<HornetSpinMove>();

        //clones the mesh
        clone = Instantiate(
            original.gameObject,
            original.transform.position + cloneHornetOffset,
            original.transform.rotation,
            this.transform
        );
        clone.name = "HornetClone";

        //clones points
        Transform origA = original.PointA;
        Transform origB = original.PointB;

        GameObject pointC = Instantiate(origA.gameObject,
            origA.position + cloneHornetOffset, origA.rotation, this.transform);
        pointC.name = "PointC";

        GameObject pointD = Instantiate(origB.gameObject,
            origB.position + cloneHornetOffset, origB.rotation, this.transform);
        pointD.name = "PointD";

        //changes targets of cloned hornet
        HornetSpinMove hornet2 = clone.GetComponent<HornetSpinMove>();
        hornet2.PointA = pointC.transform;
        hornet2.PointB = pointD.transform;
        hornet2.moveSpeed = cloneSlideSpeed;
        hornet2.rotateSpeed = cloneRotationSpeed;

    }
}
