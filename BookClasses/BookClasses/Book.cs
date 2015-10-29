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
            if (Equals(other, this))
                return 0;
            if (other == null)
                return -1;
            int returnValue = 0;
            if (this.Length != other.Length)
                return this.Length - other.Length;
            if (this.YearOfPublishing != other.YearOfPublishing)
                return this.YearOfPublishing - other.YearOfPublishing;
            if (this.EditionNumber != other.EditionNumber)
                return this.EditionNumber - other.EditionNumber;
            if((returnValue = string.Compare(this.Title, other.Title)) != 0)
                return returnValue;
            if ((returnValue = string.Compare(this.Author, other.Author)) != 0)
                return returnValue;
            return 0;
        }

        public bool Equals(Book other)
        {
            if (other == null)
                return false;
            if (this.Length == other.Length)
                if (this.YearOfPublishing == other.YearOfPublishing)
                    if (this.EditionNumber == other.EditionNumber && 
                        this.Title == other.Title &&
                        this.Author == other.Author)
                        return true;
            return false;
        }

        public override string ToString()
        {
            return $"{Title} {Author} {Length} {YearOfPublishing} {EditionNumber}";
        }
    }
}
