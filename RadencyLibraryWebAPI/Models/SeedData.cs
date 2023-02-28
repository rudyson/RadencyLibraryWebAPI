using Microsoft.EntityFrameworkCore;
using RadencyLibraryWebAPI.Entities;

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
					},
					new Book
					{
						Title = "The Catcher in the Rye",
						Cover = base64MissingCover,
						Content = "Holden Caulfield, a teenager from New York City, describes events that took place before Christmas break. He begins by noting that he is lonely, but is soon interrupted by his wandering thoughts.",
						Author = "J.D. Salinger",
						Genre = "Fiction"
					},
					new Book
					{
						Title = "To Kill a Mockingbird",
						Cover = base64MissingCover,
						Content = "Scout Finch lives with her brother, Jem, and their widowed father, Atticus, in the sleepy Alabama town of Maycomb. Maycomb is suffering through the Great Depression, but Atticus is a prominent lawyer and the Finch family is reasonably well off in comparison to the rest of society.",
						Author = "Harper Lee",
						Genre = "Fiction"
					},
					new Book
					{
						Title = "1984",
						Cover = base64MissingCover,
						Content = "The story takes place in an imagined future, the year 1984, when much of the world has fallen victim to perpetual war, omnipresent government surveillance, historical negationism, and propaganda.",
						Author = "George Orwell",
						Genre = "Fiction"
					},
					new Book
					{
						Title = "Pride and Prejudice",
						Cover = base64MissingCover,
						Content = "Pride and Prejudice tells the story of Mr. and Mrs. Bennet's five unmarried daughters: Jane, Elizabeth, Mary, Kitty, and Lydia. The novel revolves around the importance of marrying for love, not for money or social prestige, despite the communal pressure to make a wealthy match.",
						Author = "Jane Austen",
						Genre = "Romance"
					},
					new Book
					{
						Title = "One Hundred Years of Solitude",
						Cover = base64MissingCover,
						Content = "The novel chronicles the lives of the Buendía family, who live in Macondo, a fictional village in Colombia. The story spans seven generations, covering the rise and fall of the town and the family.",
						Author = "Gabriel García Márquez",
						Genre = "Fantasy"
					},
					new Book
					{
						Title = "The Lord of the Rings",
						Cover = base64MissingCover,
						Content = "The Lord of the Rings tells the story of hobbit Frodo Baggins as he and a fellowship of companions set out on a quest to destroy the One Ring, and thus ensure the destruction of its maker, the Dark Lord Sauron.",
						Author = "J.R.R. Tolkien",
						Genre = "Fantasy"
					},
					new Book
					{
						Title = "The Hitchhiker's Guide to the Galaxy",
						Cover = base64MissingCover,
						Content = "The book follows the misadventures of hapless Englishman Arthur Dent after he and his friend Ford Prefect, an alien journalist, escape Earth moments before it is destroyed to make way for a hyperspace bypass.",
						Author = "Douglas Adams",
						Genre = "Science Fiction"
					},
					new Book
					{
						Title = "The Great Gatsby",
						Cover = base64MissingCover,
						Content = "The novel is set in the fictional town of West Egg on Long Island in the summer of 1922. The story primarily concerns the young and mysterious millionaire Jay Gatsby and his quixotic passion and obsession with the beautiful former debutante Daisy Buchanan.",
						Author = "F. Scott Fitzgerald",
						Genre = "Fiction"
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
					new Rating { BookId = 3, Score = 4.3m },
					new Rating { BookId = 1, Score = 2.2m },
					new Rating { BookId = 1, Score = 2.6m },
					new Rating { BookId = 1, Score = 4.9m },
					new Rating { BookId = 1, Score = 2.5m },
					new Rating { BookId = 1, Score = 3 },
					new Rating { BookId = 1, Score = 1.5m },
					new Rating { BookId = 2, Score = 4.4m },
					new Rating { BookId = 2, Score = 4.3m },
					new Rating { BookId = 2, Score = 2.3m },
					new Rating { BookId = 3, Score = 3.7m },
					new Rating { BookId = 3, Score = 2.4m },
					new Rating { BookId = 3, Score = 1.8m },
					new Rating { BookId = 4, Score = 3.6m },
					new Rating { BookId = 4, Score = 4.6m },
					new Rating { BookId = 4, Score = 3.9m },
					new Rating { BookId = 4, Score = 4.4m },
					new Rating { BookId = 4, Score = 2.4m },
					new Rating { BookId = 5, Score = 1 },
					new Rating { BookId = 5, Score = 3.6m },
					new Rating { BookId = 5, Score = 4.5m },
					new Rating { BookId = 5, Score = 3.4m },
					new Rating { BookId = 5, Score = 4.5m },
					new Rating { BookId = 6, Score = 2.3m },
					new Rating { BookId = 6, Score = 4.1m },
					new Rating { BookId = 6, Score = 1.5m },
					new Rating { BookId = 6, Score = 1.4m },
					new Rating { BookId = 6, Score = 2.4m },
					new Rating { BookId = 6, Score = 3.2m },
					new Rating { BookId = 7, Score = 3.5m },
					new Rating { BookId = 7, Score = 4.2m },
					new Rating { BookId = 7, Score = 2.5m },
					new Rating { BookId = 7, Score = 4.9m },
					new Rating { BookId = 7, Score = 4.8m },
					new Rating { BookId = 8, Score = 3.1m },
					new Rating { BookId = 8, Score = 4.4m },
					new Rating { BookId = 8, Score = 2 },
					new Rating { BookId = 8, Score = 3 },
					new Rating { BookId = 8, Score = 4.4m },
					new Rating { BookId = 8, Score = 3 },
					new Rating { BookId = 8, Score = 2.2m },
					new Rating { BookId = 9, Score = 2.7m },
					new Rating { BookId = 9, Score = 1.7m },
					new Rating { BookId = 9, Score = 1.7m },
					new Rating { BookId = 9, Score = 3.7m },
					new Rating { BookId = 10, Score = 4.6m },
					new Rating { BookId = 10, Score = 3.2m },
					new Rating { BookId = 10, Score = 3.5m },
					new Rating { BookId = 10, Score = 3.3m },
					new Rating { BookId = 11, Score = 2.5m },
					new Rating { BookId = 11, Score = 3.3m },
					new Rating { BookId = 11, Score = 4.4m },
					new Rating { BookId = 11, Score = 5 },
					new Rating { BookId = 11, Score = 2.5m },
					new Rating { BookId = 11, Score = 2.6m });
				context.Reviews.AddRange(
					new Review { BookId = 1, Reviewer = "John Doe", Message = "I found this book to be deeply moving and emotional." },
					new Review { BookId = 2, Reviewer = "Ursula", Message = "This book was a bit too simplistic for my tastes." },
					new Review { BookId = 3, Reviewer = "Tom", Message = "It was so scaring" },
					new Review { BookId = 4, Reviewer = "Sarah", Message = "This book exceeded my expectations. It was a real page-turner." },
					new Review { BookId = 5, Reviewer = "Wendy", Message = "I really enjoyed this book and would highly recommend it to others." },
					new Review { BookId = 6, Reviewer = "Quinn", Message = "This book was amazing! I couldn't put it down." },
					new Review { BookId = 7, Reviewer = "Nick", Message = "I was impressed by the author's extensive research and knowledge on the subject." },
					new Review { BookId = 8, Reviewer = "Maggie", Message = "This book was a bit slow-paced for me, but it was still an interesting read." },
					new Review { BookId = 9, Reviewer = "Larry", Message = "This book was a bit too simplistic for me." },
					new Review { BookId = 10, Reviewer = "Karen", Message = "I learned a lot from it." }
					);
				context.SaveChanges();
			}
		}
	}
}