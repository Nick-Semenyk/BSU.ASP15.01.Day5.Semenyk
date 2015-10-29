using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClasses
{
    class BookListService
    {
        private Stream stream;
        private List<Book> books; 

        public BookListService(Stream stream)
        {
            this.stream = stream;
            books = GetBooks();
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException();
            }
            books.Add(book);
        }

        public Book FindByTag()
        {

            return null;
        }

        public void SortBooksByTag()
        {

        }

        public void RemoveBook(Book book)
        {

        }

        private List<Book> GetBooks()
        {
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
            BinaryWriter writer = new BinaryWriter(stream);
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
}
