using FactsThrowingAPI.DAL;
using FactsThrowingAPI.DTO;
using FactsThrowingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactsThrowingAPI.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagController : ControllerBase
    {

        private readonly TagRepository _repository;

        public TagController(TagRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// - Return all existing tags
        /// * Retourne tous les tags existants
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetAll()
        {
            List<Tag> dtos = new List<Tag>();


            var tags = _repository.List();

            return Ok(tags);

        }

        /// <summary>
        /// - Create a new tag and returns it
        /// * Créer un nouveau tag et le renvoie
        /// </summary>
        [HttpPost]
        public ActionResult<Tag> Create([FromBody] TagDTO dto)
        {

            var tag = new Tag(dto.Name);
            _repository.Add(tag);

            return StatusCode(201, tag);
        }

        /// <summary>
        /// - Delete a tag if exists
        /// * Supprime un tag s'il existe
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {

            var result = _repository.Remove(id);

            if(result == null)
            {
                return NotFound();
            }

            else { return Ok(); }


        }

        /// <summary>
        /// - Return all facts associated with a list of tags
        /// * Retourne une liste de fait correspondant à une liste de tags fournie
        /// </summary>
        [HttpGet]
        [Route("facts")]
        public ActionResult<IEnumerable<Fact>> getFactsFromTags([FromBody] List<Guid> tagsId)
        {
            List<Fact> facts = new List<Fact>();
            foreach(Guid id in tagsId)
            {
                facts.AddRange(_repository.RelatedFacts(id));
            }

            return Ok(facts);
        }

    }
}
