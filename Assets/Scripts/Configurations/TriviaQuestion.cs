using UnityEngine;

[CreateAssetMenu(fileName = "TriviaQuestion", menuName = "ScriptableObjects/Trivia Question", order = 1)]
public class TriviaQuestion : ScriptableObject
{
    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public int correctAnswerIndex = 0;
}
