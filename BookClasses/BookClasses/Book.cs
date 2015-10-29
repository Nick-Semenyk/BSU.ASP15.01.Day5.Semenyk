using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClasses
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public int Length { get; set; }
        public int YearOfPublishing { get; set; }
        public int EditionNumber { get; set; }

        public int CompareTo(Book other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Book other)
        {
            if (this.Length == other.Length)
                if (this.YearOfPublishing == other.YearOfPublishing)
                    if (this.EditionNumber == other.EditionNumber && 
                        this.Title == other.Title &&
                        this.Author == other.Author)
                        return true;
            return false;
        }
    }
}
