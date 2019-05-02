using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext dbContext):base(dbContext)
        {

        }

        public void UpdateState(Document d)
        {
         //   Context.Entry(d.State).State = EntityState.Modified;
        }
    }
}
