using UnityEngine;
using UnityEngine.Events;

public class OnCollisionScript : MonoBehaviour
{
    [Header("Set to true to end the game")]
    public bool endGame;
    [Header("Kinematic, sets .this and siblings of this obj to Kinematic = false to apply physics, also plays sound on break if exists")]
    public bool setRigidBodyKinematicWhenCollide;
    public UnityEvent alarm;


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){

        if (!other.gameObject.CompareTag("MainCamera") && !other.gameObject.CompareTag("StolenItem") ) return;

        //If object is set to "break up" on onTriggerEnter, like the dino, remember dino?
        if (setRigidBodyKinematicWhenCollide && GetComponent<Rigidbody>() != null)
        {
            /*
             * REMEMBER TO SET THE MESH COLLIDER OF THE SIBLINGS TO FUCKING ____CONVEX____
             * Structure for this shit to work:
             * |> Parent
             * |--> This Object
             * |--> Sibling
             * |--> Sibling
             * It gets the children of the parent, including trhis object and sets the vars to false
             */
            for(int i = 0; i < transform.parent.childCount; i++)
            {
                GameObject sibling = transform.parent.GetChild(i).gameObject;
                sibling.GetComponent<Rigidbody>().isKinematic = false;
                //Gotta set the isTrigger to false bc if not it'll just fall through just about anything
                sibling.GetComponent<MeshCollider>().isTrigger = false;

                if (!sibling.GetComponent<AudioSource>()) return;
                sibling.GetComponent<AudioSource>().Play();
            }
        }

        if (!endGame) return;
        alarm.Invoke();
    }


}
