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

        public BookListService()
        {
            stream = null;
            books = new List<Book>();
        }

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

        public List<Book> FindByTag(Predicate<Book> predicate)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(this.GetType));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException();
            }
            List<Book> result = books.FindAll(predicate);
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

        public void Load(Stream input)
        {
            if (input == null)
                throw new ArgumentNullException();
            stream = input;
            GetBooks();
        }

        public void Save(Stream output)
        {
            if (output == null)
                throw new ArgumentNullException();
            stream = output;
            WriteBooks();
        }

        public void Dispose()
        {
            WriteBooks();
            disposed = true;
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

        private void WriteBooks()
        {
            if (stream == null)
                return;
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
