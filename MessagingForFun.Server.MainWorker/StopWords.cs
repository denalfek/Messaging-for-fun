namespace MessagingForFun.Server.MainWorker;

public static class StopWords
{
    public static IReadOnlyCollection<string> Content = new[]
    {
        "a", "an", "and", "are", "as", "at", "be", "by", "for", "from",
        "has", "he", "in", "is", "it", "its", "of", "on", "that", "the",
        "to", "was", "were", "will", "with", "I", "you", "your", "he", "she",
        "his", "her", "we", "us", "they", "them", "this", "these", "those",
        "my", "mine", "our", "ours", "your", "yours", "their", "theirs",
        "which", "who", "whom", "what", "where", "when", "why", "how", "all",
        "any", "both", "each", "few", "more", "most", "other", "some", "such",
        "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very",
        "s", "t", "can", "will", "just", "don", "should", "now", "do", "does",
        "did", "doing", "done", "but", "because", "however", "although", "since",
        "while", "whereas", "unless", "until", "while", "though", "even", "if",
        "once", "unless", "until", "rather", "whether", "either", "neither",
        "love", "hate", "sister", "mother", "father", "brother", "son", "daughter",
        "wife", "husband", "girlfriend", "boyfriend", "friend", "friends", "family",
        "water", "food", "money", "job", "work", "school", "college", "university",
        "car", "house", "home", "phone", "computer", "laptop", "tv", "television"
    };
}