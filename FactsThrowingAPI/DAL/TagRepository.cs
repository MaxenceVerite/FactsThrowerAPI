using FactsThrowingAPI.DAL.DAO;
using FactsThrowingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.DAL
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly FactsContext _Context;
        public TagRepository(FactsContext context)
        {
            _Context = context;

        }

        public Tag Add(Tag entity)
        {

            _Context.Tags.Add(new DAO.TagDAO()
            {
                Id = entity.Id,
                Name= entity.Name
            }) ;

            _Context.SaveChanges();

            return entity;
        }

        public async Task<Tag?> FindAsync(Guid Id)
        {
            var tag = await _Context.Tags.Where(c => c.Id == Id).FirstOrDefaultAsync();

            if (tag is null) { return null; }     


            return new Tag(tag.Id, tag.Name);
        }

        public IList<Tag> List()
        {
            var tags = _Context.Tags.ToList();
            var taglist = new List<Tag>();
            foreach(TagDAO tag in tags)
            {
                taglist.Add(new Tag(tag.Id, tag.Name));
            }

            return taglist;
        }

        public Tag? Remove(Guid id)
        {
            var entityToDelete = _Context.Tags.Single(c => c.Id == id
             );
            if(entityToDelete == null) { return null; }

            var tracked = _Context.Tags.Remove(entityToDelete
             );

            var deletedEntity = tracked.Entity;
            _Context.SaveChanges();


            return new Tag(deletedEntity.Id, deletedEntity.Name);
        }

        public Tag? Update(Guid id, Tag entity)
        {

            var updated = _Context.Tags.Single(c => c.Id == id);
            if(updated is null) { return null; }
            updated.Name = entity.Name;

            _Context.Tags.Update(updated);
                
               
            _Context.SaveChanges();

            return new Tag(updated.Id, updated.Name);
        }

        public List<Fact> RelatedFacts(Guid tagId)
        {
            var factlist = _Context.Facts_Tags.Where(c => c.IdTag == tagId).Select(c => c.Fact).AsEnumerable();
            var result = new List<Fact>();
            foreach(var fact in factlist)
            {
                result.Add(new Fact(fact.Id, fact.Title, fact.Content));
            }

            return result;
        }
    }
}
