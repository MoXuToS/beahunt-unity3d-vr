using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorLocationMountains : MonoBehaviour
{

    public int SizeLocation = 500; // Overall size of the forest (a square of SizeLocation X SizeLocation).
    public int elementSpacing = 5; // The spacing between element placements. Basically grid size.

    public Element2[] elements;

    private void Start()
    {

        // Loop through all the positions within our forest boundary.
        for (int x = 1; x < SizeLocation; x += elementSpacing)
        {
            for (int z = -1; z > -SizeLocation; z -= elementSpacing)
            {

                // For each position, loop through each element...
                for (int i = 0; i < elements.Length; i++)
                {

                    // Get the current element.
                    Element2 element = elements[i];

                    // Check if the element can be placed.
                    if (element.CanPlace2())
                    {

                        // Add random elements to element placement.
                        Vector3 position = new Vector3(x, 0f, z);
                        Vector3 offset = new Vector3(Random.Range(-0.35f, 0.55f), 0f, Random.Range(-1.25f, 1.25f));
                        Vector3 rotation = new Vector3(Random.Range(0, 5f), Random.Range(0, 360f), Random.Range(0, 5f));
                        Vector3 scale = Vector3.one * Random.Range(10.0f, 15.25f);

                        // Instantiate and place element in world.
                        GameObject newElement = Instantiate(element.GetRandom());
                        newElement.transform.SetParent(transform);
                        newElement.transform.position = Clamp(position + offset, 1, 500);
                        newElement.transform.eulerAngles = rotation;
                        newElement.transform.localScale = scale;

                        // Break out of this for loop to ensure we don't place another element at this position.
                        break;

                    }

                }
            }
        }

    }
    private Vector3 Clamp(Vector3 vector3, float min, float max)
    {
        if (vector3.x < min)
        {
            if (vector3.z < -min)
            {
                return new Vector3(min, vector3.y, -min);
            }
            else if (vector3.z > -max)
            {
                return new Vector3(min, vector3.y, -max);
            }
        }
        else if (vector3.x > max)
        {
            if (vector3.z < -min)
            {
                return new Vector3(max, vector3.y, -min);
            }
            else if (vector3.z > -max)
            {
                return new Vector3(max, vector3.y, -max);
            }
        }
        return vector3;
    }
}




[System.Serializable]
public class Element2
{

    public string name;
    [Range(1, 10)]
    public int density;

    public GameObject[] prefabs;

    public bool CanPlace2()
    {

        // Validation check to see if element can be placed. More detailed calculations can go here, such as checking perlin noise.

        if (Random.Range(0, 10) < density)
            return true;
        else
            return false;

    }

    public GameObject GetRandom()
    {

        // Return a random GameObject prefab from the prefabs array.

        return prefabs[Random.Range(0, prefabs.Length)];

    }

}