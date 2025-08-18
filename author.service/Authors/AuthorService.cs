using author.Models;
using AuthorDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace author.service.Authors
{
    public class AuthorService : IAUthorRepo
    {

        public readonly AuthordbContext dbcontext = new AuthordbContext();
        public Author AddAuthor(Author author)
        {
            dbcontext.Author.Add(author);
            dbcontext.SaveChanges();

            return dbcontext.Author.Find(author.ID);
        }

        public Author GetAuthor(int ID)
        {
            return dbcontext.Author.Find(ID);

        }

        public Author GetAuthorByEmail(string email)
        {
            return dbcontext.Author.FirstOrDefault(a => a.EmailAddress == email); // we didnt use find for this we use primary key for find
        }
    }
}
