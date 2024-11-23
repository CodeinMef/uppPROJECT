using System;
using System.Diagnostics;

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