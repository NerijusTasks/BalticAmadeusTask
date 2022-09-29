using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;

        public BooksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("Get All Books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _context.Books.ToListAsync();
            return Ok(result);

        }

        [HttpPost("Admin Add New Book To Library")]
        public async Task<IActionResult> AdminAddNewBookToLibrary(AdminAddNewBook addBookRequest)
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Name = addBookRequest.Name,
                Author = addBookRequest.Author,
                Publisher = addBookRequest.Publisher,
                RealiseYears = addBookRequest.RealiseYears,
                Genre = addBookRequest.Genre,
                ISBN = addBookRequest.ISBN,
                ImageUrl = addBookRequest.ImageUrl,
                IsReserved = addBookRequest.IsReserved,
                IsTaked = addBookRequest.IsTaked

            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPut("Admin Change Books Status by {id:guid}")]
        public async Task<IActionResult> AdminAddReturnedBooks([FromRoute] Guid id, AdminAddReturnedBook adminAddReturnedBook)
        {
            var book = _context.Books.Find(id);

            if (book != null)
            {
                book.IsReserved = adminAddReturnedBook.IsReserved;
                book.IsTaked = adminAddReturnedBook.IsTaked;

                await _context.SaveChangesAsync();
            }

            return NotFound();
        }

        [HttpPut("Client Reserve Or Take Book by {id:guid}")]
        public async Task<IActionResult> ClienReservedOrTakeBooks([FromRoute] Guid id, ClientTakeOrReserveBook clientTakeOrReserveBook)
        {

            var msg = "Book is reserved or taked";
            var book = _context.Books.Find(id);

            if (book.IsReserved == true || book.IsTaked == true)
            {
                return NotFound(msg);
            }
            else
            {
                book.IsReserved = clientTakeOrReserveBook.IsReserved;
                book.IsTaked = clientTakeOrReserveBook.IsTaked;

                await _context.SaveChangesAsync();
            }

            return NotFound();
        }

        [HttpDelete("Admin Delete Books by {id:guid}")]
        public async Task<IActionResult> AdminCanDeleteBooks([FromRoute] Guid id)
        {
            var book = _context.Books.Find(id);

            if (book != null)
            {
                _context.Remove(book);
                await _context.SaveChangesAsync();

                return Ok(book);
            }

            return NotFound();
        }

        [HttpGet("Client Find Books by Name {name}")]
        public async Task<IActionResult> GetAllBooksByName(string name)
        {
            var searchName = _context.Books.Where(x => x.Name.ToLower().Contains(name.ToLower()));

            if (searchName == null)
            {
                return BadRequest("No book found!");
            }

            return Ok(searchName);
        }

        [HttpGet("Client Find Books by Author {author}")]
        public async Task<IActionResult> GetAllBooksByAuthor(string author)
        {
            var searchName = _context.Books.Where(x => x.Author.ToLower().Contains(author.ToLower()));

            if (searchName == null)
            {
                return BadRequest("No book found!");
            }

            return Ok(searchName);
        }

        [HttpGet("Client Find Books by Publisher {publisher}")]
        public async Task<IActionResult> GetAllBooksByPublisher(string publisher)
        {
            var searchName = _context.Books.Where(x => x.Publisher.ToLower().Contains(publisher.ToLower()));

            if (searchName == null)
            {
                return BadRequest("No book found!");
            }

            return Ok(searchName);
        }

        [HttpGet("Client Find Books by RealiseYears {realiseYears}")]
        public async Task<IActionResult> GetAllBooksByRealiseYears(int realiseYears)
        {
            var searchName = _context.Books.Where(x => x.RealiseYears == realiseYears);

            if (searchName == null)
            {
                return BadRequest("No book found!");
            }

            return Ok(searchName);
        }

        [HttpGet("Client Find Books by Genre {genre}")]
        public async Task<IActionResult> GetAllBooksByGenre(string genre)
        {
            var searchName = _context.Books.Where(x => x.Genre.ToLower().Contains(genre.ToLower()));

            if (searchName == null)
            {
                return BadRequest("No book found!");
            }

            return Ok(searchName);
        }

        [HttpGet("Client Find Books by ISBN {isbn}")]
        public async Task<IActionResult> GetAllBooksByISBN(string isbn)
        {
            var searchName = _context.Books.Where(x => x.ISBN.ToLower().Contains(isbn.ToLower()));

            if (searchName == null)
            {
                return BadRequest("No book found!");
            }

            return Ok(searchName);
        }

        [HttpGet("Client Find Books Which Is Free")]
        public async Task<IActionResult> GetAllBooksWhichIsFree()
        {
            var searchName = _context.Books.Where(x => x.IsTaked == false && x.IsReserved == false);

            return Ok(searchName);
        }
    }
}
