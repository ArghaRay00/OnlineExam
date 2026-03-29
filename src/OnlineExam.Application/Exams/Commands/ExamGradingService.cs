using OnlineExam.Domain.Entities;

namespace OnlineExam.Application.Exams.Commands;

public record GradingResult(int Score, int TotalQuestions, bool Passed);

public static class ExamGradingService
{
    public static GradingResult Grade(
        ICollection<Question> questions,
        List<AnswerSubmission> answers,
        double cutoffScore)
    {
        var answerMap = questions.ToDictionary(q => q.Id, q => q.CorrectOption);
        var score = answers.Count(a => answerMap.TryGetValue(a.QuestionId, out var correct) && a.SelectedOption == correct);
        var total = questions.Count;
        var passed = score >= cutoffScore;
        return new GradingResult(score, total, passed);
    }
}

public record AnswerSubmission(int QuestionId, int SelectedOption);
