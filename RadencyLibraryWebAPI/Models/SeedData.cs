using Microsoft.EntityFrameworkCore;

namespace RadencyLibraryWebAPI.Models
{
	public class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			LibraryDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<LibraryDbContext>();
			context.Database.Migrate();
			string base64MissingCover = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAMSURBVBhXY2BgYAAAAAQAAVzN/2kAAAAASUVORK5CYII=";
			if (!context.Books.Any())
			{
				context.Books.AddRange(
					new Book
					{
						Title = "Harry Potter and the Philosopher's Stone",
						Cover = base64MissingCover,
						Content = "One of my favourite books is Harry Potter and the Philosopher's Stone by J.K. Rowling. It is a story about Harry Potter, an orphan brought up by his aunt and uncle because his parents were killed when he was a baby. Harry is unloved by his uncle and aunt but everything changes when he is invited to join Hogwarts School of Witchcraft and Wizardry and he finds out he's a wizard. At Hogwarts Harry realises he's special and his adventures begin when he and his new friends Ron and Hermione attempt to unravel the mystery of the Philosopher's Stone. I can read this book over and over again. From the very beginning until the end J.K. Rowling has me gripped! There is never a dull moment, whether it's battling with trolls, a three-headed dog, or Harry facing Lord Voldermort. I would definitely recommend this book because it keeps you reading without ever wanting to put the book down. By the end of the book you come to love the characters and you want to read more. You won't be disappointed because the second book in the series, Harry Potter and the Chamber of Secrets is just as great! If you haven't read any of the Harry Potter books you are missing out on the best series ever!",
						Author = "J.K. Rowling",
						Genre = "Fantasy"
					},
					new Book
					{
						Title = "To Kill a Mockingbird",
						Cover = base64MissingCover,
						Content = "It is set in the fictional town of Maycomb, Ala., during the Great Depression. The protagonist is Jean Louise (“Scout”) Finch, an intelligent and unconventional girl who ages from six to nine years old during the course of the novel. She and her brother, Jem, are raised by their widowed father, Atticus Finch, a prominent lawyer who encourages his children to be empathetic and just. When Tom Robinson, a Black man, is falsely accused of raping Mayella Ewell, a white woman, Atticus agrees to defend him despite threats from the community. Although Atticus’s defense is strong, Tom is convicted, and he is later killed while trying to escape custody. A character compares his death to “the senseless slaughter of songbirds,” which echoes Atticus’s comment to his children that it is “a sin to kill a mockingbird.” To Kill a Mockingbird was praised for its sensitive treatment of a child’s awakening to racism and prejudice in the American South. Enormously popular, it was translated into some 40 languages and sold more than 30 million copies worldwide. In 1961 it won a Pulitzer Prize.",
						Author = "Harper Lee",
						Genre = "Classic"
					},
					new Book
					{
						Title = "It",
						Cover = base64MissingCover,
						Content = "Welcome to Derry, Maine... It’s a small city, a place as hauntingly familiar as your own hometown. Only in Derry the haunting is real... They were seven teenagers when they first stumbled upon the horror. Now they are grown-up men and women who have gone out into the big world to gain success and happiness. But none of them can withstand the force that has drawn them back to Derry to face the nightmare without an end, and the evil without a name.",
						Author = "Stephen King",
						Genre = "Horror"
					});
				context.Ratings.AddRange(
					new Rating { BookId = 1, Score = 4.5m },
					new Rating { BookId = 1, Score = 4.7m },
					new Rating { BookId = 1, Score = 4.6m },
					new Rating { BookId = 1, Score = 4.6m },
					new Rating { BookId = 2, Score = 4.9m },
					new Rating { BookId = 2, Score = 5 },
					new Rating { BookId = 2, Score = 4.6m },
					new Rating { BookId = 2, Score = 5 },
					new Rating { BookId = 3, Score = 4.2m },
					new Rating { BookId = 3, Score = 3.75m },
					new Rating { BookId = 3, Score = 3.9m },
					new Rating { BookId = 3, Score = 4.3m });
				context.Reviews.AddRange(
					new Review { BookId = 3, Reviewer = "John Doe", Message="It was so scaring"}
					);
				context.SaveChanges();
			}
		}
	}
}