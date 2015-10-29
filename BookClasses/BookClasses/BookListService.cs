using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookClasses
{
    public class BookListService : IDisposable
    {
        private Stream stream;
        private List<Book> books;
        private bool disposed = false;

        public BookListService(Stream stream)
        {
            this.stream = stream;
            books = GetBooks();
        }

        ~BookListService()
        {
            if (!disposed)
                Dispose();
        }

        public void AddBook(Book book)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            if (!books.Contains(book))
            {
                books.Add(book);
            }
            else
            {
                throw new BookAlreadyExistsException();
            }
        }

        public List<Book> FindByTag(string author = "", string title = "", int length = 0, int yearOfPublishing = 0, int editionNumber = 0)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            List<Book> result = books;
            if (!string.IsNullOrEmpty(author))
            {
                result = result.FindAll(book => book.Author == author);
            }
            if (!string.IsNullOrEmpty(title))
            {
                result = result.FindAll(book => book.Title == title);
            }
            if (length >= 0)
            {
                result = result.FindAll(book => book.Length == length);
            }
            if (yearOfPublishing != 0)
            {
                result = result.FindAll(book => book.YearOfPublishing == yearOfPublishing);
            }
            if (editionNumber != 0)
            {
                result = result.FindAll(book => book.EditionNumber == editionNumber);
            }
            return result;
        }

        public void SortBooksByTag(IComparer<Book> comparer)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            if (comparer == null)
            {
                throw new ArgumentNullException();
            }
            books.Sort(comparer);
        }

        public void RemoveBook(Book book)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            if (books.Contains(book))
            {
                books.Remove(book);
            }
            else
            {
                throw new BookDoesntExistException();
            }
        }

        private List<Book> GetBooks()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            List<Book> result = new List<Book>();
            BinaryReader reader = new BinaryReader(stream);
            try
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Book book = new Book();
                    book.Title = reader.ReadString();
                    book.Author = reader.ReadString();
                    book.Length = reader.ReadInt32();
                    book.YearOfPublishing = reader.ReadInt32();
                    book.EditionNumber = reader.ReadInt32();
                    result.Add(book);
                }
            }
            catch (Exception)
            {
                throw new IOException();
            }
            return result;
        }
        
        public void Dispose()
        {
            WriteBooks();
            disposed = true;
        }

        private void WriteBooks()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            BinaryWriter writer = new BinaryWriter(stream);
            writer.BaseStream.Position = 0;
            try
            {
                foreach(Book book in books)
                {
                    writer.Write(book.Title);
                    writer.Write(book.Author);
                    writer.Write(book.Length);
                    writer.Write(book.YearOfPublishing);
                    writer.Write(book.EditionNumber);
                }
            }
            catch (Exception)
            {
                throw new IOException();
            }
        }
    }

    class BookAlreadyExistsException : Exception
    {
        public BookAlreadyExistsException()
        {
        }

        public BookAlreadyExistsException(string message) : base(message)
        {
        }

        public BookAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BookAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    class BookDoesntExistException : Exception
    {
        public BookDoesntExistException()
        {
        }

        public BookDoesntExistException(string message) : base(message)
        {
        }

        public BookDoesntExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BookDoesntExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
