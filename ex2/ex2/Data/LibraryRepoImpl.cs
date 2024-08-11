using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Model.Book;

namespace ex2.Data
{
    class LibraryRepoImpl: ILibraryRepo
    {
        private List<Book> bookStore;
        public LibraryRepoImpl()
        {
            bookStore = new List<Book>();
            Book book1 = new Book(Title = "eee", Author="eeee", ISBN="eeeee");
        }

    }
}
