using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // ============================ MATERIALS FUNCTIONS ============================  \\
    //Returns a list of all Materials on this GameObject and its children
    static public Material[] GetAllMaterials(GameObject go)
    {
        Renderer[] rends = go.GetComponentsInChildren<Renderer>();

        List<Material> mats = new List<Material>();
        foreach (Renderer rend in rends)
        {
            mats.Add(rend.material);
        }

        return (mats.ToArray());
    }

    // ============== Hitscan/Raycast shooting function and sfx spawning =============== \\
    public void Shoot()
    {
        //Reseting the laser lifetime
        laserLife = laserTime;
        laser.enabled = true;

        //The real Raycast
        RaycastHit hit;
        if (Physics.Raycast(barrel.transform.position, barrel.transform.forward, out hit))
        {

            //Start Hit logic

            if (hit.transform.gameObject.tag == "Thrall")
            {
                Thrall thrawl = hit.transform.GetComponent<Thrall>();
                if (thrawl != null)
                {
                    thrawl.TakeDamage(damage);
                }
            }

            //end hit logic

            //Start SFX
            //Starting point for the laser line
            laser.SetPosition(0, barrel.transform.position);
            //Ending point for the line
            laser.SetPosition(1, hit.point);

            GameObject sparkGO = Instantiate(sparkEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(sparkGO, 2f);
            //End SFX
        }

        //print("Shot fired");
        coolDown = coolTime;
    }
}
