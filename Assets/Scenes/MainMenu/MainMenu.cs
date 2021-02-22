using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DaggerfallWorkshop;
public class MainMenu : MonoBehaviour
{
    public GameObject Template;
    void Awake() {
        this.Template = transform.GetChild(0).gameObject;
    }
    void Start()
    {
        this.Template.SetActive(false);

        (string,string)[] texts = new (string,string)[] { 
            ("DataPath",Paths.DataPath),
            ("StreamingAssetsPath",Paths.StreamingAssetsPath),
            ("PersistentDataPath",Paths.PersistentDataPath)
        };
        int count = 0;
        foreach ( var ( key, value) in texts ) {
            var clone = Object.Instantiate(Template);
            RectTransform childTransform = clone.GetComponent<RectTransform>();
            
            var text = childTransform.GetChild(0).GetComponent<Text>().text = $"{key}: {value}";
            clone.transform.SetParent(this.transform);
            clone.transform.position = Template.transform.position - new Vector3(0,childTransform.rect.height*count,0);
            //childTransform.rect.size = new Vector2(Camera.main.rect.width, childTransform.rect.height);
            clone.SetActive(true);

            ++count;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
