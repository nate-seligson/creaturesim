using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIHandler : MonoBehaviour
{
    public TMP_InputField[] elems;
    // Start is called before the first frame update
    void Start()
    {
        elems[0].text = CreatureCreator.mutationChance.ToString();
        elems[1].text = CreatureCreator.maxcreatures.ToString();
        elems[2].text = eyescript.sight.ToString();
        elems[3].text = Genes.max_amount_of_parts.ToString();
        elems[4].text = Genes.predatorChance.ToString();
    }

    // Update is called once per frame
    void OnGUI()
    {
        CreatureCreator.mutationChance = float.Parse(elems[0].text);
        CreatureCreator.maxcreatures = (int)int.Parse(elems[1].text);
        eyescript.sight = float.Parse(elems[2].text);
        Genes.max_amount_of_parts = int.Parse(elems[3].text);
        Genes.predatorChance = float.Parse(elems[4].text);
    }
    void Update()
    {
        Debug.Log(CreatureCreator.amtofcreatures);
        if(CreatureCreator.amtofcreatures == 0)
        {
            CreatureCreator.amtofcreatures = 250;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
