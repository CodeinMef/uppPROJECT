using System;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        Magazine magazine = new Magazine("Ежемесячный технический журнал", Frequency.Monthly, DateTime.Now, 5000);
        Console.WriteLine(magazine.ToShortString());
        Console.WriteLine($"Еженедельно: {magazine[Frequency.Weekly]}");
        Console.WriteLine($"Ежемесячно: {magazine[Frequency.Monthly]}");
        Console.WriteLine($"Ежегодно: {magazine[Frequency.Yearly]}");
        magazine.Title = "Еженедельный научный журнал";
        magazine.Frequency = Frequency.Weekly;
        magazine.ReleaseDate = new DateTime(2024, 12, 31);
        magazine.Circulation = 3000;
        Console.WriteLine(magazine.ToString());
        Article article1 = new Article(new Person("Илья", "Ilya@example.com"), "Будущее искусственного интеллекта", 4.8);
        Article article2 = new Article(new Person("Макар", "Makar@example.com"), "Космонавтика", 4.5);
        magazine.AddArticles(article1, article2);
        Console.WriteLine(magazine.ToString());
        int numberOfArticles = 1000;
        Article[] oneDimensionalArray = new Article[numberOfArticles];
        Article[,] twoDimensionalArray = new Article[10, 100];
        Article[][] jaggedArray = new Article[10][];
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            jaggedArray[i] = new Article[100];
        }

        for (int i = 0; i < numberOfArticles; i++)
        {
            Article article = new Article(new Person($"Author {i}", $"author{i}@example.com"), $"Article {i}", 4.0 + (i % 5) * 0.1);
            oneDimensionalArray[i] = article;
            twoDimensionalArray[i / 100, i % 100] = article;
            jaggedArray[i / 100][i % 100] = article;
        }
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (var article in oneDimensionalArray)
        {
            var title = article.Title;
        }
        stopwatch.Stop();
        Console.WriteLine($"One-dimensional array time: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch.Restart();
        for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
        {
            for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
            {
                var title = twoDimensionalArray[i, j].Title;
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Two-dimensional array time: {stopwatch.ElapsedMilliseconds} ms");
        stopwatch.Restart();
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                var title = jaggedArray[i][j].Title;
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Jagged array time: {stopwatch.ElapsedMilliseconds} ms");
    }
}
public enum Frequency
{
    Weekly,
    Monthly,
    Yearly
}

public class Article
{
    public Person Author { get; set; }
    public string Title { get; set; }
    public double Rating { get; set; }

    public Article(Person author, string title, double rating)
    {
        Author = author;
        Title = title;
        Rating = rating;
    }

    public override string ToString()
    {
        return $"Author: {Author.Name}, Title: {Title}, Rating: {Rating}";
    }
}

public class Person
{
    public string Name { get; set; }
    public string Email { get; set; }

    public Person(string name, string email)
    {
        Name = name;
        Email = email;
    }
}

public class Magazine
{
    private string title;
    private Frequency frequency;
    private DateTime releaseDate;
    private int circulation;
    private Article[] articles;

    public Magazine(string title, Frequency frequency, DateTime releaseDate, int circulation)
    {
        this.title = title;
        this.frequency = frequency;
        this.releaseDate = releaseDate;
        this.circulation = circulation;
        this.articles = new Article[0];
    }

    public Magazine()
    {
        title = "Default Magazine";
        frequency = Frequency.Monthly;
        releaseDate = DateTime.Now;
        circulation = 1000;
        articles = new Article[0];
    }

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public Frequency Frequency
    {
        get { return frequency; }
        set { frequency = value; }
    }

    public DateTime ReleaseDate
    {
        get { return releaseDate; }
        set { releaseDate = value; }
    }

    public int Circulation
    {
        get { return circulation; }
        set { circulation = value; }
    }

    public Article[] Articles
    {
        get { return articles; }
        set { articles = value; }
    }

    public double AverageRating
    {
        get
        {
            if (articles.Length == 0) return 0.0;
            double totalRating = 0.0;
            foreach (var article in articles)
            {
                totalRating += article.Rating;
            }
            return totalRating / articles.Length;
        }
    }

    public bool this[Frequency frequency]
    {
        get { return this.frequency == frequency; }
    }

    public void AddArticles(params Article[] newArticles)
    {
        int currentLength = articles.Length;
        Array.Resize(ref articles, currentLength + newArticles.Length);
        Array.Copy(newArticles, 0, articles, currentLength, newArticles.Length);
    }

    public override string ToString()
    {
        string articleList = string.Join(", ", Array.ConvertAll(articles, article => article.ToString()));
        return $"Title: {title}, Frequency: {frequency}, Release Date: {releaseDate.ToShortDateString()}, Circulation: {circulation}, Articles: [{articleList}]";
    }

    public virtual string ToShortString()
    {
        return $"Title: {title}, Frequency: {frequency}, Release Date: {releaseDate.ToShortDateString()}, Circulation: {circulation}, Average Rating: {AverageRating}";
    }
}