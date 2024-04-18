using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordAnimator : MonoBehaviour
{
    [SerializeField] private string[] wordsToAnimate;
    private TMP_Text textMesh;
    private Mesh mesh;
    private Vector3[] vertices;
    private List<int> wordIndexes;
    private List<int> wordLengths;

    public Gradient rainbow;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();

        wordIndexes = new List<int>{0};
        wordLengths = new List<int>();

        string s = textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        Color[] colors = mesh.colors;

        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            string word = textMesh.text.Substring(wordIndex, wordLengths[w]);

            // Verifica si la palabra actual está en el array wordsToWobble
            if (ArrayContainsString(wordsToAnimate, word))
            {
                Vector3 offset = AnimateWord(Time.time + w);

                for (int i = 0; i < wordLengths[w]; i++)
                {
                    TMP_CharacterInfo c = textMesh.textInfo.characterInfo[wordIndex+i];

                    int index = c.vertexIndex;

                    colors[index] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index].x*0.001f, 1f));
                    colors[index + 1] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x*0.001f, 1f));
                    colors[index + 2] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x*0.001f, 1f));
                    colors[index + 3] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x*0.001f, 1f));

                    vertices[index] += offset;
                    vertices[index + 1] += offset;
                    vertices[index + 2] += offset;
                    vertices[index + 3] += offset;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.colors = colors;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 AnimateWord(float time) {
        return new Vector2(Mathf.Sin(time*3.3f), Mathf.Cos(time*2.5f));
    }

    // Función para verificar si un array contiene una cadena específica
    bool ArrayContainsString(string[] array, string str)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == str)
            {
                return true;
            }
        }
        return false;
    }
}