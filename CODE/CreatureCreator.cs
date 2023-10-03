using System.Collections.Generic;
using UnityEngine;

public class CreatureCreator:MonoBehaviour
{
    public static GameObject[] BaseCreature;
    public GameObject[] set;
    public static float mutationChance = 0.01f;
    public static int amtofcreatures=250;
    public static int maxcreatures = 250;
    void Start()
    {
        if (set != null)
        {
            BaseCreature = set;
        }
    }
    public static void createChild(GameObject parent)
    {
        if (amtofcreatures >= maxcreatures)
        {
            return;
        }
        amtofcreatures++;
        int basecreature;
        if (roll())
        {
            basecreature = Random.Range(0, BaseCreature.Length);
        }
        else
        {
            basecreature = parent.GetComponent<Creature>().genes.bodytype;
        }
        GameObject creature = Instantiate(BaseCreature[basecreature], parent.transform.position * 1000, Quaternion.Euler(0,0,0));
        Creature code = creature.GetComponent<Creature>();
        code.genes = new Genes(parent.GetComponent<Creature>().genes);
        placeChild(creature,parent);
        code.Eyes = creature.GetComponentsInChildren<eyescript>();
        code.genes.bodytype = basecreature;
        if (code.Eyes.Length > 0)
        {
            code.network = new NeuralNetwork(parent.GetComponent<Creature>().network, creature);

        }
        creature.GetComponent<SpriteRenderer>().color = code.genes.color;
        creature.transform.position = parent.transform.position;
        creature.transform.rotation = parent.transform.rotation;
        
    }
    public static bool roll()
    {
        if(Random.Range(0,101) <= mutationChance)
        {
            return true;
        }
        return false;
    }
    public void createRandom()
    {
        int bodytype = Random.Range(0, BaseCreature.Length);
        GameObject creature = Instantiate(BaseCreature[bodytype], transform.position * 1000, transform.rotation);
        Creature code = creature.GetComponent<Creature>();
        code.genes = new Genes();
        placeParts(creature);
        code.genes.bodytype = bodytype;
        code.Eyes = creature.GetComponentsInChildren<eyescript>();
        if (code.Eyes.Length > 0)
        {
            code.network = new NeuralNetwork((code.Eyes.Length * 12) + 1, 6 * code.Eyes.Length, 3, code.Eyes.Length);
        }
        creature.GetComponent<SpriteRenderer>().color = code.genes.color;
        creature.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        creature.transform.position = transform.position;
        Destroy(gameObject);
    }
    public static void placeChild(GameObject creature, GameObject parent)
    {
        Creature code = creature.GetComponent<Creature>();
        code.genes.partpositions = parent.GetComponent<Creature>().genes.partpositions;
        float accentcoloramount = 0.1f;
        Color clr = new Color
                (
                    Genes.randnormal(accentcoloramount, code.genes.color.r),
                    Genes.randnormal(accentcoloramount, code.genes.color.g),
                    Genes.randnormal(accentcoloramount, code.genes.color.b)
                );
        var iterator = 0;
        foreach (int id in code.genes.parts)
        {
            var part = GetPartFromId(id);
            if (part == null)
            {
                continue;
            }
            float rotation;
            if (roll())
            {
                rotation = Random.Range(0f, 180f);
                try
                {
                    code.genes.partpositions[iterator] = rotation;
                }
                catch
                {
                    code.genes.partpositions.Add(rotation);
                }
            }
            else
            {
                try
                {
                    rotation = code.genes.partpositions[iterator];
                }
                catch
                {
                    rotation = Random.Range(0f, 180f);
                    try
                    {
                        code.genes.partpositions[iterator] = rotation;
                    }
                    catch
                    {
                        code.genes.partpositions.Add(rotation);
                    }
                }
            }
            int amount = 2;
            if (rotation == 0 || rotation == 180)
            {
                amount = 1;
            }
            for (var i = 0; i < amount; i++)
            {
                GameObject local = Instantiate(part.obj, creature.transform.position + Vector3.up, creature.transform.rotation);
                local.transform.RotateAround(creature.transform.position, Vector3.forward, rotation);
                RaycastHit2D hit = Physics2D.Raycast(local.transform.position, local.transform.TransformDirection(Vector2.down), 10);
                local.transform.position = hit.point;
                local.transform.localScale = Vector2.Scale(local.transform.localScale, creature.transform.localScale);
                local.transform.parent = creature.transform;
                rotation = -rotation;
                if (!part.keepColor)
                {
                    local.GetComponent<SpriteRenderer>().color = clr;
                }
            }
            part.changeStats(code);
            iterator++;
        }
    }
    void placeParts(GameObject creature)
    {
        Creature code = creature.GetComponent<Creature>();
        float accentcoloramount = 0.25f;
        Color clr = new Color
                (
                    Genes.randnormal(accentcoloramount, code.genes.color.r),
                    Genes.randnormal(accentcoloramount, code.genes.color.g),
                    Genes.randnormal(accentcoloramount, code.genes.color.b)
                );
        foreach (int id in code.genes.parts)
        {
            var part = GetPartFromId(id);
            if (part == null)
            {
                continue;
            }
            float rotation = Random.Range(0f, 180f);
            code.genes.partpositions.Add(rotation);
            int amount = 2;
            if (rotation == 0 || rotation == 180)
            {
                amount = 1;
            }
            for (var i = 0; i<amount; i++)
            {
                GameObject local = Instantiate(part.obj, creature.transform.position + Vector3.up, creature.transform.rotation);
                local.transform.RotateAround(creature.transform.position, Vector3.forward, rotation);
                RaycastHit2D hit = Physics2D.Raycast(local.transform.position, local.transform.TransformDirection(Vector2.down), 10);
                local.transform.position = hit.point;
                local.transform.localScale = Vector2.Scale(local.transform.localScale, creature.transform.localScale);
                local.transform.parent = creature.transform;
                rotation = -rotation;
                if (!part.keepColor)
                {
                    local.GetComponent<SpriteRenderer>().color = clr;
                }
            }
            part.changeStats(code);
        }
    }
    public static Part GetPartFromId(int id)
    {
        Part part = null;
        switch(id)
        {
            case 1:
                part = new Eye();
                break;
            case 2:
                part = new Flipper();
                break;
            case 3:
                part = new Stomach();
                break;
            case 4:
                part = new Pouch();
                break;
            case 5:
                part = new Spike();
                break;
        }
        return part;
    }
}
