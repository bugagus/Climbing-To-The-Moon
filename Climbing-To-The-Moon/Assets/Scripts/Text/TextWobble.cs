using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWobble : MonoBehaviour
{
    TMP_Text textMesh;

    Mesh mesh;

    Vector3[] vertices;

    List<int> wordIndexes;
    List<int> wordLengths;

    [SerializeField] private DialogueController dialogueController;

    private List<Word> _words;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    public ref List<int> UpdateTextState()
    {
        wordIndexes = new List<int> { 0 };
        wordLengths = new List<int>();

        string s = dialogueController.displayedLines[dialogueController._currentLine].lineText;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
        }
        wordLengths.Add(s.Length - wordIndexes[wordIndexes.Count - 1]);

        _words = dialogueController.displayedLines[dialogueController._currentLine].words;

        return ref wordLengths;
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        WobbleLine();

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    private void WobbleLine()
    {
        Color[] colors = mesh.colors;

        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];
            Word word = _words[w];
            Vector3 offset = Wobble(Time.time + w, word.xOffset, word.yOffset);

            for (int i = 0; i < wordLengths[w]; i++)
            {
                int charIndex = wordIndex + i;
                TMP_CharacterInfo c = textMesh.textInfo.characterInfo[charIndex];

                int index = c.vertexIndex;

                Gradient gradient;

                if (charIndex < dialogueController.CurrentLetter)
                    gradient = word.color;
                else
                    gradient = new Gradient();


                colors[index] = gradient.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.001f, 1f));
                colors[index + 1] = gradient.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.001f, 1f));
                colors[index + 2] = gradient.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.001f, 1f));
                colors[index + 3] = gradient.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.001f, 1f));

                if (word.wobbleByChar)
                    offset = Wobble(Time.time + i, word.xOffset, word.yOffset);

                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }
        }
        mesh.colors = colors;
    }

    Vector2 Wobble(float time, float xOffset, float yOffset)
    {
        return new Vector2(Mathf.Sin(time * xOffset), Mathf.Cos(time * yOffset));
    }
}