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
    [Route("api/facts")]
    public class FactController : ControllerBase

    {
        private readonly FactRepository _repository;

        public FactController(FactRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// - Return all the facts
        /// * Retourne tous les faits
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Fact>> GetAll()
        {
          


            var facts = _repository.List();

            return Ok(facts);

        }

        /// <summary>
        /// - Return a random fact (without conditions)
        /// * Retourne un fait tiré aléatoirement sans conditions
        /// </summary>

        [HttpGet("rand")]
        public ActionResult<Fact> GetRandom()
        {


            var facts = _repository.List();
            var rand = new Random();



            return Ok(facts.ElementAt(rand.Next(facts.Count)));

        }

        /// <summary>
        /// - Return the fact associated with the given ID
        /// * Retourne le fait associé à l'identifiant spécifié
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Fact>> Get([FromRoute] Guid id)
        {
            List<Fact> dtos = new List<Fact>();

            var fact = await _repository.FindAsync(id);

            if(fact is null) { return NotFound(); }

            return Ok(fact);

        }

        /// <summary>
        /// - Create a new fact in the database with the information given in request's body and return it
        /// * Crée dans la base de données et renvoie un nouveau fait avec les informations données dans le body de la requête
        /// </summary>
        [HttpPost]
        public ActionResult<Fact> Create([FromBody] FactDTO dto)
        {

            var fact = new Fact(dto.Title, dto.Content);
            _repository.Add(fact);

            return Ok(fact);
        }


        /// <summary>
        /// - Update an existing fact considering the new information given in the request's body
        /// * Met à jour un fait existant avec les informations données dans le body de la requête
        /// </summary>
        [HttpPatch]
        public ActionResult<Fact> Update([FromRoute] Guid id, [FromBody] FactDTO data)
        {
            var updatedfact = _repository.Update(id, new Fact(id, data.Title, data.Content));
            if(updatedfact is null)
            {
                return NotFound();
            }
            return Ok(updatedfact);
        }

        /// <summary>
        /// - Delete the fact associated with the URL given ID 
        /// * Supprime le fait associé à l'ID donné dans l'URL
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {

           var result = _repository.Remove(id);

            if(result is null) { return NotFound(); }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// - Return all the tags that a fact is associated to, given its ID
        /// * Retourne une liste contenant tous les tags associés à un fait donné (ID)
        /// </summary>
        [HttpGet]
        [Route("{id}/tags")]
        public ActionResult<IEnumerable<Fact>> getTagsFromFact([FromRoute] Guid factId)
        {
            var associatedTags = _repository.RelatedTags(factId);

            return Ok(associatedTags);
        }
    }
}
