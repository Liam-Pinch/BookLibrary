using Microsoft.AspNetCore.Mvc;
using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System.Numerics;

namespace BookLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookController : ControllerBase
    {
        private readonly BookLibraryContext _context;

        public BookController(BookLibraryContext context)
        {
            _context = context;
        }

        [HttpGet] // return all books in DB.
        public ActionResult<List<Book>> GetBook()
        {
            return Ok(_context.Books);
        }

        [HttpGet("id/{Id}")] // returns book by ID.
        public ActionResult<Book> GetBookByID(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpGet("Title/{Title}")]
        public ActionResult<Book> GetBookByTitle(string title)
        {
            var books = _context.Books.Where(books => books.Title == title).ToList();

            if (!books.Any())
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpGet("author/{Author}")] // return list of books by Author.
        public ActionResult<List<Book>> GetBookByAuthor(string author)
        {
            var books = _context.Books.Where(books => books.Author == author).ToList();

            if (!books.Any())
            {
                return NotFound();
            }
            return Ok(books);
        }



        [HttpGet("Genre/{Genre}")] // returns list of books by Genre.
        public ActionResult<List<Book>> GetBookByGenre(string genre)
        {
            var books = _context.Books.Where(books => books.Genre.ToLower() == genre.ToLower()).ToList();

            if (!books.Any())
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpGet("Published/{YearPublished}")] // returns list of books by Year of Publications.
        public ActionResult<List<Book>> GetBookByYear(int year)
        {
            var books = _context.Books.Where(books => books.YearOfPublication == year).ToList();

            if (!books.Any())
            {
                return NotFound();
            }
            return Ok(books);
        }

                [HttpGet("OriginalLanguage/{OriginalLanguage}")]
        public ActionResult<List<Book>> GetBookByLanguage(string language)
        {
            var books = _context.Books.Where(books => books.OriginalLanguage == language).ToList();

            if (!books.Any())
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpGet("AvailableLanguage/{AvailableLanguage}")]
        public ActionResult<List<Book>> GetBookByAvailableLanguage(string languages)
        {
            var books = _context.Books.Where(books => books.AvailableLanguages == languages).ToList();

            if (!books.Any())
            {
                return NotFound();
            }

            return Ok(books);
        }

        [HttpPost] // adds a new book to the DB.
        public ActionResult<Book> AddBook(Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            _context.Add(newBook);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBookByID), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{Id}")] // modifies a book in the database.
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = updatedBook.Title;
            book.YearOfPublication = updatedBook.YearOfPublication;
            book.Genre = updatedBook.Genre;
            book.Rating = updatedBook.Rating;
            book.OriginalLanguage = updatedBook.OriginalLanguage;
            book.AvailableLanguages = updatedBook.AvailableLanguages;

            return NoContent();
        }

        [HttpDelete("{Id}")] // deletes a book from the db.
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }
    
    }

    
}