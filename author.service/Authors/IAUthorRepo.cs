using author.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author.service.Authors
{
    public interface IAUthorRepo
    {

        public Author GetAuthor(int ID);
        public Author AddAuthor(Author author);
        public Author GetAuthorByEmail(string email);

    }
}
